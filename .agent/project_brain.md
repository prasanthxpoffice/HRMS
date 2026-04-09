# HRMS Project Brain

This file serves as the core memory for the HRMS development team (AI and User). It ensures project continuity across different sessions and machines.

## Project Vision
- **Goal**: Transform the HRMS Administrative Module into a modern, high-performance SaaS experience.
- **Branding**: Official UAE Federal Identity (Warm 'Pleasant' Style: UAE Green primary, UAE Bronze accents, UAE Beige surfaces).
- **Philosophy**: Extreme code simplicity. Maintain robust C# backend logic while elevating the UI through a centralized, HSL-based design system.

## Technical Architecture
- **Framework**: Blazor InteractiveServer.
- **Styling**: Centralized `app.css` using HSL tokens for global theme control.
- **Localization**: Multi-language support (EN/AR) via `AppResources.resx`.
- **Data Access**: Repository pattern via `IDataService`. Business logic and validation are handled by backend stored procedures for performance and security.

## Core Modules
- **Admin**: Central dashboard for managing system-level identity attributes.
- **HolidayDetails**: Geographic and demographic-based holiday calendar management.

## Current State
- The project has been reverted to the last clean GitHub push structure.
- Implementation of the **HSL-based Design System** is currently in **Phase 1**.
