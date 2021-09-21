using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Force support
    [SerializeField]
    float forceMagnitude = 50;


    // Timer support
    Timer timer;



    // Start is called before the first frame update
    void Start()
    {
        // 1.) Destroy bullet after 2 seconds

        // 1.a) Start a timer
        timer = Camera.main.GetComponent<Timer>();
        timer.Duration = 2;
        timer.RunTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // 1.b) Destroy object if timer is done
        if(timer.Finished)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Detect collision with asteroids and explode
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid Big")
        {
            // 1.) Set up collision for big asteroid

            // 1.a) Instantiate big explosion
            // [INCOMPLETE]

            // 1.b) Play sound effect for big explosion
            AudioManager.Play(AudioClipName.AsteroidHitBig);

            // 1.c) Destroy bullet
            Destroy(gameObject);

            // 1.d) Destroy big asteroid
            Destroy(collision.gameObject);

            // 1.e) Break into 2 smaller asteroids 

            // Asteroid location and velocity
            Vector2 location = collision.gameObject.transform.position;

            // Random direction
            int random = (int)Random.Range(1, 5);
            Direction direction;
            if (random == 1)
            {
                direction = Direction.Up;
            }
            else if (random == 2)
            {
                direction = Direction.Right;
            }
            else if (random == 3)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Down;
            }

            // Instantiate 2 smaller asteroids
            Camera.main.GetComponent<AsteroidSpawner>().SpawnAsteroid(direction, location, 0);
            Camera.main.GetComponent<AsteroidSpawner>().SpawnAsteroid(direction, location, 0);
           
        }
        else if(collision.gameObject.tag == "Asteroid Small")
        {
            // 2.a) Play small explosion audio
            AudioManager.Play(AudioClipName.AsteroidHitSmall);

            // 2.b) Destroy bullet
            Destroy(gameObject);

            // 2.c) Instantiate small explosion
            // [INCOMPLETE]

            // 2.d) Destroy small asteroid
            Destroy(collision.gameObject);
        }
    }


    /// <summary>
    /// Apply force and make bullet move
    /// </summary>
    /// <param name="direction">direction of force to be applied</param>
    public void ApplyForce(Vector2 direction)
    {
        // 3.) Apply force to bullet with magnitude and direction
        GetComponent<Rigidbody2D>().AddForce(forceMagnitude * direction,
           ForceMode2D.Impulse);
    }
}
