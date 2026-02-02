# Project Folder Index

**Project:** git版本 (Unity)  
**Root:** `d:\Unity\git版本`

---

## Root

| Item | Type | Description |
|------|------|-------------|
| CITN532.slnx | Solution | Solution file |
| CTIN532.slnx | Solution | Solution file |
| .gitignore | Config | Git ignore rules |
| .vscode/ | Folder | VS Code / Cursor settings |
| Assets/ | Folder | Main Unity content |
| Packages/ | Folder | Unity package manifest |
| ProjectSettings/ | Folder | Unity project settings |

---

## .vscode/

| File | Description |
|------|-------------|
| extensions.json | Recommended extensions |
| launch.json | Debug/run configurations |
| settings.json | Editor settings |

---

## Assets/

### Top-level assets
- DefaultVolumeProfile.asset
- InputSystem_Actions.inputactions
- InputSystem.inputsettings.asset
- UniversalRenderPipelineGlobalSettings.asset

### Animator/
- **Caesar/** — Caesar animations (Back, Forward, Idle, Left, Right) + controller
- **武器/枪/** — Gun shoot animation + controller

### Art/
- **剑/** — Sword slash sprites (挥剑135度 1–4)
- **枪/** — Gun weapon animation sprite
- **角色/** — Character sprites (stand, walk, roll, hand walking, etc.)

### music/
- **True 8-bit Sound Effect Collection - Lite/** — Sound effects (Alarms, Gunshots, Jumps, UI Clicks, etc.)

### Profab/
- 子弹.prefab (bullet)
- 敌人1.prefab (enemy 1)

### Scenes/
- Tom场景.unity

### ScriptableObject/
- CaesarBaseData.asset, Enemy1.asset
- **关卡/主题1/** — Theme 1 + levels 1, 2, 3
- **敌人/** — Enemy base data
- **武器/** — GunBaseData.asset

### Scripts/

| Folder | Contents |
|--------|----------|
| **Buff/** | Buff system (definitions, manager, pool, triggers) and buff assets (Damage/Heal/MoveSpeed) |
| **Enemy/** | Enemy1/Enemy2 HFSM and states, combat/health/movement, EnemyHit, EnemyMaker, EnemyBaseData, EnemyData |
| **UI/** | CheckoutPage, HPUpdata |
| **关卡/** | LevelController, LevelData, ThemeData |
| **插件/** | ChineseLab (ChineseLabel, ChineseListLabel), StateMachine (BaseState), Timer (DownTimer, UpTimer, MultiTimerManager), 画圈 (AttackRangeGizmo) |
| **模版/** | Singleton |
| **武器/** | WeaponBase, WeaponBaseData; 枪: GunBaseData, GunController |
| **游戏对象通用方法/** | CharacterMove, CharacterRotation |
| **管理器/** | GameManager, WeaponManager; 关卡管理器, 敌人管理器, 角色管理器 |
| **角色/** | BulletAttack; 凯撒: CaesarBaseData, CaesarData, 角色状态机 (Attack, Die, Idle, Move); 角色数据基类, 角色状态机基类 |
| **角色数据基类/** | ObjectBaseData, ObjectData |
| **输入/** | InputData, PlayerInput |

### Settings/
- URP and related settings, scene template

### TextMesh Pro/
- Fonts, Resources (Fonts & Materials, Sprite Assets, Style Sheets, TMP Settings), Shaders, Sprites

---

## Packages/

| File | Description |
|------|-------------|
| manifest.json | Package dependencies |
| packages-lock.json | Locked versions |

---

## ProjectSettings/

Unity configuration assets: AudioManager, DynamicsManager, EditorBuildSettings, GraphicsSettings, InputManager, Physics2DSettings, ProjectSettings, QualitySettings, TagManager, URPProjectSettings, etc.

---

*Generated index of project folder structure. `.meta` files omitted for readability.*
