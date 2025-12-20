# Core Game Loop – Unity Project

This repository contains the **gameplay scripts and scene setup** for a simple but complete **core game loop** built in Unity.

The project demonstrates:
- A quest-giving NPC with dialogue and interaction
- Player combat against an enemy
- Looting an item and completing a quest
- A full end-to-end gameplay loop you can build on

This project accompanies a step-by-step walkthrough on YouTube where the full loop is built and explained.

👉 [*Core Game Loop*](https://www.youtube.com/watch?v=TaEOnaX_eRU)

![alt text](core_loop.png)

This repo is intended as a **learning and reference project**, created alongside a YouTube walkthrough.

---

## Unity Version

Built with **Unity 6.3 LTS (6000.3.x)** using URP.

⚠️ **Important:**  
Open the project using a matching Unity 6000.3.x editor to avoid upgrade prompts or unexpected changes.

---

## Getting Started

1. Clone this repository.
2. Open Unity Hub and select **Unity 6000.3.x**.
3. Add the yt-core-game-loop folder from Disk
4. Open the project folder in Unity.
5. Import the third-party assets listed below into the `Assets/` folder.
6. For Stylized Forest, make sure to import `Assets/TriForge Assets/_URP Content - Fantasy Worlds - Old Forest DEMO.unitypackage`
6. Open `/Assets/Scenes/CoreLoop.Unity`
7. Allow Unity to regenerate solution files and complete the initial domain reload.
8. Open the main scene and press **Play**.
9. If you get an error about Player Input, re-run Hero Combat Controller Auto Wiring on the Colwyn object in the scene (see YouTube video for exact steps)

---

## Third-Party Assets (Not Included)

To keep this repository lightweight and license-compliant, **third-party asset packages are not included** in git.

You’ll need to import the following assets locally before opening the project:

 - [Stylized Forest Environment](https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-worlds-forest-free-stylized-forest-environment-open-worl-282610)
 - [Human Melee Animations](https://assetstore.unity.com/packages/3d/animations/human-melee-animations-free-165785)
 - [Human Basic Motions](https://assetstore.unity.com/packages/3d/animations/human-basic-motions-free-154271)
 - [RPG Animation Pack](https://assetstore.unity.com/packages/3d/animations/rpg-animations-pack-free-288783)
 - [Hero Combat Controller](https://github.com/mdj128/hero-combat-controller.git)

---

## Project Scope & Notes

- This project focuses on **system wiring and gameplay flow**, not polish or production-ready content.
- Code is intentionally kept straightforward and readable.
- Some systems (combat, UI, etc.) are expected to evolve in follow-up videos.
- Generated/build artifacts (`Library/`, `Temp/`, `Builds/`, etc.) are ignored — only source assets and scripts are tracked.

---

## License & Usage

All original code in this repository is free to use for learning and experimentation.

Third-party assets are subject to their **original licenses** and must be obtained separately via the Unity Asset Store or their respective authors.

---
