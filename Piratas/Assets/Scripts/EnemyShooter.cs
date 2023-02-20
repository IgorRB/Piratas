using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyScript
{
    [SerializeField]
    float safeRange;

    [SerializeField]
    GameObject cannonBall;

    [SerializeField]
    Transform[] cannons;

    [SerializeField]
    float reloadTime;
    float reloading;

    private void Update()
    {
        if (HudController.HC.IsOver())
            return;

        if (player == null)
            return;

        Vector3 playerDir = player.position - transform.position;
        float rot_z = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(transform.rotation.eulerAngles.z, rot_z - 90, turnSpeed * Time.deltaTime));

        Move();

        Vector2 rayVec = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector2.up;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, rayVec, 4);

        reloading -= Time.deltaTime;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Player") && reloading <= 0f && maxSpeed > 0)
            {
                Shoot();
                reloading = reloadTime;
            }
        }
        
    }

    protected override void Move()
    {
        Vector3 playerDir = player.position - transform.position;

        if(playerDir.magnitude > safeRange)
        {
            speed += acceleration * Time.deltaTime;
            speed = speed > maxSpeed ? maxSpeed : speed;
        }
        else
        {
            speed -= acceleration / 2 * Time.deltaTime;
            if (speed < 0)
                speed = 0f;
        }


        Vector3 myDir = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        myRb.velocity = myDir * speed;
    }

    void Shoot()
    {
        GameObject ball = Instantiate(cannonBall, cannons[0].position, cannons[0].rotation);
    }
}
