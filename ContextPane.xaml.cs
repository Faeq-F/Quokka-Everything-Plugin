using Quokka;
using Quokka.ListItems;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Keys = System.Windows.Forms.Keys;

namespace PluginEverything
{

  /// <summary>
  /// The context pane for file / folder items
  /// </summary>
  public partial class ContextPane : ItemContextPane
  {

    private readonly EverythingItem? Item;

    /// <summary>
    /// Creates the context pane
    /// </summary>
    public ContextPane()
    {
      InitializeComponent();
      Item = (EverythingItem)((SearchWindow)Application.Current.MainWindow).SelectedItem!;
      DetailsImage.Source = Item.Icon;
      NameText.Text = Item.Name;
      DescText.Text = Item.Description;
      ExtraDetails.Text = "Date Modified: " + Item.dateModified + "\nSize: " + Item.size;
    }

    private void Preview(object sender, RoutedEventArgs e)
    {
      try
      {
        using Process previewer = new();
        previewer.StartInfo.FileName = Everything.PluginSettings.Previewer;
        previewer.StartInfo.Arguments = '"' + Item!.Description + '"';
        previewer.Start();
        Application.Current.MainWindow.Close();
      }
      catch (Exception ex) { App.ShowErrorMessageBox(ex, "Could not preview item"); }
    }

    private void OpenContainingFolder(object sender, RoutedEventArgs e)
    {
      using Process folderopener = new();
      folderopener.StartInfo.FileName = (string)Application.Current.Resources["FileManager"];
      folderopener.StartInfo.Arguments = '"' + Item!.Description.Remove(Item.Description.LastIndexOf('\\')) + '"';
      folderopener.Start();
      Application.Current.MainWindow.Close();
    }

    private void OpenWith(object sender, RoutedEventArgs e)
    {
      Application.Current.MainWindow.Close();
      string args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
      args += ",OpenAs_RunDLL " + Item!.Description;
      Process.Start("rundll32.exe", args);
    }

    private void OpenContextMenu(object sender, RoutedEventArgs e)
    {
      SearchWindow window = (SearchWindow)Application.Current.MainWindow;
      // Allows setting focus to the menu before closing the window
      window.Hide();
      ShellContextMenu scm = new();
      FileInfo[] files = [new FileInfo(Item!.Description)];
      scm.ShowContextMenu(files, System.Windows.Forms.Cursor.Position);
      window.Close();
    }

    /// <summary>
    /// <inheritdoc/><br />
    /// Up and down keys select list items and the enter key executes the item's action
    /// </summary>
    /// <param name="sender"><inheritdoc/></param>
    /// <param name="e"><inheritdoc/></param>
    protected override void PageKeyDown(object sender, KeyEventArgs e)
    {
      ButtonsListView.Focus();
      if (e != null)
      {
        switch (e.Key)
        {
          case Key.Enter:
            if (ButtonsListView.SelectedIndex == -1)
            {
              ButtonsListView.SelectedIndex = 0;
            }
            Grid CurrentItem = (Grid)ButtonsListView.SelectedItem;
            Button CurrentButton = (Button)((Grid)CurrentItem.Children[1]).Children[0];
            CurrentButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            break;
          case Key.Down:
            if (ButtonsListView.SelectedIndex == -1)
            {
              ButtonsListView.SelectedIndex = 1;
            }
            else if (ButtonsListView.SelectedIndex == ButtonsListView.Items.Count - 1)
            {
              ButtonsListView.SelectedIndex = 0;
            }
            else
            {
              ButtonsListView.SelectedIndex++;
            }
            ButtonsListView.ScrollIntoView(ButtonsListView.SelectedItem);
            break;
          case Key.Up:
            if (ButtonsListView.SelectedIndex is -1 or 0)
            {
              ButtonsListView.SelectedIndex = ButtonsListView.Items.Count - 1;
            }
            else
            {
              ButtonsListView.SelectedIndex--;
            }
            ButtonsListView.ScrollIntoView(ButtonsListView.SelectedItem);
            break;
          case var value when value == KeyInterop.KeyFromVirtualKey((int)(Keys)Application.Current.Resources["ContextPaneKey"]):
            ReturnToSearch();
            break;
          default:
            return;
        }
        e.Handled = true;
      }
    }
  }

}
