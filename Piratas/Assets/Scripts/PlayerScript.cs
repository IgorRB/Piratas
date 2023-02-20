using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D myRb;

    [SerializeField]
    float acceleration, maxSpeed, turnSpeed;
    float speed;

    [SerializeField]
    GameObject cannonBall;

    [SerializeField]
    Transform[] cannons;

    [SerializeField]
    float reloadTime;
    float reloading;

    // Start is called before the first frame update
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HudController.HC.IsOver())
            return;

        float velx = Input.GetAxis("Horizontal");
        float vely = Input.GetAxis("Vertical");

        if (vely > 0)
        {
            speed += acceleration * vely * Time.deltaTime;

            speed = speed > maxSpeed ? maxSpeed : speed;
        }
        else if(speed > 0)
        {
            
            speed -= acceleration / 2 * Time.deltaTime;
            if (speed < 0)
                speed = 0f;
            
        }

        if (velx != 0)
        {
            transform.Rotate(new Vector3(0, 0, turnSpeed * -velx * Time.deltaTime));
        }

        Vector2 myDir = RotateVector2(Vector2.up, transform.rotation.eulerAngles.z);
        myRb.velocity = myDir * speed;

        //Shooting
        reloading -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && reloading <= 0)
        {
            Shoot();
            reloading = reloadTime;
        }
        else if (Input.GetKeyDown(KeyCode.E) && reloading <= 0)
        {
            ShootRight();
            reloading = reloadTime;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && reloading <= 0)
        {
            ShootLeft();
            reloading = reloadTime;
        }
    }

    Vector2 RotateVector2(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }

    void Shoot()
    {
        GameObject ball = Instantiate(cannonBall, cannons[0].position, cannons[0].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;
    }

    void ShootRight()
    {
        Vector3 offset = new Vector3(0, 0.2f, 0);
        offset = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * offset;
        GameObject ball;
        ball = Instantiate(cannonBall, cannons[1].position, cannons[1].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;

        ball = Instantiate(cannonBall, cannons[1].position + offset, cannons[1].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;

        ball = Instantiate(cannonBall, cannons[1].position - offset, cannons[1].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;
    }

    void ShootLeft()
    {
        Vector3 offset = new Vector3(0, 0.2f, 0);
        offset = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * offset;
        GameObject ball;
        ball = Instantiate(cannonBall, cannons[2].position, cannons[2].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;

        ball = Instantiate(cannonBall, cannons[2].position + offset, cannons[2].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;

        ball = Instantiate(cannonBall, cannons[2].position - offset, cannons[2].rotation);
        ball.GetComponent<CannonBallScript>().friendly = true;
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
