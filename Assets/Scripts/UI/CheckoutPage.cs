using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CheckoutPage : MonoBehaviour
{
    [SerializeField, ChineseLabel("成功页面")] private GameObject successPanel;
    [SerializeField, ChineseLabel("失败页面")] private GameObject gameOverPanel;
    [SerializeField, ChineseLabel("下一关页面")] private GameObject nextLevelPanel;
    [SerializeField, ChineseLabel("Buff选择页面")] private GameObject buffSelectionPanel;
    [SerializeField, ChineseLabel("Buff按钮")] private Button[] buffButtons = new Button[3];
    [SerializeField, ChineseLabel("Buff按钮文字")] private TextMeshProUGUI[] buffButtonTexts = new TextMeshProUGUI[3];
    [SerializeField, ChineseLabel("选择Buff时暂停时间")] private bool pauseTimeDuringSelection = true;
    [SerializeField, ChineseLabel("结束按钮")] private GameObject restartButton;
    [SerializeField, ChineseLabel("显示下一关页面")] private bool showNextLevelPanel = false;
    private GameManager gameManager => GameManager.Instance;

    private EnemyManager enemyManager => EnemyManager.Instance;

    private CharacterManager characterManager => CharacterManager.Instance;

    private LevelManager levelManager => LevelManager.Instance;

    private BuffManager buffManager => BuffManager.Instance;
    private IReadOnlyList<BuffDefinition> currentSelection;
    private float cachedTimeScale = 1f;
    private CursorLockMode cachedCursorLockMode;
    private bool cachedCursorVisible;
    private bool cursorStateCached;

    void Awake()
    {
        gameManager.GameCheckout += checkPageOpen;
        EnsureCanvasScale();
        CacheCursorState();
        if (successPanel != null)
        {
            successPanel.SetActive(false);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (buffSelectionPanel != null)
        {
            buffSelectionPanel.SetActive(false);
        }
        SetNextLevelPanel(false);
    }

    private void OnEnable()
    {
        if (buffManager != null)
        {
            buffManager.BuffSelectionRequested += OnBuffSelectionRequested;
        }
    }

    private void OnDisable()
    {
        if (buffManager != null)
        {
            buffManager.BuffSelectionRequested -= OnBuffSelectionRequested;
        }
    }



    public void RestartGame()
    {
        gameManager.IsGameOver = false;
        Time.timeScale = 1f;
        RestoreCursorState();
        enemyManager.ClearEnemyData();
        ResetSingletons();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void checkPageOpen()
    {
        if (characterManager.GetCurrentPlayerCharacterData == null)
        {
            return;
        }

        if (characterManager.GetCurrentPlayerCharacterData.CurrentHealth <= 0)
        {
            ShowGameOverPage();
            return;
        }

        if (CanShowSuccessPage())
        {
            ShowSuccessPage();
            return;
        }
        if (CanNextLevel())
        {
            ShowBuffSelection();
        }
    }

    bool CanNextLevel()
    {
        return characterManager.GetCurrentPlayerCharacterData.CurrentHealth > 0 && enemyManager.GetEnemyDataDict.Count == 0 && !levelManager.IsLastLevel();
    }

    bool CanShowSuccessPage()
    {
        return characterManager.GetCurrentPlayerCharacterData.CurrentHealth > 0 && enemyManager.GetEnemyDataDict.Count == 0 && levelManager.IsLastLevel();
    }

    private void ShowBuffSelection()
    {
        EnsureCanvasScale();
        if (buffSelectionPanel == null || buffButtons.Length < 3 || buffButtonTexts.Length < 3)
        {
            SetNextLevelPanel(true);
            return;
        }

        SetCursorForUI(true);
        SetNextLevelPanel(false);

        if (buffManager == null)
        {
            SetNextLevelPanel(true);
            return;
        }

        if (!buffManager.HasPendingSelection)
        {
            buffManager.RequestBuffSelection();
        }
        else
        {
            OnBuffSelectionRequested(buffManager.CurrentSelection);
        }
    }

    private void OnBuffSelectionRequested(IReadOnlyList<BuffDefinition> buffs)
    {
        if (buffSelectionPanel == null || buffs == null || buffs.Count == 0)
        {
            return;
        }

        currentSelection = buffs;
        buffSelectionPanel.SetActive(true);

        if (pauseTimeDuringSelection)
        {
            cachedTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        for (int i = 0; i < buffButtons.Length; i++)
        {
            bool hasBuff = i < buffs.Count;
            buffButtons[i].gameObject.SetActive(hasBuff);
            if (!hasBuff)
            {
                continue;
            }

            BuffDefinition buff = buffs[i];
            buffButtonTexts[i].text = FormatBuffText(buff);

            int index = i;
            buffButtons[i].onClick.RemoveAllListeners();
            buffButtons[i].onClick.AddListener(() => SelectBuff(index));
        }
    }

    private void SelectBuff(int index)
    {
        if (currentSelection == null || index < 0 || index >= currentSelection.Count)
        {
            return;
        }

        CharacterDate player = characterManager.GetCurrentPlayerCharacterData;
        BuffDefinition selected = currentSelection[index];
        if (selected != null && player != null)
        {
            selected.Apply(player);
        }

        buffManager.ClearSelection();
        currentSelection = null;
        buffSelectionPanel.SetActive(false);

        ResumeTime();

        SetNextLevelPanel(false);
    }

    private string FormatBuffText(BuffDefinition buff)
    {
        if (buff == null)
        {
            return "Unknown Buff";
        }

        string name = string.IsNullOrWhiteSpace(buff.DisplayName) ? buff.name : buff.DisplayName;
        string desc = string.IsNullOrWhiteSpace(buff.Description) ? string.Empty : buff.Description;
        return string.IsNullOrWhiteSpace(desc) ? name : $"{name}\n{desc}";
    }

    private void ResumeTime()
    {
        if (!pauseTimeDuringSelection)
        {
            return;
        }

        Time.timeScale = cachedTimeScale > 0f ? cachedTimeScale : 1f;
    }

    private void SetNextLevelPanel(bool active)
    {
        if (nextLevelPanel == null)
        {
            return;
        }

        if (!showNextLevelPanel)
        {
            nextLevelPanel.SetActive(false);
            return;
        }

        nextLevelPanel.SetActive(active);
    }



    private void ShowSuccessPage()
    {
        EnsureCanvasScale();
        SetCursorForUI(true);
        if (successPanel != null)
        {
            successPanel.SetActive(true);
        }
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        if (buffSelectionPanel != null)
        {
            buffSelectionPanel.SetActive(false);
        }
        SetNextLevelPanel(false);
        restartButton.SetActive(true);
        ResumeTime();
    }

    private void ShowGameOverPage()
    {
        EnsureCanvasScale();
        SetCursorForUI(true);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        if (successPanel != null)
        {
            successPanel.SetActive(false);
        }
        if (buffSelectionPanel != null)
        {
            buffSelectionPanel.SetActive(false);
        }
        SetNextLevelPanel(false);
        restartButton.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CacheCursorState()
    {
        if (cursorStateCached)
        {
            return;
        }

        cachedCursorLockMode = Cursor.lockState;
        cachedCursorVisible = Cursor.visible;
        cursorStateCached = true;
    }

    private void SetCursorForUI(bool visible)
    {
        CacheCursorState();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = visible;
    }

    private void RestoreCursorState()
    {
        if (!cursorStateCached)
        {
            return;
        }

        Cursor.lockState = cachedCursorLockMode;
        Cursor.visible = cachedCursorVisible;
    }

    private void ResetSingletons()
    {
        DestroyIfExists<GameManager>();
        DestroyIfExists<LevelManager>();
        DestroyIfExists<EnemyManager>();
        DestroyIfExists<CharacterManager>();
        DestroyIfExists<BuffManager>();
        DestroyIfExists<WeaponManager>();
        DestroyIfExists<InputData>();
        DestroyIfExists<MultiTimerManager>();
    }

    private void DestroyIfExists<T>() where T : MonoBehaviour
    {
        T instance = FindAnyObjectByType<T>();
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
    }

    private void EnsureCanvasScale()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (rect == null)
        {
            return;
        }

        Vector3 scale = rect.localScale;
        if (Mathf.Abs(scale.x) < 0.0001f || Mathf.Abs(scale.y) < 0.0001f)
        {
            rect.localScale = Vector3.one;
        }
    }
}
