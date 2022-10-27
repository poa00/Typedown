﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typedown.Universal.Utilities
{
    public static class Common
    {
        public static nint PackPoint(this System.Drawing.Point point) => point.X | (point.Y << 16);
    }
}