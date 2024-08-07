using Quokka;
using Quokka.ListItems;
using System.Windows.Media.Imaging;


namespace Plugin_Everything {

  class EverythingItem : ListItem {

    public EverythingItem(string Name, string Path, bool isFolder, string size, string dateModified) {
      this.Name = Name;
      this.Description = Path;
      if (isFolder) {
        string? iconName = null;
        if (Name.ToLower() == "desktop") iconName = "desktopFolder.png";
        else if (Name.ToLower() == "documents") iconName = "documentsFolder.png";
        else if (Name.ToLower() == "downloads") iconName = "downloadsFolder.png";
        else if (Name.ToLower() == "music") iconName = "musicFolder.png";
        else if (Name.ToLower() == "pictures") iconName = "imagesFolder.png";
        else if (Name.ToLower() == "videos") iconName = "videosFolder.png";
        else if (Name.ToLower() == ".git") iconName = "gitFolder.png";
        else if (Name.ToLower() == "onedrive") iconName = "onedriveFolder.png";
        if (iconName != null) {
          this.Icon = new BitmapImage(new Uri(
        Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\" + iconName));
        } else {
          this.Icon = new BitmapImage(new Uri(
          Environment.CurrentDirectory + "\\Config\\Resources\\folder.png"));
        }
      } else {
        string iconName = "file.png";
        string ext = System.IO.Path.GetExtension(Path);
        if (Everything.PluginSettings.ItemTypes.Document.Contains(ext)) iconName = "document.png";
        else if (Everything.PluginSettings.ItemTypes.Image.Contains(ext)) iconName = "image.png";
        else if (Everything.PluginSettings.ItemTypes.Video.Contains(ext)) iconName = "video.png";
        else if (Everything.PluginSettings.ItemTypes.Audio.Contains(ext)) iconName = "audio.png";
        else if (Everything.PluginSettings.ItemTypes.Archive.Contains(ext)) iconName = "archive.png";
        else if (Everything.PluginSettings.ItemTypes.Font.Contains(ext)) iconName = "font.png";
        else if (Everything.PluginSettings.ItemTypes.Code.Contains(ext)) iconName = "code.png";
        else if (Everything.PluginSettings.ItemTypes.Database.Contains(ext)) iconName = "database.png";
        else if (Everything.PluginSettings.ItemTypes.System.Contains(ext)) iconName = "system.png";
        this.Icon = new BitmapImage(new Uri(
        Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\" + iconName));
      }
    }

    //When item is selected, open
    public override void Execute() {

      App.Current.MainWindow.Close();
    }
  }

}
