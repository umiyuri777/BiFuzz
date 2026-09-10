# Scene registry

## Current inventory

This inventory is a rename-safety aid. A scene's purpose and ownership must be verified before editing it.

| Scene path | Status / notes |
| --- | --- |
| `Assets/Scenes/Game.unity` | Primary gameplay scene; renamed from `Prospect.unity` on 2026-09-03 and opened by the README workflow |
| `Assets/Scenes/Start.unity` | Project scene; purpose to verify |
| `Assets/Scenes/Main.unity` | Project scene; purpose to verify |
| `Assets/Scenes/HowToPlay.unity` | Project scene; purpose to verify |
| `Assets/Scenes/Demo.unity` | Project scene; purpose to verify |
| `Assets/Scenes/SampleScene.unity` | Project/sample scene; ownership to verify |
| `Assets/Scenes/test_20240815.unity` | Test/backup candidate; do not rename without explicit rationale |
| `Assets/Scenes/検出機能バックアップ_test_20240815.unity` | Test/backup candidate; do not rename without explicit rationale |
| `Assets/InitTestScene638363364902858117.unity` | Generated/test candidate; ownership to verify |
| `Assets/InitTestScene638363343962344284.unity` | Generated/test candidate; ownership to verify |
| `Assets/InitTestScene638356915792695348.unity` | Generated/test candidate; ownership to verify |
| `Assets/AurynSky/Gems Ultimate Pack/Scenes/DemoScene_Gems.unity` | Imported asset demo; exclude by default |
| `Assets/ProceduralTerrainPainter/_Demo/DemoScene.unity` | Imported asset demo; exclude by default |
| `Assets/Plugins/Banana Yellow Games/Characters/Banana Man/Demo Scene/Banana Man Demo Scene.unity` | Imported asset demo; exclude by default |

## Required procedure for a scene rename

1. Confirm the source scene, target name, and project ownership.
2. Record Editor and scene dirty state; protect unsaved user work.
3. Search C# code, tests, Build Settings, configuration, and documentation for the old name/path.
4. Rename through Unity tooling/MCP, not raw YAML edits.
5. Update intentional string/path references and documentation.
6. Verify Build Settings and open the renamed scene in the Editor.
7. Run relevant scene-transition and experiment tests.

## Rename log

After each approved rename, add date, old path, new path, affected references, verification result, and checkpoint.

### 2026-09-03 — `Prospect.unity` → `Game.unity`

- Affected references: enabled Build Settings entry, `StartButtonAction`, and README setup instructions.
- Verification: Unity MCP confirmed that `Game` is active and clean, its original GUID is retained, and it is an enabled build scene. A Play Mode smoke test completed with no new Console errors.
