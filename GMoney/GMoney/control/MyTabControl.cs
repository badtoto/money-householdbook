using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Reflection;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Money.control
{
    [ToolboxBitmap(typeof(TabControl)), ToolboxItemFilter("Containers")]
    public class MyTabControl : TabControl
    {
        #region Fields
        private Bitmap BufferImage;
        private bool firstShown;
        private Color myBackColor;
        private bool myDoubleBufferTabpages;
        private TabDrawMode myDrawMode;
        private Color myHotColor;
        private int myHotTabID;
        private Color mySelectedTabColor;
        private Color myTabColor;
        private bool myUseBackColorBehindTabs;
        #endregion

        #region Methods
        public MyTabControl()
        {
            this.myBackColor = Color.Empty;
            this.myHotTabID = -1;
            this.myTabColor = SystemColors.Control;
            this.mySelectedTabColor = SystemColors.Control;
            this.myHotColor = SystemColors.HotTrack;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.Opaque, false);
            this.DoubleBuffered = false;
        }

        private Bitmap CreateTabBaseBitmap(int id)
        {
            Rectangle tabRect = base.GetTabRect(id);
            if ((base.Appearance == TabAppearance.Normal) && (id == base.SelectedIndex))
            {
                tabRect.Inflate(2, 2);
            }
            Bitmap image = new Bitmap(tabRect.Width, tabRect.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                GraphicsContainer container = graphics.BeginContainer();
                if (this.TabIsPartiallyTransparent(id) && ((this.BackColor.A < 0xff) || !this.UseBackColorBehindTabs))
                {
                    graphics.DrawImage(this.BufferImage, 0, 0, tabRect, GraphicsUnit.Pixel);
                }
                else
                {
                    graphics.Clear(this.BackColor);
                }
                graphics.EndContainer(container);
                graphics.Flush();
            }
            switch (base.Alignment)
            {
                case TabAlignment.Bottom:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;

                case TabAlignment.Left:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;

                case TabAlignment.Right:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            image = new Bitmap(image);
            using (Graphics graphics2 = Graphics.FromImage(image))
            {
                GraphicsContainer container2 = graphics2.BeginContainer();
                switch (base.Appearance)
                {
                    case TabAppearance.Buttons:
                        this.PaintButtonTab(graphics2, new Rectangle(Point.Empty, image.Size), id);
                        break;

                    case TabAppearance.FlatButtons:
                        this.PaintFlatTab(graphics2, new Rectangle(Point.Empty, image.Size), id);
                        break;

                    default:
                        this.Paint3DTab(graphics2, new Rectangle(Point.Empty, image.Size), id);
                        break;
                }
                graphics2.EndContainer(container2);
                graphics2.Flush();
            }
            if (base.Alignment == TabAlignment.Bottom)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            else if (base.Alignment == TabAlignment.Left)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if ((this.RightToLeftLayout && (this.RightToLeft == RightToLeft.Yes)) && (this.DrawMode == TabDrawMode.Normal))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            return image;
        }

        private static Color DarkenColor(Color colorIn, int percent)
        {
            if ((percent < 0) || (percent > 100))
            {
                throw new ArgumentOutOfRangeException("percent");
            }
            int alpha = 0xff;
            int red = colorIn.R - ((colorIn.R / 100) * percent);
            int green = colorIn.G - ((colorIn.G / 100) * percent);
            int blue = colorIn.B - ((colorIn.B / 100) * percent);
            return Color.FromArgb(alpha, red, green, blue);
        }

        private void DrawBorder(Graphics graphics)
        {
            if (base.Appearance == TabAppearance.Normal)
            {
                Rectangle displayRectangle = this.DisplayRectangle;
                displayRectangle.Inflate(4, 4);
                switch (base.Alignment)
                {
                    case TabAlignment.Top:
                        displayRectangle.Y++;
                        displayRectangle.Height--;
                        break;

                    case TabAlignment.Bottom:
                        displayRectangle.Height++;
                        break;

                    case TabAlignment.Left:
                        displayRectangle.X++;
                        displayRectangle.Width--;
                        break;

                    case TabAlignment.Right:
                        displayRectangle.Width++;
                        break;
                }
                if (this.VisualStylesEnabled)
                {
                    new VisualStyleRenderer(VisualStyleElement.Tab.Pane.Normal).DrawBackground(graphics, displayRectangle);
                }
                else
                {
                    Rectangle empty = Rectangle.Empty;
                    if (base.SelectedIndex != -1)
                    {
                        empty = base.GetTabRect(base.SelectedIndex);
                        empty.Inflate(2, 2);
                    }
                    graphics.SetClip(empty, CombineMode.Exclude);
                    Rectangle rect = this.DisplayRectangle;
                    rect.Inflate(4, 4);
                    if (base.Alignment <= TabAlignment.Bottom)
                    {
                        rect.Height--;
                        if (base.Alignment == TabAlignment.Top)
                        {
                            rect.Y++;
                        }
                    }
                    else
                    {
                        rect.Width--;
                        if (base.Alignment == TabAlignment.Left)
                        {
                            rect.X++;
                        }
                    }
                    Color color = (base.SelectedIndex == -1) ? this.BackColor : (base.SelectedTab.UseVisualStyleBackColor ? Control.DefaultBackColor : base.SelectedTab.BackColor);
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        graphics.FillRectangle(brush, rect);
                    }
                    Color color2 = LightenColor(color, 40);
                    Color color3 = LightenColor(color, 80);
                    Color color4 = DarkenColor(color, 0x19);
                    Color color5 = DarkenColor(color, 40);
                    using (Pen pen = new Pen(color3))
                    {
                        Point[] points = new Point[] { new Point(rect.Left, rect.Bottom - 2), new Point(rect.Left, rect.Top), new Point(rect.Right - 2, rect.Top) };
                        graphics.DrawLines(pen, points);
                        pen.Color = color2;
                        Point[] pointArray2 = new Point[] { new Point(rect.Left + 1, rect.Bottom - 3), new Point(rect.Left + 1, rect.Top + 1), new Point(rect.Right - 3, rect.Top + 1) };
                        graphics.DrawLines(pen, pointArray2);
                        pen.Color = color5;
                        Point[] pointArray3 = new Point[] { new Point(rect.Right - 1, rect.Top), new Point(rect.Right - 1, rect.Bottom - 1), new Point(rect.Left, rect.Bottom - 1) };
                        graphics.DrawLines(pen, pointArray3);
                        pen.Color = color4;
                        Point[] pointArray4 = new Point[] { new Point(rect.Right - 2, rect.Top + 1), new Point(rect.Right - 2, rect.Bottom - 2), new Point(rect.Left + 1, rect.Bottom - 2) };
                        graphics.DrawLines(pen, pointArray4);
                    }
                    graphics.ResetClip();
                }
            }
            graphics.Flush();
        }

        private void DrawBufferImage()
        {
            if (((base.Parent != null) && base.Created) && ((base.Width > 0) && (base.Height > 0)))
            {
                if ((this.BufferImage == null) || !this.BufferImage.Size.Equals(base.Size))
                {
                    this.BufferImage = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppPArgb);
                }
                using (Graphics graphics = Graphics.FromImage(this.BufferImage))
                {
                    GraphicsContainer container = graphics.BeginContainer();
                    Rectangle bounds = base.Bounds;
                    graphics.TranslateTransform((float)-base.Left, (float)-base.Top);
                    PaintEventArgs e = new PaintEventArgs(graphics, bounds);
                    base.InvokePaintBackground(base.Parent, e);
                    base.InvokePaint(base.Parent, e);
                    graphics.ResetTransform();
                    graphics.EndContainer(container);
                    graphics.Flush();
                }
                if ((this.RightToLeft == RightToLeft.Yes) && this.RightToLeftLayout)
                {
                    this.BufferImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
        }

        private static void GdiDrawStateText(IntPtr hdc, string text, Font font, Color textColor, Rectangle bounds, int flags)
        {
            IntPtr hgdiobj = font.ToHfont();
            IntPtr ptr2 = NativeMethods.SelectObject(hdc, hgdiobj);
            int nBkMode = NativeMethods.SetBkMode(hdc, 1);
            int crColor = NativeMethods.SetTextColor(hdc, ColorTranslator.ToWin32(textColor));
            NativeMethods.DrawState(hdc, IntPtr.Zero, IntPtr.Zero, Marshal.StringToHGlobalAnsi(text), new IntPtr(text.Length), bounds.Left, bounds.Top, bounds.Width, bounds.Height, flags);
            NativeMethods.SetTextColor(hdc, crColor);
            NativeMethods.SetBkMode(hdc, nBkMode);
            NativeMethods.SelectObject(hdc, ptr2);
            NativeMethods.DeleteObject(hgdiobj);
        }

        private TabPage GetFirstEnabledTab()
        {
            if (base.TabCount > 0)
            {
                TabPage page = null;
                for (int i = 0; i < base.TabCount; i++)
                {
                    page = base.TabPages[i];
                    if (page.Enabled)
                    {
                        return page;
                    }
                }
            }
            return null;
        }

        private TabPage GetLastEnabledTab()
        {
            if (base.TabCount > 0)
            {
                TabPage page = null;
                for (int i = base.TabCount - 1; i >= 0; i--)
                {
                    page = base.TabPages[i];
                    if (page.Enabled)
                    {
                        return page;
                    }
                }
            }
            return null;
        }

        private TabPage GetNextEnabledTab(bool forward, bool wrap)
        {
            if (forward)
            {
                for (int i = base.SelectedIndex + 1; i <= (base.TabCount - 1); i++)
                {
                    if (base.TabPages[i].Enabled)
                    {
                        return base.TabPages[i];
                    }
                }
                if (wrap)
                {
                    for (int j = 0; j <= base.SelectedIndex; j++)
                    {
                        if (base.TabPages[j].Enabled)
                        {
                            return base.TabPages[j];
                        }
                    }
                }
            }
            else
            {
                for (int k = base.SelectedIndex - 1; k >= 0; k--)
                {
                    if (base.TabPages[k].Enabled)
                    {
                        return base.TabPages[k];
                    }
                }
                if (wrap)
                {
                    for (int m = base.TabCount - 1; m > base.SelectedIndex; m--)
                    {
                        if (base.TabPages[m].Enabled)
                        {
                            return base.TabPages[m];
                        }
                    }
                }
            }
            return null;
        }

        private VisualStyleElement GetVisualStyleElement(int id)
        {
            bool flag = base.Alignment >= TabAlignment.Left;
            Rectangle tabRect = base.GetTabRect(id);
            bool flag2 = flag ? (tabRect.Top <= 3) : (tabRect.Left <= 3);
            bool flag3 = flag ? (tabRect.Bottom >= (base.Height - 4)) : (tabRect.Right >= (base.Width - 4));
            bool flag4 = flag ? ((base.Alignment == TabAlignment.Left) ? (tabRect.Left <= 3) : (tabRect.Right >= (base.Width - 3))) : (tabRect.Top <= 3);
            int part = 1;
            TabItemState normal = TabItemState.Normal;
            if (flag2)
            {
                part++;
            }
            if (flag3)
            {
                part += 2;
            }
            if (flag4)
            {
                part += 4;
            }
            if (id == base.SelectedIndex)
            {
                normal = TabItemState.Selected;
            }
            else
            {
                TabPage page = base.TabPages[id];
                if (page.Enabled)
                {
                    if (((base.TabPages[id] == this.TabUnderMouse) && base.HotTrack) && !base.DesignMode)
                    {
                        normal = TabItemState.Hot;
                    }
                }
                else
                {
                    normal = TabItemState.Disabled;
                }
            }
            return VisualStyleElement.CreateElement("TAB", part, (int)normal);
        }

        private bool HandleArrowKeys(Keys keys)
        {
            if (base.Appearance == TabAppearance.Normal)
            {
                Point point;
                Point point2;
                if (base.SelectedIndex == -1)
                {
                    base.SelectedTab = this.GetFirstEnabledTab();
                    return true;
                }
                Rectangle tabRect = base.GetTabRect(base.SelectedIndex);
                bool flag = base.Alignment >= TabAlignment.Left;
                TabPage nextEnabledTab = null;
                if ((this.RightToLeft == RightToLeft.Yes) && this.RightToLeftLayout)
                {
                    if (keys == Keys.Left)
                    {
                        keys = Keys.Right;
                    }
                    else if (keys == Keys.Right)
                    {
                        keys = Keys.Left;
                    }
                }
                switch (keys)
                {
                    case Keys.Left:
                        if (flag)
                        {
                            point = new Point(tabRect.Left - 3, tabRect.Top + (tabRect.Height / 2));
                            point2 = flag ? new Point(0, -3) : Point.Empty;
                            break;
                        }
                        nextEnabledTab = this.GetNextEnabledTab(false, false);
                        if (nextEnabledTab != null)
                        {
                            base.SelectedTab = nextEnabledTab;
                        }
                        return true;

                    case Keys.Up:
                        if (!flag)
                        {
                            point = new Point(tabRect.Left + (tabRect.Width / 2), tabRect.Top - 3);
                            point2 = flag ? Point.Empty : new Point(-3, 0);
                            break;
                        }
                        nextEnabledTab = this.GetNextEnabledTab(false, false);
                        if (nextEnabledTab != null)
                        {
                            base.SelectedTab = nextEnabledTab;
                        }
                        return true;

                    case Keys.Right:
                        if (flag)
                        {
                            point = new Point(tabRect.Right + 3, tabRect.Top + (tabRect.Height / 2));
                            point2 = flag ? new Point(0, -3) : Point.Empty;
                            break;
                        }
                        nextEnabledTab = this.GetNextEnabledTab(true, false);
                        if (nextEnabledTab != null)
                        {
                            base.SelectedTab = nextEnabledTab;
                        }
                        return true;

                    case Keys.Down:
                        if (!flag)
                        {
                            point = new Point(tabRect.Left + (tabRect.Width / 2), tabRect.Bottom + 3);
                            point2 = flag ? Point.Empty : new Point(-3, 0);
                            break;
                        }
                        nextEnabledTab = this.GetNextEnabledTab(true, false);
                        if (nextEnabledTab != null)
                        {
                            base.SelectedTab = nextEnabledTab;
                        }
                        return true;

                    default:
                        return false;
                }
                while (base.ClientRectangle.Contains(point) && !this.DisplayRectangle.Contains(point))
                {
                    nextEnabledTab = this.TabFromPoint(point);
                    if ((nextEnabledTab != null) && nextEnabledTab.Enabled)
                    {
                        base.SelectedTab = nextEnabledTab;
                        return true;
                    }
                    if (point2.IsEmpty)
                    {
                        return true;
                    }
                    point.Offset(point2);
                }
            }
            return true;
        }

        private static Color LightenColor(Color colorIn, int percent)
        {
            if ((percent < 0) || (percent > 100))
            {
                throw new ArgumentOutOfRangeException("percent");
            }
            int alpha = 0xff;
            int red = colorIn.R + ((int)(((255f - colorIn.R) / 100f) * percent));
            int green = colorIn.G + ((int)(((255f - colorIn.G) / 100f) * percent));
            int blue = colorIn.B + ((int)(((255f - colorIn.B) / 100f) * percent));
            return Color.FromArgb(alpha, red, green, blue);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            base.Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnFontChanged(EventArgs.Empty);
        }

        protected override void OnDeselected(TabControlEventArgs e)
        {
            base.OnDeselected(e);
            base.Invalidate();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.DrawMode == TabDrawMode.OwnerDrawFixed)
            {
                base.OnDrawItem(e);
            }
            else
            {
                TabPage page = base.TabPages[e.Index];
                Rectangle bounds = e.Bounds;
                bool flag = (this.RightToLeft == RightToLeft.Yes) && this.RightToLeftLayout;
                bool visualStylesEnabled = this.VisualStylesEnabled;
                bool flag3 = ((page.ImageIndex >= 0) || !string.IsNullOrEmpty(page.ImageKey)) && (base.ImageList != null);
                bool flag4 = e.Index == base.SelectedIndex;
                NativeMethods.RECT lpRect = new NativeMethods.RECT();
                IntPtr hdc = e.Graphics.GetHdc();
                IntPtr hgdiobj = this.Font.ToHfont();
                IntPtr ptr3 = NativeMethods.SelectObject(hdc, hgdiobj);
                NativeMethods.DrawText(hdc, page.Text, page.Text.Length, ref lpRect, NativeMethods.DRAWTEXTFLAGS.HIDEPREFIX | NativeMethods.DRAWTEXTFLAGS.CALCRECT);
                NativeMethods.SelectObject(hdc, ptr3);
                NativeMethods.DeleteObject(hgdiobj);
                e.Graphics.ReleaseHdc(hdc);
                Rectangle rectangle2 = new Rectangle(Point.Empty, lpRect.Size);
                Rectangle empty = Rectangle.Empty;
                Rectangle rectangle4 = rectangle2;
                if (flag3)
                {
                    empty.Size = base.ImageList.ImageSize;
                    rectangle4.Width += empty.Width + base.Padding.X;
                    rectangle4.Height = Math.Max(rectangle2.Height, empty.Height);
                }
                rectangle4.Offset((bounds.Width - rectangle4.Width) / 2, (bounds.Height - rectangle4.Height) / 2);
                empty.X = flag ? (rectangle4.Right - empty.Width) : rectangle4.Left;
                rectangle2.X = rectangle4.Right - rectangle2.Width;
                empty.Offset(0, (bounds.Height - empty.Height) / 2);
                rectangle2.Offset(0, (bounds.Height - lpRect.Size.Height) / 2);
                if (flag4)
                {
                    if (base.Appearance == TabAppearance.Normal)
                    {
                        empty.Offset(0, (base.Alignment == TabAlignment.Bottom) ? 2 : -2);
                        rectangle2.Offset(0, (base.Alignment == TabAlignment.Bottom) ? 2 : -2);
                    }
                    else
                    {
                        empty.Offset(1, 1);
                        rectangle2.Offset(flag ? -1 : 1, 1);
                    }
                }
                if (base.Alignment != TabAlignment.Bottom)
                {
                    empty.Offset(0, 1);
                    rectangle2.Offset(0, 1);
                }
                if (flag3)
                {
                    Bitmap bitmap;
                    if (page.ImageIndex == -1)
                    {
                        bitmap = (Bitmap)base.ImageList.Images[page.ImageKey];
                    }
                    else
                    {
                        bitmap = (Bitmap)base.ImageList.Images[page.ImageIndex];
                    }
                    if (page.Enabled)
                    {
                        e.Graphics.DrawImage(bitmap, empty);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(e.Graphics, bitmap, empty.X, empty.Y, Color.Empty);
                    }
                    bitmap.Dispose();
                }
                NativeMethods.DrawStateFlags pREFIXTEXT = NativeMethods.DrawStateFlags.PREFIXTEXT;
                Color textColor = (((e.Index == this.HotTabID) && base.HotTrack) && ((base.Appearance != TabAppearance.FlatButtons) && !visualStylesEnabled)) ? this.HotColor : this.ForeColor;
                if (!page.Enabled)
                {
                    if (visualStylesEnabled)
                    {
                        textColor = SystemColors.GrayText;
                    }
                    else
                    {
                        pREFIXTEXT |= NativeMethods.DrawStateFlags.DISABLED;
                    }
                }
                if (!this.ShowKeyboardCues)
                {
                    pREFIXTEXT |= NativeMethods.DrawStateFlags.HIDEPREFIX;
                }
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    pREFIXTEXT |= NativeMethods.DrawStateFlags.RTLREADING | NativeMethods.DrawStateFlags.RIGHT;
                }
                IntPtr ptr4 = e.Graphics.GetHdc();
                if (flag)
                {
                    NativeMethods.SetLayout(ptr4, 9);
                }
                GdiDrawStateText(ptr4, page.Text, this.Font, textColor, rectangle2, (int)pREFIXTEXT);
                e.Graphics.ReleaseHdc(ptr4);
                e.Graphics.Flush();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr wParam = this.Font.ToHfont();
            NativeMethods.SendMessage(base.Handle, 0x30, wParam, (IntPtr)(-1));
            NativeMethods.SendMessage(base.Handle, 0x1d, IntPtr.Zero, IntPtr.Zero);
            base.UpdateStyles();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            NativeMethods.SendMessage(base.Handle, 0x31a, IntPtr.Zero, IntPtr.Zero);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.Focused)
            {
                TabPage lastEnabledTab = null;
                switch (e.KeyCode)
                {
                    case Keys.Space:
                    case Keys.Return:
                        if (base.Appearance != TabAppearance.Normal)
                        {
                            TabPage tabWithFocus = this.TabWithFocus;
                            if (tabWithFocus != null)
                            {
                                e.Handled = !tabWithFocus.Enabled;
                                if (e.Handled)
                                {
                                    SystemSounds.Beep.Play();
                                }
                            }
                        }
                        break;

                    case Keys.End:
                        e.Handled = true;
                        lastEnabledTab = this.GetLastEnabledTab();
                        if (lastEnabledTab != null)
                        {
                            base.SelectedTab = lastEnabledTab;
                        }
                        base.Focus();
                        break;

                    case Keys.Home:
                        e.Handled = true;
                        lastEnabledTab = this.GetFirstEnabledTab();
                        if (lastEnabledTab != null)
                        {
                            base.SelectedTab = lastEnabledTab;
                        }
                        base.Focus();
                        break;

                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                        if (base.Appearance == TabAppearance.Normal)
                        {
                            e.Handled = this.HandleArrowKeys(e.KeyCode);
                        }
                        break;
                }
            }
            base.OnKeyDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!base.DesignMode)
            {
                this.HotTabID = -1;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!base.DesignMode)
            {
                TabPage tabUnderMouse = this.TabUnderMouse;
                this.HotTabID = (tabUnderMouse == null) ? -1 : base.TabPages.IndexOf(tabUnderMouse);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Flush();
            IntPtr hdc = pevent.Graphics.GetHdc();
            using (Bitmap bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppPArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    if (this.BufferImage != null)
                    {
                        graphics.DrawImage(this.BufferImage, Point.Empty);
                    }
                    Rectangle clientRectangle = base.ClientRectangle;
                    if (!this.myUseBackColorBehindTabs)
                    {
                        clientRectangle = this.DisplayRectangle;
                    }
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        graphics.FillRectangle(brush, clientRectangle);
                    }
                    this.DrawBorder(graphics);
                    for (int i = 0; i < base.TabCount; i++)
                    {
                        if (i != base.SelectedIndex)
                        {
                            this.PaintTab(graphics, i);
                        }
                    }
                    if (base.SelectedIndex != -1)
                    {
                        this.PaintTab(graphics, base.SelectedIndex);
                    }
                    IntPtr hbitmap = bitmap.GetHbitmap();
                    IntPtr ptr3 = graphics.GetHdc();
                    IntPtr ptr4 = NativeMethods.CreateCompatibleDC(ptr3);
                    IntPtr ptr5 = NativeMethods.SelectObject(ptr4, hbitmap);
                    NativeMethods.BitBlt(hdc, 0, 0, base.Width, base.Height, ptr4, 0, 0, 0xcc0020);
                    NativeMethods.SelectObject(ptr5, hbitmap);
                    NativeMethods.DeleteDC(ptr4);
                    graphics.ReleaseHdc(ptr3);
                    NativeMethods.DeleteObject(hbitmap);
                }
            }
            pevent.Graphics.ReleaseHdc(hdc);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.DrawBufferImage();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            if (base.SelectedIndex != -1)
            {
                this.SetDoubleBuffered(base.SelectedTab);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (base.Visible && !this.firstShown)
            {
                this.firstShown = true;
                if (base.SelectedIndex != -1)
                {
                    base.SelectedTab = this.GetFirstEnabledTab();
                }
                if (Application.RenderWithVisualStyles && (base.Appearance == TabAppearance.Normal))
                {
                    NativeMethods.SetWindowTheme(base.Handle, "", "");
                    NativeMethods.SetWindowTheme(base.Handle, null, null);
                }
            }
        }

        private void Paint3DTab(Graphics g, Rectangle r, int id)
        {
            if (id == -1)
            {
                return;
            }
            if (this.VisualStylesEnabled)
            {
                new VisualStyleRenderer(this.GetVisualStyleElement(id)).DrawBackground(g, r);
            }
            else
            {
                Color tabColor = this.TabColor;
                if (base.SelectedIndex == id)
                {
                    tabColor = this.SelectedTabColor;
                }
                Color color2 = LightenColor(tabColor, 40);
                Color color = LightenColor(tabColor, 80);
                Color color4 = DarkenColor(tabColor, 0x19);
                Color color5 = DarkenColor(tabColor, 40);
                if (tabColor.A != 0)
                {
                    Rectangle rect = r;
                    rect.Inflate(-2, -2);
                    rect.Height += 2;
                    using (SolidBrush brush = new SolidBrush(tabColor))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                using (Pen pen = new Pen(color))
                {
                    switch (base.Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Left:
                            {
                                Point[] points = new Point[] { new Point(r.Left, r.Bottom), new Point(r.Left, r.Top + 2), new Point(r.Left + 2, r.Top), new Point(r.Right - 3, r.Top) };
                                g.DrawLines(pen, points);
                                pen.Color = color2;
                                Point[] pointArray2 = new Point[] { new Point(r.Left + 1, r.Bottom), new Point(r.Left + 1, r.Top + 2), new Point(r.Left + 2, r.Top + 1), new Point(r.Right - 3, r.Top + 1) };
                                g.DrawLines(pen, pointArray2);
                                pen.Color = color4;
                                Point[] pointArray3 = new Point[] { new Point(r.Right - 2, r.Top + 1), new Point(r.Right - 2, r.Bottom) };
                                g.DrawLines(pen, pointArray3);
                                pen.Color = color5;
                                Point[] pointArray4 = new Point[] { new Point(r.Right - 1, r.Top + 2), new Point(r.Right - 1, r.Bottom) };
                                g.DrawLines(pen, pointArray4);
                                goto Label_04F1;
                            }
                        case TabAlignment.Bottom:
                        case TabAlignment.Right:
                            {
                                Point[] pointArray5 = new Point[] { new Point(r.Left, r.Bottom), new Point(r.Left, r.Top + 2) };
                                g.DrawLines(pen, pointArray5);
                                pen.Color = color2;
                                Point[] pointArray6 = new Point[] { new Point(r.Left + 1, r.Bottom), new Point(r.Left + 1, r.Top + 1) };
                                g.DrawLines(pen, pointArray6);
                                pen.Color = color5;
                                Point[] pointArray7 = new Point[] { new Point(r.Left + 2, r.Top), new Point(r.Right - 3, r.Top), new Point(r.Right - 1, r.Top + 2), new Point(r.Right - 1, r.Bottom) };
                                g.DrawLines(pen, pointArray7);
                                pen.Color = color4;
                                Point[] pointArray8 = new Point[] { new Point(r.Left + 2, r.Top + 1), new Point(r.Right - 3, r.Top + 1), new Point(r.Right - 2, r.Top + 2), new Point(r.Right - 2, r.Bottom) };
                                g.DrawLines(pen, pointArray8);
                                goto Label_04F1;
                            }
                    }
                }
            }
        Label_04F1:
            g.Flush();
        }

        private void PaintButtonTab(Graphics g, Rectangle r, int id)
        {
            Color tabColor = this.TabColor;
            TabPage page = base.TabPages[id];
            if (base.SelectedIndex == id)
            {
                tabColor = this.SelectedTabColor;
            }
            bool flag = (id == base.SelectedIndex) || (page == this.TabWithFocus);
            Color color2 = flag ? DarkenColor(tabColor, 0x19) : LightenColor(tabColor, 40);
            Color color = flag ? DarkenColor(tabColor, 40) : LightenColor(tabColor, 80);
            Color color4 = flag ? LightenColor(tabColor, 40) : DarkenColor(tabColor, 0x19);
            Color color5 = flag ? LightenColor(tabColor, 80) : DarkenColor(tabColor, 40);
            using (SolidBrush brush = new SolidBrush(tabColor))
            {
                g.FillRectangle(brush, r);
            }
            using (Pen pen = new Pen(color))
            {
                switch (base.Alignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Left:
                        {
                            Point[] points = new Point[] { new Point(r.Left, r.Bottom - 1), new Point(r.Left, r.Top), new Point(r.Right - 1, r.Top) };
                            g.DrawLines(pen, points);
                            pen.Color = color5;
                            Point[] pointArray2 = new Point[] { new Point(r.Right - 1, r.Top), new Point(r.Right - 1, r.Bottom - 1), new Point(r.Left, r.Bottom - 1) };
                            g.DrawLines(pen, pointArray2);
                            pen.Color = color2;
                            Point[] pointArray3 = new Point[] { new Point(r.Left + 1, r.Bottom - 2), new Point(r.Left + 1, r.Top + 1), new Point(r.Right - 2, r.Top + 1) };
                            g.DrawLines(pen, pointArray3);
                            pen.Color = color4;
                            Point[] pointArray4 = new Point[] { new Point(r.Right - 2, r.Top + 1), new Point(r.Right - 2, r.Bottom - 2), new Point(r.Left + 1, r.Bottom - 2) };
                            g.DrawLines(pen, pointArray4);
                            goto Label_0500;
                        }
                    case TabAlignment.Bottom:
                    case TabAlignment.Right:
                        {
                            Point[] pointArray5 = new Point[] { new Point(r.Left, r.Top), new Point(r.Left, r.Bottom - 1), new Point(r.Right - 1, r.Bottom - 1) };
                            g.DrawLines(pen, pointArray5);
                            pen.Color = color5;
                            Point[] pointArray6 = new Point[] { new Point(r.Right - 1, r.Bottom - 1), new Point(r.Right - 1, r.Top), new Point(r.Left, r.Top) };
                            g.DrawLines(pen, pointArray6);
                            pen.Color = color2;
                            Point[] pointArray7 = new Point[] { new Point(r.Left + 1, r.Top + 1), new Point(r.Left + 1, r.Bottom - 2), new Point(r.Right - 2, r.Bottom - 2) };
                            g.DrawLines(pen, pointArray7);
                            pen.Color = color4;
                            Point[] pointArray8 = new Point[] { new Point(r.Right - 2, r.Bottom - 2), new Point(r.Right - 2, r.Top + 1), new Point(r.Left + 1, r.Top + 1) };
                            g.DrawLines(pen, pointArray8);
                            goto Label_0500;
                        }
                }
            }
        Label_0500:
            g.Flush();
        }

        private void PaintFlatTab(Graphics g, Rectangle r, int id)
        {
            Color tabColor = this.TabColor;
            TabPage page = base.TabPages[id];
            if (base.SelectedIndex == id)
            {
                tabColor = this.SelectedTabColor;
            }
            bool flag = id == base.SelectedIndex;
            Color color2 = flag ? DarkenColor(tabColor, 0x19) : LightenColor(tabColor, 40);
            Color color = flag ? DarkenColor(tabColor, 40) : LightenColor(tabColor, 80);
            Color color4 = flag ? LightenColor(tabColor, 40) : DarkenColor(tabColor, 0x19);
            Color color5 = flag ? LightenColor(tabColor, 80) : DarkenColor(tabColor, 0x19);
            using (SolidBrush brush = new SolidBrush(tabColor))
            {
                g.FillRectangle(brush, r);
            }
            if (((page == this.TabWithFocus) || (id == base.SelectedIndex)) || ((id == this.HotTabID) && base.HotTrack))
            {
                using (Pen pen = new Pen(color))
                {
                    switch (base.Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Left:
                            {
                                Point[] points = new Point[] { new Point(r.Left, r.Bottom - 1), new Point(r.Left, r.Top), new Point(r.Right - 1, r.Top) };
                                g.DrawLines(pen, points);
                                pen.Color = color5;
                                Point[] pointArray2 = new Point[] { new Point(r.Right - 1, r.Top), new Point(r.Right - 1, r.Bottom - 1), new Point(r.Left, r.Bottom - 1) };
                                g.DrawLines(pen, pointArray2);
                                if (flag)
                                {
                                    pen.Color = color2;
                                    Point[] pointArray3 = new Point[] { new Point(r.Left + 1, r.Bottom - 2), new Point(r.Left + 1, r.Top + 1), new Point(r.Right - 2, r.Top + 1) };
                                    g.DrawLines(pen, pointArray3);
                                    pen.Color = color4;
                                    Point[] pointArray4 = new Point[] { new Point(r.Right - 2, r.Top + 1), new Point(r.Right - 2, r.Bottom - 2), new Point(r.Left + 1, r.Bottom - 2) };
                                    g.DrawLines(pen, pointArray4);
                                }
                                goto Label_0529;
                            }
                        case TabAlignment.Bottom:
                        case TabAlignment.Right:
                            {
                                Point[] pointArray5 = new Point[] { new Point(r.Left, r.Top), new Point(r.Left, r.Bottom - 1), new Point(r.Right - 1, r.Bottom - 1) };
                                g.DrawLines(pen, pointArray5);
                                pen.Color = color5;
                                Point[] pointArray6 = new Point[] { new Point(r.Right - 1, r.Bottom - 1), new Point(r.Right - 1, r.Top), new Point(r.Left, r.Top) };
                                g.DrawLines(pen, pointArray6);
                                if (flag)
                                {
                                    pen.Color = color2;
                                    Point[] pointArray7 = new Point[] { new Point(r.Left + 1, r.Top + 1), new Point(r.Left + 1, r.Bottom - 2), new Point(r.Right - 2, r.Bottom - 2) };
                                    g.DrawLines(pen, pointArray7);
                                    pen.Color = color4;
                                    Point[] pointArray8 = new Point[] { new Point(r.Right - 2, r.Bottom - 2), new Point(r.Right - 2, r.Top + 1), new Point(r.Left + 1, r.Top + 1) };
                                    g.DrawLines(pen, pointArray8);
                                }
                                goto Label_0529;
                            }
                    }
                }
            }
        Label_0529:
            g.Flush();
        }

        private void PaintTab(Graphics graphics, int id)
        {
            if (id != -1)
            {
                TabAlignment alignment = base.Alignment;
                Bitmap image = this.CreateTabBaseBitmap(id);
                Rectangle rect = new Rectangle(Point.Empty, image.Size);
                using (Graphics graphics2 = Graphics.FromImage(image))
                {
                    IntPtr hdc = graphics2.GetHdc();
                    IntPtr hbitmap = image.GetHbitmap();
                    IntPtr ptr3 = NativeMethods.CreateCompatibleDC(hdc);
                    IntPtr ptr4 = NativeMethods.SelectObject(ptr3, hbitmap);
                    if (this.DrawMode == TabDrawMode.Normal)
                    {
                        using (Graphics graphics3 = Graphics.FromHdc(ptr3))
                        {
                            DrawItemEventArgs e = new DrawItemEventArgs(graphics3, this.Font, rect, id, DrawItemState.Default);
                            this.OnDrawItem(e);
                        }
                    }
                    NativeMethods.BitBlt(hdc, 0, 0, rect.Width, rect.Height, ptr3, 0, 0, 0xcc0020);
                    NativeMethods.SelectObject(ptr4, hbitmap);
                    NativeMethods.DeleteDC(ptr3);
                    NativeMethods.DeleteObject(hbitmap);
                    graphics2.ReleaseHdc(hdc);
                    graphics2.Flush();
                }
                switch (base.Alignment)
                {
                    case TabAlignment.Left:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;

                    case TabAlignment.Right:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
                Rectangle tabRect = base.GetTabRect(id);
                if ((id == base.SelectedIndex) && (base.Appearance == TabAppearance.Normal))
                {
                    tabRect.Inflate(2, 2);
                }
                if (this.DrawMode == TabDrawMode.OwnerDrawFixed)
                {
                    bool flag = (this.RightToLeft == RightToLeft.Yes) && this.RightToLeftLayout;
                    if (flag)
                    {
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    IntPtr hgdiobj = image.GetHbitmap();
                    using (Graphics graphics4 = Graphics.FromImage(image))
                    {
                        IntPtr ptr6 = graphics4.GetHdc();
                        IntPtr ptr7 = NativeMethods.CreateCompatibleDC(ptr6);
                        IntPtr ptr8 = NativeMethods.SelectObject(ptr7, hgdiobj);
                        Graphics graphics5 = Graphics.FromHdc(ptr7);
                        graphics5.TranslateTransform((float)-tabRect.Left, (float)-tabRect.Top);
                        DrawItemEventArgs args2 = new DrawItemEventArgs(graphics5, this.Font, tabRect, id, DrawItemState.Default);
                        this.OnDrawItem(args2);
                        NativeMethods.BitBlt(ptr6, 0, 0, tabRect.Width, tabRect.Height, ptr7, 0, 0, 0xcc0020);
                        graphics5.ResetTransform();
                        NativeMethods.SelectObject(ptr8, hgdiobj);
                        graphics5.Dispose();
                        NativeMethods.DeleteObject(ptr7);
                        graphics4.ReleaseHdc(ptr6);
                    }
                    NativeMethods.DeleteObject(hgdiobj);
                    if (flag)
                    {
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                }
                graphics.DrawImage(image, tabRect);
                image.Dispose();
                if (base.Appearance == TabAppearance.FlatButtons)
                {
                    using (Pen pen = new Pen(DarkenColor(this.BackColor, 0x19)))
                    {
                        graphics.DrawLine(pen, tabRect.Right + 4, tabRect.Top, tabRect.Right + 4, tabRect.Bottom);
                        pen.Color = LightenColor(this.BackColor, 80);
                        graphics.DrawLine(pen, tabRect.Right + 5, tabRect.Top, tabRect.Right + 5, tabRect.Bottom);
                    }
                }
                tabRect.Inflate(-2, -2);
                if ((this.Focused && this.ShowFocusCues) && (id == base.SelectedIndex))
                {
                    ControlPaint.DrawFocusRectangle(graphics, tabRect);
                }
                graphics.Flush();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool flag = base.ProcessCmdKey(ref msg, keyData);
            if (flag)
            {
                return flag;
            }
            if ((keyData == (Keys.Control | Keys.Tab)) || (keyData == (Keys.Control | Keys.Shift | Keys.Tab)))
            {
                base.SelectedTab = this.GetNextEnabledTab((keyData & Keys.Shift) == Keys.None, true);
                base.Focus();
                return true;
            }
            if ((keyData != (Keys.Control | Keys.Prior)) && (keyData != (Keys.Control | Keys.Next)))
            {
                return flag;
            }
            base.SelectedTab = this.GetNextEnabledTab((keyData & Keys.Next) == Keys.Next, true);
            base.Focus();
            return true;
        }

        protected override bool ProcessMnemonic(char charCode)
        {
            foreach (TabPage page in base.TabPages)
            {
                if (page.Enabled && Control.IsMnemonic(charCode, page.Text))
                {
                    base.SelectedTab = page;
                    return true;
                }
            }
            return base.ProcessMnemonic(charCode);
        }

        public new void ResetBackColor()
        {
            this.BackColor = Color.Empty;
        }

        private void SetDoubleBuffered(TabPage page)
        {
            PropertyInfo property = page.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                property.SetValue(page, this.myDoubleBufferTabpages, null);
            }
        }

        private bool ShouldSerializeBackColor()
        {
            return !this.myBackColor.Equals(Color.Empty);
        }

        private TabPage TabFromPoint(Point point)
        {
            NativeMethods.TCHITTESTINFO lParam = new NativeMethods.TCHITTESTINFO(point.X, point.Y);
            int num = NativeMethods.SendMessage(base.Handle, 0x130d, IntPtr.Zero, ref lParam).ToInt32();
            if ((num >= 0) && (num < base.TabCount))
            {
                return base.TabPages[num];
            }
            return null;
        }

        private bool TabIsPartiallyTransparent(int id)
        {
            if (this.VisualStylesEnabled)
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(this.GetVisualStyleElement(id));
                return renderer.IsBackgroundPartiallyTransparent();
            }
            if (base.Appearance == TabAppearance.Normal)
            {
                return true;
            }
            if (id == base.SelectedIndex)
            {
                return (this.SelectedTabColor.A < 0xff);
            }
            return (this.TabColor.A < 0xff);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x201:
                    {
                        TabPage tabUnderMouse = this.TabUnderMouse;
                        if ((tabUnderMouse != null) && !tabUnderMouse.Enabled)
                        {
                            m.Msg = 0;
                        }
                        break;
                    }
                case 0x31a:
                    base.Invalidate(true);
                    break;

                case 20:
                    m.Msg = 0;
                    m.Result = IntPtr.Zero;
                    break;

                case 0x114:
                    base.Invalidate();
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Properties
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override Color BackColor
        {
            get
            {
                if (this.myBackColor.Equals(Color.Empty))
                {
                    return base.BackColor;
                }
                return this.myBackColor;
            }
            set
            {
                this.myBackColor = value;
                this.OnBackColorChanged(EventArgs.Empty);
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                int num;
                int height;
                int x = 0;
                if (base.Appearance == TabAppearance.Normal)
                {
                    x = 4;
                }
                if (base.Alignment <= TabAlignment.Bottom)
                {
                    height = base.ItemSize.Height;
                }
                else
                {
                    height = base.ItemSize.Width;
                }
                if (base.Appearance == TabAppearance.Normal)
                {
                    num = 5 + (height * base.RowCount);
                }
                else
                {
                    num = (3 + height) * base.RowCount;
                }
                switch (base.Alignment)
                {
                    case TabAlignment.Bottom:
                        return new Rectangle(x, x, base.Width - (x * 2), (base.Height - num) - x);

                    case TabAlignment.Left:
                        return new Rectangle(num, x, (base.Width - num) - x, base.Height - (x * 2));

                    case TabAlignment.Right:
                        return new Rectangle(x, x, (base.Width - num) - x, base.Height - (x * 2));
                }
                return new Rectangle(x, num, base.Width - (x * 2), (base.Height - num) - x);
            }
        }

        [Description("Sets DoubleBuffer on TabPages to help with flicker. Should only be set if using transparency."), Category("Behavior"), DefaultValue(false)]
        public bool DoubleBufferTabPages
        {
            get
            {
                return this.myDoubleBufferTabpages;
            }
            set
            {
                if (this.myDoubleBufferTabpages != value)
                {
                    this.myDoubleBufferTabpages = value;
                    if (base.SelectedIndex != -1)
                    {
                        this.SetDoubleBuffered(base.SelectedTab);
                    }
                }
            }
        }

        [DefaultValue(typeof(TabDrawMode), "Normal")]
        public new TabDrawMode DrawMode
        {
            get
            {
                return this.myDrawMode;
            }
            set
            {
                this.myDrawMode = value;
                base.DrawMode = TabDrawMode.Normal;
                base.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [Category("Appearance"), Description("The color of text on a tab which the mouse is over. Only applies if HotTrack is true."), DefaultValue(typeof(Color), "HotTrack")]
        public Color HotColor
        {
            get
            {
                return this.myHotColor;
            }
            set
            {
                this.myHotColor = value;
                base.Invalidate();
            }
        }

        private int HotTabID
        {
            get
            {
                return this.myHotTabID;
            }
            set
            {
                if (this.myHotTabID != value)
                {
                    this.myHotTabID = value;
                    base.Invalidate();
                }
            }
        }

        [Description("The color of the selected tab without Visual Style."), Category("Appearance"), DefaultValue(typeof(Color), "Control")]
        public Color SelectedTabColor
        {
            get
            {
                return this.mySelectedTabColor;
            }
            set
            {
                this.mySelectedTabColor = value;
                base.Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "Control"), Description("The color of unselected tabs without Visual Style.")]
        public Color TabColor
        {
            get
            {
                return this.myTabColor;
            }
            set
            {
                this.myTabColor = value;
                base.Invalidate();
            }
        }

        private TabPage TabUnderMouse
        {
            get
            {
                Point point = base.PointToClient(Control.MousePosition);
                return this.TabFromPoint(point);
            }
        }

        private TabPage TabWithFocus
        {
            get
            {
                if (base.Appearance == TabAppearance.Normal)
                {
                    return base.SelectedTab;
                }
                int num = NativeMethods.SendMessage(base.Handle, 0x132f, IntPtr.Zero, IntPtr.Zero).ToInt32();
                if (num != -1)
                {
                    return base.TabPages[num];
                }
                return null;
            }
        }

        [Description("Gets/sets whether or not the BackColor should be painted behind tabs."), Category("Appearance"), DefaultValue(false)]
        public bool UseBackColorBehindTabs
        {
            get
            {
                return this.myUseBackColorBehindTabs;
            }
            set
            {
                this.myUseBackColorBehindTabs = value;
                base.Invalidate();
            }
        }

        private bool VisualStylesEnabled
        {
            get
            {
                if ((base.Appearance != TabAppearance.Normal) || !Application.RenderWithVisualStyles)
                {
                    return false;
                }
                if (base.DesignMode)
                {
                    return VisualStyleRenderer.IsSupported;
                }
                return (NativeMethods.GetWindowTheme(base.Handle) != IntPtr.Zero);
            }
        }
        #endregion
    }

    internal sealed class NativeMethods
    {
        #region Fields
        public const int CCM_FIRST = 0x2000;
        public const int LAYOUT_BITMAPORIENTATIONPRESERVED = 8;
        public const int LAYOUT_RTL = 1;
        public const int NM_FIRST = 0;
        public const int OPAQUE = 2;
        public const int SRCCOPY = 0xcc0020;
        public const int TCM_FIRST = 0x1300;
        public const int TCN_FIRST = -550;
        public const int TRANSPARENT = 1;
        #endregion

        #region Methods
        private NativeMethods()
        {
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [return: MarshalAs(UnmanagedType.I4)]
        [DllImport("user32.dll")]
        public static extern int DrawState(IntPtr hdc, IntPtr hbr, IntPtr lpOutputFunc, IntPtr lData, IntPtr wData, int x, int y, int cx, int cy, int fuFlags);
        [return: MarshalAs(UnmanagedType.I4)]
        [DllImport("user32.dll")]
        public static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref RECT lpRect, DRAWTEXTFLAGS uFormat);
        [DllImport("uxtheme")]
        public static extern IntPtr GetWindowTheme(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);
        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hDC, int nBkMode);
        [DllImport("gdi32.dll")]
        public static extern int SetLayout(IntPtr hdc, int dwLayout);
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hdc, int crColor);
        [DllImport("uxtheme")]
        public static extern int SetWindowTheme(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszSubAppName, [MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);
        #endregion

        #region Nested Types
        public enum ClassPartsTab
        {
            BODY = 10,
            PANE = 9,
            TABITEM = 1,
            TABITEMBOTHEDGE = 4,
            TABITEMLEFTEDGE = 2,
            TABITEMRIGHTEDGE = 3,
            TOPTABITEM = 5,
            TOPTABITEMBOTHEDGE = 8,
            TOPTABITEMLEFTEDGE = 6,
            TOPTABITEMRIGHTEDGE = 7
        }

        [Flags]
        public enum DrawStateFlags
        {
            BITMAP = 4,
            COMPLEX = 0,
            DISABLED = 0x20,
            HIDEPREFIX = 0x200,
            ICON = 3,
            PREFIXTEXT = 2,
            RIGHT = 0x8000,
            RTLREADING = 0x20000,
            TEXT = 1
        }

        [Flags]
        public enum DRAWTEXTFLAGS
        {
            BOTTOM = 8,
            CALCRECT = 0x400,
            CENTER = 1,
            EXPANDTABS = 0x40,
            EXTERNALLEADING = 0x200,
            HIDEPREFIX = 0x100000,
            INTERNAL = 0x1000,
            LEFT = 0,
            NOCLIP = 0x100,
            NOPREFIX = 0x800,
            RIGHT = 2,
            SINGLELINE = 0x20,
            TABSTOP = 0x80,
            TOP = 0,
            VCENTER = 4,
            WORDBREAK = 0x10
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hWnd;
            public int idFrom;
            public int code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NMOBJECTNOTIFY
        {
            public NativeMethods.NMHDR hdr;
            public int iItem;
            public IntPtr piid;
            public IntPtr pObject;
            public IntPtr hResult;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Height
            {
                get
                {
                    return (this.Bottom - this.Top);
                }
            }
            public int Width
            {
                get
                {
                    return (this.Right - this.Left);
                }
            }
            public Size Size
            {
                get
                {
                    return new Size(this.Width, this.Height);
                }
            }
        }

        public enum TC_NOTIFYMESSAGECODE
        {
            CLICK = -2,
            FOCUSCHANGE = -554,
            GETOBJECT = -553,
            KEYDOWN = -550,
            RCLICK = -5,
            RELEASEDCAPTURE = -16,
            SELCHANGE = -551,
            SELCHANGING = -552
        }

        [Flags]
        public enum TCHITTESTFLAGS
        {
            NOWHERE = 1,
            ONITEM = 6,
            ONITEMICON = 2,
            ONITEMLABEL = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TCHITTESTINFO
        {
            public Point pt;
            public NativeMethods.TCHITTESTFLAGS flags;
            public TCHITTESTINFO(int x, int y)
            {
                this.pt = new Point(x, y);
                this.flags = NativeMethods.TCHITTESTFLAGS.ONITEM;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TCKEYDOWN
        {
            public NativeMethods.NMHDR hdr;
            public short wVKey;
            public int flags;
        }

        public enum TCMessage
        {
            ADJUSTRECT = 0x1328,
            DELETEALLITEMS = 0x1309,
            DELETEITEM = 0x1308,
            DESELECTALL = 0x1332,
            GETCURFOCUS = 0x132f,
            GETCURSEL = 0x130b,
            GETEXTENDEDSTYLE = 0x1335,
            GETIMAGELIST = 0x1302,
            GETITEMA = 0x1305,
            GETITEMCOUNT = 0x1304,
            GETITEMRECT = 0x130a,
            GETITEMW = 0x133c,
            GETROWCOUNT = 0x132c,
            GETTOOLTIPS = 0x132d,
            HIGHLIGHTITEM = 0x1333,
            HITTEST = 0x130d,
            INSERTITEMA = 0x1307,
            INSERTITEMW = 0x133e,
            REMOVEIMAGE = 0x132a,
            SETCURFOCUS = 0x1330,
            SETCURSEL = 0x130c,
            SETEXTENDEDSTYLE = 0x1334,
            SETIMAGELIST = 0x1303,
            SETITEMA = 0x1306,
            SETITEMEXTRA = 0x130e,
            SETITEMSIZE = 0x1329,
            SETITEMW = 0x133d,
            SETMINTABWIDTH = 0x1331,
            SETPADDING = 0x132b,
            SETTOOLTIPS = 0x132e
        }

        public enum WinMessage
        {
            ACTIVATE = 6,
            ACTIVATEAPP = 0x1c,
            AFXFIRST = 0x360,
            AFXLAST = 0x37f,
            APP = 0x8000,
            APPCOMMAND = 0x319,
            ASKCBFORMATNAME = 780,
            CANCELJOURNAL = 0x4b,
            CANCELMODE = 0x1f,
            CAPTURECHANGED = 0x215,
            CHANGECBCHAIN = 0x30d,
            CHANGEUISTATE = 0x127,
            CHAR = 0x102,
            CHARTOITEM = 0x2f,
            CHILDACTIVATE = 0x22,
            CLEAR = 0x303,
            CLOSE = 0x10,
            COMMAND = 0x111,
            COMMNOTIFY = 0x44,
            COMPACTING = 0x41,
            COMPAREITEM = 0x39,
            CONTEXTMENU = 0x7b,
            COPY = 0x301,
            COPYDATA = 0x4a,
            CREATE = 1,
            CTLCOLORBTN = 0x135,
            CTLCOLORDLG = 310,
            CTLCOLOREDIT = 0x133,
            CTLCOLORLISTBOX = 0x134,
            CTLCOLORMSGBOX = 0x132,
            CTLCOLORSCROLLBAR = 0x137,
            CTLCOLORSTATIC = 0x138,
            CUT = 0x300,
            DEADCHAR = 0x103,
            DELETEITEM = 0x2d,
            DESTROY = 2,
            DESTROYCLIPBOARD = 0x307,
            DEVICECHANGE = 0x219,
            DEVMODECHANGE = 0x1b,
            DISPLAYCHANGE = 0x7e,
            DRAWCLIPBOARD = 0x308,
            DRAWITEM = 0x2b,
            DROPFILES = 0x233,
            ENABLE = 10,
            ENDSESSION = 0x16,
            ENTERIDLE = 0x121,
            ENTERMENULOOP = 0x211,
            ENTERSIZEMOVE = 0x231,
            ERASEBKGND = 20,
            EXITMENULOOP = 530,
            EXITSIZEMOVE = 0x232,
            FONTCHANGE = 0x1d,
            GETDLGCODE = 0x87,
            GETFONT = 0x31,
            GETHOTKEY = 0x33,
            GETICON = 0x7f,
            GETMINMAXINFO = 0x24,
            GETOBJECT = 0x3d,
            GETTEXT = 13,
            GETTEXTLENGTH = 14,
            HANDHELDFIRST = 0x358,
            HANDHELDLAST = 0x35f,
            HELP = 0x53,
            HOTKEY = 0x312,
            HSCROLL = 0x114,
            HSCROLLCLIPBOARD = 0x30e,
            ICONERASEBKGND = 0x27,
            IME_CHAR = 0x286,
            IME_COMPOSITION = 0x10f,
            IME_COMPOSITIONFULL = 0x284,
            IME_CONTROL = 0x283,
            IME_ENDCOMPOSITION = 270,
            IME_KEYDOWN = 0x290,
            IME_KEYLAST = 0x10f,
            IME_KEYUP = 0x291,
            IME_NOTIFY = 0x282,
            IME_REQUEST = 0x288,
            IME_SELECT = 0x285,
            IME_SETCONTEXT = 0x281,
            IME_STARTCOMPOSITION = 0x10d,
            INITDIALOG = 0x110,
            INITMENU = 0x116,
            INITMENUPOPUP = 0x117,
            INPUT = 0xff,
            INPUTLANGCHANGE = 0x51,
            INPUTLANGCHANGEREQUEST = 80,
            KEYDOWN = 0x100,
            KEYFIRST = 0x100,
            KEYLAST = 0x108,
            KEYLAST_XP = 0x109,
            KEYUP = 0x101,
            KILLFOCUS = 8,
            LBUTTONDBLCLK = 0x203,
            LBUTTONDOWN = 0x201,
            LBUTTONUP = 0x202,
            MBUTTONDBLCLK = 0x209,
            MBUTTONDOWN = 0x207,
            MBUTTONUP = 520,
            MDIACTIVATE = 0x222,
            MDICASCADE = 0x227,
            MDICREATE = 0x220,
            MDIDESTROY = 0x221,
            MDIGETACTIVE = 0x229,
            MDIICONARRANGE = 0x228,
            MDIMAXIMIZE = 0x225,
            MDINEXT = 0x224,
            MDIREFRESHMENU = 0x234,
            MDIRESTORE = 0x223,
            MDISETMENU = 560,
            MDITILE = 550,
            MEASUREITEM = 0x2c,
            MENUCHAR = 0x120,
            MENUCOMMAND = 0x126,
            MENUDRAG = 0x123,
            MENUGETOBJECT = 0x124,
            MENURBUTTONUP = 290,
            MENUSELECT = 0x11f,
            MOUSEACTIVATE = 0x21,
            MOUSEFIRST = 0x200,
            MOUSEHOVER = 0x2a1,
            MOUSELAST = 0x209,
            MOUSELAST_2K = 0x20d,
            MOUSELAST_NT = 0x20a,
            MOUSELEAVE = 0x2a3,
            MOUSEMOVE = 0x200,
            MOUSEWHEEL = 0x20a,
            MOVE = 3,
            MOVING = 0x216,
            NCACTIVATE = 0x86,
            NCCALCSIZE = 0x83,
            NCCREATE = 0x81,
            NCDESTROY = 130,
            NCHITTEST = 0x84,
            NCLBUTTONDBLCLK = 0xa3,
            NCLBUTTONDOWN = 0xa1,
            NCLBUTTONUP = 0xa2,
            NCMBUTTONDBLCLK = 0xa9,
            NCMBUTTONDOWN = 0xa7,
            NCMBUTTONUP = 0xa8,
            NCMOUSEHOVER = 0x2a0,
            NCMOUSELEAVE = 0x2a2,
            NCMOUSEMOVE = 160,
            NCPAINT = 0x85,
            NCRBUTTONDBLCLK = 0xa6,
            NCRBUTTONDOWN = 0xa4,
            NCRBUTTONUP = 0xa5,
            NCXBUTTONDBLCLK = 0xad,
            NCXBUTTONDOWN = 0xab,
            NCXBUTTONUP = 0xac,
            NEXTDLGCTL = 40,
            NEXTMENU = 0x213,
            NOTIFY = 0x4e,
            NOTIFYFORMAT = 0x55,
            NULL = 0,
            PAINT = 15,
            PAINTCLIPBOARD = 0x309,
            PAINTICON = 0x26,
            PALETTECHANGED = 0x311,
            PALETTEISCHANGING = 0x310,
            PARENTNOTIFY = 0x210,
            PASTE = 770,
            PENWINFIRST = 0x380,
            PENWINLAST = 0x38f,
            POWERBROADCAST = 0x218,
            PRINT = 0x317,
            PRINTCLIENT = 0x318,
            QUERYDRAGICON = 0x37,
            QUERYENDSESSION = 0x11,
            QUERYNEWPALETTE = 0x30f,
            QUERYOPEN = 0x13,
            QUERYUISTATE = 0x129,
            QUEUESYNC = 0x23,
            QUIT = 0x12,
            RBUTTONDBLCLK = 0x206,
            RBUTTONDOWN = 0x204,
            RBUTTONUP = 0x205,
            REFLECT = 0x2000,
            RENDERALLFORMATS = 0x306,
            RENDERFORMAT = 0x305,
            SETCURSOR = 0x20,
            SETFOCUS = 7,
            SETFONT = 0x30,
            SETHOTKEY = 50,
            SETICON = 0x80,
            SETREDRAW = 11,
            SETTEXT = 12,
            SETTINGCHANGE = 0x1a,
            SHOWWINDOW = 0x18,
            SIZE = 5,
            SIZECLIPBOARD = 0x30b,
            SIZING = 0x214,
            SPOOLERSTATUS = 0x2a,
            STYLECHANGED = 0x7d,
            STYLECHANGING = 0x7c,
            SYNCPAINT = 0x88,
            SYSCHAR = 0x106,
            SYSCOLORCHANGE = 0x15,
            SYSCOMMAND = 0x112,
            SYSDEADCHAR = 0x107,
            SYSKEYDOWN = 260,
            SYSKEYUP = 0x105,
            TABLET_FIRST = 0x2c0,
            TABLET_LAST = 0x2df,
            TASKBUTTONMENU = 0x313,
            TCARD = 0x52,
            THEMECHANGED = 0x31a,
            TIMECHANGE = 30,
            TIMER = 0x113,
            UNDO = 0x304,
            UNICHAR = 0x109,
            UNINITMENUPOPUP = 0x125,
            UPDATEUISTATE = 0x128,
            USER = 0x400,
            USERCHANGED = 0x54,
            VKEYTOITEM = 0x2e,
            VSCROLL = 0x115,
            VSCROLLCLIPBOARD = 0x30a,
            WINDOWPOSCHANGED = 0x47,
            WINDOWPOSCHANGING = 70,
            WININICHANGE = 0x1a,
            WTSSESSION_CHANGE = 0x2b1,
            XBUTTONDBLCLK = 0x20d,
            XBUTTONDOWN = 0x20b,
            XBUTTONUP = 0x20c
        }
        #endregion
    }
}
