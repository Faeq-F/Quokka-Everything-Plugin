using Quokka;
using Quokka.ListItems;
using System.Windows.Media.Imaging;

namespace Plugin_Everything {

  class EverythingItem : ListItem {

    public EverythingItem(string Name, string Path) {
      this.Name = Name;
      this.Description = Path;
      this.Icon = new BitmapImage(new Uri(
          Environment.CurrentDirectory + "\\Config\\Resources\\information.png"));
    }

    //When item is selected, copy text
    public override void Execute() {

      App.Current.MainWindow.Close();
    }
  }

}
