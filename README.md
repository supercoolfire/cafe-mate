# ☕ Internet Café Management System (Cloud + Local Hybrid)

A full-featured **Internet Café Management SaaS** that allows café owners to manage client computers from a central server with optional cloud-based monitoring.
Includes **free trial**, **subscription model**, and **premium feature unlocks**.

---

## ✨ Features

### 🆓 Free (Trial Mode)

* Client login using generated code
* Session timer + alarm when time runs out
* Lock/unlock client screen

### 💎 Premium (Paid Subscription)

* Real-time client screen monitoring (screenshots or streaming)
* Remote commands: shutdown, restart, broadcast messages
* Cloud dashboard: monitor from anywhere
* Reports: usage logs, revenue tracking
* Multi-café management under one account

---

## 🔑 Licensing & Subscription

* Trial: 7 days (default), configurable by developer
* License server controls access:

  * Trial keys generated on first install
  * Premium unlock via license key or cloud login
* Payment integration (Stripe, PayPal, local gateways)
* Automatic downgrade to free mode if subscription expires

---

## 🏗️ Architecture

```
 ┌─────────────────────────────┐
 │         Cloud Server         │
 │ (License + Payments + Admin) │
 │  • License verification      │
 │  • Subscription management   │
 │  • Remote monitoring         │
 │  • Reports / Analytics       │
 └───────────────▲─────────────┘
                 │ Internet
                 │
 ┌───────────────┴─────────────┐
 │     Local Café Server        │
 │ (Admin PC in the café)       │
 │  • Manages client sessions   │
 │  • Works offline if needed   │
 │  • Syncs data with cloud     │
 │  • Local admin dashboard     │
 └───────▲──────────────────┬───┘
         │ LAN              │
         │                  │
 ┌───────┴───────┐  ┌───────┴───────┐
 │   Client PC   │  │   Client PC   │
 │  • Login code │  │  • Timer      │
 │  • Timer/lock │  │  • Alarm      │
 │  • Screen cap │  │  • Lockout    │
 └───────────────┘  └───────────────┘
```

---

## ⚙️ Development Roadmap

### Phase 1: Core LAN Features

* [ ] Client login with session code
* [ ] Timer + alarm on client
* [ ] Lock/unlock client
* [ ] Local server tracks active sessions

### Phase 2: Cloud Licensing

* [ ] License server API (trial & subscription)
* [ ] Client verification with cloud
* [ ] Trial auto-expiry & feature limiting
* [ ] Payment integration

### Phase 3: Premium Features

* [ ] Screen monitoring (screenshot polling or live streaming)
* [ ] Remote commands (shutdown, restart, broadcast)
* [ ] Cloud-based reporting & analytics
* [ ] Multi-café support

### Phase 4: SaaS Polish

* [ ] Admin cloud dashboard (web app)
* [ ] User accounts & roles
* [ ] Subscription management portal
* [ ] Automatic updates for client apps

---

## 🔐 Security Notes

* All client ↔ server communication via HTTPS/WebSocket (TLS).
* License keys signed with JWT to prevent tampering.
* Client app checks license regularly (daily/weekly).
* Offline fallback: basic LAN features still work if internet is down.

---

## 📦 Tech Stack

* **Client App:** C# .NET (WinForms/WPF), SQLite for local cache
* **Local Server:** C# .NET / ASP.NET Core with SignalR for real-time comms
* **Cloud Server:** Node.js / Django / ASP.NET Core with PostgreSQL/MySQL
* **Frontend (Cloud Dashboard):** React / Next.js or Blazor
* **Payments:** Stripe, PayPal, local gateways

---

## 🚀 Deployment

* **Local Café Server:** Installed on admin PC
* **Client App:** Installed on each customer PC
* **Cloud Server:** Hosted on VPS / AWS / Render / Azure
* **Database:** PostgreSQL/MySQL (cloud-hosted)

---

## 📜 License

Proprietary — not open source.
Distributed only with a valid license from the developer.

---
