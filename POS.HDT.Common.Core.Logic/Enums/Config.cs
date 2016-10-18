using System;
using System.Drawing;

namespace POS.HDT.Common.Core.Logic.Enums
{
    public static class Config
    {

        /// <summary>
        /// Defined types of messages: Success/Warning/Error.
        /// </summary>
        public enum TypeOfMessage
        {
            Success,
            Warning,
            Error,
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
