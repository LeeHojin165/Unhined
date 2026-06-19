using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleFlicker : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Graphic[] glowGraphics;

    [Header("Slow Blink")]
    [SerializeField] private float minAlpha = 0.35f;
    [SerializeField] private float maxAlpha = 1.0f;
    [SerializeField] private float blinkSpeed = 1.2f;

    private void Awake()
    {
        if (titleText == null)
            titleText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        float t = Mathf.Sin(Time.unscaledTime * blinkSpeed) * 0.5f + 0.5f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);

        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        if (titleText != null)
        {
            Color c = titleText.color;
            c.a = alpha;
            titleText.color = c;
        }

        foreach (Graphic g in glowGraphics)
        {
            if (g == null) continue;

            Color c = g.color;
            c.a = alpha * 0.5f;
            g.color = c;
        }
    }
}