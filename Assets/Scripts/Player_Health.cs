using UnityEngine;
using UnityEngine.UI;
public class Player_Health : MonoBehaviour
{
    public int current_Health;
    public int max_Health;
    public Image healthBarFill;
    public GameObject healthBarCanvas;

    void Start()
    {
        current_Health = max_Health;
        UpdateHealthBar();
    }
    public void change_Health(int amount)
    {
        current_Health += amount;
        current_Health = Mathf.Clamp(current_Health, 0, max_Health);
        UpdateHealthBar();
          if (current_Health <= 0)
        {
            gameObject.SetActive(false);
            if (healthBarCanvas != null) healthBarCanvas.SetActive(false);
        }
    }
    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)current_Health / max_Health;
        }
    }
}
