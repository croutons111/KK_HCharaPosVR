# KK_HCharaPosVR

**Koikatsu VR** の H シーンで、VR コントローラーを使ってキャラクターの位置をリアルタイムに調整する BepInEx プラグインです。

> **β 0.7 — ベータ版**

---

## 動作環境

| 項目 | 要件 |
|---|---|
| ゲーム | Koikatsu (HF Patch 3.36) |
| 実行ファイル | `KoikatuVR.exe`（公式 VR 版） |
| フレームワーク | BepInEx 5.4.x |
| 依存プラグイン | [KKAPI](https://github.com/ManlyMarco/KKAPI) |
| VR ヘッドセット | Meta Quest 2（Oculus Link / Air Link 経由） |

> **注意:** 現時点では **Meta Quest 2** でのみ動作確認済みです。  
> HTC Vive / Valve Index などの他のヘッドセットでは未確認です。

---

## インストール

1. [Releases](../../releases) から最新の `KK_HCharaPosVR.dll` をダウンロード
2. `BepInEx/plugins/` フォルダに配置
3. `KoikatuVR.exe` を起動（初回起動時に設定ファイルが自動生成されます）

---

## 使い方

H シーンに入ると自動的に有効になります。**右コントローラーの A ボタン**のみで操作します。

### 操作方法

| 操作 | 動作 |
|---|---|
| A ボタンを**長押し**（0.2 秒以上） | 選択中のキャラがコントローラーに追従。離すと位置を確定 |
| A ボタンを**素早く 2 回押す**（0.4 秒以内） | Female1 ↔ Female2 の切り替え |

- **切り替え時にコントローラーが振動**して切り替わったことを通知します
- Female2 がいないシーンでは切り替え操作は無視されます
- 選択対象は H シーンに入るたびに Female1 にリセットされます

---

## 設定

BepInEx Configuration Manager（ゲーム内で `F1` キー）から変更できます。

| セクション | 設定名 | デフォルト | 説明 |
|---|---|---|---|
| Female 1 | Move Scale | 1.0 | Female1 の移動量の倍率 |
| Female 2 | Move Scale | 1.0 | Female2 の移動量の倍率 |

---

## 既知の制限

- **X ボタン（左コントローラー）は使用不可**  
  Meta Quest 2 の X ボタンは SteamVR レガシー入力 API でボタン押下が検出できないため、現バージョンでは右コントローラーの A ボタンのみに対応しています。

- **ベータ版につき動作保証なし**  
  限られた環境での確認にとどまります。予期しない挙動が発生する可能性があります。

---

## ライセンス

[GNU General Public License v3.0](LICENSE)
