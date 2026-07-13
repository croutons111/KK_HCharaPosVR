# KK_HCharaPosVR

[English](README.md) | **日本語**

> **Koikatsu VR** の H シーンで、VR コントローラーを使ってキャラクターの位置をリアルタイムに調整する BepInEx プラグインです。

---

## 概要

VR の H シーンで、右コントローラーでキャラをつかんで動かし、動かす対象を切り替えられます。位置ズレの修正や配置の微調整をその場で行えます。

> **β 版（0.7）** — HF Patch 3.36 ＋ Meta Quest 2 で動作確認済み。

---

## 動作環境

| 項目 | 要件 |
|---|---|
| ゲーム | Koikatsu（HF Patch 3.36） |
| 実行ファイル | `KoikatuVR.exe`（公式 VR 版） |
| フレームワーク | BepInEx 5.4.x |
| 依存プラグイン | [KKAPI](https://github.com/ManlyMarco/KKAPI) |
| VR ヘッドセット | Meta Quest 2（Oculus Link / Air Link 経由） |

> 動作確認は **Meta Quest 2** のみです。HTC Vive / Valve Index など他のヘッドセットは未確認です。

---

## 導入方法

1. [Releases](../../releases) から最新の `KK_HCharaPosVR.dll` をダウンロード。
2. `BepInEx/plugins/` フォルダに配置。
3. `KoikatuVR.exe` を起動（初回起動時に設定ファイルが自動生成されます）。

---

## 使い方

H シーンに入ると自動で有効になります。操作は**右コントローラーの A ボタン**のみです。

| 操作 | 動作 |
|---|---|
| A ボタンを**長押し**（0.2 秒以上） | 選択中のキャラがコントローラーに追従。離すと位置を確定。 |
| A ボタンを**素早く2回押す**（0.4 秒以内） | Female1 ↔ Female2 の切り替え。 |

- 切り替え時に**コントローラーが振動**して通知します。
- Female2 がいないシーンでは切り替え操作は無視されます。
- 選択対象は H シーンに入るたびに Female1 にリセットされます。

---

## 設定

ゲーム内 **BepInEx Configuration Manager**（デフォルト: `F1`）から変更できます。

| セクション | 設定名 | デフォルト | 説明 |
|---|---|---|---|
| General | Enabled | `ON` | プラグイン全機能の有効/無効。OFF=バニラ動作。 |
| Female 1 | Move Scale | `1.0` | Female1 の移動量の倍率。 |
| Female 2 | Move Scale | `1.0` | Female2 の移動量の倍率。 |

---

## 既知の制限

- **X ボタン（左コントローラー）は使用不可。** Meta Quest 2 の X ボタンは SteamVR レガシー入力 API でボタン押下を検出できないため、現バージョンは右コントローラーの A ボタンのみ対応です。
- **KK_HCharaAdjustmentEx の自動位置補正と干渉します。** 自動補正がキャラ位置を独自に調整するため、本プラグインで動かした位置がズレたり上書きされたりすることがあります。併用する場合は、KK_HCharaAdjustmentEx 側の自動補正を無効にするか、本プラグインを OFF（`General > Enabled`）にしてください。
- **ベータ版につき動作保証なし。** 限られた環境での確認にとどまり、予期しない挙動が発生する可能性があります。

---

## ライセンス

[GNU General Public License v3.0](LICENSE)

---

## 免責

本 Mod は H シーン向けのアダルト（R18）コンテンツです。自己責任でご利用ください。
