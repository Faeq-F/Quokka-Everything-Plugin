using Quokka.ListItems;

namespace Plugin_Everything {

  /// <summary>
  /// 
  /// </summary>
  public partial class ContextPane : ItemContextPane {

    /// <summary>
    /// 
    /// </summary>
    public ContextPane() {
      InitializeComponent();
      base.ReturnToSearch();
    }
  }

}
