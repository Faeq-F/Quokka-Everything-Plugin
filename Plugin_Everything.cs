using Quokka.ListItems;
using Quokka.PluginArch;
using System.Reflection;

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
    private Assembly EverythingSearchClient;
    private Type SearchClient;
    private object everything;
    private MethodInfo EverythingSearchMethod;
    private Type Result;
    private Type SearchFlags;
    private Type BehaviorWhenBusy;
    private Type SortBy;
    private Type SortDirection;

    /// <summary>
    /// 
    /// </summary>
    public Everything() {
      EverythingSearchClient = Assembly.LoadFrom(
        Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\EverythingSearchClient.dll"
      );
      SearchClient = EverythingSearchClient.GetType("EverythingSearchClient.SearchClient")!;
      Result = EverythingSearchClient.GetType("EverythingSearchClient.Result")!;
      SearchFlags = EverythingSearchClient.GetType("EverythingSearchClient.SearchClient.SearchFlags")!;
      BehaviorWhenBusy = EverythingSearchClient.GetType("EverythingSearchClient.SearchClient.BehaviorWhenBusy")!;
      SortBy = EverythingSearchClient.GetType("EverythingSearchClient.SearchClient.SortBy")!;
      SortDirection = EverythingSearchClient.GetType("EverythingSearchClient.SearchClient.SortDirection")!;
      everything = Activator.CreateInstance(SearchClient)!;
      MethodInfo[] methodInfos = SearchClient.GetMethods(BindingFlags.Public | BindingFlags.Instance);
      EverythingSearchMethod = SearchClient.GetMethod("Search", new Type[] { typeof(string), SearchFlags, typeof(uint), typeof(uint), BehaviorWhenBusy, typeof(uint), SortBy, SortDirection })!;
    }

    /// <summary>
    /// This will get called when user types a query into the search field
    /// </summary>
    public List<ListItem> OnQueryChange(string query) {
      dynamic res = EverythingSearchMethod.Invoke(everything, new string[] { query })!;
      List<ListItem> ItemList = new List<ListItem>();
      foreach (dynamic item in res.Items) {
        ItemList.Add(new EverythingItem(item.Name, item.Path));
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
