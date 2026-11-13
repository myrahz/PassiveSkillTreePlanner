using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;
using System.Windows.Forms;

namespace PassiveSkillTreePlanner;

public class PassiveSkillTreePlannerSettings : ISettings
{
    public string SelectedCharacterBuild { get; set; } = "";
    public string SelectedAtlasBuild { get; set; } = "";

    // Keep legacy if you already had it (for migration)
    public string SelectedBuild { get; set; } = "default";
    public string LastSelectedCharacterUrl { get; set; }
    public string LastSelectedAtlasUrl { get; set; }

    public HotkeyNode CtrlClickHighlightedHotkey { get; set; } = Keys.F6;
    public RangeNode<int> InputDelay { get; set; } = new(50, 25, 150);

    // Max passive allocations made per hotkey press (limited by available points)
    public RangeNode<int> MaxAllocationsPerClick { get; set; } = new(1, 1, 20);

    public ToggleNode AutoAdvanceCharacterTree { get; set; } = new ToggleNode(true);
    public ToggleNode AutoAdvanceAtlasTree { get; set; } = new ToggleNode(true);
    public RangeNode<int> LineWidth { get; set; } = new(3, 0, 5);

    public ColorNode PickedBorderColor { get; set; } = new ColorNode();
    public ColorNode UnpickedBorderColor { get; set; } = new ColorNode(Color.Green);
    public ColorNode WrongPickedBorderColor { get; set; } = new ColorNode(Color.Red);

    public ToggleNode ShowControlPanel { get; set; } = new ToggleNode(true);
    public ToggleNode SaveChangesAutomatically { get; set; } = new ToggleNode(true);
    public ToggleNode Debug { get; set; } = new ToggleNode(false);
    public ToggleNode Enable { get; set; } = new ToggleNode(false);
}