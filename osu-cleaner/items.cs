using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DarkUI.Config;

namespace osu_cleaner
{
    public sealed class Consts
    {
        public const int toolWindowHeaderSize = 25;
        public const int documentTabAreaSize = 24;
        public const int toolWindowTabAreaSize = 21;
        public static int Padding = 10;

        public static int ScrollBarSize = 15;
        public static int ArrowButtonSize = 15;
        public static int MinimumThumbSize = 11;

        public static int CheckBoxSize = 12;
        public static int RadioButtonSize = 12;
    }

    public class DarkProgressBar : ProgressBar
    {
        public DarkProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Nothing here. Helps control the flicker.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var ctx = new BufferedGraphicsContext();
            var bg = ctx.Allocate(e.Graphics, e.ClipRectangle);

            var progress = (float) (Value / (double) Maximum);
            if (progress <= 0.95) // Prevents blurry progress bar freezing at end
            {
                LinearGradientBrush brush = null;
                var bounds = new Rectangle(0, 0, Width, Height);

                var bgc = Color.FromArgb(68, 71, 90);
                var fgc = Color.FromArgb(255, 128, 191);
                brush = new LinearGradientBrush(new Point(0, 0), new Point(bounds.Width, 0), fgc, bgc);

                var cblend = new ColorBlend(3);
                cblend.Colors = new Color[4] {fgc, fgc, bgc, bgc};
                cblend.Positions = new float[4] {0f, progress, progress, 1f};

                brush.InterpolationColors = cblend;

                bg.Graphics.FillRectangle(brush, 0, 0, bounds.Width, bounds.Height);
            }
            else
            {
                using (var b = new SolidBrush(Color.FromArgb(255, 128, 191)))
                {
                    var boxRect = new Rectangle(0, 0, Width, Height);
                    bg.Graphics.FillRectangle(b, boxRect);
                }
            }
            
            bg.Render(e.Graphics);
            bg.Dispose();
            ctx.Dispose();
        }
    }

    public class DarkCheckedListBox : CheckedListBox // Created by TechNobo :)
    {
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var ctx = new BufferedGraphicsContext();
            var bg = ctx.Allocate(e.Graphics, e.Bounds);
            // Draw element background
            using (var b = new SolidBrush(Color.FromArgb(34, 33, 44)))
            {
                bg.Graphics.FillRectangle(b, e.Bounds);
            }


            if (e.Index >= 0)
            {
                var checkSize = CheckBoxRenderer.GetGlyphSize(bg.Graphics, CheckBoxState.MixedNormal);
                var dx = (e.Bounds.Height - checkSize.Width) / 2;
                e.DrawBackground();
                var isChecked = GetItemChecked(e.Index);
                //CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(dx, e.Bounds.Top + dx), isChecked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                var rect = new Rectangle(dx + 1, e.Bounds.Top + dx, 13, 13);

                var size = Consts.CheckBoxSize;

                var textColor = Colors.LightText;
                var borderColor = Colors.LightText;
                var fillColor = Colors.LightestBackground;

                //if (Enabled)
                //{
                //    if (GetSelected(e.Index))
                //    {
                //        borderColor = Colors.BlueHighlight;
                //        fillColor = Colors.BlueSelection;
                //    }
                //    if (_controlState == DarkControlState.Pressed && GetSelected(e.Index))
                //    {
                //        borderColor = Colors.GreyHighlight;
                //        fillColor = Colors.GreySelection;
                //    }
                //}
                //else
                if (!Enabled)
                {
                    textColor = Colors.DisabledText;
                    borderColor = Colors.GreyHighlight;
                    fillColor = Colors.GreySelection;
                }

                using (var b = new SolidBrush(Colors.GreyBackground))
                {
                    bg.Graphics.FillRectangle(b, rect);
                }

                using (var p = new Pen(borderColor))
                {
                    var boxRect = new Rectangle(rect.X, rect.Y, 13, 13);
                    bg.Graphics.DrawRectangle(p, boxRect);
                }

                if (isChecked)
                    using (var b = new SolidBrush(fillColor))
                    {
                        var boxRect = new Rectangle(rect.X + 2, rect.Y + 2, 10, 10);
                        bg.Graphics.FillRectangle(b, boxRect);
                    }

                using (var b = new SolidBrush(textColor))
                {
                    var stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    };
                }

                using (var sf = new StringFormat {LineAlignment = StringAlignment.Center})
                {
                    using (Brush brush = new SolidBrush(isChecked ? Color.FromArgb(255, 128, 191) : ForeColor))
                    {
                        bg.Graphics.DrawString(Items[e.Index].ToString(), Font, brush,
                            new Rectangle(e.Bounds.Height + 2, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height,
                                e.Bounds.Height), sf);
                    }
                }
                
                bg.Render(e.Graphics);
                bg.Dispose();
                ctx.Dispose();
            }
        }
    }
}