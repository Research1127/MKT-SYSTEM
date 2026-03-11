# Tuition & Attendance Management System Documentation

## Table of Contents
1. [Overview](#overview)
2. [Goals & Requirements](#goals--requirements)
3. [System Architecture](#system-architecture)
4. [Modules & Features](#modules--features)
5. [Database Design](#database-design)
6. [User Roles & Access](#user-roles--access)
7. [Workflows](#workflows)
8. [Frontend & Backend Interaction](#frontend--backend-interaction)
9. [Implementation Plan](#implementation-plan)
10. [Scalability & Future Enhancements](#scalability--future-enhancements)

---

## 1. Overview

The system is designed for a **tuition center** to manage:

- Student fee payments
- Teacher and student attendance
- Registration / Google Form access for parents
- Multi-year tracking (e.g., 2025, 2026, etc.)

Key points:

- Current users: < 200 (students + 4 admins)
- Teachers can mark attendance
- Parents can view fee status by IC
- Google Form access can be toggled ON/OFF
- Multi-year support for tuition and attendance data

---

## 2. Goals & Requirements

### Functional Requirements

1. **Parent Fee Check**
2. **Fee Management (Admin)**
3. **Attendance System**
4. **Registration**
5. **Reports**

### Non-Functional Requirements

- Fast response time (< 2 seconds)
- Secure access for admins
- Easy for non-technical staff
- Scalable up to 1000+ students
- Deployment should be free / low-cost

---

## 3. System Architecture

**High-Level Architecture:**

```
React Frontend (Vercel Free)
          │
          ▼
ASP.NET Core API (Render / Railway Free)
          │
          ▼
Supabase PostgreSQL Database
          │
          ▼
Optional Google Forms (Registration)
```

---

## 4. Modules & Features

### 4.1 Parent Module

| Feature | Description |
|---------|------------|
| IC Lookup | Enter IC → see monthly fee status |
| Outstanding Fee | Highlight unpaid months |
| Google Form | Link shows only if `GF open` toggle is TRUE |

### 4.2 Admin Module

| Feature | Description |
|---------|------------|
| Student Management | Add, edit, view students |
| Fee Management | Track monthly fees, update payment status |
| Attendance | View / update student & teacher attendance |
| Reports | Export fee and attendance summaries |
| Settings | Toggle Google Form availability |

### 4.3 Attendance Module

| Feature | Description |
|---------|------------|
| Student Attendance | Teachers mark student presence |
| Teacher Attendance | Teachers mark own presence |
| Auto Calculations | Weekly, monthly, yearly statistics |
| Reports | Attendance percentages per student/teacher |

### 4.4 Registration Module

| Feature | Description |
|---------|------------|
| Google Form | For parent/student registration |
| Toggle ON/OFF | Admin controls availability |
| Data Storage | Responses stored in Supabase or Google Sheet |

---

## 5. Database Design

### Students
| Field | Type | Description |
|-------|------|------------|
| id | PK | Unique student ID |
| name | Text | Student name |
| ic_number | Text | Parent IC |
| class | Text | Class / batch |
| year | Int | Academic year |

### Payments
| Field | Type | Description |
|-------|------|------------|
| id | PK | Payment record ID |
| student_id | FK | Links to Students |
| month | Int | Month (1–12) |
| year | Int | Academic year |
| status | Enum | Paid / Unpaid |

### Attendance
| Field | Type | Description |
|-------|------|------------|
| id | PK | Attendance record ID |
| student_id | FK | Links to Students |
| date | Date | Attendance date |
| status | Enum | Present / Absent / MC / Late |

### Teachers
| Field | Type | Description |
|-------|------|------------|
| id | PK | Teacher ID |
| name | Text | Teacher name |

### TeacherAttendance
| Field | Type | Description |
|-------|------|------------|
| id | PK | Record ID |
| teacher_id | FK | Links to Teachers |
| date | Date | Attendance date |
| status | Enum | Present / Absent |

### Settings
| Key | Value | Description |
|-----|------|------------|
| gf_open | Boolean | Toggle Google Form availability |

---

## 6. User Roles & Access

| Role | Access |
|------|-------|
| Parent | Fee lookup, view attendance (optional) |
| Teacher | Mark attendance, view own records |
| Admin | Manage students, fees, attendance, reports, settings |
| Super Admin | Full access, can manage teachers |

---

## 7. Workflows

### 7.1 Parent Fee Check
1. Parent enters IC
2. System fetches student + fee records
3. Monthly fee table displayed
4. Total outstanding calculated
5. Google Form link shows if allowed

### 7.2 Fee Management (Admin)
1. Admin selects student
2. Update fee status per month
3. System recalculates totals
4. Records stored in database

### 7.3 Attendance
1. Teacher logs in
2. Marks student attendance per class
3. Marks own attendance
4. System auto calculates weekly/monthly/yearly stats
5. Admin can generate attendance reports

### 7.4 Registration (Google Form)
1. Parent fills Google Form
2. Data stored in Google Sheet or Supabase
3. Admin reviews registration
4. Google Form availability controlled by toggle

---

## 8. Frontend & Backend Interaction

| Module | Endpoint | Description |
|--------|---------|------------|
| Student | GET /student/:ic | Fetch student info |
| Fee | GET /fees/:studentId | Fetch monthly fee |
| Fee | POST /fees/:studentId | Update fee status |
| Attendance | GET /attendance/:studentId | Fetch attendance stats |
| Attendance | POST /attendance | Submit attendance |
| Registration | GET /registration | Fetch registration data |
| Settings | GET /settings | Fetch system settings |

---

## 9. Implementation Plan

### Phase 1 — MVP
- Parent fee check
- Admin fee management
- Google Form integration

### Phase 2 — Attendance Module
- Teacher attendance input
- Student attendance input
- Weekly/monthly/yearly calculations
- Reports

### Phase 3 — Admin Dashboard
- View all data
- Export reports
- Manage Google Form toggle
- Multi-year support

### Phase 4 — Optional Enhancements
- Notifications for unpaid fees
- Graphical dashboards
- Mobile-friendly attendance input
- Role-based access control

---

## 10. Scalability & Future Enhancements

- Current system (< 200 users) fully supported on free tier
- Can handle 1000+ students on Supabase if needed
- Modular backend allows future split into microservices if traffic grows
- Attendance and payment logic reusable for future academic years
- Admin-friendly design reduces training requirements

---

**Summary**

System uses:
- React + Tailwind (Frontend)
- ASP.NET Core API (Backend)
- Supabase PostgreSQL (Database)
- Google Forms (optional Registration)
- Modular but simple architecture

Supports:
- Parent fee check
- Teacher & student attendance
- Admin management
- Multi-year usage
- Free deployment options

