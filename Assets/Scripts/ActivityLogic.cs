using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Linq;

public enum ActivityType { walking, bodyWeights, diet, random }

public class ActivityLogic : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject instructionCard;
    
    [Header("Activity Cards")]
    [SerializeField] private GameObject[] walkingCards;
    [SerializeField] private GameObject[] bodyWeightCards;
    [SerializeField] private GameObject[] dietCards;
    
    // Combined array for random selection
    private GameObject[] allActivityCards;
    
    [Header("Events")]
    public UnityEvent OnInstructionCardTurnedOff;
    
    private ActivityType currentActivityType = ActivityType.random;
    
    private void Awake()
    {
        // Combine all activity arrays into one for random selection
        allActivityCards = walkingCards
            .Concat(bodyWeightCards)
            .Concat(dietCards)
            .ToArray();
    }
    
    public void SetWalking()  => currentActivityType = ActivityType.walking;
    public void SetWeights()  => currentActivityType = ActivityType.bodyWeights;
    public void SetDiet()     => currentActivityType = ActivityType.diet;
    public void SetRandom()   => currentActivityType = ActivityType.random;
    
    public void DisplayInstruction(int sliceIndex)
    {
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
            // Ensure the parent instruction card is active
            if (instructionCard != null && !instructionCard.activeSelf)
            {
                instructionCard.SetActive(true);
            }

            selectedCard.SetActive(true);
            
            // Stop any previous coroutine
            StopAllCoroutines();
            
            // Disable the card after a delay
            StartCoroutine(DisableCardAfterDelay(selectedCard, 5f));
        }
    }
    
    private IEnumerator DisableCardAfterDelay(GameObject card, float delay)
    {
        yield return new WaitForSeconds(delay);
        card.SetActive(false);
        OnInstructionCardTurnedOff?.Invoke();
    }
}
