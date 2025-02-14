using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCircleAnim : MonoBehaviour
{
    public float speed = 1f; // Movement speed for the circles
    public Vector2 screenBounds; // Screen boundaries for bouncing
    public Color[] colors = new Color[]
    {
        new Color(0.769f, 0.718f, 0.831f), // Muted Lavender
        new Color(0.843f, 0.910f, 0.831f), // Pale Mint
        new Color(0.949f, 0.910f, 0.812f), // Light Sand
        new Color(0.655f, 0.776f, 0.855f), // Muted Blue
        new Color(0.490f, 0.490f, 0.490f)  // Warm Gray
    };

    private List<Circle> circles = new List<Circle>(); // List to manage all circles

    void Start()
    {
        // Get screen bounds based on the camera view
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Initialize all circle objects
        foreach (var obj in FindObjectsOfType<CircleCollider2D>())
        {
            GameObject circleObject = obj.gameObject;

            // Assign a random color
            var spriteRenderer = circleObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = colors[Random.Range(0, colors.Length)];
            }

            // Add a random velocity to each circle
            Rigidbody2D rb = circleObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized; // Random direction
                rb.velocity = randomDirection * speed;
                rb.gravityScale = 0f; // Ensure no gravity affects the circles
            }

            // Add the circle to the list for tracking
            circles.Add(new Circle { GameObject = circleObject, RigidBody = rb });
        }
    }

    void Update()
    {
        // Update movement and check for screen bounds
        foreach (var circle in circles)
        {
            Vector2 position = circle.GameObject.transform.position;

            // Check horizontal bounds
            if (position.x > screenBounds.x || position.x < -screenBounds.x)
            {
                circle.RigidBody.velocity = new Vector2(-circle.RigidBody.velocity.x, circle.RigidBody.velocity.y);
            }

            // Check vertical bounds
            if (position.y > screenBounds.y || position.y < -screenBounds.y)
            {
                circle.RigidBody.velocity = new Vector2(circle.RigidBody.velocity.x, -circle.RigidBody.velocity.y);
            }
        }
    }

    private class Circle
    {
        public GameObject GameObject;
        public Rigidbody2D RigidBody;
    }
}
