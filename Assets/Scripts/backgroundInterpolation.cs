using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInterpolation : MonoBehaviour
{
    [SerializeField] private Camera _camera; // Reference to the Camera
    [SerializeField] [Range(0f, 0.4f)] private float interpolationSpeed = 0.2f; // Speed of color transition

    // Pastel versions of the new brand colors
    private Color[] colors = new Color[]
    {
        // #F5F5F5
        new Color(0.96f, 0.96f, 0.96f),

        // #D0FAFA (pastel version of #00F5F5)
        new Color(0.82f, 0.98f, 0.98f),

        // #E0FBD1 (pastel version of #a6e866)
        new Color(0.88f, 0.98f, 0.82f),

        // #FFE6CC (pastel version of #ff9a26)
        new Color(1.00f, 0.90f, 0.80f),

    };

    private int currentColorIndex = 0; // Index of the current color in the array
    private int nextColorIndex = 1;    // Index of the next color in the array
    private float t = 0.75f;          // Time variable for interpolation

    void Start()
    {
        // Get the Camera component if not already assigned
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
        }

        // Set the initial background color
        _camera.backgroundColor = colors[currentColorIndex];
    }

    void Update()
    {
        // Gradually interpolate between the current color and the next color
        t += Time.deltaTime * interpolationSpeed;

        // Use a smoothstep interpolation for a smoother transition
        _camera.backgroundColor = Color.Lerp(
            colors[currentColorIndex],
            colors[nextColorIndex],
            Mathf.SmoothStep(0f, 1f, t)
        );

        // If the interpolation is complete, update the color indices
        if (t >= 1f)
        {
            t = 0f; // Reset the time variable
            currentColorIndex = nextColorIndex; // Move to the next color
            nextColorIndex = (nextColorIndex + 1) % colors.Length; // Loop to the start if needed
        }
    }
}
