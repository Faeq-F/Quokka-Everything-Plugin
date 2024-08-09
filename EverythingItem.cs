using Quokka;
using Quokka.ListItems;
using System.Diagnostics;
using System.Windows.Media.Imaging;


namespace Plugin_Everything {

  class EverythingItem : ListItem {

    public bool isFolder;

    public EverythingItem(string Name, string Path, bool isFolder, string size, string dateModified) {
      this.Name = Name;
      this.Description = Path;
      this.isFolder = isFolder;

      if (isFolder) {
        string? iconName = null;
        switch (Name.ToLower()) {
          case "desktop": iconName = "desktopFolder.png"; break;
          case "documents": iconName = "documentsFolder.png"; break;
          case "downloads": iconName = "downloadsFolder.png"; break;
          case "music": iconName = "musicFolder.png"; break;
          case "pictures": iconName = "imagesFolder.png"; break;
          case "videos": iconName = "videosFolder.png"; break;
          case ".git": iconName = "gitFolder.png"; break;
          case "onedrive": iconName = "onedriveFolder.png"; break;
        }

        if (iconName != null) {
          this.Icon = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\" + iconName));
        } else {
          this.Icon = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Config\\Resources\\folder.png"));
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

        this.Icon = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\PlugBoard\\Plugin_Everything\\Plugin\\" + iconName));
      }
    }

    /// <summary>
    /// Opens the file / folder
    /// </summary>
    public override void Execute() {
      if (isFolder) {
        Process.Start((string) App.Current.Resources["FileManager"], '"' + Description + '"');
      } else {
        Process.Start(Description);
      }
      App.Current.MainWindow.Close();
    }
  }

}
