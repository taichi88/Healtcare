# Healthcare Backend API

This is a **C# ASP.NET Core Web API** project designed to manage a simple healthcare system.
The system supports person registration, appointment scheduling, role management (Doctor, Patient, Desk Staff), and diagnosis assignment.

---

## 📌 Project Overview

The goal of this project is to build the backend for a healthcare platform where:

- **Desk Staff** can register new persons and schedule appointments.
- **Doctors** can view appointments and write diagnoses.
- **Patients** can pay and attend appointments.

All persons are stored in a common table and differentiated by roles such as Doctor, Patient, and Desk Staff.

---

## 🏗️ Features

- Register persons (generic table for all people).
- Assign roles: Doctor, Patient, Desk Staff.
- Schedule appointments between patients and doctors.
- Doctors can write diagnoses.
- Patients can pay for visits.

---

## 🔧 Tech Stack

- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure)
- **Authentication & Roles**: ASP.NET Identity (planned/optional)

---



