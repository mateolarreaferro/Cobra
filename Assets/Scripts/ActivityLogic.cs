using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using System;
using TMPro; // For TextMeshPro

public enum ActivityType { walking, bodyWeights, diet, random }

public class ActivityLogic : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject instructionCard;
    [SerializeField] private GameObject ActivityCategories; // Disabled when card is displayed
    [SerializeField] private GameObject CardOptions;         // Enabled when card is displayed
    [SerializeField] private TMP_Text pointsText;            // Displays the current points

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
    
    private int points = 0;
    
    private void Awake()
    {
        // Combine all activity arrays into one for random selection.
        allActivityCards = walkingCards.Concat(bodyWeightCards).Concat(dietCards).ToArray();
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
    
    /// <summary>
    /// Displays the instruction card with a selected activity card.
    /// Also disables ActivityCategories and enables CardOptions.
    /// The instruction card will play its grow animation.
    /// </summary>
    public void DisplayInstruction(int sliceIndex)
    {
        // Cancel any previous instruction display.
        CancelInstruction();
    
        // Hide all activity cards to clear any prior instruction.
        foreach (var card in allActivityCards)
        {
            if(card != null)
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
                    if (walkingCards.Length > activityIndex)
                        selectedCard = walkingCards[activityIndex];
                    break;
                case ActivityType.bodyWeights:
                    if (bodyWeightCards.Length > activityIndex)
                        selectedCard = bodyWeightCards[activityIndex];
                    break;
                case ActivityType.diet:
                    if (dietCards.Length > activityIndex)
                        selectedCard = dietCards[activityIndex];
                    break;
            }
        }
    
        if (selectedCard != null)
        {
            // Enable the instruction card.
            if (instructionCard != null && !instructionCard.activeSelf)
            {
                instructionCard.SetActive(true);
                // Trigger the grow animation if the instruction card has a GrowAndShrink component.
                GrowAndShrink growScript = instructionCard.GetComponent<GrowAndShrink>();
                if(growScript != null)
                {
                    // Optionally reset the scale to its shrunk state before animating.
                    instructionCard.transform.localScale = growScript.shrunkScale;
                    growScript.Grow();
                }
            }
    
            // Show the selected activity card.
            selectedCard.SetActive(true);
    
            // Disable ActivityCategories and enable CardOptions.
            if (ActivityCategories != null)
                ActivityCategories.SetActive(false);
            if (CardOptions != null)
                CardOptions.SetActive(true);
    
            // Optionally, start a fallback auto-hide coroutine if needed.
            // disableCardCoroutine = StartCoroutine(DisableCardAfterDelay(selectedCard, 5f));
        }
    }
    
    /// <summary>
    /// Called when the user completes the card.
    /// Adds a point, disables the instruction card and CardOptions,
    /// and re-enables ActivityCategories.
    /// </summary>
    public void CompleteCard()
    {
        CancelInstruction();
    
        // Add a point.
        points++;
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    
        // Disable the instruction card and CardOptions, and re-enable ActivityCategories.
        if (instructionCard != null)
            instructionCard.SetActive(false);
        if (CardOptions != null)
            CardOptions.SetActive(false);
        if (ActivityCategories != null)
            ActivityCategories.SetActive(true);
    
        OnInstructionCardTurnedOff?.Invoke();
    }
    
    /// <summary>
    /// Called when the user discards the card.
    /// Does not add a point, but disables the instruction card and CardOptions,
    /// and re-enables ActivityCategories.
    /// </summary>
    public void DiscardCard()
    {
        CancelInstruction();
    
        if (instructionCard != null)
            instructionCard.SetActive(false);
        if (CardOptions != null)
            CardOptions.SetActive(false);
        if (ActivityCategories != null)
            ActivityCategories.SetActive(true);
    
        OnInstructionCardTurnedOff?.Invoke();
    }
    
    /// <summary>
    /// Cancels any active instruction display.
    /// Stops any pending disable coroutine, hides all activity cards,
    /// disables the instruction card and CardOptions, and re-enables ActivityCategories.
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
            if(card != null)
                card.SetActive(false);
        }
    
        if (instructionCard != null)
            instructionCard.SetActive(false);
    
        if (CardOptions != null)
            CardOptions.SetActive(false);
        if (ActivityCategories != null)
            ActivityCategories.SetActive(true);
    }
    
    // Optional fallback coroutine if you want auto-hide after a delay.
    private IEnumerator DisableCardAfterDelay(GameObject card, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (card != null)
            card.SetActive(false);
        if (instructionCard != null)
            instructionCard.SetActive(false);
        if (CardOptions != null)
            CardOptions.SetActive(false);
        if (ActivityCategories != null)
            ActivityCategories.SetActive(true);
    
        OnInstructionCardTurnedOff?.Invoke();
        disableCardCoroutine = null;
    }
}
