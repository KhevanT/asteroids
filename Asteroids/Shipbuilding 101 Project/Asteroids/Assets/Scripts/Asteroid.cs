using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Sprite selector support
    [SerializeField]
    Sprite asteroidSprite0;
    [SerializeField]
    Sprite asteroidSprite1;
    [SerializeField]
    Sprite asteroidSprite2;


    // Impulse Force Support
    const float MinImpulseForce = 3f;
    const float MaxImpulseForce = 5f;
    new Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// For asteroid spawner to set position and direction to object
    /// </summary>
    /// <param name="direction">To provide direction in "Direction.Up/Down/etc"</param>
    /// <param name="location">Vector3 with spawn location</param>
    /// <param name="size">Int with 0 for small and 1 for big</param>
    public void Initialise(Direction direction, Vector3 location, int size)
    {
        // 1. Select a random sprite for prefab

        // 1.a) Get sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // 1.b) Generate a random number to select a sprite
        int spriteNumber = Random.Range(0, 3);

        if (spriteNumber == 0)
        {
            spriteRenderer.sprite = asteroidSprite0;
        }
        else if (spriteNumber == 1)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite2;
        }

        

        // 2) Make changes for small asteroid

        if (size == 0)
        {
            // Set tag for small asteroid
            gameObject.tag = "Asteroid Small";

            // Reduce object scale by half
            gameObject.transform.localScale /= 2;

            // Reduce collider radius by half
            gameObject.GetComponent<CircleCollider2D>().radius /= 2;
        }
        else
        {
            // Set tag for big asteroid
            gameObject.tag = "Asteroid Big";
        }

        // 3.) Set spawn location for asteroid

        // 3.a) Convert location to world point
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);

        // 3.b) Set that location to the location of the object
        transform.position = worldLocation;


        // 4.) Make sure asteroid is moving in right direction

        // 4.a) find a random angle between 0 and 30
        float ranAngle = Random.Range(0, Mathf.PI / 6);

        // 4.b) Add that angle to base angle from direction paramater
        float angle;
        if (direction == Direction.Up)
        {
            //Up
            angle = 75 * Mathf.Deg2Rad + ranAngle;    
        }
        else if (direction == Direction.Down)
        {
            //Down
            angle = 255 * Mathf.Deg2Rad + ranAngle;
        }
        else if(direction == Direction.Left)
        {
            //Left
            angle = 165 * Mathf.Deg2Rad + ranAngle;
        }
        else
        {
            //Right
            angle = -15 * Mathf.Deg2Rad + ranAngle;
        }

        // 4.c) convert angle to vector
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));

        // 4.d) Get strength of force
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);

        // 4.e) Add this force with direction
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(moveDirection * magnitude, ForceMode2D.Impulse);
    }
}
