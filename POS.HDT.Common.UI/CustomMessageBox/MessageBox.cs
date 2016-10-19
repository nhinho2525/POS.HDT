using POS.HDT.Common.Core.Logic.Classes;
using POS.HDT.Common.Core.Logic.Enums;
using System;
using System.Windows.Forms;

namespace POS.HDT.Common.Core.UI.CustomMessageBox
{
    public partial class MessageBox : Form
    {
        static MessageBox messageBox;
        static DialogResult dialogResult;

        public MessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show custom MessageBox with title
        /// </summary>
        /// <param name="title">Title of message box</param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string title)
        {
            messageBox = new MessageBox();
            messageBox.lblTitle.Text = title;
            messageBox.ShowDialog();
            messageBox.btnOK.Visible = false;
            return dialogResult;
        }

        /// <summary>
        /// Show custom MessageBox with title, messageButton
        /// </summary>
        /// <param name="title"></param>
        /// <param name="messageButton"></param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string title, Config.CUSTOM_MESSAGEBOX_BUTTON messageButton)
        {
            messageBox = new MessageBox();
            messageBox.lblTitle.Text = title;
            if (messageButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.OK))
            {
                messageBox.btnAccept.Visible = false;
                messageBox.btnCancel.Visible = false;
                messageBox.btnOK.Location = messageBox.btnCancel.Location;
            }
            if (messageButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.YESNO))
            {
                messageBox.btnOK.Visible = false;
            }
            messageBox.ShowDialog();
            return dialogResult;
        }

        /// <summary>
        /// Show custom MessageBox with message and title
        /// </summary>
        /// <param name="message">Message of message box</param>
        /// <param name="title">Title of message box</param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string message, string title)
        {
            messageBox = new MessageBox();
            messageBox.lblMessage.Text = message;
            messageBox.lblTitle.Text = title;
            messageBox.btnOK.Visible = false;
            messageBox.ShowDialog();
            return dialogResult;
        }

        /// <summary>
        /// Show custom MessageBox with message, title and icon
        /// </summary>
        /// <param name="message">Message of message box</param>
        /// <param name="title">Title of message box</param>
        /// <param name="icon">Icon of message box</param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string message, string title, Config.CUSTOM_MESSAGEBOX_ICON icon)
        {
            messageBox = new MessageBox();
            messageBox.lblMessage.Text = message;
            messageBox.lblTitle.Text = title;
            if (icon == Config.CUSTOM_MESSAGEBOX_ICON.Information)
            {
                //TODO
                //messageBox.pbIcon.BackgroundImage = Properties.Resources.information;
            }
            if (icon == Config.CUSTOM_MESSAGEBOX_ICON.Error)
            {
                //TODO
                //messageBox.pbIcon.BackgroundImage = Properties.Resources.thoat;
            }
            messageBox.btnOK.Visible = false;
            messageBox.ShowDialog();
            return dialogResult;
        }

        /// <summary>
        /// Show custom MessageBox with message, title, icon, button
        /// </summary>
        /// <param name="message">Message of message box</param>
        /// <param name="title">Title of message box</param>
        /// <param name="icon">Icon of message box</param>
        /// <param name="messageBoxButton">Button that show in message box</param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string message, string title, Config.CUSTOM_MESSAGEBOX_ICON icon, Config.CUSTOM_MESSAGEBOX_BUTTON messageBoxButton)
        {
            messageBox = new MessageBox();
            messageBox.lblMessage.Text = message;
            messageBox.lblTitle.Text = title;
            if (icon == Config.CUSTOM_MESSAGEBOX_ICON.Information)
            {
                //TODO
                //messageBox.pbIcon.BackgroundImage = Properties.Resources.information;
            }
            if (icon == Config.CUSTOM_MESSAGEBOX_ICON.Error)
            {
                //TODO
                //messageBox.pbIcon.BackgroundImage = Properties.Resources.thoat;
            }
            if (messageBoxButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.OK))
            {
                messageBox.btnCancel.Visible = false;
                messageBox.btnAccept.Visible = false;
                messageBox.btnOK.Location = messageBox.btnCancel.Location;
            }
            if (messageBoxButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.YESNO))
            {
                messageBox.btnOK.Visible = false;
            }
            messageBox.ShowDialog();
            return dialogResult;
        }

        /// <summary>
        /// Show custom MessageBox with message , title, button
        /// </summary>
        /// <param name="message">Message of message box</param>
        /// <param name="title">Title of message box</param>
        /// <param name="messageBoxButton">Button of message box</param>
        /// <returns></returns>
        public static DialogResult ShowCustomMessageBox(string message, string title, Config.CUSTOM_MESSAGEBOX_BUTTON messageBoxButton)
        {
            messageBox = new MessageBox();
            messageBox.lblMessage.Text = message;
            messageBox.lblTitle.Text = title;
            if (messageBoxButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.OK))
            {
                messageBox.btnCancel.Visible = false;
                messageBox.btnAccept.Visible = false;
                messageBox.btnOK.Location = messageBox.btnCancel.Location;
            }
            if (messageBoxButton.Equals(Config.CUSTOM_MESSAGEBOX_BUTTON.YESNO))
            {
                messageBox.btnOK.Visible = false;
            }
            messageBox.ShowDialog();
            return dialogResult;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Yes;
            messageBox.Dispose();
            messageBox.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;
            messageBox.Dispose();
            messageBox.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            messageBox.Dispose();
            messageBox.Close();
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            btnOK.Text = Languages.GetResource("Agree");
            btnAccept.Text = Languages.GetResource("Agree");
            btnCancel.Text = Languages.GetResource("Cancel");
        }
    }
}
