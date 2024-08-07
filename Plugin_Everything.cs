﻿using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Plugin_Everything {
  /// <summary>
  /// 
  /// </summary>
  public partial class Everything : IPlugger {

    /// <summary>
    /// <inheritdoc/><br />
    /// The name is "Everything"
    /// </summary>
    public string PluggerName { get; set; } = "Everything";

    private static Settings pluginSettings = new();
    internal static Settings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    /// <summary>
    /// 
    /// </summary>
    public Everything() {
      Everything_SetRequestFlags(EVERYTHING_REQUEST_FILE_NAME | EVERYTHING_REQUEST_FULL_PATH_AND_FILE_NAME | EVERYTHING_REQUEST_DATE_MODIFIED | EVERYTHING_REQUEST_SIZE);
      Everything_SetSort(13);
      Everything_SetMax(700);
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// This will get called when user types a query into the search field
    /// </summary>
    public List<ListItem> OnQueryChange(string query) {
      UInt32 i;
      Everything_SetSearchW(query);
      Everything_QueryW(true);
      List<ListItem> ItemList = new List<ListItem>();
      for (i = 0; i < Everything_GetNumResults(); i++) {
        long date_modified;
        long size;
        StringBuilder path = new StringBuilder(256);

        Everything_GetResultDateModified(i, out date_modified);
        Everything_GetResultSize(i, out size);
        Everything_GetResultFullPathName(i, path, 256);
        bool isFolderResult = Everything_IsFolderResult(i);
        string dateModified = DateTime.FromFileTime(date_modified).Year + "/" + DateTime.FromFileTime(date_modified).Month + "/" + DateTime.FromFileTime(date_modified).Day + " " + DateTime.FromFileTime(date_modified).Hour + ":" + DateTime.FromFileTime(date_modified).Minute.ToString("D2");
        ItemList.Add(new EverythingItem(Marshal.PtrToStringUni(Everything_GetResultFileName(i)), path.ToString(), isFolderResult, size.ToString(), dateModified));
      }
      return ItemList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<String> SpecialCommands() {
      return new List<String>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public List<ListItem> OnSpecialCommand(string command) {
      return new List<ListItem>();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnAppStartup() { }

    /// <summary>
    /// 
    /// </summary>
    public void OnAppShutdown() { }

    /// <summary>
    /// 
    /// </summary>
    public void OnSearchWindowStartup() { }

  }

}
