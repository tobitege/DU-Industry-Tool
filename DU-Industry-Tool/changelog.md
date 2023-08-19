# Changelog

## v1.4.2
- Added "Cost for 1" to results
- Adjusted "Related Talents" display

## v1.4.1
- Fixed: all industry for recipes should now be correct (on which something is produced)
- Several more updates to production times

## v1.4.0
- Added more default values to quantity search combobox
- Updated recipes based on du-lua.dev recipes data of DU v1.4
  - Concrete product: fixed Carbon/Silicon ingredients from 38.0 to 37.5 L
  - Added Uncommon Firing System XS/S/M
  - Fixed names of several very large modern screens
  - Fixed name for "Rare Control System l" (had "Advanced")
  - Applied updates to several recipes to unit volume, mass or production time
  - Renamed Repair Unit xl to Maintenance Unit xl

## v1.1.5
- Transparent Screens: fixed element schematics
- Excluded Hydrogen, Oxygen, Catalyst from calculation/grid display

## v1.1.4
- Concrete product ingredients fix
- Fixed industry for Poly* products
- Added Kergon-X5
- Fixed recipe calculation for Refined Materials

## v1.1.3
- Fixed: calculation of production list was broken
- Fixed: empty entries in production list caused exception

## v1.1.2
- Fixed: added 2 missing parts in recipes
- File|Export to CSV: it now takes the currently entered quantity instead of just 1.
- File|Export to CSV: renamed "Cost to make" to "Ore Cost" and added columns "Schematic Cost" and "Total Cost"

## v1.1.1
- Options: added "Full schematic quantities" (default: checked) to calculate
full costs of schematics (on) or fractional costs (off).
Results display also shows either e.g. 7 schematics vs. 6.44 schematics.

## v1.1.0
- Updated recipes to include most changes as per DU patch 1.1

## v1.0.6
- Applied v1.0.5 corrections to recipes file (was left out due to time constraints)
- Textual changes from "" to null for unset schematic keys in recipes file

## v1.0.5
- More corrections in schematics file with regard to release data

## v1.0.4
- Fixed 771 legacy schematic prices in recipes file

## v1.0.3
- Fixed fuels schematic quantity calculation
- Fix duplicate addition of element schematic
- Fixed version number in About dialog

## v1.0.2
- Fixed industry size detail for 259 recipes
- Ore values: improved editing of price

## v1.0.1
- Fix for market order reading regex (thanks to Ferry)

## v1.0.0
- Major revamp of calculation output as a tree layout 
- Double-click an ore, pure, product or part in tree to drill-down.
- Ores will display additional details, like refiner input/output values and time to refine, taking talents into account.
- Related talents for ores and pures are shown and can instantly be changed/corrected.
- Updated recipe base with current data from release thanks to du-lua.dev!
- Added "Recalculate" button on the results page (for both regular items as well as Production List).
- Added "[tier] Pure Refinery Efficiency" talents for Pures to allow for correct production batch sizes and times.
- Added display of number of entries in the recipe tree's nodes
- Added nanocraft filter checkbox above recipe tree
- Added checkbox option in ribbon bar to restore window position/size.
- Added checkbox option in ribbon bar to load last opened Production List file on startup.
- Moved setup-related items from the "File" menu (Ore prices,Talents,Schematics) to new Setup group under Tools.
- Added multiple ribbon buttons for managing a Production List (add/remove/clear).
- Added "F+"/"F-" buttons on top line of results window to (temporarily) increase/decrease font size (will be improved later).
- Added "Load Config"/"Save Config" buttons on top line of results window to save or restore grid column sizes.
- Added a combobox to pick a file from all recently opened production list files.
- Added hotkey CTRL+W to close current tab
- Added hotkeys CTRL+O to open and CTRL+S to save a currently open Production List.
- Search "quantity" box now allows entering of manual numeric values.
- Name of currently open Production List is shown in window title.
- Tabs now each have their own close button.
- Several more fixes of schematics data and their calculation.
- Internal refactoring, especially of the IndustryManager class.
- NOTE: Options/settings in the ribbon bar are all stored in file "DU-Industry-Tool.usersettings.json" (where the app is located).
- NOTE: windows restore **currently** only supports 1 screen (will be improved later).
- NOTE: not all ore/pures/products talents are yet implemented!

## v0.8.1
- Fix in DLL version mismatch at startup since 0.8.0

## v0.8.0
- New feature: in new "Tools" menu you can find a new "Production List" item:
  in a special dialogue build a list of craftable recipes to be calculated in one bulk operation.
- Production lists can be saved to and loaded from files. Dialogue will remember last used directory.
- Added time display for recipe to be crafted.
- Added "Themes" menu item with 3 themes to choose from: Blue (default), Silver and Black.
- Fixed (again) industry attributes on several recipes and added a hint that this is not 100% correct
  (was determined programmatically).
- Change main form's icon to a factory.
- Some reorganisation of source files into specific sub-folders (Classes, Forms).

