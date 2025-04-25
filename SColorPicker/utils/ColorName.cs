using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SColorPicker.utils
{
    internal static class ColorName
    {
        const string _dataFileName = ".\\data\\lookup.txt";
        const string _dataFileNameLatest = "D:\\Saves\\colors\\web_lookup.txt";
        static List<string> _colorNames = new List<string>();

        static ColorName()
        {
            if (_colorNames.Count.Equals(0) && File.Exists(_dataFileName))
            {
                FileInfo fileInfoLatest = new FileInfo(_dataFileNameLatest);
                FileInfo fileInfoInfo = new FileInfo(_dataFileName);

                if (fileInfoLatest.Exists && fileInfoLatest.LastWriteTime > fileInfoInfo.LastWriteTime)
                    File.Copy(_dataFileNameLatest, _dataFileName, true);

                _colorNames.AddRange(File.ReadAllLines(_dataFileName));
            }
        }

        internal static string GetColorName(string hex)
        {
            var retVal = string.Empty;
            if(string.IsNullOrWhiteSpace(hex))
                return retVal;

            hex = hex.Trim();
            if (!hex.StartsWith("#"))
                hex = $"#{hex.Trim()}";

            if(hex.Length>7)
                hex = hex.Substring(0, 7);

            var find = _colorNames.FirstOrDefault(f=>f.StartsWith(hex.ToUpper()));

            if (find != null)
            {
                var finds = find.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
                if(finds.Length > 1) 
                    retVal = find.Split('/')[1].Trim();
            }

            return retVal;
        }
    }
}
