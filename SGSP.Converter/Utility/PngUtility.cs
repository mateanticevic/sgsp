using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.Utility
{
    class PngUtility
    {
        public static Transform GetBoundingBox(Bitmap bmp)
        {
            int minX = 10000, minY = 10000, maxX = 0, maxY = 0;

            Color t = Color.Transparent;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    var c = bmp.GetPixel(x, y);
                    if (c.A == 255)
                    {
                        if (minX > x) minX = x;
                        if (minY > y) minY = y;
                        if (maxX < x) maxX = x;
                        if (minY < y) maxY = y;
                    }
                }
            }

            Transform b = new Transform();

            b.X = minX;
            b.Y = minY;

            b.Width = maxX - minX;
            b.Height = maxY - minY;

            return b;
        }
    }
}
