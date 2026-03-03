using UnityEngine;

public class ChooseWeaponBranch : MonoBehaviour
{
    [SerializeField, ChineseLabel("分支选择界面")] private GameObject branchSelectionUI;

    [SerializeField, ChineseLabel("分支卡")] private WeaponBranch[] weaponBranches;

    private WeaponData currentWeaponDataProfab;
    private WeaponManager weaponManager => WeaponManager.Instance;
    private BuffManager buffManager => BuffManager.Instance;
    private int selectedBranchIndex = -1;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (branchSelectionUI != null)
        {
            branchSelectionUI.SetActive(false);
        }

        var currentWeaponData = weaponManager.GetCurrentWeapon;
        if(currentWeaponData != null)
        {
            UpdateSelectionUI(currentWeaponDataProfab, currentWeaponData);
        }
    }

    private void OnEnable()
    {
        weaponManager.UpgradeCurrentWeapon += OpenBranchSelectionUI;
        weaponManager.OnWeaponSwitched += UpdateSelectionUI;
    }

    private void OnDisable()
    {
        if(weaponManager != null)
        {
            weaponManager.UpgradeCurrentWeapon -= OpenBranchSelectionUI;
            weaponManager.OnWeaponSwitched -= UpdateSelectionUI;
        }
    }

    private void OpenBranchSelectionUI()
    {
        if (branchSelectionUI != null)
        {
            branchSelectionUI.SetActive(true);
        }
    }

    private void UpdateSelectionUI(WeaponData weaponDataPrefab, WeaponData newWeapon)
    {
        if(newWeapon.WeaponBaseData is InitialGunData initialGunData)
        {
            GunBrach[] gunBranches = initialGunData.GunBrachs;
            for (int i = 0; i < weaponBranches.Length; i++)
            {
                if (i < gunBranches.Length)
                {
                    weaponBranches[i].SetWeaponBranch(gunBranches[i].Data, gunBranches[i].Type);
                }
                else
                {
                    weaponBranches[i].SetWeaponBranch(null, default);
                }
            }
        }
    }

    /// <summary>
    /// 选择武器分支
    /// </summary>
    /// <param name="index">武器分支索引</param>
    public void SelectWeaponBranch(int index)
    {
        selectedBranchIndex = index;
        for (int i = 0; i < weaponBranches.Length; i++)
        {
            weaponBranches[i].SetSelectedIndicator(i == index);
        }
    }

    public void ConfirmSelection()
    {
        // 切换到选中的武器分支
        if (selectedBranchIndex >= 0 && weaponManager.GetCurrentWeapon.WeaponBaseData is InitialGunData initialGunData)
        {
            GunBrach selectedBranch = initialGunData.GunBrachs[selectedBranchIndex];
            weaponManager.SwitchWeapon(selectedBranch.Data);
        }
        // 结束升级状态，关闭分支选择界面
        weaponManager.SetIsUpgradeInProgress(false);
        branchSelectionUI.SetActive(false);

        // 触发 Buff 选择
        buffManager.RequestBuffSelection();
    }
}
