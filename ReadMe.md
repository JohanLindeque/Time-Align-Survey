# TimeAlign - Team Time Allocation Survey Application

## Overview
TimeAlign is a web-based survey application that assesses how software development teams distribute their time across different work activities during a productive week. The application compares actual time allocation against managerial objectives to reveal the "expectation gap" - showing where team reality differs from management expectations.

## Purpose

This application helps teams and managers:
- Understand actual time distribution across various development activities
- Compare team performance against expected allocations
- Identify gaps between expectations and reality
- Make data-driven decisions about resource allocation and process improvements

---
## Key Features
### User Workflows

1. Respondent Survey Flow
    - Login with provided credentials
    - Complete randomized survey assigning percentage weights to 10 focus areas
    - Validation ensures total allocation equals 100%
    - Receive confirmation upon successful submission



2. Admin Results Dashboard
   - Login with admin credentials
   - View aggregated team results
   - Compare average allocations against manager's objectives
   - See expectation gaps and accuracy percentages with visual indicators

---

## Technical Stack

Framework: Blazor (Server-Side)
Language: C#
Database: SQL Server or MySQL
ORM: Entity Framework Core