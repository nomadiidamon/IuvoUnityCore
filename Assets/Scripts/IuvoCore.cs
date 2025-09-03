
namespace IuvoUnity
{
    namespace Core
    {
        using IuvoUnity.BaseClasses;
        using IuvoUnity.Configurations;
        using IuvoUnity.DataStructs;
        using IuvoUnity.Extensions;
        using IuvoUnity.Debug;
        using IuvoUnity.Editor;

        public static class IuvoCore
        {
            private static string Version => "1.0.0";
            private static string ReleaseDate => "2025-08-22";

            public static string GetVersion()
            {
                return $" {Version}, Release Date: {ReleaseDate}";
            }

            public static string GetReleaseDate()
            {
                return ReleaseDate;
            }
        }
    }
}