using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShip : MonoBehaviour
{
    #region Fields

    // HUD Support
    [SerializeField]
    GameObject CanvasHUD;

    // Rigidbody Support
    Rigidbody2D rb2d;

    // Thrust Support
    Vector2 thrustDirection = new Vector2(1, 0);
    [SerializeField]
    float thrustForce = 5;

    // Circle Collider Support
    CircleCollider2D circleCollider;
    float circleColliderRadius;

    // Location Support
    Vector3 shipPosition;

    // Rotation Support
    Quaternion shipRotation;
    [SerializeField]
    float RotateDegreesPerSecond = 180;
   
    // Input Support
    float thrustInput;
    float rotateInput;

    // Bullet Fire support
    [SerializeField]
    GameObject prefabBullet;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        // To obtain rigidbody
        rb2d = GetComponent<Rigidbody2D>();

        // To obtain circle collider and its radius
        circleCollider = GetComponent<CircleCollider2D>();
        circleColliderRadius = circleCollider.radius;

    }


    // Update is called once per frame
    void Update()
    {
        // Track ship position and rotation
        shipPosition = transform.position;
        shipRotation = transform.rotation;

        // Detect input for fire
        bool fireInput = Input.GetKeyDown(KeyCode.LeftControl);
        if(fireInput == true)
        {
            Fire();
        }

        // Store input for rotate
        float rotateInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;

        // Rotate ship according to direction of input
        if (rotateInput != 0)
        {
            if (rotateInput > 0)
            {
                // Rotate left
                rotationAmount *= -1;
            }
            else if (rotateInput < 0)
            {
                // Rotate right
                rotationAmount *= 1;
            }
            else
            {
                // Do not rotate
                rotationAmount *= 0;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
        }

        // For thrust in right direction
        // Store eulerAngles.z of transform of object and convert it to radians using Mathf
        float ZRotation = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Then finding and applying the appropriate x and y components of thrustDirection
        // By using the sine and cosine of ZRotation
        thrustDirection.x = Mathf.Cos(ZRotation);
        thrustDirection.y = Mathf.Sin(ZRotation);

        // Note that here we are using cosine for x and sine for y values because 
        // On a graph, x value is the adjacent side(i.e cos) 
        // and y value is opposite side(i.e sin)
        // This is a bit tricky but I know you'll understand well

    }

    // Method used for physics-based actions
    void FixedUpdate()
    {
        // Store input for thrust
        float thrustInput = Input.GetAxis("Thrust");

        // To detect input on thrust axis and apply thrust force
        if (thrustInput > 0)
        {
            // Add force to ship
            rb2d.AddForce(thrustForce * thrustDirection, ForceMode2D.Force);

            // Play thrust sound
            AudioManager.Play(AudioClipName.ShipThrust);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid Big" ^ collision.gameObject.tag == "Asteroid Small")
        {
            // Play death sound
            AudioManager.Play(AudioClipName.PlayerDeath);

            // Destroy ship on collision with asteroid
            Destroy(gameObject);

            // Stop game timer
            HUD hud = CanvasHUD.GetComponent<HUD>();
            hud.StopGameTimer();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Fire()
    {
        // 1.) Fire the bullet

        // 1.a) Instantiate the bullet
        GameObject bullet = Instantiate<GameObject>(prefabBullet, shipPosition, shipRotation);

        // 1.b) Use apply force method from bullet
        bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);

        // 1.c) Play shoot sound effect
        AudioManager.Play(AudioClipName.PlayerShot);

    }


    #endregion
}
