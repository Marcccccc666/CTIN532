using UnityEngine;

public class HateRangeGizmo : MonoBehaviour
{
    [SerializeField, ChineseLabel("仇恨范围")] private float radius = 8f;
    [SerializeField, ChineseLabel("颜色")] private Color color = Color.yellow;
    [SerializeField, ChineseLabel("分段数")] private int segments = 60;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Vector3 center = transform.position;
        Vector3 prevPoint = center + Vector3.right * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            Vector3 newPoint = center + new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f
            );

            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }

    public float GetHateRange => radius;
}
