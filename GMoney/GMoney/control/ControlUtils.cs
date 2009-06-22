using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace GMoney.control
{
    class ControlUtils
    {
        #region Members
        static Color UnFocusedBorderColor = Color.FromArgb(127, 157, 185);
        static Color FocusedBorderColor = Color.FromArgb(78, 122, 171);

#if DEBUG1
        static Color DropDownButtonNormalColorFrom = Color.FromArgb(177, 200, 242);
        static Color DropDownButtonNormalColorTo = Color.FromArgb(210, 224, 253);

        static Color DropDownButtonHotColorFrom = Color.FromArgb(226, 234, 253);
        //public static Color DropDownButtonHotColorFrom = Color.FromArgb(0, 0, 0);
        static Color DropDownButtonHotColorTo = Color.FromArgb(201, 215, 251);

        static Color DropDownButtonPressedColorFrom = Color.FromArgb(191, 211, 237);
        static Color DropDownButtonPressedColorTo = Color.FromArgb(210, 224, 253);

        static Color DropDownButtonArrowColor = Color.FromArgb(49, 85, 153);
#endif
        #endregion

        #region ComboInfoHelper
        internal class ComboInfoHelper
        {
            [DllImport("user32")]
            private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

            #region RECT struct
            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                public int Left;
                public int Top;
                public int Right;
                public int Bottom;
            }
            #endregion

            #region ComboBoxInfo Struct
            [StructLayout(LayoutKind.Sequential)]
            private struct ComboBoxInfo
            {
                public int cbSize;
                public RECT rcItem;
                public RECT rcButton;
                public IntPtr stateButton;
                public IntPtr hwndCombo;
                public IntPtr hwndEdit;
                public IntPtr hwndList;
            }
            #endregion

            public static int GetComboDropDownWidth()
            {
                ComboBox cb = new ComboBox();
                int width = GetComboDropDownWidth(cb.Handle);
                cb.Dispose();
                return width;
            }
            public static int GetComboDropDownWidth(IntPtr handle)
            {
                ComboBoxInfo cbi = new ComboBoxInfo();
                cbi.cbSize = Marshal.SizeOf(cbi);
                GetComboBoxInfo(handle, ref cbi);
                int width = cbi.rcButton.Right - cbi.rcButton.Left;
                return width;
            }
        }
        #endregion

        #region Methods
        public static void DrawDropDown(Graphics g, Rectangle DropDownRect, ComboBoxState state)
        {
            if (ComboBoxRenderer.IsSupported)
            {
                ComboBoxRenderer.DrawDropDownButton(g, DropDownRect, state);
            }
            else
            {
                ButtonState bState = ButtonState.Normal;

                if (state == ComboBoxState.Pressed)
                {
                    bState = ButtonState.Pushed;
                }

                ControlPaint.DrawComboButton(g, DropDownRect, bState);
            }
#if DEBUG1
            #region draw background
            using (GraphicsPath pathGradient = new GraphicsPath())
            {
                RectangleF rectGradient = Rectangle.Empty;
                rectGradient.X = DropDownRect.Left - DropDownRect.Width / 2;
                rectGradient.Y = DropDownRect.Top - DropDownRect.Height / 3;
                rectGradient.Width = DropDownRect.Width * 2;
                rectGradient.Height = DropDownRect.Height + DropDownRect.Height / 2;

                pathGradient.AddEllipse(rectGradient);

                PointF radialCenterPoint = new PointF();
                radialCenterPoint.X = DropDownRect.Left + DropDownRect.Width / 2;
                radialCenterPoint.Y = DropDownRect.Top + DropDownRect.Height / 2;

                using (PathGradientBrush brushGradient = new PathGradientBrush(pathGradient))
                {
                    brushGradient.CenterPoint = radialCenterPoint;

                    switch (state)
                    {
                        case ComboBoxState.Hot:
                            brushGradient.CenterColor = ControlUtils.DropDownButtonHotColorFrom;
                            brushGradient.SurroundColors = new Color[] { ControlUtils.DropDownButtonHotColorTo };
                            break;
                        case ComboBoxState.Pressed:
                            brushGradient.CenterColor = ControlUtils.DropDownButtonPressedColorFrom;
                            brushGradient.SurroundColors = new Color[] { ControlUtils.DropDownButtonPressedColorTo };
                            break;
                        default:
                            brushGradient.CenterColor = ControlUtils.DropDownButtonNormalColorFrom;
                            brushGradient.SurroundColors = new Color[] { ControlUtils.DropDownButtonNormalColorTo };
                            break;
                    }

                    g.FillRectangle(brushGradient, DropDownRect);

                }
            }
            #endregion

            #region draw arrow
            using (SolidBrush sb = new SolidBrush(ControlUtils.DropDownButtonArrowColor))
            {
                int left_x = DropDownRect.X + DropDownRect.Width / 3;
                int bottom_x = DropDownRect.X + DropDownRect.Width / 2;
                int size = bottom_x - left_x + 1;
                int right_x = left_x + size * 2 - 1;
                int left_y = DropDownRect.Y + (DropDownRect.Height - size) / 2;
                int bottom_y = left_y + size;

                PointF pLeftTop = new PointF(left_x, left_y);
                PointF pRigntTop = new PointF(right_x, left_y);
                PointF pBottom = new PointF(bottom_x, bottom_y);

                PointF[] ps = { pLeftTop, pRigntTop, pBottom };

                g.FillPolygon(sb, ps);
            }
            #endregion
#endif
        }

        public static void DrawTextBoxBorder(Graphics g, Rectangle rect, bool focused)
        {
#if DEBUG1
            if (TextBoxRenderer.IsSupported)
                TextBoxRenderer.DrawTextBox(g, ClientRectangle, TextBoxState.Normal);
            else
                ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Sunken);
#endif
            if (!focused)
                ControlPaint.DrawBorder(g, rect, ControlUtils.UnFocusedBorderColor, ButtonBorderStyle.Solid);
            else
                ControlPaint.DrawBorder(g, rect, ControlUtils.FocusedBorderColor, ButtonBorderStyle.Solid);
        }
        #endregion
    }
}
