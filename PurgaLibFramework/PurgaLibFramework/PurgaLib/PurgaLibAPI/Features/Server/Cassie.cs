using System.Text.RegularExpressions;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public static class Cassie
    {
        private static string CleanMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return "";
            
            message = Regex.Replace(message, @"pitch_\d+(\.\d+)?", "", RegexOptions.IgnoreCase);
            message = Regex.Replace(message, @"\.g\d+", "", RegexOptions.IgnoreCase);

            return message;
        }

        public static void Message(string message, bool isNoisy = true, bool isSubtitles = true)
        {
            LabApi.Features.Wrappers.Cassie.Clear();

            var cleanSubtitles = CleanMessage(message);

            LabApi.Features.Wrappers.Cassie.Message(
                message,
                isNoisy: isNoisy,
                isSubtitles: isSubtitles,
                customSubtitles: cleanSubtitles
            );
        }

        public static void Clear()
        {
            LabApi.Features.Wrappers.Cassie.Clear();
        }
    }
}
