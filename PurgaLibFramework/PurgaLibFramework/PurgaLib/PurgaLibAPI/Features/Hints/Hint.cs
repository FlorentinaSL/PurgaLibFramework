namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Hints;

public class PlayerHint
{
    public PlayerHint()
        : this(string.Empty)
    {
        
    }

    public PlayerHint(string message, float duration = 3, bool show = true)
    {
        Message = message;
        Duration = duration;
        Show = show;
    }
    
    public string Message { get; set; }
    public float Duration { get; set; }
    public bool Show { get; set; }
}
