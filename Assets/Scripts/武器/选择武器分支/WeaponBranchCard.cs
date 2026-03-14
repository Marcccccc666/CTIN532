using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBranchCard : MonoBehaviour
{
    [SerializeField, ChineseLabel("武器名称")] private TextMeshProUGUI weaponNameText;

    [SerializeField,ChineseLabel("武器数据")] private WeaponData weaponData;

    [SerializeField,ChineseLabel("选中时的提示UI")] private GameObject selectedIndicator;
    [SerializeField, ChineseLabel("武器图片")] private Image weaponImage;

    void OnEnable()
    {
        SetSelectedIndicator(false);
    }

    /// <summary>
    /// 设置武器分支显示内容
    /// </summary>
    public void SetWeaponBranch(WeaponData weaponData, WeaponType weaponType, Sprite weaponSprite)
    {
        this.weaponData = weaponData;
        if (weaponData != null)
        {
            string weaponName = weaponData.WeaponBaseData.WeaponName;
            weaponNameText.text = weaponName;
        }
        else
        {
            weaponNameText.text = "None";
        }

        if (weaponImage != null)
        {
            weaponImage.sprite = weaponSprite;
        }
    }

    /// <summary>
    /// 设置选中状态的显示
    /// </summary> 
    /// <param name="isSelected">是否选中</param>
    public void SetSelectedIndicator(bool isSelected)
    {
        if (selectedIndicator != null)
        {
            selectedIndicator.SetActive(isSelected);
        }
    }
}
