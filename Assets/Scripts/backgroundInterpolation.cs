using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInterpolation : MonoBehaviour
{
    [SerializeField] private Camera _camera; // Reference to the Camera
    [SerializeField] [Range(0f, 0.4f)] private float interpolationSpeed = 0.25f; // Speed of color transition

    // Predefined meditation app colors (Hex values converted to Color)
    private Color[] colors = new Color[]
    {
        new Color(0.769f, 0.718f, 0.831f), // #C4B7D4 - Muted Lavender
        new Color(0.843f, 0.910f, 0.831f), // #D7E8D4 - Pale Mint
        new Color(0.949f, 0.910f, 0.812f), // #F2E8CF - Light Sand
        new Color(0.655f, 0.776f, 0.855f), // #A7C6DA - Muted Blue
        new Color(0.490f, 0.490f, 0.490f)  // #7D7D7D - Warm Gray
    };

    private int currentColorIndex = 0; // Index of the current color in the array
    private int nextColorIndex = 1; // Index of the next color in the array
    private float t = 0.75f; // Time variable for interpolation

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
