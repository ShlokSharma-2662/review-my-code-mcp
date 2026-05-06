# Performance Rules

## Existing Rules
- Detect ToList().Count allocation pattern
- Detect Count() > 0 and suggest Any()
- Detect new string( usage and suggest reducing string allocations

## Add New Rules
- Example: Detect avoidable boxing in non-generic collections
