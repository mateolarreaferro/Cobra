using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using System;

public enum ActivityType { walking, bodyWeights, diet, random }

public class ActivityLogic : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject instructionCard;
    
    [Header("Activity Cards")]
    [SerializeField] private GameObject[] walkingCards;
    [SerializeField] private GameObject[] bodyWeightCards;
    [SerializeField] private GameObject[] dietCards;
    
    // Combined array for random selection.
    private GameObject[] allActivityCards;
    
    [Header("Events")]
    public UnityEvent OnInstructionCardTurnedOff;
    
    // Static event for category changes.
    public static event Action<ActivityType> OnCategoryChanged;
    
    private ActivityType currentActivityType = ActivityType.random;
    private Coroutine disableCardCoroutine;

    private void Awake()
    {
        // Combine all activity arrays into one for random selection.
        allActivityCards = walkingCards
            .Concat(bodyWeightCards)
            .Concat(dietCards)
            .ToArray();
    }
    
    public void SetWalking()
    {
        CancelInstruction(); // Hide any active instruction immediately.
        currentActivityType = ActivityType.walking;
        OnCategoryChanged?.Invoke(currentActivityType);
    }
    
    public void SetWeights()
    {
        CancelInstruction();
        currentActivityType = ActivityType.bodyWeights;
        OnCategoryChanged?.Invoke(currentActivityType);
    }
    
    public void SetDiet()
    {
        CancelInstruction();
        currentActivityType = ActivityType.diet;
        OnCategoryChanged?.Invoke(currentActivityType);
    }
    
    public void SetRandom()
    {
        CancelInstruction();
        currentActivityType = ActivityType.random;
        OnCategoryChanged?.Invoke(currentActivityType);
    }
    
    public void DisplayInstruction(int sliceIndex)
    {
        // Cancel any previous instruction display.
        CancelInstruction();

        // Hide all activity cards to clear any prior instruction.
        foreach (var card in allActivityCards)
        {
            card.SetActive(false);
        }
        
        GameObject selectedCard = null;
        
        if (currentActivityType == ActivityType.random)
        {
            int randomIndex = sliceIndex % allActivityCards.Length;
            selectedCard = allActivityCards[randomIndex];
        }
        else
        {
            int activityIndex = sliceIndex % 10;
            switch (currentActivityType)
            {
                case ActivityType.walking:
                    selectedCard = walkingCards[activityIndex];
                    break;
                case ActivityType.bodyWeights:
                    selectedCard = bodyWeightCards[activityIndex];
                    break;
                case ActivityType.diet:
                    selectedCard = dietCards[activityIndex];
                    break;
            }
        }

        if (selectedCard != null)
        {
            // Ensure the parent instruction card is active.
            if (instructionCard != null && !instructionCard.activeSelf)
            {
                instructionCard.SetActive(true);
            }

            selectedCard.SetActive(true);
            
            // Start the disable sequence and store its reference.
            disableCardCoroutine = StartCoroutine(DisableCardAfterDelay(selectedCard, 5f));
        }
    }
    
    private IEnumerator DisableCardAfterDelay(GameObject card, float delay)
    {
        yield return new WaitForSeconds(delay);
        card.SetActive(false);
        OnInstructionCardTurnedOff?.Invoke();
        disableCardCoroutine = null;
    }
    
    /// <summary>
    /// Cancels any active instruction display and hides all cards.
    /// </summary>
    public void CancelInstruction()
    {
        if (disableCardCoroutine != null)
        {
            StopCoroutine(disableCardCoroutine);
            disableCardCoroutine = null;
        }
        
        foreach (var card in allActivityCards)
        {
            card.SetActive(false);
        }
        
        if (instructionCard != null)
        {
            instructionCard.SetActive(false);
        }
    }
}
