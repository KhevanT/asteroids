using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    // Circle Collider Support
    CircleCollider2D circleCollider;
    float circleColliderRadius;

    // Start is called before the first frame update
    void Start()
    {
        // To obtain circle collider and its radius
        circleCollider = GetComponent<CircleCollider2D>();
        circleColliderRadius = circleCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Object Wrapping

        // 1.a) Obtain position of object
        Vector2 position = transform.position;

        // 1.b) Change position if object leaves bounds
        // If object goes right
        if (position.x + circleColliderRadius > ScreenUtils.ScreenRight)
        {
            position.x -= ScreenUtils.ScreenRight * 2;
        }

        // If object goes left
        if (position.x - circleColliderRadius < ScreenUtils.ScreenLeft)
        {
            position.x += -ScreenUtils.ScreenLeft * 2;
        }

        // If object goes up
        if (position.y + circleColliderRadius > ScreenUtils.ScreenTop)
        {
            position.y -= ScreenUtils.ScreenTop * 2;
        }

        // If object goes down
        if (position.y - circleColliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y += -ScreenUtils.ScreenBottom * 2;
        }

        // 1.c) Apply newly changed position
        transform.position = position;
    }
}
