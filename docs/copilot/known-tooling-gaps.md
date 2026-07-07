# Moneynotique known tooling gaps

## Current gaps that affect future Copilot work

| Gap | Evidence | Impact |
| --- | --- | --- |
| No automated tests | No test projects or test files were found in the repository | Future code changes need extra care around finance logic, persistence, and bindings |
| No repository CI/CD workflows | No `.github/workflows/**` files were found | There is no built-in PR or push validation surface for Copilot to target |
| Very limited project docs | Root `README.md` is effectively empty | Agents must rely on code inspection instead of maintained contributor guidance |
| Legacy Xamarin build environment | Android and iOS heads are old-style Xamarin projects, while the audit only confirmed general .NET SDK availability | Build or runtime validation can be machine-specific and may require Visual Studio/Xamarin tooling beyond repository files |
| No existing Copilot customization baseline | No `.github/**` Copilot files and no `AGENTS.md` were present before this change | There was nothing to preserve or merge, so repository knowledge had to be lifted directly from code into instructions, skills, and docs |
| No repository MCP configuration | No repository-level MCP settings were present | Research and review rely on local repository inspection instead of external project systems |

## Deliberate architecture choices made because of those gaps

- No prompt files were added. Prompt files are not supported in Copilot CLI, so the repository uses portable agents, skills, and instruction files instead.
- No Android or iOS build-diagnostics skill was added yet. The repository has platform heads, but no repeatable in-repo CI surface or documented local diagnostic workflow to codify.
- The root instruction file stays short, and the path-specific files carry the Xamarin, MVVM, persistence, and platform rules so future prompts stay compact.

## When to revisit this file

Update this document if the repository gains any of the following:

- test projects or repeatable test commands
- GitHub Actions workflows
- documented local build or release procedures
- repository MCP configuration
- explicit MAUI migration planning or execution work
- a real need for additional Copilot skills beyond the current XAML/MVVM, finance-review, and persistence checks
