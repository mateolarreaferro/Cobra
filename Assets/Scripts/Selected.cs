using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject bodyCard;
    [SerializeField] private GameObject walkingCard;
    [SerializeField] private GameObject dietCard;

    /// <summary>
    /// Activates the card corresponding to the given activity type and disables the others.
    /// </summary>
    public void SetCards(ActivityType activityType)
    {
        switch (activityType)
        {
            case ActivityType.walking:
                walkingCard.SetActive(true);
                bodyCard.SetActive(false);
                dietCard.SetActive(false);
                break;
            case ActivityType.bodyWeights:
                walkingCard.SetActive(false);
                bodyCard.SetActive(true);
                dietCard.SetActive(false);
                break;
            case ActivityType.diet:
                walkingCard.SetActive(false);
                bodyCard.SetActive(false);
                dietCard.SetActive(true);
                break;
            case ActivityType.random:
            default:
                // For random, you might decide to hide all cards or pick one at random.
                walkingCard.SetActive(false);
                bodyCard.SetActive(false);
                dietCard.SetActive(false);
                break;
        }
    }

    public void EnableSelectedCircle()
    {
        selectedObject.SetActive(true);
    }

    public void DisableSelectedCircle()
    {
        selectedObject.SetActive(false);
    }

    // Subscribe to category change events so that this Selected updates its card display.
    private void OnEnable()
    {
        ActivityLogic.OnCategoryChanged += UpdateCards;
    }

    private void OnDisable()
    {
        ActivityLogic.OnCategoryChanged -= UpdateCards;
    }

    private void UpdateCards(ActivityType newActivityType)
    {
        SetCards(newActivityType);
    }
}
