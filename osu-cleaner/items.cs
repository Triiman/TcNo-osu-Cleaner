using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using DarkUI.Config;
using DarkUI.Controls;

namespace osu_cleaner
{
    public sealed class Consts
    {
        public static int Padding = 10;

        public static int ScrollBarSize = 15;
        public static int ArrowButtonSize = 15;
        public static int MinimumThumbSize = 11;

        public static int CheckBoxSize = 12;
        public static int RadioButtonSize = 12;

        public const int ToolWindowHeaderSize = 25;
        public const int DocumentTabAreaSize = 24;
        public const int ToolWindowTabAreaSize = 21;
    }

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
            var progress = (float)(((double)base.Value) / ((double)base.Maximum));
            if (progress <= 0.95) // Prevents blurry progress bar freezing at end
            {
                LinearGradientBrush brush = null;
                Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);

                Color bg = Color.FromArgb(68, 71, 90);
                Color fg = Color.FromArgb(255, 128, 191);
                brush = new LinearGradientBrush(new Point(0, 0), new Point(bounds.Width, 0), fg, bg);

                ColorBlend cblend = new ColorBlend(3);
                cblend.Colors = new Color[4] { fg, fg, bg, bg };
                cblend.Positions = new float[4] { 0f, progress, progress, 1f };

                brush.InterpolationColors = cblend;

                e.Graphics.FillRectangle(brush, 0, 0, bounds.Width, bounds.Height);
            }
            else
            {
                using (var b = new SolidBrush(Color.FromArgb(255, 128, 191)))
                {
                    Rectangle boxRect = new Rectangle(0, 0, base.Width, base.Height);
                    e.Graphics.FillRectangle(b, boxRect);
                }
            }
        }
    }

    public class DarkCheckedListBox : CheckedListBox // Created by TechNobo :)
    {        
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Size checkSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                int dx = (e.Bounds.Height - checkSize.Width) / 2;
                e.DrawBackground();
                bool isChecked = GetItemChecked(e.Index);
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
                    e.Graphics.FillRectangle(b, rect);
                }

                using (var p = new Pen(borderColor))
                {
                    var boxRect = new Rectangle(rect.X, rect.Y, 13, 13);
                    e.Graphics.DrawRectangle(p, boxRect);
                }

                if (isChecked)
                {
                    using (var b = new SolidBrush(fillColor))
                    {
                        Rectangle boxRect = new Rectangle(rect.X + 2, rect.Y + 2, 10, 10);
                        e.Graphics.FillRectangle(b, boxRect);
                    }
                }

                using (var b = new SolidBrush(textColor))
                {
                    var stringFormat = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    };
                }
                using (StringFormat sf = new StringFormat { LineAlignment = StringAlignment.Center })
                {
                    using (Brush brush = new SolidBrush(isChecked ? Color.FromArgb(255, 128, 191) : ForeColor))
                    {
                        e.Graphics.DrawString(Items[e.Index].ToString(), Font, brush, new Rectangle(e.Bounds.Height + 2, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height), sf);
                    }
                }
            }
        }
    }
}
