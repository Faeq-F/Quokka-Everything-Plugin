using Quokka;
using Quokka.ListItems;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
      try {
        using Process previewer = new();
        previewer.StartInfo.FileName = (string) Everything.PluginSettings.Previewer;
        previewer.StartInfo.Arguments = '"' + Item!.Description + '"';
        previewer.Start();
        App.Current.MainWindow.Close();
      } catch (Exception ex) { App.ShowErrorMessageBox(ex, "Could not preview item"); }
    }

    private void OpenContainingFolder(object sender, System.Windows.RoutedEventArgs e) {
      using Process folderopener = new();
      folderopener.StartInfo.FileName = (string) App.Current.Resources["FileManager"];
      folderopener.StartInfo.Arguments = '"' + Item!.Description.Remove(Item.Description.LastIndexOf('\\')) + '"';
      folderopener.Start();
      App.Current.MainWindow.Close();
    }

    private void OpenWith(object sender, System.Windows.RoutedEventArgs e) {
      App.Current.MainWindow.Close();
      var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
      args += ",OpenAs_RunDLL " + Item!.Description;
      Process.Start("rundll32.exe", args);
    }

    private void OpenContextMenu(object sender, System.Windows.RoutedEventArgs e) {
      App.Current.MainWindow.Close();
      ShellContextMenu scm = new ShellContextMenu();
      FileInfo[] files = new FileInfo[1];
      files[0] = new FileInfo(Item!.Description);
      scm.ShowContextMenu(files, System.Windows.Forms.Cursor.Position);
    }

    /// <summary>
    /// <inheritdoc/><br />
    /// Up and down keys select list items and the enter key executes the item's action
    /// </summary>
    /// <param name="sender"><inheritdoc/></param>
    /// <param name="e"><inheritdoc/></param>
    protected override void Page_KeyDown(object sender, KeyEventArgs e) {
      ButtonsListView.Focus();
      switch (e.Key) {
        case Key.Enter:
          if (( ButtonsListView.SelectedIndex == -1 )) ButtonsListView.SelectedIndex = 0;
          Grid CurrentItem = (Grid) ButtonsListView.SelectedItem;
          Button CurrentButton = (Button) ( (Grid) CurrentItem!.Children[1] ).Children[0];
          CurrentButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
          break;
        case Key.Down:
          if (( ButtonsListView.SelectedIndex == -1 )) {
            ButtonsListView.SelectedIndex = 1;
          } else if (ButtonsListView.SelectedIndex == ButtonsListView.Items.Count - 1) {
            ButtonsListView.SelectedIndex = 0;
          } else {
            ButtonsListView.SelectedIndex++;
          }
          ButtonsListView.ScrollIntoView(ButtonsListView.SelectedItem);
          break;
        case Key.Up:
          if (( ButtonsListView.SelectedIndex == -1 ) || ( ButtonsListView.SelectedIndex == 0 )) {
            ButtonsListView.SelectedIndex = ButtonsListView.Items.Count - 1;
          } else {
            ButtonsListView.SelectedIndex--;
          }
          ButtonsListView.ScrollIntoView(ButtonsListView.SelectedItem);
          break;
        case Key.Apps: //This is the menu key
          base.ReturnToSearch();
          break;
        default:
          return;
      }
      e.Handled = true;
    }
  }

}
