## Current version: 2026.2.20

The work on the wiki has started, barely. But I'll add infos and hints when I can.

## Branching Model

This repo uses a two-lane model (`master` + `next-2026`) with `.slnx` for the next lane.
See `BRANCHING.md` for the exact policy.

### Discord

You can contact me on Discord in the [DU Open Source Initiative](https://discord.gg/gkn8ScASNy) server, where a channel for this tool exists.

## Release edition

- v1.0+ requires .NET Framework 4.8.1 from here: <https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net481-web-installer>
- Maintained by tobitege since v0.5+ by tobitege (<https://github.com/tobitege/DU-Industry-Tool>)
- Major updates to recipes to be current as of 2022-11-19.
- User interface completely overhauled with tree-display of all ingredients and schematics.
- Incremental recipe search with customizable quantity dropdown.
- Double-click items in tree to "drill down".
- Schematic cost is now maintained and associated with the corresponding pures, products and elements.
- Production List: load/save a list of any items to be calculated together.
- Display of industry, clickable to see a list of all items produced by that type.
- Display of mass, volume (of item itself) and nanocraftable status.
- Options to remember window position and to auto-load production list on startup.

Big thanks to Jericho1060 for making available item/recipe data dumps via
his website <https://du-lua.dev> which helped me a lot to update the data displayed!

Binary releases available here:
<https://github.com/tobitege/DU-Industry-Tool/releases>

# DU-Industry-Tool

Basic WinForms project that allows you to view calculated values of all known in-game recipes. Has search bar, and ability to drill down through intermediates to find their recipes as well

GIF showing functionality
<https://gyazo.com/a8740425ac2fe244d87e87980c16a2cf>

# Older releases < v1.x are no longer maintained

Anyone could add the mining units or other new items, it's just a json file - but please share it, either through a fork or a PR.  Either add the new stuff manually, or:

1. Do a hyperion export of all item NqRecipeId's for all items (particularly the new ones), along with Name
2. Create a lua script to run core.getSchematicInfo(id) for every NqRecipeId.  Store all the data it gives you, and export it to json when done (writing to logfile is usually best to be able to copy it out)
3. Do a hyperion export of all item Groups (it's a separate category in lua export), the GroupId and ParentGroupId (see: Groups.json in this repo, but there may be new ones)
4. Using any language, lua or out of game at this point, load the recipe info json file, along with the Groups json file, and combine them.  Add fields as necessary to the recipe data to match the 'SchematicRecipe' class in Recipe.cs, such as 'Name', 'GroupId', and 'ParentGroupName' (Key doesn't need to exist and/or should be null).  I think those three vars are the only thing different from core.getSchematicInfo's output
5. Save as RecipesGroups.json and everything should work

## Recipe Calculation Process

The recipe calculation process in DU-Industry-Tool is managed predominantly by `DU-Industry-Tool/Classes/CalculatorClass.cs`, orchestrating through `Calculator.CalculateRecipe(...)` and `Calculator.Collect()`.

Here is a structural overview of how items in Dual Universe are evaluated based on materials, player talents, and system constraints.

### 1. Initialization and Input Parameters

Calculation starts in `Calculator.CalculateRecipe(string key, decimal amount, ...)`.

- The system sets up the internal repository (`Calculator.All`) for the overall product and all sub-components.
- Parsing begins at `depth = 0`.
- Base quantity, batch mode, and target yield settings are initialized for the selected item.

### 2. Player Talents and Variable Adjustments

Before material costs are computed, player talents are applied through `CalculatorClass.GetTalents()` and `Calculator.GetTalentsForKey()`.

This produces four key values:

- `inputMultiplier` and `inputAdder`: modify required input quantities.
- `outputMultiplier` and `outputAdder`: modify produced output quantities.

### 3. Base Materials (Ores and Plasma)

If the requested item is a bottom-tier material (Ore or Plasma), recursion ends immediately.

Cost is calculated as:

`required amount * base market/data value`

Raw material costs are sourced from `DUData.Ores`.

### 4. Recursive Ingredient Tree Parsing

For craftable items (Parts, Products, Pures), the calculator iterates over all `Ingredients` in the item's `SchematicRecipe`.

- A factor is computed to match required output, using talent-adjusted inputs and outputs.
- Special exceptions (for example Catalysts or Pure Oxygen/Hydrogen) can affect required volume but bypass direct monetary cost in specific paths.
- For each ingredient, `CalculateRecipe(...)` is called again with `depth + 1` until raw ores are reached.
- Items are categorized and cached by `SummationType` (for example `ORES`, `INGREDIENTS`, `PARTS`, `PRODUCTS`, `PURES`).

### 5. Collection and Roll-Up (Aggregation)

When recursion collapses back to the top node (`depth == 0`), `Calculator.Collect()` performs final aggregation.

- Tree resolution maps sub-components from the global dictionary back to their parent nodes.
- Blueprint schematics are resolved by tier/type (for example T2+, products vs pures).
- `Calculator.CalcSchematic()` computes schematic cost, copy/batch requirements, and copy-time related values.

### 6. Retail and Margin Evaluation

After `OreCost` and `SchematicsCost` are known, final values are produced through `Calculator.CalcRetail()`.

Depending on `CalcOptions`, the engine can:

- apply configured gross margin,
- round retail values for DU market-style display.

The finalized hierarchy is then consumed by UI-layer helpers like `RecipeCalculation`, which prepares recursive categorized totals into bindable trees for display.
