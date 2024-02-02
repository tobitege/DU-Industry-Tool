# Latest Changes

## v2024.1.12

- Minor fixes

## v2024.1.11

### **IMPORTANT READ:**  

- **Important:**  
- *Talent values* moved to a separate, new file **talentValues.json**
(in folder %APPDATA%\Roaming\DU-Industry-Tool), thus it is no longer in
danger of being overwritten by the release archive.  
- Run this version once and values from the old talentSettings file will be
ported over. Only the schematic skills need to be set up again once.
- Then the old talentSettings.json file is obsolete and can be removed
- The talents structure and its dialog have been rewritten.  
The new file "talentSettings v2.json" file contains structural info
on talents for them being displayed in an ingame-like structure.
- From now on always replace the talentSettings v2.json file with the
the one provided in the release archive to stay current.  
Do not manually edit it!
- Added long-time missing talents for **product refinery efficency** (for all
tiers from Basic to Exotic), **assembly-** / **industry efficiency** and their
companion **efficiency handling** talents, for sizes XS to XL.  

### Other changes

- **Theming** of the application has been completely overhauled and several more
themes were added, offering a range of colors, down to almost all-black.
- With the above talents now present, the batch time calculation has been fixed,
in most ways.  
For example: *Nitron fuel*, with maxed out talents on atmo fuel production and the
chemical industry type, it will now show 105 L input (for first ingredient),
875 L output and require 7 schematics per batch.  
The time may be off by a couple of seconds due to however DU rounds values.  
In the calculation results it will still refer to batches as the volume-based
result, e.g. for 1750 L of Nitron output, it will show 2 batches (and 14 schematics
required in the main results grid).
- Enabled multi-row selection in results grid, so multiple rows can be copied to clipboard.
- Fixed Discord link in Options with a non-expiring link.
- For developers: the solution has been overhauled structurally, there are
a couple of new classes, some moved to separate folders. Also upgraded the
used KryptonToolkit to the current Canary version.