## v0.7.7
- The daily production rate is now linked to open the current item's calculation with the daily rate.
- Removed 260+ unusable API entries that are parts, but not ingredient in any recipe.
- Fixed ingredient type for "Painted Black Duralumin", which caused a stack overflow.
- Added internal checks to prevent stack overflow in calculation in case of faulty ingredients.
- Fixed Honeycomber naming from Refiner to Refinery in recipes.
- Re-enabled CSV export menu item.

## v0.7.6
- Fixed industry for Territory Unit XL.
- Removed **oreValues.json** and **talentSettings.json** from archive to not overwrite userdata.
- Fixed an error with a DLL version mismatch for export to CSV.
- Updated README.md file.
 
- ## v0.7.5
- Fixed wrong factors in calculations. Thanks to the people reporting this!
- Ingredients: their links will temporarily set the quantity for the item to be produced, overriding the quantity selection box.
- This makes it more convenient for drill-downs.
- Fixed potentially using wrong ore prices.
- Changing tabs no longer issues a recalculation.

## v0.7.4

- Industry attribute is now a clickable link to show all products producable on this type.
- Recipe Search box now features auto-completion with suggestions.
- Fixed doofus interim bug, where parts suddenly got schematics assigned.
- Fixed tree group assignment for antimatter cores.
- Fixes for industry assignments where the tier and size could be wrong.
- Values in Schematics form are now read-only.
- Minor layout changes to have ingredients' and products' columns have the same size.
- Changed default font and switched internal scaling functionalities.

## v0.7.3

- Added display of Industry below Products with tier and size.
- Added "Industry" attribute to all 3203 recipes.
- Removed all "optical sensor" and several "lights" parts as these are not available in game.
- Code reviewed to have no more warnings in VS2019 IntelliSense list.

## v0.7.2

- Improved search in tree to prioritize exact match over partial match.
- Fixed version number handling in About dialogue
- Some code cleanup

## v0.7.1

- Switching between result tabs will now synchronize the tree to highlight the same item.
- Changing the amount selection box will now re-calculate a currently selected recipe with the selected amount of items.
- Removed forgotten sample code resources.

## v0.7.0

- Renamed solution and application (dashes instead of blanks)
- Added Github Actions for automatic binary release generation incl. assets
- Switched version tagging to SemVer (major/minor/patch)

## v0.640

- Byproducts are now linked as well
- Added About dialogue with links to discord and github repositories
- Plasmas are now also clickable links in ingredients
- Fixed interim bug with diverging amounts in top and bottom results for pures
- Fixed a couple of recipes where plasma was still missing (Bonsai, Warp Beacon)
- Some internal code cleanup

## v0.630

- Revised a lot of items' group assignments that were listed in wrong branches.
- Fixed Nickel pure and Silver pure not being used due to a previous renaming.
- Fixed: Pure Hydrogren is now being treated as a "pure" in the results.
- Listings in calculation results are now all prefixed by tier (T1,T2...) and sorted alphabetically.
- Added application icon and version number to EXE.
- For now disabled re-saving of recipes file upon application start.
- Known issues: there might appear a slight difference for pure amounts across top and bottom lists.

## v0.620

- Added even more missing recipes for variations of Vertical Boosters and Exotic Atmo/Space Engines
- Added missing Stasis Weapons and Stasis Ammo
- Added display of mass, volume and nanocraftable status
- Internal dev features extended to automatically convert dumps from du-lua.dev to Json, import the data and log cross-checks to debugging console for review to identify missing recipes
- Big thanks to Jericho1060 for help with items/recipes data via his website https://du-lua.dev/!

## v0.610

- Added several more missing recipes for variations of e.g. Hover Engines and Vertical Boosters.
- Tree members are now sorted by tier first, then by name, i.e. "uncommon" (T2) items are listed after "basic" (T1), but before "advanced" (T3) etc.
- Fixed group assignments of several atmo/space engines and some honeycomb.
- Fixed tier number for all industry (all were wrongly on T1).
- Fixed issues with broken layouts after resizing the application and then switching recipes.
- Fixed a couple of ammo group names to show size at end of their name.
- In factory breakdown, omitted formulas for Ore to avoid DIV0 errors.

## v0.600

- Updated a lot of recipes for patch v0.31 to fix wrong ID's and add more recipes, that may be available via the item API, but may not/no longer/not yet available as items to players. Those items are marked with "(!)" in their name in the tree.
- Calculation values for costs padded which results in better readability.
- Added display of list of all superior items that have the current one as ingredient. These are clickable and basically allow "drill up".
- Internal code added to convert an item dump lua file to an almost correct JSON (more a dev tool).
- Program now opens in approx. full HD size due to the data layout needing space. :)

## v0.500

- Updated and extended version to support recipes as of patch v0.30.
- Where appropriate, recipes have gotten a schematic cost assigned to them (T2+ ores, pures, products, elements, but not parts).
- Many recipes have been corrected or added (new containers, shield generators, plasmas).
- Plasma prices are editable in the ore price dialogue now.
- A new schematics dialogue allows to edit the cost per single schematic for each category.
- Calculation output tabulates schematic cost per item(s) and also offers a new total ingredients list.
- The UI has been changed to support tabbed display of multiple results, an amount dropbox for how many items the calculation is to be performed as well as a "back" button to go to the previous recipe.
- The talents dialogue will highlight the talents related to a selected recipe (if any).
