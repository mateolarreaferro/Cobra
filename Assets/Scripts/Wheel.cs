using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class Wheel : MonoBehaviour
{
    [Header("Circle Reference")]
    [SerializeField] private CircleCollider2D spawnCircle;

    [Header("Prefabs / Settings")]
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private int count = 12;
    [SerializeField] private float radiusFactor = 0.9f;

    [Header("Spin Events")]
    [SerializeField] private UnityEvent onStartRotation;
    [SerializeField] private UnityEvent onFinishRotation;

    [Header("Activity Logic Reference")]
    [SerializeField] private ActivityLogic activityLogic;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private bool isSpinning = false;

    void Start()
    {
        if (spawnCircle == null)
        {
            Debug.LogError("No spawnCircle assigned!");
            return;
        }
        if (prefabToSpawn == null)
        {
            Debug.LogError("No prefabToSpawn assigned!");
            return;
        }

        SpawnSlices();
    }

    private void SpawnSlices()
    {
        Vector2 circleCenter = spawnCircle.bounds.center;
        float circleRadius = spawnCircle.bounds.extents.x;
        float finalRadius = circleRadius * radiusFactor;

        spawnedObjects.Clear();

        for (int i = 0; i < count; i++)
        {
            float angleDeg = i * (360f / count);
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float xPos = circleCenter.x + finalRadius * Mathf.Cos(angleRad);
            float yPos = circleCenter.y + finalRadius * Mathf.Sin(angleRad);
            Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

            // Rotate each slice so its “front” faces outward.
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleDeg - 90f);

            GameObject newSlice = Instantiate(prefabToSpawn, spawnPos, rotation, spawnCircle.transform);
            newSlice.name = (i + 1).ToString();

            spawnedObjects.Add(newSlice);
        }
    }

    /// <summary>
    /// Call this method (e.g., via a button) to start the wheel spin.
    /// </summary>
    public void StartWheel()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinWheelRoutine());
        }
    }

    private IEnumerator SpinWheelRoutine()
    {
        isSpinning = true;
        onStartRotation?.Invoke();

        // Get the current rotation of the circle.
        float startAngle = spawnCircle.transform.eulerAngles.z;
        
        // --- RANDOMIZE THE SPIN FORCE ---
        // Use a random number of full spins plus a random extra angle.
        int randomSpins = Random.Range(2, 8);
        float randomExtraAngle = Random.Range(0f, 360f);
        float totalRotation = randomSpins * 360f + randomExtraAngle;
        float finalAngle = startAngle + totalRotation;

        float duration = 2f;
        float timeElapsed = 0f;

        // Animate the spin with easing.
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            float easedT = EaseInOutQuint(t);

            float currentAngle = Mathf.Lerp(startAngle, finalAngle, easedT);
            spawnCircle.transform.eulerAngles = new Vector3(0f, 0f, currentAngle);

            yield return null;
        }

        // Set the wheel to its final rotation without snapping it to a perfect 12 o'clock.
        spawnCircle.transform.eulerAngles = new Vector3(0f, 0f, finalAngle);

        // --- DETERMINE THE CHOSEN SLICE (CLOSEST TO 12 O'CLOCK) ---
        // World 12 o'clock corresponds to 90° in Unity.
        float rotatedAngle = finalAngle % 360f;
        float sliceAngle = 360f / count;
        // Calculate the angular difference from 90°.
        float diff = 90f - rotatedAngle;
        diff = (diff + 360f) % 360f;
        int chosenIndex = Mathf.RoundToInt(diff / sliceAngle) % count;

        // Use the chosen slice.
        GameObject chosenObject = spawnedObjects[chosenIndex];
        Debug.Log("Wheel landed on slice: " + chosenObject.name);

        // Call the EnableSelectedCircle() method on the 'Selected' component.
        Selected selectedComponent = chosenObject.GetComponent<Selected>();
        if (selectedComponent != null)
        {
            selectedComponent.EnableSelectedCircle();
            // Disable the selected circle 4 seconds later.
            StartCoroutine(DisableSelectedCircleAfterDelay(selectedComponent, 4f));
        }

        // Display instruction via ActivityLogic (if assigned).
        if (activityLogic != null)
        {
            activityLogic.DisplayInstruction(chosenIndex);
        }

        onFinishRotation?.Invoke();
        isSpinning = false;
    }

    private IEnumerator DisableSelectedCircleAfterDelay(Selected selectedComp, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (selectedComp != null)
        {
            selectedComp.DisableSelectedCircle();
        }
    }

    // Easing function for a smooth in/out spin.
    private float EaseInOutQuint(float t)
    {
        return t < 0.5f
            ? 16f * t * t * t * t * t
            : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
    }
}
