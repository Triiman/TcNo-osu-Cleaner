using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace osu_cleaner
{
    public class DarkProgressBar : ProgressBar
    {
        public DarkProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Nothing here. Helps control the flicker.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush brush = null;
            Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);

            Color bg = Color.FromArgb(68, 71, 90);
            Color fg = Color.FromArgb(80, 250, 123);
            brush = new LinearGradientBrush(new Point(0, 0), new Point(bounds.Width, 0), fg, bg);

            ColorBlend cblend = new ColorBlend(3);
            cblend.Colors = new Color[4] { fg, fg, bg, bg };
            var progress = (float) (((double) base.Value) / ((double) base.Maximum));
            cblend.Positions = new float[4] { 0f, progress, progress, 1f };

            brush.InterpolationColors = cblend;



            e.Graphics.FillRectangle(brush, 0, 0, bounds.Width, bounds.Height);
        }

    }

}
