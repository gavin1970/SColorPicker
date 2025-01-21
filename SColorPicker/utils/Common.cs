using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SColorPicker.utils
{
    internal class Common
    {
        internal static bool CleanHexColorString(string hex, out string newHex)
        {
            var retVal = true;
            var ignCase = StringComparison.CurrentCultureIgnoreCase;

            newHex = hex;

            if (string.IsNullOrWhiteSpace(newHex))
                return !retVal;

            newHex = newHex.Trim();

            while (newHex.StartsWith("0X", ignCase) || newHex.StartsWith("X", ignCase) || newHex.StartsWith("#", ignCase))
                newHex = newHex.Substring(1);

            if (newHex.Length == 3)
                newHex = $"{newHex}{newHex}";
            else if (newHex.Length != 6)
                newHex = newHex.Substring(newHex.Length - 6);    //removes hex8 or anything else added

            var reg = @"^[a-fA-F0-9]";
            Regex rX = new Regex(reg);
            if (!rX.IsMatch(newHex))
            {
                retVal = false;
                newHex = $"Hexadecimal value, after clean up, '{newHex}', is an invalid hex.";
            }

            return retVal;
        }
    }
}
