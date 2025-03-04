using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private GameObject bodyCard;
    [SerializeField] private GameObject walkingCard;
    [SerializeField] private GameObject dietCard;

    private void Start(){
        SetCards(ActivityType.random);
    }

    /// <summary>
    /// Activates the card corresponding to the given activity type and disables the others.
    /// </summary>
    public void SetCards(ActivityType activityType)
    {
        // First, disable all cards.
        walkingCard.SetActive(false);
        bodyCard.SetActive(false);
        dietCard.SetActive(false);

        switch (activityType)
        {
            case ActivityType.walking:
                walkingCard.SetActive(true);
                break;
            case ActivityType.bodyWeights:
                bodyCard.SetActive(true);
                break;
            case ActivityType.diet:
                dietCard.SetActive(true);
                break;
            case ActivityType.random:
            default:
                // For random, pick one at random from the three options.
                int randomChoice = Random.Range(0, 3); // returns 0, 1, or 2
                switch (randomChoice)
                {
                    case 0:
                        walkingCard.SetActive(true);
                        break;
                    case 1:
                        bodyCard.SetActive(true);
                        break;
                    case 2:
                        dietCard.SetActive(true);
                        break;
                }
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
