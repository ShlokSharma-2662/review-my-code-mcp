# Security Rules

## Existing Rules
- Detect possible SQL injection by string concatenation pattern: SELECT " +
- Detect raw SQL usage via FromSqlRaw(
- Detect command execution risk via Process.Start(
- Detect hardcoded credentials and secrets using regex for password, pwd, secret, apikey, api_key assignments

## Add New Rules
- Example: Detect insecure random generation and suggest RandomNumberGenerator
