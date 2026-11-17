# Alt Biomes

Plugin allows to edit and add new Alt Biomes into Valheim.

## Notes

- Loading world with modified Alt Biomes, will change the placement of Alt Biomes within world.
- Naming `addLocations` locations the same as locations that already exist will log an Error, which does not break the game, but to avoid the Error log, it is recommended to name your locations uniquely.
- `clutterOverride` is a custom field added by plugin to override alt biome grass.

## Commands

```yml
altbiomes export AltBiome [name]
altbiomes export Location [name]
altbiomes export Vegetation [name]
altbiomes find [name]

```