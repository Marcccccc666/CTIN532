using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
public class DefaultCameraController : MonoBehaviour
{
    [SerializeField, ChineseLabel("默认相机")] private CinemachineCamera defaultCamera;
    private CameraManager cameraManager => CameraManager.Instance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        cameraManager.SetDefaultCamera(defaultCamera);
    }

#region UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    private void OnValidate()
    {
        if (defaultCamera == null)
        {
            defaultCamera = GetComponent<CinemachineCamera>();
            if (defaultCamera == null)
            {
                Debug.LogError("DefaultCameraController脚本未找到CinemachineCamera组件，请检查！");
            }
        }
    }
#endregion
}
