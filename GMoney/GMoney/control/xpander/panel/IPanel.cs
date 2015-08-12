using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Money.control.xpander.util;

namespace Money.control.xpander.panel
{
    public interface IPanel
    {
        /// <summary>
        /// Gets or sets the style of the panel.
        /// </summary>
        PanelStyle PanelStyle { get; set;}
        /// <summary>
        /// Gets or sets the color schema which is used for the panel.
        /// </summary>
        ColorScheme ColorScheme { get; set;}
        /// <summary>
        /// The foreground color of this component, that is used to display the caption bar text.
        /// </summary>
        Color CaptionForeColor { get; set;}
        /// <summary>
        /// Gets or sets the starting color of the gradient used in the caption bar.
        /// </summary>
        Color ColorCaptionGradientBegin { get; set;}
        /// <summary>
        /// Gets or sets the middle color of the gradient used in the caption bar.
        /// </summary>
        Color ColorCaptionGradientMiddle { get; set;}
        /// <summary>
        /// Gets or sets the end color of the gradient used in the caption bar.
        /// </summary>
        Color ColorCaptionGradientEnd { get; set;}
        /// <summary>
        /// Gets or sets the background color of the panel.
        /// </summary>
        Color BackColor { get; set; }
        /// <summary>
        /// Gets or sets the border color of the panel.
        /// </summary>
        Color BorderColor { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the control shows a border
        /// </summary>
        bool ShowBorder { get; set;}
        /// <summary>
        /// Expands the panel or xpanderpanel.
        /// </summary>
        bool Expand { get; set; }
    }
}
