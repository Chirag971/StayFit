
namespace StayFitz.Helper
{
    public class GlobalHelper
    {
        public static string AudioFilePath = string.Empty;

        public static Guid LoginGuid { get; set; }
        public static string IdToken { get; set; } = string.Empty;
        public static MemoryStream MemoryStream { get; internal set; }
    }

}
