using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Money.control
{
    class CaptionLabel : Label
    {
        private Color ColorGlowStart;
        private Color ColorGlowEnd;

        private RectangleF rectGradient;
        private GraphicsPath pathGradient;
        private PathGradientBrush brushGradient;
        private Color ColorBacklightEnd;
        private Color[] ColorGradientSurround;

        private LinearGradientBrush brushGlow;
        private RectangleF rectGlow;
        private PointF gradientCenterPoint;

        public CaptionLabel()
        {
            ColorBacklightEnd = Color.FromArgb(80, 0, 0, 0);
            ColorGradientSurround = new Color[] { ColorBacklightEnd };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // paint background
            //g.FillRectangle(brushFill, base.ClientRectangle);

            // Draw gradients
            CreateGradientBrush();
            g.FillRectangle(brushGradient, base.ClientRectangle);
            CreateGlowBrush();
            g.FillRectangle(brushGlow, rectGlow);


            // paint text
            using (SolidBrush sb = new SolidBrush(base.ForeColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(base.Text, base.Font, sb, base.ClientRectangle, sf);
            }
        }

        // In case chart uses a theme that needs a gradient
        private void CreateGradientBrush()
        {
            // Reset all objects
            if (pathGradient == null)
            {
                pathGradient = new GraphicsPath();
            }
            if (brushGradient != null)
            {
                brushGradient.Dispose();
                brushGradient = null;
            }

            // Create or reset objects
            this.rectGradient.X = base.ClientRectangle.Left - base.ClientRectangle.Width / 2;
            this.rectGradient.Y = base.ClientRectangle.Top - base.ClientRectangle.Height / 8;
            this.rectGradient.Width = base.ClientRectangle.Width * 2;
            this.rectGradient.Height = base.ClientRectangle.Height * 2;

            this.gradientCenterPoint.X = base.ClientRectangle.Left + base.ClientRectangle.Width / 2;
            this.gradientCenterPoint.Y = base.ClientRectangle.Bottom;

            pathGradient.Reset();
            pathGradient.AddEllipse(rectGradient);

            brushGradient = new PathGradientBrush(pathGradient);
            brushGradient.CenterPoint = this.gradientCenterPoint;
            brushGradient.CenterColor = base.BackColor;
            brushGradient.SurroundColors = ColorGradientSurround;
        }

        // In case chart uses Glass theme
        void CreateGlowBrush()
        {
            // Caculate Glow density
            int nAlphaStart = (int)(185 + 5 * base.ClientRectangle.Width / 24),
                nAlphaEnd = (int)(10 + 4 * base.ClientRectangle.Width / 24);

            if (nAlphaStart > 255) nAlphaStart = 255;
            else if (nAlphaStart < 0) nAlphaStart = 0;

            if (nAlphaEnd > 255) nAlphaEnd = 255;
            else if (nAlphaEnd < 0) nAlphaEnd = 0;

            ColorGlowStart = Color.FromArgb(nAlphaEnd, 255, 255, 255);
            ColorGlowEnd = Color.FromArgb(nAlphaStart, 255, 255, 255);

            if (brushGlow != null)
            {
                brushGlow.Dispose();
                brushGlow = null;
            }

            rectGlow = new RectangleF(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width, base.ClientRectangle.Height / 2);
            brushGlow = new LinearGradientBrush(rectGlow, ColorGlowEnd, ColorGlowStart, LinearGradientMode.Vertical);
        }

    }
}
