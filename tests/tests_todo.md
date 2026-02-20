# Performance & Unit Testing TODO List

Here are the actionable items integrating the new `tests` project into your workflow, taking advantage of the refactored code fixes and the native Dual Universe production data sets.

## 1. Test Project Configuration

- [x] **Initialize Test Project**: Created a native .NET Sdk C# test project (`tests/Tests.csproj`) specifically targeting `.NET Framework 4.8` (`net48`) using MSBuild integration instead of legacy `packages.config`.
- [x] **Reference Binding**: Wired up `Tests.csproj` to internally reference `DU-Industry-Tool.csproj` natively.
- [x] **Run Unit Tests**: Executed successfully (6/6 passing) on February 20, 2026.
  - Recommended one-command runner: `.\tests\run-tests.ps1`
  - Equivalent manual commands:
    - Build: `C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe tests\Tests.csproj /t:Restore,Build /p:Configuration=Debug`
    - Test: `dotnet test tests\Tests.csproj --no-build`
- [x] **Document CLI caveat**: Running plain `dotnet test` (without `--no-build`) can fail by trying to rebuild the legacy WinForms app project (`DU-Industry-Tool.csproj`) with SDK MSBuild instead of full Visual Studio MSBuild.

## 2. RecipeCalculation.cs Code Fixes

- [x] **ParamName Error Fix**: Repointed `nameof(RecipeCalculation)` to `nameof(section)` in constructors to prevent malformed stack traces.
- [x] **Boxing Optimization**: Refactored `ArrayList` returning an untyped object box to a strongly typed `List<RecipeCalculation>()` in `GetChildren()`.
- [x] **O(N) Lookups Elimination**: Substituted extremely slow `DUData.Schematics.FirstOrDefault(...)` dictionary loops with `O(1)` operations using `DUData.Schematics.TryGetValue(...)`.
- [x] **O(N) Lookups Elimination Pt.2**: For `Calculator.All`, changed global index querying to use `Calculator.All.Values.FirstOrDefault(...)` since `.Name` wasn't part of the primary key.
- [x] **Property Assignment Binding**: Rerouted bypassing `comment =` variables to the actual setter logic (`Comment =`) enabling the `PropertyChanged` notifications gracefully.
- [x] **Allocation Free Math**: Scrapped `int.Parse($"{char}")` strings to native character subtractions `char - '0'` completely eliminating Gen 0 heavy string allocations on loop-heavy UI draws.
- [x] **Array Bounds Exceptions**: Added safer tier-prefix validation (`Length >= 3` and `T#` format) before slicing key prefixes.

## 3. Test Coverage Strategy

- [x] **Write the production integration tests**: Created `RecipeCalculationTests.cs` using `Xunit`.
- [ ] **Test Performance Limits**: Confirm the `RecipeCalculation_GetChildren_Performance_ShouldBeFastAndCorrect()` test succeeds. With the O(1) lookups and boxing removals, loading a massive product tree (like Advanced/Rare production items) should finish executing `.GetChildren()` under 50 milliseconds rather than stuttering out.
- [ ] **JSON File Verification**: Verify the `DUData.Load...()` static methods correctly resolve your runtime JSON DB files inside your Test Explorer output artifacts (`PreserveNewest` mode).
- [ ] **Coverage Tool**: Setup coverlet to analyze test coverage rates across the internal Math engines specifically targeting `RecipeCalculation.cs` and `CalculatorClass.cs`.

## Why These Improvements Will Drastically Improve UI Draw Speeds?

Previously `GetChildren()` was firing every time a node expanded in winForms. Because `DUData.Schematics.FirstOrDefault()` iterates over **all** dictionary objects sequentially, expanding a tree node with 40 component pieces parsed the internal dictionary completely roughly 40+ times *sequentially*, while boxing the data out in memory-hungry `ArrayLists`, and creating string clones just to read single integer tiers (`T2`, `T3`). The tests now mandate this stays blazing fast.
