# Behavior-preserving refactoring contract

## Objective

Improve readability, navigability, naming consistency, and maintainability of StarCollection and BiFuzz while keeping behavior unchanged.

## In scope

- Project-owned C# runtime, test, editor, and BiFuzz code after ownership classification.
- Naming cleanup, responsibility extraction, duplicate-code reduction, focused comments, and dependency clarification.
- Documentation and tests that characterize existing behavior.

## Out of scope without explicit approval

- New gameplay, altered fuzzing strategy, changed test oracle, changed UI/scene flow, or asset replacement.
- Third-party asset/package source, Unity version changes, project-setting migrations, and broad formatting-only rewrites.

## Naming and structure

- Use PascalCase for types, methods, properties, events, and constants; camelCase for parameters and local variables.
- Use one consistent private-field convention in each touched class. Preserve serialized data before renaming serialized fields.
- Give classes one primary responsibility and methods names that state their domain action.
- Replace unexplained literals with named constants only when serialization and public API behavior remain unchanged.
- Prefer comments that explain constraints, experimental intent, or non-obvious Unity behavior.

## Unity safeguards

- Inventory references before renaming scripts, classes, methods, fields, scenes, tags, layers, or resources.
- Preserve serialized data with compatible migrations where needed and verify affected scene/prefab instances in the Editor.
- Preserve method names used by Unity events, animation events, reflection, or the test harness.
- Use Unity MCP against a ready Editor for scene/asset operations; do not hand-edit live Unity YAML.

## Required workflow

1. Map the target responsibility and write a behavior hypothesis.
2. Establish a build/test baseline.
3. Apply one cohesive refactoring unit.
4. Compile and run the narrowest relevant tests.
5. Inspect the diff and Unity Console; check serialized references where applicable.
6. Record evidence and the next checkpoint in `refactoring-progress.md`.

## Acceptance criteria

- No intentional user-visible, experiment-visible, or serialization-visible behavior change.
- Relevant build and tests pass, or a pre-existing failure is clearly recorded.
- Changed code has clearer ownership, names, and dependencies than before.
- Architecture and progress documentation are updated for material changes.
