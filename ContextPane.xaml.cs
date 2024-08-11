using Quokka;
using Quokka.ListItems;
using System.Windows;

namespace Plugin_Everything {

  /// <summary>
  /// The context pane for file / folder items
  /// </summary>
  public partial class ContextPane : ItemContextPane {

    private readonly EverythingItem? Item;

    /// <summary>
    /// Creates the context pane
    /// </summary>
    public ContextPane() {
      InitializeComponent();
      this.Item = (EverythingItem) ( (SearchWindow) Application.Current.MainWindow ).SelectedItem!;
      DetailsImage.Source = Item.Icon;
      NameText.Text = Item.Name;
      DescText.Text = Item.Description;
      ExtraDetails.Text = "Date Modified: " + Item.dateModified + "\nSize: " + Item.size;
    }

    private void Preview(object sender, System.Windows.RoutedEventArgs e) {

    }

    private void OpenWith(object sender, System.Windows.RoutedEventArgs e) {

    }

    private void OpenContainingFolder(object sender, System.Windows.RoutedEventArgs e) {

    }
  }

}
