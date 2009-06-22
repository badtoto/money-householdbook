using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GMoney.control
{
    class MyLabel : Label
    {
        #region Members
        private Color ColorGlowStart;
        private Color ColorGlowEnd;

        private RectangleF rectGradient;
        private GraphicsPath pathGradient;
        private PathGradientBrush brushGradient;

        private LinearGradientBrush brushGlow;
        private RectangleF rectGlow;
        private PointF gradientCenterPoint;
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            #region Draw Background

            CreateGradientBrush();
            g.FillRectangle(brushGradient, base.ClientRectangle);
            CreateGlowBrush();
            g.FillRectangle(brushGlow, rectGlow);

            #endregion

            #region Draw String
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            using (SolidBrush sb = new SolidBrush(base.ForeColor))
                g.DrawString(base.Text, base.Font, sb, base.ClientRectangle, sf);
            #endregion
        }
        #endregion

        #region Methods
        private void CreateGradientBrush()
        {
            // Reset all objects
            if (pathGradient == null)
            {
                pathGradient = new GraphicsPath();
                //pathGradient.Dispose();
                //pathGradient = null;
            }
            if (brushGradient != null)
            {
                brushGradient.Dispose();
                brushGradient = null;
            }

            // Create or reset objects
            this.rectGradient.X = base.ClientRectangle.Left - base.ClientRectangle.Width / 2;
            this.rectGradient.Y = base.ClientRectangle.Top - base.ClientRectangle.Height / 4;
            this.rectGradient.Width = base.ClientRectangle.Width * 2;
            this.rectGradient.Height = base.ClientRectangle.Height * 2;

            this.gradientCenterPoint.X = base.ClientRectangle.Left + base.ClientRectangle.Width / 2;
            this.gradientCenterPoint.Y = base.ClientRectangle.Bottom;

            pathGradient.Reset();
            pathGradient.AddEllipse(rectGradient);

            brushGradient = new PathGradientBrush(pathGradient);
            brushGradient.CenterPoint = this.gradientCenterPoint;
            brushGradient.CenterColor = base.BackColor;
            brushGradient.SurroundColors = new Color[] { Color.FromArgb(80, 0, 0, 0) };
        }

        private void CreateGlowBrush()
        {
            // Caculate Glow density
            int nAlphaStart = 185,
                nAlphaEnd = 10;

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
        #endregion
    }
}
