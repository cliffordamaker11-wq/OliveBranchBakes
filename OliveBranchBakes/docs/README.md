# Olive Branch Bakes — Phase 3A

## Open and run
1. Install Visual Studio 2022 with the **.NET desktop development** workload.
2. Open `OliveBranchBakes.sln`.
3. Allow NuGet/SDK restore if prompted.
4. Set `OliveBranchBakes` as the startup project and press **F5**.

Target framework: .NET 8 Windows Forms (`net8.0-windows`).

## What is included
- Domain models for bakery products, variants, customers, orders, and order items.
- JSON `DataStore` that seeds demo data on first run and saves/reloads it on later runs.
- A touch-friendly prototype window with a product list and a Variant Card.
- Hand-calculated pricing test cases documented before a pricing engine is implemented.
- Demo script and GitHub release checklist.

## Data location
The application writes `bakery-data.json` under the current Windows user's LocalApplicationData folder in `OliveBranchBakes`. The app does not require administrator permissions.

## Scope notes
This checkpoint is a foundation, not a production checkout system. It does not process payments, send email, or implement authentication. Pricing cases are specifications and calculations only; the pricing engine should be written after these cases are reviewed.
