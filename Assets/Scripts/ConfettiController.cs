using UnityEngine;
using UnityEngine.UI;

public class ConfettiController : MonoBehaviour
{
    // Reference to the confetti ParticleSystem
    public ParticleSystem confettiParticle;

    // Reference to the UI Button
    public Button celebrationButton;

    void Start()
    {
        // Ensure the particle system is not playing at start.
        confettiParticle.Stop();

        // Add listener for button click
        celebrationButton.onClick.AddListener(PlayConfetti);
    }

    void PlayConfetti()
    {
        // Play the confetti effect
        confettiParticle.Play();
    }
}
