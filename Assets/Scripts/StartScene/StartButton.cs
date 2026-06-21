using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class StartButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private string sceneToLoad = "GameScene";
    [SerializeField] private float normalSize = 36f;
    [SerializeField] private float hoverSize = 40f;
    [SerializeField] private SceneDoorController door;   // DoorHinge 드래그

    private TMP_Text text;

    void Awake() { text = GetComponent<TMP_Text>(); SetNormal(); }

    public void OnPointerEnter(PointerEventData e)
    {
        text.fontStyle = FontStyles.Bold;
        text.fontSize = hoverSize;
        if (door != null) door.SetHover(true);
    }

    public void OnPointerExit(PointerEventData e)
    {
        SetNormal();
        if (door != null) door.SetHover(false);
    }

    public void OnPointerClick(PointerEventData e)
        => SceneManager.LoadScene(sceneToLoad);

    private void SetNormal()
    {
        text.fontStyle = FontStyles.Normal;
        text.fontSize = normalSize;
    }
}