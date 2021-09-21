using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To spawn asteroids in scene
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    // spawn location support
    const int SpawnBorderSize = 100;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;

    // saved to support resolution changes
    static int screenWidth;
    static int screenHeight;

    // Impulse Force Support
    const float MinImpulseForce = 3f;
    const float MaxImpulseForce = 5f;
    new Rigidbody2D rigidbody;

    //For efficiency
    GameObject asteroid;

    // Reference to other scripts
    // public Asteroid asteroidScript;
    Asteroid asteroidScript;


    // Start is called before the first frame update
    void Start()
    {
        // For efficiency
        float screenZ = -Camera.main.transform.position.z;

        // Intitialise the screenutils script
        // ScreenUtils.Initialize();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Reference to asteroid script
        // asteroidScript = asteroid.GetComponent<Asteroid>();

        // Save the collider radius of asteroid
        GameObject tempAsteroid = Instantiate(prefabAsteroid) as GameObject;
        CircleCollider2D circleCollider = tempAsteroid.GetComponent<CircleCollider2D>();
        float colliderRadius = circleCollider.radius;
        Destroy(tempAsteroid);

        
        // Spawn an asteroid (Test)
        // Right side of screen
        SpawnAsteroid(Direction.Left, new Vector2(ScreenUtils.ScreenRight + colliderRadius,
            ScreenUtils.ScreenBottom + screenHeight / 2), 1);

        // Right side of screen
        SpawnAsteroid(Direction.Left, new Vector2(ScreenUtils.ScreenRight + colliderRadius,
            ScreenUtils.ScreenBottom + screenHeight / 2), 1);

        /*
        // Left side of screen
        SpawnAsteroid(Direction.Right, new Vector2(ScreenUtils.ScreenLeft - colliderRadius,
                ScreenUtils.ScreenTop - screenHeight / 2), 1);

        // Top of screen
        SpawnAsteroid(Direction.Down, new Vector2(ScreenUtils.ScreenLeft + screenWidth / 2,
                ScreenUtils.ScreenTop + colliderRadius), 1);

        
        // Bottom of screen
        SpawnAsteroid(Direction.Up, new Vector2(ScreenUtils.ScreenLeft - screenWidth / 2,
                ScreenUtils.ScreenBottom - colliderRadius), 1);
        */

    }


    /// <summary>
    /// Method to spawn asteroid
    /// </summary>
    /// <param name="direction">Direction of movement in form "Direction.Up/Down/etc"</param>
    /// <param name="location">Vector3 with spawn location</param>
    /// <param name="size">Int with 0 for small and 1 for big</param>
    public void SpawnAsteroid(Direction direction, Vector3 location, int size)
    {
        //Instantiate asteroid prefab as game object
        asteroid = Instantiate(prefabAsteroid) as GameObject;

        //Reference to asteroid script
        asteroidScript = asteroid.GetComponent<Asteroid>();

        //Initialise asteroid with direction from asteroid script
        asteroidScript.Initialise(direction, location, size);
        
    }
}
