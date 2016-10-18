using System;
using System.Drawing;

namespace POS.HDT.Common.Core.Logic.Enums
{
    public static class Config
    {

        /// <summary>
        /// Status of bracelet
        /// </summary>
        public enum STATUSBRACELET
        {
            UnusedStatus = 1,
            UsedStatus,
            LossStaus
        }

        /// <summary>
        /// Custom MessageBox Button
        /// </summary>
        public enum CUSTOM_MESSAGEBOX_BUTTON
        {
            OK,
            YESNO
        }

        /// <summary>
        /// Custom MessageBox Icon
        /// </summary>
        public enum CUSTOM_MESSAGEBOX_ICON
        {
            Information,
            Error
        }
    }
}
