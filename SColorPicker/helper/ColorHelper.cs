using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SColorPicker.helper
{
    internal class ColorHelper
    {
        internal int GetRadius(Size rct)
        {
            return Convert.ToInt32(Math.Min(rct.Width, rct.Height) / 2);
        }
    }
}
