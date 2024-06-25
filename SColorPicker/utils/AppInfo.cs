using System.Diagnostics;
using System.Windows.Forms;

namespace Chizl.Utils
{
    internal class AppInfo
    {
        static readonly object _assemblyLock = new object();

        static bool _loaded = false;
        static string _fileVersion = null;
        static string _productVersion = null;
        static string _title = null;
        static string _company = null;
        static string _productName = null;
        static string _copyright = null;
        static string _description = null;
        static string _trademark = null;

        internal static string FileVersion 
        { 
            get 
            {
                LoadAppInfo();
                return _fileVersion;
            } 
        }
        internal static string ProductVersion
        {
            get
            {
                LoadAppInfo();
                return _productVersion;
            }
        }
        internal static string Title
        {
            get
            {
                LoadAppInfo();
                return _title;
            }
        }
        internal static string Company
        {
            get
            {
                LoadAppInfo();
                return _company;
            }
        }
        internal static string ProductName
        {
            get
            {
                LoadAppInfo();
                return _productName;
            }
        }
        internal static string Copyright
        {
            get
            {
                LoadAppInfo();
                return _copyright;
            }
        }
        internal static string Description
        {
            get
            {
                LoadAppInfo();
                return _description;
            }
        }
        internal static string Trademark
        {
            get
            {
                LoadAppInfo();
                return _trademark;
            }
        }

        private static void LoadAppInfo()
        {
            lock (_assemblyLock)
            {
                if (!_loaded)
                {
                    FileVersionInfo fi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
                    _fileVersion = fi.FileVersion;
                    _productVersion = fi.ProductVersion;
                    _title = fi.FileDescription.Trim();
                    _company = fi.CompanyName.Trim();
                    _productName = fi.ProductName.Trim();
                    _copyright = fi.LegalCopyright.Trim();
                    _description = fi.Comments.Trim();
                    _trademark = fi.LegalTrademarks.Trim();
                    _loaded = true;
                }
            }
        }
    }
}
