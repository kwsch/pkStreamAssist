using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace pkStreamAssist
{
    public static class Util
    {
        // Image Layering/Blending Utility
        public static Bitmap LayerImage(Image baseLayer, Image overLayer, int x, int y, double trans)
        {
            if (baseLayer == null)
                return overLayer as Bitmap;
            Bitmap img = new Bitmap(baseLayer.Width, baseLayer.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(baseLayer, new Point(0, 0));
                Bitmap o = ChangeOpacity(overLayer, trans);
                gr.DrawImage(o, new Rectangle(x, y, overLayer.Width, overLayer.Height));
            }
            return img;
        }
        public static Bitmap ChangeOpacity(Image img, double trans)
        {
            if (img == null)
                return null;
            if (img.PixelFormat.HasFlag(PixelFormat.Indexed))
                return (Bitmap)img;

            Bitmap bmp = (Bitmap)img.Clone();
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            IntPtr ptr = bmpData.Scan0;

            int len = bmp.Width * bmp.Height * 4;
            byte[] data = new byte[len];

            Marshal.Copy(ptr, data, 0, len);

            for (int i = 0; i < data.Length; i += 4)
                data[i + 3] = (byte)(data[i + 3] * trans);

            Marshal.Copy(data, 0, ptr, len);
            bmp.UnlockBits(bmpData);

            return bmp;
        }
        public static Bitmap ChangeAllColorTo(Image img, Color c)
        {
            if (img == null)
                return null;
            if (img.PixelFormat.HasFlag(PixelFormat.Indexed))
                return (Bitmap)img;

            Bitmap bmp = (Bitmap)img.Clone();
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            IntPtr ptr = bmpData.Scan0;

            int len = bmp.Width * bmp.Height * 4;
            byte[] data = new byte[len];

            Marshal.Copy(ptr, data, 0, len);

            byte R = c.R;
            byte G = c.G;
            byte B = c.B;
            for (int i = 0; i < data.Length; i += 4)
                if (data[i + 3] != 0)
                {
                    data[i + 0] = B;
                    data[i + 1] = G;
                    data[i + 2] = R;
                }

            Marshal.Copy(data, 0, ptr, len);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        internal static int getIndex(ComboBox cb)
        {
            return (int)(cb?.SelectedValue ?? 0);
        }
        internal static string[] getStringList(string f)
        {
            object txt = Properties.Resources.ResourceManager.GetObject(f); // Fetch File, \n to list.
            string[] rawlist = ((string)txt).Split('\n');
            for (int i = 0; i < rawlist.Length; i++)
                rawlist[i] = rawlist[i].Trim();
            return rawlist;
        }
        internal static string[] getStringList(string f, string l)
        {
            object txt = Properties.Resources.ResourceManager.GetObject("text_" + f + "_" + l); // Fetch File, \n to list.
            string[] rawlist = ((string)txt).Split('\n');
            for (int i = 0; i < rawlist.Length; i++)
                rawlist[i] = rawlist[i].Trim();
            return rawlist;
        }
        internal static List<cbItem> getCBList(string[] inStrings, params int[][] allowed)
        {
            List<cbItem> cbList = new List<cbItem>();
            if (allowed?.First() == null)
                allowed = new[] { Enumerable.Range(0, inStrings.Length).ToArray() };

            foreach (int[] list in allowed)
            {
                // Sort the Rest based on String Name
                string[] unsortedChoices = new string[list.Length];
                for (int i = 0; i < list.Length; i++)
                    unsortedChoices[i] = inStrings[list[i]];

                string[] sortedChoices = new string[unsortedChoices.Length];
                Array.Copy(unsortedChoices, sortedChoices, unsortedChoices.Length);
                Array.Sort(sortedChoices);

                // Add the rest of the items
                cbList.AddRange(sortedChoices.Select(s => new cbItem
                {
                    Text = s,
                    Value = list[Array.IndexOf(unsortedChoices, s)]
                }));
            }
            return cbList;
        }
        internal static List<cbItem> getCBList(string textfile, string lang)
        {
            // Set up
            string[] inputCSV = getStringList(textfile);

            // Get Language we're fetching for
            int index = Array.IndexOf(new[] { "ja", "en", "fr", "de", "it", "es", "ko", "zh", }, lang);

            // Set up our Temporary Storage
            string[] unsortedList = new string[inputCSV.Length - 1];
            int[] indexes = new int[inputCSV.Length - 1];

            // Gather our data from the input file
            for (int i = 1; i < inputCSV.Length; i++)
            {
                string[] countryData = inputCSV[i].Split(',');
                indexes[i - 1] = Convert.ToInt32(countryData[0]);
                unsortedList[i - 1] = countryData[index + 1];
            }

            // Sort our input data
            string[] sortedList = new string[inputCSV.Length - 1];
            Array.Copy(unsortedList, sortedList, unsortedList.Length);
            Array.Sort(sortedList);

            // Arrange the input data based on original number
            return sortedList.Select(s => new cbItem
            {
                Text = s,
                Value = indexes[Array.IndexOf(unsortedList, s)]
            }).ToList();
        }
        public class cbItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }
    }
}
