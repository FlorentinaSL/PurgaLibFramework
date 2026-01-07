namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;

public static class Cassie
{
    public static void Message(string message)
        => Message(message, false, true);

    public static void Message(
        string message,
        bool isNoisy,
        bool isSubtitles
    )
    {
        LabApi.Features.Wrappers.Cassie.Clear();
        LabApi.Features.Wrappers.Cassie.Message(
            message,
            isNoisy: isNoisy,
            isSubtitles: isSubtitles
        );
    }
    public static void Clear()
    {
        LabApi.Features.Wrappers.Cassie.Clear();
    }
}