using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField, ChineseLabel("Boss数据")] private EnemyData bossData;
    [SerializeField, ChineseLabel("血条Slider")] private Slider hpSlider;
    [SerializeField, ChineseLabel("Boss名称文本")] private TextMeshProUGUI nameText;
    [SerializeField, ChineseLabel("Boss名称")] private string bossName = "BOSS";

    private CanvasGroup canvasGroup;
    private bool isSubscribed;
    private bool isShowing;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        SetVisible(false);
    }

    private void Start()
    {
        if (nameText != null && !string.IsNullOrEmpty(bossName))
            nameText.text = bossName;
        TrySubscribe();
    }

    private void Update()
    {
        if (!isSubscribed)
            TrySubscribe();

        if (!isShowing && bossData != null && bossData.PlayerEnterRoom)
            Show();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void TrySubscribe()
    {
        if (isSubscribed || bossData == null)
            return;

        bossData.OnDamage += OnHPChanged;
        bossData.OnHeal += OnHPChanged;
        bossData.OnDie += OnBossDie;
        isSubscribed = true;
    }

    private void Unsubscribe()
    {
        if (!isSubscribed || bossData == null)
            return;

        bossData.OnDamage -= OnHPChanged;
        bossData.OnHeal -= OnHPChanged;
        bossData.OnDie -= OnBossDie;
        isSubscribed = false;
    }

    private void Show()
    {
        isShowing = true;
        SetVisible(true);
        RefreshHP();
    }

    private void Hide()
    {
        isShowing = false;
        SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        if (canvasGroup == null)
            return;
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.blocksRaycasts = visible;
    }

    private void OnHPChanged(int _)
    {
        RefreshHP();
    }

    private void OnBossDie()
    {
        RefreshHP();
        Hide();
        Unsubscribe();
    }

    private void RefreshHP()
    {
        if (hpSlider == null || bossData == null)
            return;

        hpSlider.maxValue = bossData.MaxHealth;
        hpSlider.value = bossData.CurrentHealth;
    }
}
