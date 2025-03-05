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

    [Header("Celebration Effects")]
    // Reference to the confetti ParticleSystem.
    [SerializeField] private ParticleSystem confettiParticle;

    private void Awake()
    {
        // Load saved points from PlayerPrefs (default to 0 if not found)
        points = PlayerPrefs.GetInt("PlayerPoints", 0);

        // Update the UI text with loaded points
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }

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
            // Enable the instruction card and trigger its grow animation.
            if (instructionCard != null && !instructionCard.activeSelf)
            {
                instructionCard.SetActive(true);
                GrowAndShrink growScript = instructionCard.GetComponent<GrowAndShrink>();
                if(growScript != null)
                {
                    // Reset scale to shrunk state and animate grow.
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
    
            // Optionally, you can start a fallback auto-hide coroutine if desired.
            // disableCardCoroutine = StartCoroutine(DisableCardAfterDelay(selectedCard, 5f));
        }
    }
    
    /// <summary>
    /// Called when the user completes the card.
    /// Adds a point, triggers the confetti effect, triggers the shrink animation on the instruction card,
    /// and after the animation resets the UI.
    /// </summary>
    public void CompleteCard()
    {
        CancelAutoHide();
    
        // Add a point.
        points++;
    
        // Save the updated points value
        PlayerPrefs.SetInt("PlayerPoints", points);
        PlayerPrefs.Save();
    
        // Update UI text
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    
        // Trigger confetti effect.
        if (confettiParticle != null)
        {
            StartCoroutine(PlayConfettiEffect());
        }
    
        // Trigger shrink animation on the instruction card.
        if (instructionCard != null)
        {
            GrowAndShrink growScript = instructionCard.GetComponent<GrowAndShrink>();
            if (growScript != null)
            {
                growScript.ShrinkAndDisable();
            }
            else
            {
                instructionCard.SetActive(false);
            }
        }
    
        // Wait for the shrink animation to complete, then reset UI.
        StartCoroutine(WaitAndResetUI());
    
        OnInstructionCardTurnedOff?.Invoke();
    }
    
    /// <summary>
    /// Called when the user discards the card.
    /// Does not add a point, but triggers the shrink animation on the instruction card,
    /// and after the animation resets the UI.
    /// </summary>
    public void DiscardCard()
    {
        CancelAutoHide();
    
        if (instructionCard != null)
        {
            GrowAndShrink growScript = instructionCard.GetComponent<GrowAndShrink>();
            if (growScript != null)
            {
                growScript.ShrinkAndDisable();
            }
            else
            {
                instructionCard.SetActive(false);
            }
        }
    
        StartCoroutine(WaitAndResetUI());
    
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
    
    /// <summary>
    /// Cancels any auto-hide coroutine without deactivating the instruction card.
    /// </summary>
    private void CancelAutoHide()
    {
        if (disableCardCoroutine != null)
        {
            StopCoroutine(disableCardCoroutine);
            disableCardCoroutine = null;
        }
    }
    
    /// <summary>
    /// Waits for the shrink animation duration (if available) then resets the UI:
    /// disables CardOptions and re-enables ActivityCategories.
    /// </summary>
    private IEnumerator WaitAndResetUI()
    {
        float waitTime = 0.5f; // default fallback duration
        if (instructionCard != null)
        {
            GrowAndShrink growScript = instructionCard.GetComponent<GrowAndShrink>();
            if (growScript != null)
            {
                waitTime = growScript.duration;
            }
        }
        yield return new WaitForSeconds(waitTime);
    
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
    
    /// <summary>
    /// Coroutine to play the confetti effect for a set duration.
    /// It plays the particle system, waits for 1 second to let it emit,
    /// then stops emission while letting the already emitted particles finish their lifetimes.
    /// </summary>
    private IEnumerator PlayConfettiEffect()
    {
        confettiParticle.Play();
        yield return new WaitForSeconds(1f);
        // Stops emitting new particles without clearing existing ones.
        confettiParticle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }
}
