using UnityEngine;
using TMPro;

public class BuffUIUpdata : MonoBehaviour
{
    [SerializeField, ChineseLabel("Buff名字")] private TextMeshProUGUI buffNameText;
    [SerializeField, ChineseLabel("Buff描述")] private TextMeshProUGUI buffDescriptionText;
    [SerializeField, ChineseLabel("Buff图标")] private TextMeshProUGUI buffIconText;

    [SerializeField, ChineseLabel("被选中时的提示UI")] private GameObject selectedIndicator;

    /// <summary>
    /// 更新 Buff UI 显示内容
    /// </summary>
    public void UpdateBuffUI(BuffDefinition buffDefinition)
    {
        if (buffDefinition != null)
        {
            buffNameText.text = buffDefinition.DisplayName;
            buffDescriptionText.text = buffDefinition.Description;
        }
        else
        {
            buffNameText.text = "无";
            buffDescriptionText.text = "无描述";
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
