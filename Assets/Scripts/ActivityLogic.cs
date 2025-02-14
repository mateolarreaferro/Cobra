using UnityEngine;
using TMPro; 
using System.Collections;
using UnityEngine.Events;
using System.Linq;

public enum ActivityType { walking, bodyWeights, diet, random }

public class ActivityLogic : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private GameObject instructionCard;

    [Header("Events")]
    /// <summary>
    /// Invoked immediately after the instruction card is deactivated.
    /// </summary>
    public UnityEvent OnInstructionCardTurnedOff;


    private string[] walkingActivities = new string[]
    {
        "Sensory Stroll: Focus on sounds, sights, smells, and how the air feels.",
        "Gratitude Walk: Think of 3 things you're thankful for and why.",
        "Color Hunt: Pick a color; notice how many items match it.",
        "Breath & Steps: Inhale for 4 steps, exhale for 4 steps. Adjust as needed.",
        "Nature Noticing: Look for trees, clouds, birds, or any sign of nature.",
        "Memory Lane: Recall a favorite memory as you walk.",
        "One Thing Focus: Pick one object to observe—shape, texture, color.",
        "Walking Mantra: Repeat a calming phrase like 'I am present' each step.",
        "Step Count: Count your steps in sets of 10 to stay focused.",
        "Feel the Ground: Notice the shift from heel to toe with every step."
    };

    private string[] bodyWeightsActivities = new string[]
    {
        "Lower Body: 10 squats, 20s wall sit, 15 calf raises. Rest 30s.",
        "Core Activation: 15 bird-dogs, 20s plank, 10 glute bridges.",
        "Upper Body Ease: 10 wall push-ups, 15 arm circles, 10 shoulder taps.",
        "Stretch & Strength: 12 sumo squats, quad + hamstring stretches.",
        "Balance & Stability: 10 step-ups, side leg raises, 20s single-leg stand.",
        "Seated Strength: 15 seated knee lifts, 10 chair squats, 15 calf raises.",
        "Slow Core: 20s side plank, 10 bicycle crunches, 15 glute bridges.",
        "Full-Body Flow: 8 incline push-ups, 10 squats, 15 bird-dogs, 20s wall sit.",
        "Lower Body Stretch: 12 lunges, 15 calf raises, 20s hamstring stretch.",
        "Cool-Down Flow: 5 cat-cows, 20s cobra, 20s shoulder stretch, deep breaths."
    };

    // "Healthy Habits" mapped to 'diet'
    private string[] dietActivities = new string[]
    {
        "Drink ~11 cups of water today.",
        "Include fruit/veggies at every meal.",
        "Aim for ~30g protein each meal.",
        "Take three 5-10 min walks today.",
        "Get at least 8 hours of sleep tonight.",
        "Put phone away 30 mins before bed.",
        "Find 10 min for meditation today.",
        "Write 10 things you're grateful for.",
        "Write 10 positive affirmations.",
        "List 3 future goals + 1 step each."
    };

    // -----------------------------------------------------------------
    // COMBINED ARRAY FOR RANDOM
    // -----------------------------------------------------------------
    private string[] allActivities; // This will hold a combined list of walking, bodyWeights, and diet.

    // The currently selected activity type
    private ActivityType currentActivityType = ActivityType.random;

    private void Awake()
    {
        // Combine the existing three arrays into one big array for random usage
        allActivities = walkingActivities
            .Concat(bodyWeightsActivities)
            .Concat(dietActivities)
            .ToArray(); 
        // Now 'allActivities' has 30 items (10 from each).
    }

    public void SetWalking()  => currentActivityType = ActivityType.walking;
    public void SetWeights()  => currentActivityType = ActivityType.bodyWeights;
    public void SetDiet()     => currentActivityType = ActivityType.diet;
    public void SetRandom()   => currentActivityType = ActivityType.random;

    /// <summary>
    /// Called by the wheel to show one of the 10 activities in the selected category.
    /// Pass in the random slice index from the wheel.
    /// </summary>
    public void DisplayInstruction(int sliceIndex)
    {
        // We'll still use sliceIndex % 10 for the "non-random" categories.
        // For random, we can mod by the length of the combined array (30).
        
        string instruction = "";

        if (currentActivityType == ActivityType.random)
        {
            // pick from the combined array
            int randomIndex = sliceIndex % allActivities.Length; // e.g. sliceIndex % 30
            instruction = allActivities[randomIndex];
        }
        else
        {
            // for the specific category
            int activityIndex = sliceIndex % 10; 
            switch (currentActivityType)
            {
                case ActivityType.walking:
                    instruction = walkingActivities[activityIndex];
                    break;
                case ActivityType.bodyWeights:
                    instruction = bodyWeightsActivities[activityIndex];
                    break;
                case ActivityType.diet:
                    instruction = dietActivities[activityIndex];
                    break;
            }
        }

        // Hide the text object initially
        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false);
            instructionText.text = "";
        }

        // Enable the instruction card
        if (instructionCard != null)
        {
            instructionCard.SetActive(true);

            // Stop any previous coroutine so we don't stack multiple timeouts
            StopAllCoroutines();

            // Start a coroutine to show the text after 1.5 seconds
            StartCoroutine(ShowTextAfterDelay(instruction, 1f));

            // Then disable the card after 5 seconds
            StartCoroutine(DisableCardAfterDelay(5f));
        }
    }

    /// <summary>
    /// Clears the text and hides the instruction card, then invokes the UnityEvent.
    /// </summary>
    public void TurnOffInstruction()
    {
        if (instructionText != null)
        {
            instructionText.text = "";
            instructionText.gameObject.SetActive(false);
        }
        if (instructionCard != null)
        {
            OnInstructionCardTurnedOff?.Invoke();
        }
    }

    /// <summary>
    /// Coroutine that waits for 'delay' seconds, then reveals the text.
    /// </summary>
    private IEnumerator ShowTextAfterDelay(string textToShow, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (instructionText != null)
        {
            instructionText.text = textToShow;
            instructionText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Coroutine that waits for 'delay' seconds, then turns off the instruction.
    /// </summary>
    private IEnumerator DisableCardAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TurnOffInstruction();
    }
}
