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

1. Set up solution in **Visual Studio** with two projects: `ServerApp` and `ClientApp`.
2. Implement **LAN socket communication** (Server ↔ Client).
3. Create **basic GUI** for Admin App (WinForms/WPF dashboard).
4. Implement **Login, Timer, and Lock Screen** forms in Client App.
5. Build **SQLite integration** for session tracking on Server.
6. Test communication with multiple client PCs in the same LAN.
7. Add **basic error handling + logging**.

---

## 📂 File/Folder Structure (C# Blueprint)

### Root Repository

```
/internet-cafe-system  
│── /server-app        # Admin / Local Café Server  
│── /client-app        # Client PC Application  
│── /docs              # Documentation (README, Phase1.md, etc.)  
│── InternetCafe.sln   # Visual Studio solution file  
```

---

### **Server App (Admin PC)**

```
/server-app  
│── Program.cs                # Entry point  
│── App.config                # Configurations (DB path, ports, etc.)  
│  
├── /Forms  
│   ├── MainForm.cs           # Main admin dashboard  
│   ├── ClientListForm.cs     # List of connected clients  
│   ├── SessionControlForm.cs # Start/extend/end sessions  
│  
├── /Database  
│   ├── Database.cs           # SQLite connection handler  
│   ├── SessionModel.cs       # Session entity  
│   ├── ClientModel.cs        # Client entity  
│   ├── Migrations/           # DB schema updates  
│  
├── /Networking  
│   ├── ServerSocket.cs       # TCP/SignalR server  
│   ├── MessageProtocol.cs    # Defines JSON message formats  
│   ├── EncryptionHelper.cs   # Simple message encryption  
│  
└── /Utils  
    ├── CodeGenerator.cs      # Creates unique session codes  
    ├── TimerManager.cs       # Handles server-side timers  
    ├── Logger.cs             # Logs events and errors  
```

---

### **Client App (Customer PC)**

```
/client-app  
│── Program.cs                # Entry point  
│── App.config                # Configurations (server IP, port, etc.)  
│  
├── /Forms  
│   ├── LoginForm.cs          # Enter session code  
│   ├── TimerForm.cs          # Shows countdown timer  
│   ├── LockScreenForm.cs     # Fullscreen lock when time is up  
│  
├── /Networking  
│   ├── ClientSocket.cs       # Connects to server  
│   ├── MessageHandler.cs     # Processes server commands  
│   ├── EncryptionHelper.cs   # Matches server’s encryption  
│  
├── /Session  
│   ├── SessionManager.cs     # Handles login, start, extend, expire  
│   ├── AlarmManager.cs       # Popup + sound warning before time ends  
│   ├── LockManager.cs        # Activates lock screen overlay  
│  
└── /Utils  
    ├── ConfigHelper.cs       # Reads/writes config (server IP, etc.)  
    ├── Logger.cs             # Logs events locally  
```

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
