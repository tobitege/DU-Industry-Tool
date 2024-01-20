# Latest Changes

## v2024.1.10

- Hotfix for atmo fuel calculation (Nitron)
- Hotfix for Excel export with margins applied showing wrong values
- Added "Fuel Efficiency" to the talentSettings.json file. It has only influence
on additional infos about batch sizes and refining times.
However, since we do not yet have any industry efficiency and handling talents,
the displayed values will still differ from values shown in game.
- Fixed Kergon-X5 not having fuel talents been applied
- **Note:** with one of next updates I will separate the talents and
and the entered user values into separate files, so that the talentSettings.json
file can be updated at any time without loosing your entered values.  
For now, keep a backup of that file before updating from the ZIP file and
restore it if necessary.  
For now the calculations should not be influenced by its changes in this patch.

## v2024.1.9

- Main form: added "Apply Gross Margin (%)" and "Round topline sums" options (of power of tens).
Like rounding up to "next 100", "next 1000" quanta etc.  
![Search with wildcard](docs/margin_rounding_options.png)
- Each calculation tab has these options independently, so there is also a button
to save the ones on the current tab as default values for any tab that is opened next.
- In a production list, the margin is applied to individual items and summed up. 
The same applies for the Excel export
- The rounding is applied to the total gross price only
- Enhanced the Excel export to take these new options into account
- Revised ordering and labeling of calculation result values, added display of the margin (q)  
![Search with wildcard](docs/prod_calc_results_with_margin.png)
- Main and production list form: recipe search boxes now support ingame-like search
with partial matches. Type at least 2 characters. When popup with entries appears,
select an entry by up/down keys or mouse.  
Then either hit ENTER or double-click the entry to run the calculation.  
Hit ESC to clear the search box.
- Search boxes support "\*" (asterisk) as wildcard, like "re mil\*eng\*" to list
all Rare Military Engines  
![Search with wildcard](docs/recipe_wildcard_search.gif)
- Revised calculation to better deal with common rounding issues as we -
especially for production lists - want to be able to provide a per item cost
(also relevant to detailed Excel export)
- Main form: fixed ribbon button "Add to List" to actually start a new production
list if currently none is active/loaded
- Main form: calculation grid now allows CTRL+C to copy values of a line to the clipboard
- Main form: the production list selection box in the Tools ribbon bar can now
be cleared via a new button. The most recent list will be the first entry now
- Production list: now allows to add honeycombs
- Production list: grid now allows to delete a row with CTRL+DEL key combo
- Production list: fixed empty grid issue after having loaded a list from file
- Calculation grid with alternating row colors for better readability  
![Search with wildcard](docs/results_grid_colored_rows.png)
- Performance improvements for calculating production lists with dozens of items
- Talents form: talents are now grouped, by e.g. Crafting, Ammo, Fuel, Scraps
- Settings storage for options etc. has been rewritten. Now the settings file is
created in folder "%APPDATA%\Roaming\DU-Industry-Tool" in file "settings.json"  
That file contains e.g. above mentioned margin and rounding options as well
as the recently added schematic talent values
- Recipe name for "Surrogate Pod Station m" fixed to be "s" instead
- Revised again display of forms with Windows scaling at 100% and e.g. 125%.
If you experience "misplaced" labels or entry fields, i.e. their content
becomes cut off, please let me know.
- Lots of fixes for bugs unfortunately introduced in the last patch(es)
- Release date: 2024-01-20

## v2024.1.8

- Talents form: finally resized it to fit long names so that these aren't truncated anymore
- Ore values form: max. allowed value increased to 999K
- Schematics form: added skill import button for du-craft.online website (via clipboard)
- Schematics form: fix Export button location, wasn't anchored correctly
- Export to Excel for calculations/production lists
- Main form: cosmetic fixes for 100% vs. 125% Windows scaling
