using Quokka.ListItems;

namespace Plugin_Everything {

  /// <summary>
  /// The context pane for file / folder items
  /// </summary>
  public partial class ContextPane : ItemContextPane {

    /// <summary>
    /// Creates the context pane
    /// </summary>
    public ContextPane() {
      InitializeComponent();
      base.ReturnToSearch();
    }
  }

}
