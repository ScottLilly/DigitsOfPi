using System;

namespace DigitsOfPi.Engine.Helpers
{
    public static class StringExtensions
    {
        public static string ToFormattedVersion(this Version version)
        {
            return $"{version.Major}.{version.Minor}.{version.MajorRevision}";
        }
    }
}