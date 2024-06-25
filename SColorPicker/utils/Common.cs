using System.Diagnostics;
using System.Reflection;

namespace SColorPicker.utils
{
    internal class Common
    {
        static string _fileVersion = null;
        static string _productVersion = null;

        internal string FileVersion 
        { 
            get 
            {
                if (string.IsNullOrWhiteSpace(_fileVersion))
                    LoadVersions();

                return _fileVersion;
            } 
        }
        internal string ProductVersion
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_productVersion)) 
                    LoadVersions();

                return _productVersion;
            }
        }

        private void LoadVersions()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            var appFullPath = currentAssembly.Location;

            FileVersionInfo fi = FileVersionInfo.GetVersionInfo(appFullPath);
            _fileVersion = fi.FileVersion;
            _productVersion = fi.ProductVersion;
        }
    }
}
