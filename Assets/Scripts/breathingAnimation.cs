using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _circleObject; // The 2D object to animate
    [SerializeField] private float scaleAmplitude = 0.2f; // Maximum scale deviation from the base scale
    [SerializeField] private float scaleFrequency = 0.3f; // Breathing frequency (in cycles per second)

    private Vector3 originalScale; // Store the original scale of the object

    void Start()
    {
        // Save the original scale of the object
        if (_circleObject != null)
        {
            originalScale = _circleObject.transform.localScale;
        }
        else
        {
            Debug.LogError("No GameObject assigned to _circleObject.");
        }
    }

    void Update()
    {
        if (_circleObject != null)
        {
            // Calculate the scale factor using a sine wave
            float scaleFactor = 1.0f + Mathf.Sin(Time.time * Mathf.PI * 2 * scaleFrequency) * scaleAmplitude;

            // Apply the new scale uniformly (X and Y only for 2D objects)
            _circleObject.transform.localScale = new Vector3(originalScale.x * scaleFactor, originalScale.y * scaleFactor, originalScale.z);
        }
    }
}
