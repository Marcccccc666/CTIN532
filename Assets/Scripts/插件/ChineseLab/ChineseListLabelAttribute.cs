using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 列表中文名称显示特性
/// 用于将Unity检查器中的List列表名称显示为中文
/// </summary>
public class ChineseListLabelAttribute : PropertyAttribute
{
    public string chineseName;
    
    public ChineseListLabelAttribute(string chineseName)
    {
        this.chineseName = chineseName;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ChineseListLabelAttribute))]
public class ChineseListLabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var att = attribute as ChineseListLabelAttribute;
        
        // 使用中文名称替换原有的标签
        var chineseLabel = new GUIContent(att.chineseName, label.tooltip);
        
        // 绘制属性字段，使用中文标签
        EditorGUI.PropertyField(position, property, chineseLabel, true);
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 返回属性的正确高度，包括展开的列表项
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}

#endif 