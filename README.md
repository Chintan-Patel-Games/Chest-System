# 🧰 Chest System - Unity

A modular, extensible **Chest System** built in **Unity (C#)** that simulates chest slot management, timed unlocking, and reward generation — commonly seen in mobile games. Designed with clean architecture, object pooling, and design patterns in mind.

---

## 🧩 Features

- 🔒 **Chest Lock/Unlock System**
  - Chests start locked and require time or gems to unlock
  - Only one chest unlocks at a time (queue system)

- 📦 **Chest Types & Rewards**
  - Common, Rare, Epic, Legendary chests
  - Each has unique timers and reward ranges (coins, gems)

- 🧠 **Queue-based Unlocking**
  - Multiple chests can be queued for unlocking
  - Next chest starts automatically after the previous one finishes

- 🖼️ **Modular UI System**
  - Grid-based slot layout
  - Visual state indicators: Empty, Locked, Unlocking, Opened
  - Chest visuals update dynamically via ScriptableObject configs

---

## 🔧 Concepts Demonstrated

| Concept                        | Where It's Used                                                                            |
|--------------------------------|--------------------------------------------------------------------------------------------|
| **MVC Architecture**           | Clear separation between Model (ChestSO), View (ChestView), Controller (ChestController)   |
| **ScriptableObjects**          | Used to define chest types, rewards, unlock times                                          |
| **Service Layer**              | `ChestService`, `ChestUnlockQueueService`, `CommandService`, `CurrencyService`, `UIService`|
| **Event System**               | Used for unlock events, UI refresh triggers, state changes                                 |
| **State Pattern**              | Each chest has states: Empty, Locked, Unlocking, Opened                                    |
| **Command Pattern (Undo)**     | Gem-based unlocks can be reverted using undo commands                                      |
| **Separation of Concerns**     | Each module handles a distinct responsibility (UI, unlocking, events, etc.)                |

---

## 📸 Game Demo Link

- https://drive.google.com/file/d/1g0x_pTt7qG2-XzFoUozCbSMsbSvS-MN5/view?usp=sharing
