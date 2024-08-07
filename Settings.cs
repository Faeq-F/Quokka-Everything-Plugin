namespace Plugin_Everything {

  /// <summary>
  /// 
  /// </summary>
  public class ItemTypes {
    /// <summary>
    /// 
    /// </summary>
    public List<string> Document { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Image { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Video { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Audio { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Archive { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Font { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Code { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> Database { get; set; } = new();
    /// <summary>
    /// 
    /// </summary>
    public List<string> System { get; set; } = new();
  }

  /// <summary>
  /// 
  /// </summary>
  public class Settings {
    /// <summary>
    /// 
    /// </summary>
    public ItemTypes ItemTypes { get; set; } = new();
  }


}
