using System;
using System.Collections.Generic;
using System.Text;

namespace Money.control.xpander.util
{
    public enum CaptionState
	{
		/// <summary>
		/// The state of a caption in its normal state (none of the other states apply).
		/// </summary>
		Normal,
		/// <summary>
		/// The state of a caption over which a mouse pointer is resting.
		/// </summary>
		Hover
	}

    /// <summary>
    /// Specifies constants that define the style of the caption in a XPanderPanel.
    /// </summary>
    public enum CaptionStyle
    {
        /// <summary>
        ///  The normal style of a caption.
        /// </summary>
        Normal,
        /// <summary>
        /// The flat style of a caption.
        /// </summary>
        Flat
    }

    /// <summary>
    /// Contains information for the drawing of panels or xpanderpanels in a xpanderpanellist. 
    /// </summary>
    public enum ColorScheme
    {
        /// <summary>
        /// Draws the panels caption with <see cref="System.Windows.Forms.ProfessionalColors">ProfessionalColors</see>
        /// </summary>
        Professional,
        /// <summary>
        /// Draws the panels caption with custom colors.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Contains information about the style of the panels or xpanderpanels
    /// </summary>
    public enum PanelStyle
    {
        /// <summary>
        /// Draws the panels caption in the default office2003 style.
        /// </summary>
        Default,
        /// <summary>
        /// Draws the caption of a panel in the aqua style.
        /// </summary>
        Aqua,
        /// <summary>
        /// Draws the panels caption in the office 2007 style.
        /// </summary>
        Office2007,
        /// <summary>
        /// Hides the caption (only for using in the panel control).
        /// </summary>
        None
    }

}
