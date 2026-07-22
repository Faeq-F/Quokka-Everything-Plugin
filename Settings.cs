using System.Collections.ObjectModel;

namespace PluginEverything
{
  /// <summary>
  /// Settings for the Everything Service - other settings should be configured in the running everything instance
  /// </summary>
  public class EverythingSettings
  {
    /// <summary>
    /// If this text is included in the query, full path matching is enabled for the query. Defaults to " --MatchPath"
    /// </summary>
    public string MatchPathFlag { get; set; } = " --MatchPath";
    /// <summary>
    /// If this text is included in the query, case sensitivity is enabled for the query. Defaults to " --MatchCase"
    /// </summary>
    public string MatchCaseFlag { get; set; } = " --MatchCase";
    /// <summary>
    /// If this text is included in the query, matching whole words is enabled for the query. Defaults to " --MatchWholeWord"
    /// </summary>
    public string MatchWholeWordFlag { get; set; } = " --MatchWholeWord";
    /// <summary>
    /// If this text is included in the query, Regular Expression searching is enabled for the query. Defaults to " --Regex"
    /// </summary>
    public string RegexFlag { get; set; } = " --Regex";
    /// <summary>
    /// The number of results, from the Everything service, to sort through. These results are obtained before Quokka's relevance sorting. Defaults to 20
    /// </summary>
    public string MaxResultsFromQuery { get; set; } = "20";
  }

  /// <summary>
  /// The different media types for files
  /// </summary>
  public class ItemTypes
  {
    /// <summary>
    /// File extensions for user documents
    /// </summary>
    public Collection<string> Document { get; } = new();
    /// <summary>
    /// File extensions for images
    /// </summary>
    public Collection<string> Image { get; } = new();
    /// <summary>
    /// File extensions for videos
    /// </summary>
    public Collection<string> Video { get; } = new();
    /// <summary>
    /// File extensions for audio / music
    /// </summary>
    public Collection<string> Audio { get; } = new();
    /// <summary>
    /// File extensions for archives
    /// </summary>
    public Collection<string> Archive { get; } = new();
    /// <summary>
    /// File extensions for font files
    /// </summary>
    public Collection<string> Font { get; } = new();
    /// <summary>
    /// File extensions for files with code
    /// </summary>
    public Collection<string> Code { get; } = new();
    /// <summary>
    /// File extensions for databases
    /// </summary>
    public Collection<string> Database { get; } = new();
    /// <summary>
    /// File extensions for system related files
    /// </summary>
    public Collection<string> System { get; } = new();
  }

  /// <summary>
  /// The plugin's settings
  /// </summary>
#pragma warning disable CA1724 // Type name conflicts with namespace name
  public class Settings
  {
    /// <summary>
    /// The application used to preview items
    /// </summary>
    public string Previewer { get; set; } = "C:\\Windows\\explorer.exe";

    /// <summary>
    /// The command signifier used to only show items from this plugin (defaults to "f? ")<br />
    /// Using this signifier does not change the output of this plugin, it only
    /// ensures that no other plugins' results are included in the search window results list
    /// </summary>
    public string EverythingSignifier { get; set; } = "f? ";

    /// <summary>
    /// Settings for the Everything Service 
    /// </summary>
    public EverythingSettings EverythingSettings { get; set; } = new();
    /// <summary>
    /// The different media types for files
    /// </summary>
    public ItemTypes ItemTypes { get; set; } = new();
  }

}
