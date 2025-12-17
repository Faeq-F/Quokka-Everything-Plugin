using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PluginEverything
{
  /// <summary>
  /// The Everything Plugin
  /// </summary>
  public partial class Everything : Plugin
  {

    /// <summary>
    /// <inheritdoc/><br />
    /// The name is "Everything"
    /// </summary>
    public override string PluginName { get; set; } = "Everything";

    private static Settings pluginSettings = new();
    internal static Settings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    /// <summary>
    /// Sets Everything Request Flags and Max values num. Also loads the plugin's settings
    /// </summary>
    public Everything()
    {
      Everything_SetRequestFlags(EVERYTHING_REQUEST_FILE_NAME | EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME | EVERYTHING_REQUEST_DATE_MODIFIED | EVERYTHING_REQUEST_SIZE);
      Everything_SetMax(uint.Parse(Everything.PluginSettings.EverythingSettings.MaxResultsFromQuery, new CultureInfo("en-US")));
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\PluginEverything\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName))!;
      if (!PluginSettings.Previewer.Contains("explorer"))
      { // the only exception - a previewer should be a user installed program
        PluginSettings.Previewer = Path.GetFullPath(PluginSettings.Previewer);
      }
    }

    private Collection<ListItem> ProduceItems(string query)
    {
      UInt32 i;
      if (query.Contains(PluginSettings.EverythingSettings.MatchPathFlag))
      {
        query = query.Replace(PluginSettings.EverythingSettings.MatchPathFlag, "");
        Everything_SetMatchPath(true);
      }
      else
      {
        Everything_SetMatchPath(false);
      }
      if (query.Contains(PluginSettings.EverythingSettings.MatchCaseFlag))
      {
        query = query.Replace(PluginSettings.EverythingSettings.MatchCaseFlag, "");
        Everything_SetMatchCase(true);
      }
      else
      {
        Everything_SetMatchCase(false);
      }
      if (query.Contains(PluginSettings.EverythingSettings.MatchWholeWordFlag))
      {
        query = query.Replace(PluginSettings.EverythingSettings.MatchWholeWordFlag, "");
        Everything_SetMatchWholeWord(true);
      }
      else
      {
        Everything_SetMatchWholeWord(false);
      }
      if (query.Contains(PluginSettings.EverythingSettings.RegexFlag))
      {
        query = query.Replace(PluginSettings.EverythingSettings.RegexFlag, "");
        Everything_SetRegex(true);
      }
      else
      {
        Everything_SetRegex(false);
      }
      Everything_SetSearchW(query);
      Everything_QueryW(true);


      Collection<ListItem> ItemList = new();
      for (i = 0; i < Everything_GetNumResults(); i++)
      {
        StringBuilder path = new(256);

        Everything_GetResultDateModified(i, out long date_modified);
        Everything_GetResultSize(i, out long size);
        Everything_GetResultFullPathName(i, path, 256);
        bool isFolderResult = Everything_IsFolderResult(i);
        bool isVolumeResult = Everything_IsVolumeResult(i);
        string dateModified = DateTime.FromFileTime(date_modified).Year + "/" + DateTime.FromFileTime(date_modified).Month + "/" + DateTime.FromFileTime(date_modified).Day + " " + DateTime.FromFileTime(date_modified).Hour + ":" + DateTime.FromFileTime(date_modified).Minute.ToString("D2");
        ItemList.Add(new EverythingItem(Marshal.PtrToStringUni(Everything_GetResultFileName(i)), path.ToString(), isFolderResult, isVolumeResult, size.ToString(), dateModified));
      }
      return ItemList;
    }

    /// <summary>
    /// <inheritdoc /><br />
    /// Executes the query, obtains the relevant information for each item from Everything and then creates EverythingItems for them
    /// </summary>
    /// <param name="query"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public override Collection<ListItem> OnQueryChange(string query)
    {
      query ??= "";
      return ProduceItems(query);

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The EverythingSignifier from plugin settings
    /// </returns>
    public override Collection<string> CommandSignifiers()
    {
      return new Collection<string>() { PluginSettings.EverythingSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The EverythingSignifier (Since there is only 1 signifier for this plugin), followed by the file / folder being searched for</param>
    /// <returns>Collection of files / folders that possibly match what is being searched for</returns>
    public override Collection<ListItem> OnSignifier(string command)
    {
      command ??= "";
      command = command.Substring(PluginSettings.EverythingSignifier.Length);
      return FuzzySearch.Sort(command, ProduceItems(command));
    }

  }

}
