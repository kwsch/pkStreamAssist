using System.Drawing;
using System.Windows.Forms;

namespace pkStreamAssist
{
    public partial class TeamView : Form
    {
        public TeamView()
        {
            InitializeComponent();
        }
        public Bitmap img;
        private void TeamView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void resizeImg(object sender, System.EventArgs e)
        {
            if (img == null)
                return;
            PB_Teams.Image = ResizeImage(img, new Size(PB_Teams.Width - 2, PB_Teams.Height - 2));
        }
        internal static Image ResizeImage(Image img, Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                return img;
            var bmp = new Bitmap(size.Width, size.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gr.DrawImage(img, new Rectangle(Point.Empty, size));
            }
            return bmp;
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            // 300x100 picture in, set form width to get scale

            int width = (int)((float)numericUpDown1.Value * 300) + 2;
            int height = (int)((float)numericUpDown1.Value * 100) + 2;

            Width += width - PB_Teams.Width;
            Height += height - PB_Teams.Height;
            resizeImg(null, null);
        }
    }
}
