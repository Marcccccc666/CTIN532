using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public GameObject weapon;
    [SerializeField, ChineseLabel("提示交互")] private GameObject interactionHint;
    private InputManager inputManager => InputManager.Instance;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inputManager.OnInteractionPressed += GetWeapon;
            interactionHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inputManager.OnInteractionPressed -= GetWeapon;
            interactionHint.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (inputManager != null)
            inputManager.OnInteractionPressed -= GetWeapon;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        if (inputManager != null)
            inputManager.OnInteractionPressed -= GetWeapon;
    }

    private void GetWeapon()
    {
        weapon.SetActive(true);
        Destroy(gameObject);
    }
}
