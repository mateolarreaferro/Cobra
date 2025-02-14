using UnityEngine;
using System.Collections;

public class RectTransformScaler : MonoBehaviour
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

    [Tooltip("If true, the rect transform will automatically grow to 'grownScale' on OnEnable.")]
    public bool growOnEnable = false;

    // Cache the RectTransform component
    private RectTransform rectTrans;
    
    // To ensure only one animation is running at a time.
    private Coroutine currentAnim;

    private void Awake()
    {
        // Get the RectTransform component attached to this GameObject.
        rectTrans = GetComponent<RectTransform>();
        if (rectTrans == null)
        {
            Debug.LogError("RectTransformScaler requires a RectTransform component on the same GameObject.");
        }
    }

    private void OnEnable()
    {
        if (growOnEnable)
        {
            // Automatically trigger a "Grow" if requested
            Grow();
        }
    }

    /// <summary>
    /// Grows the rect transform to the default grown scale over the default duration.
    /// This method can be called directly from a Unity event.
    /// </summary>
    public void Grow()
    {
        Grow(grownScale, duration);
    }

    /// <summary>
    /// Grows the rect transform to the specified target scale over the given duration.
    /// </summary>
    /// <param name="targetScale">The desired target scale.</param>
    /// <param name="time">Duration of the animation in seconds.</param>
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
    /// Shrinks the rect transform to the default shrunk scale over the default duration.
    /// This method can be called directly from a Unity event.
    /// </summary>
    public void Shrink()
    {
        Shrink(shrunkScale, duration);
    }

    /// <summary>
    /// Shrinks the rect transform to the specified target scale over the given duration.
    /// </summary>
    /// <param name="targetScale">The desired target scale.</param>
    /// <param name="time">Duration of the animation in seconds.</param>
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
    /// Animates the scale change from the current scale to the target scale over a specified duration,
    /// using the selected easing function.
    /// </summary>
    private IEnumerator AnimateScale(Vector3 targetScale, float time)
    {
        Vector3 startScale = rectTrans.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            // Normalized time 0..1
            float t = Mathf.Clamp01(elapsedTime / time);
            
            // Evaluate t with the chosen easing function
            float easedT = EvaluateEase(t, easeType);

            rectTrans.localScale = Vector3.Lerp(startScale, targetScale, easedT);
            yield return null;
        }
        // Ensure the final scale is set.
        rectTrans.localScale = targetScale;
    }

    /// <summary>
    /// Returns the eased value of t based on the selected ease type.
    /// These are simplified versions of common easing formulas (similar to DOTween styles).
    /// </summary>
    private float EvaluateEase(float t, ScaleEase ease)
    {
        switch (ease)
        {
            case ScaleEase.Linear:
                return t;

            case ScaleEase.EaseInCubic:
                // f(t) = t^3
                return t * t * t;

            case ScaleEase.EaseOutCubic:
                // f(t) = 1 - (1 - t)^3
                float tInv = 1f - t;
                return 1f - tInv * tInv * tInv;

            case ScaleEase.EaseInOutCubic:
                // f(t) = 4t^3 if t < 0.5 else 1 - (-2t + 2)^3 / 2
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
                // In-bounce is just 1 - outBounce(1 - t)
                return 1f - EaseOutBounce(1f - t);

            case ScaleEase.EaseOutBounce:
                return EaseOutBounce(t);

            default:
                return t;
        }
    }

    /// <summary>
    /// A common formula for 'EaseOutBounce'.
    /// (Similar to DOTween's easeOutBounce implementation.)
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
