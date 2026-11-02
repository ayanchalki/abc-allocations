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

| Ticket | Jira (project PA) | Where | Symptom |
|---|---|---|---|
| OTS-2101 | PA-1 | `AllocationSplitter.Allocate` | 10 units over 3 equal stores allocates 9; uneven splits lose units |
| OTS-2102 | PA-2 | `PackRounder.ToPacks` | 5 units with a pack size of 6 ships 0 packs; partial packs round down |
| REF-311 | PA-3 | `StoreDirectory` | `Get("s01")` throws for store `S01`; lookups are case-sensitive |

All three are Production bugs: the agent works from `main` and opens its PRs to `main`.

Don't fix these by hand: they are the test cases for the agent. Each agent run opens a PR; close it
without merging to run the same ticket again.
