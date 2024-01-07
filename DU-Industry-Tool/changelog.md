# Changelog

## v2024.1.8

- Talents form: finally resized it to fit long names so that these aren't truncated anymore
- Ore values form: max. allowed value increased to 999K
- Schematics form: added skill import button for du-craft.online website (via clipboard)
- Schematics form: fix Export button location, wasn't anchored correctly
- Export to Excel for calculations/production lists
- Main form: cosmetic fixes for 100% vs. 125% Windows scaling

## v2024.1.7

- Schematics: UI fixes due to DPI scaling

## v2024.1.6

- Schematics dialog: it is extended to be a calculator: each schematic can have a
quantity and gets the sum displayed. The total of all is below the grid.
- Added 3 buttons to load, save the entered quantities from/to a file as well
as a button to reset all.
- Export button now has options to include/exclude data
- Usage hint: select talents and hit Apply button to recalculate all schematics.

## v2024.1.4

- IMPORTANT: "schematicsValues.json" file has been updated and should NOT be manually edited!
- Added 4 talents to the Schematics form (schematics crafting) for cost and output for accurate schematic prices
- Added Excel export button to the schematics dialog incl. time and item ID
- MarketOrders.json: added a "Description" attribute to give the actual item's name; also re-added the PlayerId if found
- Fixed some internal forms handling

## v2024.1.3

- Decided to use the "Full schematics" option also on the Schematics in the bottom calculation tree
- Quantities and amounts are now displayed rounded to 3 decimals
- With above also fixed in some cases schematics for products/ammo being rounded down instead of up

## v2024.1.2

- Fix: if quantity is below batch output, use at least 1 schematic for products/ammo, still

## v2024.1.1

- Finally fixed ammo calculations (min. batch output size of 40)
- Fixed some calculation form label placements
- Changed versioning to year.month.release so it is clearer how old a version is

## v1.4.4

- Fixed some elements' names, like Maintenance Unit and some Modern Screens
- added unit's mass and volume display
- removed pre-1.0 changelog entries from changelog

## v1.4.3

- Resized search controls for better readability

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
a
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
