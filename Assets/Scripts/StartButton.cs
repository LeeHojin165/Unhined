using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private string nextSceneName = "Stage1";

    [Header("Hover")]
    [SerializeField] private float hoverScale = 1.06f;
    [SerializeField] private float smoothSpeed = 12f;

    [Header("Color")]
    [SerializeField] private Color normalColor = new Color(0.72f, 0.69f, 0.78f, 0.75f);
    [SerializeField] private Color hoverColor = new Color(1f, 0.92f, 1f, 1f);

    private Vector3 baseScale;
    private Vector3 targetScale;
    private bool isLoading;

    private void Awake()
    {
        if (label == null)
            label = GetComponentInChildren<TMP_Text>();

        baseScale = transform.localScale;
        targetScale = baseScale;

        if (label != null)
        {
            label.color = normalColor;
            label.fontStyle = FontStyles.Normal;
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.unscaledDeltaTime * smoothSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLoading) return;

        targetScale = baseScale * hoverScale;

        if (label != null)
        {
            label.color = hoverColor;
            label.fontStyle = FontStyles.Bold;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLoading) return;

        targetScale = baseScale;

        if (label != null)
        {
            label.color = normalColor;
            label.fontStyle = FontStyles.Normal;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLoading) return;
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        isLoading = true;

        if (label != null)
        {
            label.text = "Loading...";
            label.fontStyle = FontStyles.Bold;
        }

        yield return SceneManager.LoadSceneAsync(nextSceneName);
    }
}