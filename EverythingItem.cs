using Quokka;
using Quokka.ListItems;
using System.IO;
using System.Windows.Media.Imaging;

namespace Plugin_Everything {

  class EverythingItem : ListItem {

    public EverythingItem(string Name, string Path) {
      this.Name = Name;
      this.Description = Path;
      byte[] buffer = File.ReadAllBytes(Path);
      MemoryStream memoryStream = new MemoryStream(buffer);
      BitmapImage bitmap = new BitmapImage();
      bitmap.BeginInit();
      bitmap.DecodePixelWidth = (int) App.Current.Resources["ListItemIconSize"];
      bitmap.DecodePixelHeight = (int) App.Current.Resources["ListItemIconSize"];
      bitmap.StreamSource = memoryStream;
      bitmap.EndInit();
      bitmap.Freeze();

      this.Icon = bitmap;
      //   this.Icon = Imaging.CreateBitmapSourceFromHIcon(System.Drawing.Icon.ExtractAssociatedIcon(Path)!.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
    }

    //When item is selected, open
    public override void Execute() {

      App.Current.MainWindow.Close();
    }
  }

}
