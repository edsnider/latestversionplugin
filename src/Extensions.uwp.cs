using System;
using Windows.ApplicationModel;

namespace Plugin.LatestVersion
{
    internal static class Extensions
    {
        public static string ToVersionString(this PackageVersion version)
        {
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
