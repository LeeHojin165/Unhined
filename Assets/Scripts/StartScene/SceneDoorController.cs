using UnityEngine;

public class SceneDoorController : MonoBehaviour
{
    [Header("열림 각도 (Y축, 도)")]
    [SerializeField] private float closedAngle = 0f;
    [SerializeField] private float openAngle = 90f;     // 반대로 열리면 -90

    [Header("아이들 끼익 동작")]
    [SerializeField] private float idleInterval = 5f;    // 몇 초마다
    [SerializeField] private float idleOpenAmount = 0.3f;  // 살짝만 (0~1)
    [SerializeField] private float idleSpeed = 0.6f;  // 천천히 여닫기

    [Header("호버 시")]
    [SerializeField] private float hoverSpeed = 3.5f;

    [Header("사운드")]
    [SerializeField] private AudioClip creakClip;
    [SerializeField, Range(0f, 1f)] private float creakVolume = 1f;

    private float currentOpen = 0f, targetOpen = 0f;
    private bool isHovering = false;
    private float idleTimer = 0f;
    private bool idleActive = false, idleSwingingOut = false;

    void Awake() => ApplyOpen(0f);

    void Update()
    {
        if (isHovering)
        {
            targetOpen = 1f;
            currentOpen = Mathf.MoveTowards(currentOpen, targetOpen, hoverSpeed * Time.deltaTime);
        }
        else
        {
            HandleIdle();
            currentOpen = Mathf.MoveTowards(currentOpen, targetOpen, idleSpeed * Time.deltaTime);
        }
        ApplyOpen(currentOpen);
    }

    void HandleIdle()
    {
        if (!idleActive)
        {
            targetOpen = 0f;
            if (currentOpen <= 0.01f)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleInterval)
                {
                    idleTimer = 0f;
                    idleActive = true;
                    idleSwingingOut = true;
                    targetOpen = idleOpenAmount;
                    PlayCreak();
                }
            }
            return;
        }

        if (idleSwingingOut && currentOpen >= idleOpenAmount - 0.01f)
        {
            idleSwingingOut = false;
            targetOpen = 0f;
            PlayCreak();
        }
        else if (!idleSwingingOut && currentOpen <= 0.01f)
        {
            idleActive = false;
        }
    }

    void ApplyOpen(float amount)
    {
        float y = Mathf.Lerp(closedAngle, openAngle, amount);
        transform.localEulerAngles = new Vector3(0f, y, 0f);
    }

    public void SetHover(bool hover)
    {
        if (hover != isHovering) PlayCreak();
        isHovering = hover;
        idleActive = false;
        idleTimer = 0f;
        if (!hover) targetOpen = 0f;
    }

    void PlayCreak()
    {
        if (creakClip != null && SoundManager.Instance != null)
            SoundManager.Instance.PlaySfx(creakClip, creakVolume);
    }
}