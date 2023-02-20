using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public bool friendly = false;

    [SerializeField]
    float speed, lifeTime;

    [SerializeField]
    GameObject explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (friendly)
        {
            if (collision.gameObject.CompareTag("Player"))
                return;

            HudController.HC.AddScore(1);
            collision.gameObject.GetComponent<HPScript>().TakeDamage();
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
                return;

            collision.gameObject.GetComponent<HPScript>().TakeDamage();

            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
