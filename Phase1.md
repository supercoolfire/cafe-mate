# Phase 1: Core LAN Features

This document explains the step-by-step plan for building the **minimum viable product (MVP)** of the Internet Café Management System.
Phase 1 focuses on **local network (LAN)** features without cloud licensing or subscription integration.

---

## 🎯 Objectives

* Allow clients to log in using session codes.
* Run a timer with alarm/lockout on each client.
* Enable the admin (local server) to monitor and control all client PCs.
* Ensure the system works fully **offline** (local café only).

---

## 🏗️ System Components

### **1. Local Café Server (Admin PC)**

* Runs the **Admin App** (C# .NET, WinForms/WPF).
* Generates login codes for clients.
* Tracks all active sessions.
* Sends commands to clients (start/extend/lock/unlock).
* Stores session history in **SQLite** database.

### **2. Client PCs**

* Each runs a **Client App** (C# .NET, WinForms/WPF).
* Requires login code to start a session.
* Displays countdown timer.
* Shows warnings before session ends.
* Locks screen when time expires (blocks desktop, keyboard, mouse).

### **3. LAN Communication**

* Local Server ↔ Client via TCP/IP sockets (or SignalR).
* Messages are encrypted or obfuscated to prevent bypassing.
* Example messages:

  * `START_SESSION {code, duration}`
  * `EXTEND_SESSION {minutes}`
  * `LOCK_CLIENT`
  * `UNLOCK_CLIENT`

---

## 🛠️ Development Steps

### Step 1: Project Setup

* Create Git repo with folders:

  ```
  /server-app   → Local Café Server (Admin)
  /client-app   → Client PC App
  /docs         → Documentation (README, Phase1.md, etc.)
  ```
* Initialize both projects as **C# .NET WinForms/WPF apps**.

### Step 2: Database (Server)

* Use **SQLite** for local storage.
* Tables:

  * `sessions` → id, code, client\_id, start\_time, duration, status
  * `clients` → id, name, ip\_address, status

### Step 3: Server App (Admin)

* GUI for:

  * Listing connected clients.
  * Generating login codes.
  * Assigning session duration.
  * Extending/ending sessions.
* Background process:

  * Tracks timers for each client.
  * Sends updates to clients.

### Step 4: Client App

* Login screen: enter session code.
* Timer display: countdown with warnings.
* Lock screen overlay when time runs out:

  * Covers entire desktop.
  * Prevents task switching (Ctrl+Alt+Del block optional).
* Alarm sound + popup warning at expiration.

### Step 5: LAN Communication

* Implement **socket server** in Admin App.
* Client App connects to Admin using IP + port.
* Define simple JSON protocol:

  ```json
  {
    "action": "start_session",
    "duration": 120
  }
  ```

### Step 6: Security

* Basic encryption for client-server messages.
* Prevents customers from spoofing messages.
* Store codes securely (hashed in DB).

### Step 7: Testing

* Setup 1 admin PC + 2 client PCs on same LAN.
* Test cases:

  * Client connects/disconnects.
  * Session start/extend/end.
  * Alarm & lockout.
  * Multiple clients at once.

---

## ✅ Deliverables for Phase 1

* **Admin App** with GUI for managing clients.
* **Client App** with login, timer, and lockout.
* LAN communication fully functional.
* SQLite database storing sessions & clients.
* Full café can be managed offline (local only).

---

## ⏭️ Next Step (Phase 2)

Once Phase 1 is stable:

* Add **Cloud Licensing Server**.
* Enable **trial limits + subscription unlocks**.
* Start integrating premium features.

---
