# Project Instructions

These instructions are automatically applied to all files in this project.

## core - React 19+ project conventions and architecture

# React Project Guidelines

## Stack

- React 19+ with React Compiler 1.0 (auto-memoization enabled)
- TypeScript strict mode
- Vite (bundler) + Vitest + React Testing Library

## Architecture

Organize by feature, colocate related code:

```
src/
  components/ui/       # Design system primitives
  features/[feature]/  # components/, hooks/, api/, types.ts
  hooks/               # Shared custom hooks
  lib/                 # Utilities and helpers
  api/                 # API client and endpoints
  types/               # Shared TypeScript types
```

## React 19 Baseline

- **React Compiler**: Handles memoization automatically
- **`ref` as prop**: Pass `ref` directly — `forwardRef` is unnecessary
- **`use()` hook**: Unwrap promises and context in render, supports conditional calls
- **`useActionState`**: Replaces deprecated `useFormState` for form submissions
- **`useOptimistic`**: Optimistic UI updates during async transitions
- **Actions**: Async functions for `<form action={fn}>` pattern

## Component Model

In a Vite SPA, all components are client components by default — no `"use client"` directive needed.

When using an RSC-capable framework (Next.js, React Router v7 framework mode):

| Server Components | Client Components |
|-------------------|-------------------|
| Default — no `"use client"` needed  | Add `"use client"` only when using state/effects/browser APIs |
| Fetch data directly, async allowed  | `useState`, `useEffect`, event handlers |

## Code Style

- Ban `React.FC` — use plain function declarations with typed props
- One component per file, named exports only (no default exports)
- Files: `kebab-case.tsx` / Components: `PascalCase` / Hooks: `useCamelCase`
- Props interface declared above component, destructured in signature

### Component Internal Order

1. Imports → 2. Types → 3. Component function → 4. Hooks → 5. Derived state → 6. Handlers → 7. Effects → 8. JSX

## Anti-Patterns

- Do NOT add `useMemo`/`useCallback`/`React.memo` — React Compiler handles it
- Do NOT use `React.FC` — provides no benefit over plain function declarations and adds unnecessary indirection
- Do NOT use class components or HOCs — use hooks and composition

## Commands

```bash
npm run dev             # Dev server
npm run build           # Production build
npm run test            # Run tests
npm run lint            # ESLint
npm run typecheck       # TypeScript check
```

## Performance

- Lazy load routes and heavy components with `React.lazy` + Suspense
- Use Suspense boundaries as the default loading pattern
- Extract complex object/array prop literals to named variables for readability (React Compiler handles memoization)
- Profile with React DevTools before optimizing — trust the compiler first


---

## core - .NET 9 project conventions and architecture

# .NET Project Guidelines

## Stack

- .NET 9, ASP.NET Core, Entity Framework Core, C# 12+
- xUnit + NSubstitute + FluentAssertions

## Architecture

Use Clean Architecture with strict layer dependencies:
- `WebApi -> Application -> Domain`
- `Infrastructure -> Application -> Domain`

| Layer | Contains | References |
|-------|----------|------------|
| Domain | Entities, Value Objects, Interfaces | Nothing (zero NuGet deps) |
| Application | Commands, Queries, DTOs, Validators | Domain only |
| Infrastructure | DbContext, Repos, External Services | Application, Domain |
| WebApi | Endpoints, Middleware | Application, Infrastructure |

## API Style

- Minimal APIs by default -- see api rules for TypedResults, versioning, rate limiting
- Use `AddOpenApi()` + `MapOpenApi()` for built-in OpenAPI support (no Swashbuckle needed in .NET 9)
- Reserve Controllers only for complex model binding or content negotiation scenarios

## Commands

```bash
dotnet run --project src/WebApi
dotnet test
dotnet ef migrations add Name -p src/Infrastructure -s src/WebApi
dotnet ef database update -p src/Infrastructure -s src/WebApi
```



---

## core - Shared coding conventions across all technologies

# Shared Conventions

## Naming

- Use explicit names: no `c`, `x`, `tmp`, `data` — name reveals intent
- Use named constants for magic numbers
- Small functions: < 30 lines, single responsibility
- Max nesting: 3 levels — use early returns to flatten

## Code Hygiene

- DO NOT disable lint rules without a justification and ticket reference
- DO NOT leave dead code — delete it, do not comment it out
- DO NOT swallow errors silently — log with context (user ID, request ID), then rethrow or handle

## Error Handling

- User-facing errors: clear, actionable messages
- Internal errors: detailed logs, generic user message
- Never expose stack traces to end users

## Dependencies

- Pin exact versions in lock files
- Audit regularly
- Prefer well-maintained packages — minimize dependency count



---

## interaction - AI interaction rules and communication guidelines

# Interaction Rules

## Rules Are Absolute

1. **Rules can NEVER be violated. Tasks can fail.**
2. If a task requires violating a rule, the task fails — not the rule.
3. If a task is blocked, explain the problem and ask how to proceed.

## Protected Changes

Never modify without explaining WHY and asking permission:
- Package manager config (yarn, npm, pnpm)
- Infrastructure (docker, CI/CD, deployment)
- Project structure
- Build config

## Questions vs Actions

- **Question** ("what is...", "why...", "how does...") — answer only, no code changes
- **Explicit request** ("create", "implement", "fix", "add") — action with code

When the user asks a question, answer it. Do not start coding or running commands.

## Confirmation Before Action

For non-trivial changes, confirm approach before implementing:
1. Explain what will be done
2. Wait for user approval
3. Then execute

## Honesty and Intellectual Integrity

- When you have NOT verified information (via docs, code, or web search), say explicitly "I haven't verified this" before any assertion
- NEVER present a supposition as a fact — if you are inferring or guessing, label it clearly
- When you do not know something, research the answer before responding — use available tools (docs, web search, codebase search) rather than guessing
- If challenged by the user, VERIFY your claim before defending it — never double down on an unverified assertion
- Prefer "I need to check this" over a confident guess — being wrong confidently erodes trust faster than admitting uncertainty
- NEVER fabricate conventions, APIs, function signatures, or configuration options
- If two valid approaches exist, present both with tradeoffs — do not pretend one is universally correct
- When corrected, acknowledge the correction directly without deflecting

## Language

| Context | Language |
|---------|----------|
| Communication with user (responses, explanations) | Match user's language |
| Files in repo (code, comments, docs, variable names) | English |

Code is read by international teams. All code, comments, and documentation must be in English regardless of conversation language.
