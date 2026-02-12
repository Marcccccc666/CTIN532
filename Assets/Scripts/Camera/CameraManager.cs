using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    /// <summary>
    /// 默认虚拟相机
    /// </summary>
    [SerializeField]private CinemachineCamera defaultCamera;

    /// <summary>
    /// 当前虚拟相机
    /// </summary>
    private CinemachineCamera currentCamera;

    /// <summary>
    /// 获取当前虚拟相机
    /// </summary>
    public CinemachineCamera GetCurrentCamera => currentCamera;

    /// <summary>
    /// 设置默认虚拟相机
    /// </summary>
    public void SetDefaultCamera(CinemachineCamera camera)
    {
        defaultCamera = camera;
        if (currentCamera == null)
        {
            SetCurrentCamera(defaultCamera);
        }
    }

    /// <summary>
    /// 设置当前虚拟相机
    /// </summary>
    /// <param name="camera"></param>
    public void SetCurrentCamera(CinemachineCamera camera)
    {
        if(currentCamera != null)
        {
            currentCamera.Priority = 0; // 将当前相机优先级降低
        }
        currentCamera = camera;
        currentCamera.Priority = 20; // 设置当前相机优先级为最高
    }

    /// <summary>
    /// 重置为默认相机
    /// </summary>
    public void ResetToDefaultCamera()
    {
        SetCurrentCamera(defaultCamera);
    }

}
