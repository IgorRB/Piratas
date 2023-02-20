using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : EnemyScript
{
    [SerializeField]
    GameObject destroyedExplosion;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HPScript>().TakeDamage();
            Instantiate(destroyedExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
