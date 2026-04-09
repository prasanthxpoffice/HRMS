# AI Pair-Programming Instructions

These rules are strictly enforced for all AI agents working on the HRMS project to ensure developer satisfaction and code integrity.

## 1. The "CSS-Only" Rule
- **Focus**: All design enhancements MUST be implemented within `wwwroot/app.css` using HSL variables and CSS selectors.
- **Branding**: Comply with Official UAE Government Design Language System (DLS).
- **RESTRICTION**: Do not add, remove, or modify properties in `.cs` files or rename components unless explicitly requested.
- **RESTRICTION**: Do not change the HTML structure or parameter signatures of Razor components.

## 2. No Code Pollution
- Do not add "fake" data, mockup properties, or shorthand wrappers for existing services unless specifically needed for a functional requirement.
- Maintain the original logic flow of the project as provided by the user.

## 3. Communication
- Keep responses concise and technical.
- Always verify build and runtime stability before declaring a task complete.
- When suggesting a design change, explain it through the lens of **HSL-based centralization.**
