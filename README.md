# abc-allocations (sample)

A small .NET 9 solution that stands in for ABC's Allocations mono-repo, so the
[Defect Resolution Agent](../defect-resolution-agent) can run against live source code on GitHub.

| Project | App (idx-code filter) | What it does |
|---|---|---|
| `src/Allocations.Ots` | OTS | Splits a PO quantity across stores, rounds to packs, exports CSV |
| `src/Allocations.RefData` | REF DATA | Store reference data |
| `tests/*` | | xUnit tests (all pass) |
| `infra/` | PLATFORM | Bicep; the agent's policy denies changes here |

```bash
dotnet test Allocations.sln
```

## Known defects (deliberately planted)

The existing tests pass, but the code has bugs that the agent's tickets describe. Each one can be
reproduced with a single failing unit test and fixed in one source file.

| Ticket | Env | Where | Symptom |
|---|---|---|---|
| OTS-2101 | Production | `AllocationSplitter.Allocate` | 10 units over 3 equal stores allocates 9; uneven splits lose units |
| OTS-2102 | Production | `PackRounder.ToPacks` | 5 units with a pack size of 6 ships 0 packs; partial packs round down |
| REF-311 | Production | `StoreDirectory` | `Get("s01")` throws for store `S01`; lookups are case-sensitive |
| REF-312 | QA | `TradingCalendar.WeekOf` (only on `feature/ref-90-trading-weeks`) | 1 Jan 2027 is labelled week 1 instead of ISO week 53 of 2026 |

Don't fix these by hand: they are the test cases for the agent. Each agent run opens a PR; close it
without merging to run the same ticket again.
