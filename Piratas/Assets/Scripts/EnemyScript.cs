using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    protected float acceleration, maxSpeed, turnSpeed;
    protected float speed;

    protected Transform player;
    protected Rigidbody2D myRb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 playerDir = player.position - transform.position;
        float rot_z = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (HudController.HC.IsOver())
            return;

        if (player == null)
            return;

        Vector3 playerDir = player.position - transform.position;
        float rot_z = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.eulerAngles.z, rot_z - 90, turnSpeed * Time.deltaTime));

        Move();
    }

    protected virtual void Move()
    {
        speed += acceleration * Time.deltaTime;
        speed = speed > maxSpeed ? maxSpeed : speed;

        Vector3 myDir  = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        myRb.velocity = myDir * speed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0;
    }

    public void Sink()
    {
        myRb.velocity = Vector2.zero;
        acceleration = 0f;
        speed = 0f;
        maxSpeed = 0f;
        turnSpeed = 0f;
    }

}
