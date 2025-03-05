using UnityEngine;
using System.Collections;

public class GrowAndShrink : MonoBehaviour
{
    public enum ScaleEase
    {
        Linear,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInBounce,
        EaseOutBounce
    }

    [Header("Default Scale Values")]
    [Tooltip("The scale to grow to (can be overridden by passing parameters).")]
    public Vector3 grownScale = new Vector3(1.2f, 1.2f, 1f);

    [Tooltip("The scale to shrink to (can be overridden by passing parameters).")]
    public Vector3 shrunkScale = Vector3.one;

    [Tooltip("The duration for the scale animation (seconds).")]
    public float duration = 0.5f;

    [Header("Animation Settings")]
    [Tooltip("Select the easing type for the scale animation.")]
    public ScaleEase easeType = ScaleEase.EaseInCubic;

    [Tooltip("If true, this GameObject will automatically grow to 'grownScale' on OnEnable.")]
    public bool growOnEnable = false;

    // To ensure only one animation is running at a time.
    private Coroutine currentAnim;

    private void OnEnable()
    {
        if (growOnEnable)
        {
            // Automatically trigger a "Grow" if requested.
            Grow();
        }
    }

    /// <summary>
    /// Grows this GameObject's transform to the default grown scale over the default duration.
    /// This method can be called directly from a Unity event.
    /// </summary>
    public void Grow()
    {
        Grow(grownScale, duration);
    }

    /// <summary>
    /// Grows this GameObject's transform to the specified target scale over the given duration.
    /// </summary>
    public void Grow(Vector3 targetScale, float time)
    {
        // If an animation is already running, stop it.
        if (currentAnim != null)
        {
            StopCoroutine(currentAnim);
        }
        currentAnim = StartCoroutine(AnimateScale(targetScale, time));
    }

    /// <summary>
    /// Shrinks this GameObject's transform to the default shrunk scale over the default duration.
    /// This method can be called directly from a Unity event.
    /// </summary>
    public void Shrink()
    {
        Shrink(shrunkScale, duration);
    }

    /// <summary>
    /// Shrinks this GameObject's transform to the specified target scale over the given duration.
    /// </summary>
    public void Shrink(Vector3 targetScale, float time)
    {
        // If an animation is already running, stop it.
        if (currentAnim != null)
        {
            StopCoroutine(currentAnim);
        }
        currentAnim = StartCoroutine(AnimateScale(targetScale, time));
    }

    /// <summary>
    /// Shrinks this GameObject and then disables it when the shrink animation is complete.
    /// Checks if the GameObject is active before attempting to start the coroutine.
    /// </summary>
    public void ShrinkAndDisable()
    {
        // If this GameObject is inactive, do nothing.
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        // Stop any running animations first.
        if (currentAnim != null)
        {
            StopCoroutine(currentAnim);
        }
        currentAnim = StartCoroutine(AnimateScaleAndDisable(shrunkScale, duration));
    }

    /// <summary>
    /// Animates the scale from the current scale to the target scale over a specified duration,
    /// using the selected easing function.
    /// </summary>
    private IEnumerator AnimateScale(Vector3 targetScale, float time)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / time);

            // Evaluate t with the chosen easing function.
            float easedT = EvaluateEase(t, easeType);

            transform.localScale = Vector3.Lerp(startScale, targetScale, easedT);
            yield return null;
        }

        // Ensure final scale is set.
        transform.localScale = targetScale;
    }

    /// <summary>
    /// Animates the scale from the current scale to the target scale over a specified duration,
    /// then disables the GameObject at the end of the animation.
    /// </summary>
    private IEnumerator AnimateScaleAndDisable(Vector3 targetScale, float time)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / time);

            // Evaluate t with the chosen easing function.
            float easedT = EvaluateEase(t, easeType);

            transform.localScale = Vector3.Lerp(startScale, targetScale, easedT);
            yield return null;
        }

        // Ensure final scale is set.
        transform.localScale = targetScale;

        // Disable the GameObject.
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the eased value of t based on the selected ease type.
    /// These are simplified versions of common easing formulas.
    /// </summary>
    private float EvaluateEase(float t, ScaleEase ease)
    {
        switch (ease)
        {
            case ScaleEase.Linear:
                return t;

            case ScaleEase.EaseInCubic:
                return t * t * t;

            case ScaleEase.EaseOutCubic:
                float tInv = 1f - t;
                return 1f - tInv * tInv * tInv;

            case ScaleEase.EaseInOutCubic:
                if (t < 0.5f)
                {
                    return 4f * t * t * t;
                }
                else
                {
                    float f = (2f * t) - 2f;
                    return 0.5f * f * f * f + 1f;
                }

            case ScaleEase.EaseInBounce:
                return 1f - EaseOutBounce(1f - t);

            case ScaleEase.EaseOutBounce:
                return EaseOutBounce(t);

            default:
                return t;
        }
    }

    /// <summary>
    /// A common formula for 'EaseOutBounce'.
    /// </summary>
    private float EaseOutBounce(float t)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1f / d1)
        {
            return n1 * t * t;
        }
        else if (t < 2f / d1)
        {
            t -= 1.5f / d1;
            return n1 * t * t + 0.75f;
        }
        else if (t < 2.5f / d1)
        {
            t -= 2.25f / d1;
            return n1 * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / d1;
            return n1 * t * t + 0.984375f;
        }
    }
}
