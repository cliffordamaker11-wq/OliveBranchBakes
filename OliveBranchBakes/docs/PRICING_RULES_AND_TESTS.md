# Pricing rules and hand-calculated tests — prepared before pricing code

## Proposed rules for this checkpoint
These are explicit sample rules for the student to confirm against the instructor's project rules before implementing the pricing engine.

1. Base line amount = quantity × (product base price + selected variant adjustment).
2. A 10% discount applies only when the pre-discount merchandise subtotal is **strictly greater than $50.00**. Exactly $50.00 receives no discount.
3. A 5% bulk discount applies to an individual line only when quantity is **strictly greater than 10**. Quantity 10 does not qualify; quantity 11 does.
4. The discount is applied to the merchandise subtotal after any bulk discount, then rounded to two decimal places using standard currency rounding.
5. Sales tax and delivery fees are intentionally excluded until their rules are provided.

## Hand calculations
| Case | Inputs | Hand calculation | Expected total |
|---|---|---|---:|
| P1: just below discount threshold | Sourdough standard $8.00 × 6 | 6 × $8.00 = $48.00; below $50.00 threshold | $48.00 |
| P2: exactly at threshold | Test item base $12.00 plus $0.50 adjustment × 4 (test fixture) | 4 × ($12.00 + $0.50) = $50.00 | $50.00 |
| P3: one cent above threshold | Test item $10.01 × 5 | 5 × $10.01 = $50.05; discount = $50.05 × 10% = $5.005, rounded to $5.01; total = $45.04 | $45.04 |
| P4: just below bulk limit | Sourdough standard $8.00 × 10 | 10 × $8.00 = $80.00; quantity 10 does not get bulk discount; subtotal discount applies 10% = $8.00 | $72.00 |
| P5: one past bulk limit | Sourdough standard $8.00 × 11 | 11 × $8.00 = $88.00; bulk discount = $4.40; adjusted subtotal = $83.60; 10% subtotal discount = $8.36; total = $75.24 | $75.24 |
| P6: variant adjustment | Large sourdough ($8.00 + $3.00) × 2 | 2 × $11.00 = $22.00; no discount | $22.00 |
| P7: zero quantity validation | Any item × 0 | Quantity must be at least 1; reject input | Validation error |
| P8: negative quantity validation | Any item × -1 | Quantity must be at least 1; reject input | Validation error |

## Calculation note
P2 uses a temporary test fixture with a $0.50 adjustment to produce exactly $50.00. Do not change the seeded catalog solely to make this test work. P3 demonstrates rounding at the discount boundary. Review the rounding convention with the instructor before coding.

## Acceptance checklist
- [ ] Each case is implemented as a test after pricing rules are confirmed.
- [ ] Boundary values below, exactly at, and above thresholds are covered.
- [ ] Invalid quantities are rejected.
- [ ] Currency results are rounded consistently.
- [ ] Expected values above remain written down before pricing implementation.
