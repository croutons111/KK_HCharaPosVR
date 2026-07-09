# KK_HCharaPosVR

**English** | [日本語](README.ja.md)

> A BepInEx plugin for **Koikatsu VR** that lets you reposition characters in real time with your VR controller during the H-scene.

---

## Overview

In the VR H-scene, KK_HCharaPosVR lets you grab a character and move her with your right controller, and switch which character you're moving. Useful for fixing alignment or fine-tuning positions on the fly.

> **Beta (0.7)** — verified on HF Patch 3.36 with Meta Quest 2.

---

## Requirements

| Item | Requirement |
|---|---|
| Game | Koikatsu (HF Patch 3.36) |
| Executable | `KoikatuVR.exe` (official VR build) |
| Framework | BepInEx 5.4.x |
| Dependency | [KKAPI](https://github.com/ManlyMarco/KKAPI) |
| VR headset | Meta Quest 2 (via Oculus Link / Air Link) |

> Only **Meta Quest 2** has been verified. Other headsets (HTC Vive, Valve Index, etc.) are untested.

---

## Installation

1. Download the latest `KK_HCharaPosVR.dll` from [Releases](../../releases).
2. Place it in your `BepInEx/plugins/` folder.
3. Start `KoikatuVR.exe` (a config file is generated on first launch).

---

## How to Use

Activates automatically when you enter an H-scene. All control is on the **right controller's A button**.

| Action | Result |
|---|---|
| **Hold** A (0.2 s or longer) | The selected character follows your controller. Release to lock her position. |
| **Double-tap** A (within 0.4 s) | Switch between Female 1 and Female 2. |

- The controller **vibrates** to confirm a switch.
- The switch is ignored in scenes without a Female 2.
- The selection resets to Female 1 each time you enter an H-scene.

---

## Configuration

Change these in the in-game **BepInEx Configuration Manager** (default: `F1`).

| Section | Setting | Default | Description |
|---|---|---|---|
| Female 1 | Move Scale | `1.0` | Movement multiplier for Female 1. |
| Female 2 | Move Scale | `1.0` | Movement multiplier for Female 2. |

---

## Known Limitations

- **The X button (left controller) is not usable.** On Meta Quest 2 the X button press cannot be detected through the SteamVR legacy input API, so this version only supports the right controller's A button.
- **Beta software** — verified only in a limited environment; unexpected behavior may occur.

---

## License

[GNU General Public License v3.0](LICENSE)

---

## Disclaimer

This is an adult (R18) mod intended for the H-scene. Use at your own responsibility.
