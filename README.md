# PassiveSkillTreePlanner

An ExileApi plugin that overlays a saved build's passive tree (character and atlas) on the in-game
tree panel. It is a fork of [exApiTools/PassiveSkillTreePlanter](https://github.com/exApiTools/PassiveSkillTreePlanter),
renamed to `PassiveSkillTreePlanner`.

## What it adds over upstream

**Auto-allocation (the main difference).** Upstream only highlights nodes and leaves the clicking to you.
This fork can click them for you.
- Press the hotkey (`CtrlClickHighlightedHotkey`, default F6) to toggle auto-passive mode.
- It reads the unused-passive-points button tooltip to tell whether character or atlas points are available.
- It picks the next missing node of the selected build and ctrl-clicks it.
- Before each click it checks that you are still in the same zone, the game is not loading and a tree panel is open.
- The ctrl-click is atomic: the modifier key is always released and the click is skipped if the screen changed.
- `MaxAllocationsPerClick` sets how many points are spent per press.
- `InputDelay` sets the delay between inputs.
- Your original mouse position is restored afterwards.
- The code for this is in `InputHandler.cs`, which is new, and `RunAutoPassiveTask` in the main file.

**Smarter next-node choice.**
- It picks the missing node adjacent to your allocated nodes with the shortest remaining path (BFS depth).
- If nothing is adjacent, it falls back to the best class starter node and caches that pick.
- Ascendancy nodes that don't belong to your ascendancy are ignored.

**Auto-advance between trees.**
- `AutoAdvanceCharacterTree` and `AutoAdvanceAtlasTree` move on to the next tree in a build once the current one is complete.
- They are rate limited and remember the last URL they advanced from.

**Settings and UI.**
- Upstream's separate `SettingsMenu` class is removed. Settings are drawn in `DrawSettings`, with a larger inline build editor in the main file.
- Selected character and atlas builds are stored separately, and the legacy `SelectedBuild` is kept for migration.
- The `CurvedLines` option was dropped and a `Debug` toggle was added.

**Updated data.** `SkillTreeData.json`, `AtlasTreeData.json` and the texture atlas differ from upstream's copies.

## Install

Place this folder in `Plugins/Source` and let ExileApi compile it. Builds are read from
`config/PassiveSkillTreePlanner/Builds`. Settings are stored as `PassiveSkillTreePlanner_settings.json`.
