using System;
using System.Windows.Forms;
using System.Threading;
using POS.HDT.Common.Core.UI;
using System.Configuration;
using POS.HDT.Common.Core.Logic.Enums;
using POS.HDT.Common.Core.Logic.Classes;
using POS.HDT.Store.Logic;

namespace POS.HDT.Store.UI
{
    public partial class frmLogin : Form
    {
        protected LoginLogic GetLoginLogic = new LoginLogic();
        #region Contructor

        public frmLogin()
        {
            this.Hide();
            Thread splashthread = new Thread(new ThreadStart(clsSplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            InitializeComponent();
            //set color for control
            SettingControl();
        } 

        #endregion

        #region System fuction
        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.txtServer.Text = ConfigurationManager.AppSettings["MyLastURL"];
            try
            {
                clsSplashScreen.UdpateStatusText("Connecting…");
                Thread.Sleep(1000);
                //string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                if (1 == 1) //if (Program.destopService.IsServiceReady() && Program.destopService.IsDataReady())
                {
                    clsSplashScreen.UdpateStatusTextWithStatus("Connected.", Config.TypeOfMessage.Success);
                    Thread.Sleep(1000);
                    this.Show();
                    clsSplashScreen.CloseSplashScreen();
                    this.Activate();
                    Languages.StrCulture = "vi-VN";

                    GlobalizeApp();
                }
                else
                {
                    clsSplashScreen.UdpateStatusTextWithStatus("Failed to connect. Please try again.", Config.TypeOfMessage.Error);
                    Thread.Sleep(2000);
                    this.Close();
                }
            }
            catch (Exception)
            {
                clsSplashScreen.UdpateStatusTextWithStatus("Currently the device is not connected to the network .", Config.TypeOfMessage.Error);
                Thread.Sleep(2000);
                this.Close();
            }
        }

        /// <summary>
        /// event rise when user click Agree button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgree_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrWhiteSpace(txtUsername.Text.Trim()) && String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("PleaseInputInformationLogin"),
                                Languages.GetResource("Information"),
                                Config.CUSTOM_MESSAGEBOX_ICON.Information,
                                Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("PleaseInputUsername"),
                                Languages.GetResource("Information"),
                                Config.CUSTOM_MESSAGEBOX_ICON.Information,
                                Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("PleaseInputPassword"),
                                Languages.GetResource("Information"),
                                Config.CUSTOM_MESSAGEBOX_ICON.Information,
                                Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                return;
            }
            if (chkAdvanced.Checked==true)
            {
                //Program.destopService.Url = "http://" + txtServer.Text + ":8389/DestopService.asmx";
                string tss = txtServer.Text;
                tss = tss.Replace("http://", "");
                tss = tss.Replace("/", "");
                Program.urlImage = tss;
                //TODO
                //Program.destopService.Url = "http://" + tss + "/VVPosService.asmx";

                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["MyLastURL"].Value = tss;
                configuration.Save();

                ConfigurationManager.RefreshSection("appSettings");                

                //ConfigurationManager.AppSettings["MyLastURL"] = txtServer.Text;
                
                //ConfigurationManager.AppSettings.
            }
            try
            {

                bool ok = GetLoginLogic.OnLogin(txtUsername.Text, txtPassword.Text.Trim());

                if (ok)
                {
                    // Set culture into system
                    Languages.SetCulture(Languages.StrCulture);

                    if (GetLoginLogic.userDBAccess.users.RoleId == "letan" || GetLoginLogic.userDBAccess.users.RoleId == "admin")
                    {
                        if (string.IsNullOrEmpty(GetLoginLogic.userDBAccess.ErrorString))
                        {
                            //TODO
                            //string _sObjectId = usersBLL.GetObjectIdByUserId(idOrUsername).Rows[0][0].ToString();

                            //ObjectBLL objectBLL = new ObjectBLL();
                            //Program.FullName = objectBLL.GetObjectByObjectId(_sObjectId).Rows[0]["FullName"].ToString();
                            //Program.ImageUser = objectBLL.GetObjectByObjectId(_sObjectId).Rows[0]["Image"].ToString();

                            //frmMain frm = new frmMain();
                            //frm.Show();
                            //txtUsername.Clear();
                            //txtPassword.Clear();
                            //cbbLanguage.SelectedIndex = -1;
                            //txtUsername.Focus();
                            //this.Hide();
                        }
                        else
                            MessageBox.Show(GetLoginLogic.userDBAccess.ErrorString, Languages.GetResource("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("YouAreNotPermissionAccess"),
                                    Languages.GetResource("Error"),
                                    Config.CUSTOM_MESSAGEBOX_ICON.Error,
                                    Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                        txtUsername.Focus();
                        return;
                    }
                }
                else
                {
                    Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("LoginError"),
                                    Languages.GetResource("Error"),
                                    Config.CUSTOM_MESSAGEBOX_ICON.Error,
                                    Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                    return;
                }
            }
            catch( Exception)
            {
                string code = System.Runtime.InteropServices.Marshal.GetExceptionCode().ToString();
                if (code == "-532462766")
                {
                    Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("CurrentlyTheDeviceIsNotConnectedInternet") + " - Last Link : " + GetLoginLogic.userDBAccess.PosService.Url,
                              Languages.GetResource("Information"),
                              Config.CUSTOM_MESSAGEBOX_ICON.Information,
                              Config.CUSTOM_MESSAGEBOX_BUTTON.OK);
                }
                return;
            }
        }

        /// <summary>
        /// event rise when user click cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult r = Common.Core.UI.CustomMessageBox.MessageBox.ShowCustomMessageBox(Languages.GetResource("AreYouSureCancel"), Languages.GetResource("Information"), Config.CUSTOM_MESSAGEBOX_ICON.Information, Config.CUSTOM_MESSAGEBOX_BUTTON.YESNO);

            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// event rise when user choose language for app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbbLanguage_DropDownClosed(object sender, EventArgs e)
        {
            changeLanguage();
            SetSelectedIndexForCombobox();
        }

        /// <summary>
        /// event rise when frmLogin paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
            //Form frm = (Form)sender;
            ////float conner = 30;
            //System.Drawing.Drawing2D.GraphicsPath frmPath = new System.Drawing.Drawing2D.GraphicsPath();
            //// Set a new rectangle to the same size as the button's 
            //// ClientRectangle property.
            //System.Drawing.Rectangle newRectangle = frm.ClientRectangle;
            //ControlPaint.DrawBorder(e.Graphics, newRectangle,
            //    System.Drawing.ColorTranslator.FromHtml(Common.Config.BACKGROUNDCOLOR), 10, ButtonBorderStyle.Outset,
            //    System.Drawing.ColorTranslator.FromHtml(Common.Config.BACKGROUNDCOLOR), 10, ButtonBorderStyle.Outset,
            //    System.Drawing.ColorTranslator.FromHtml(Common.Config.BACKGROUNDCOLOR), 10, ButtonBorderStyle.Inset,
            //    System.Drawing.ColorTranslator.FromHtml(Common.Config.BACKGROUNDCOLOR), 10, ButtonBorderStyle.Inset);
            /*frmPath.AddArc(newRectangle.X, newRectangle.Y, conner, conner, 180, 90);
            frmPath.AddArc(newRectangle.X + newRectangle.Width - conner, newRectangle.Y, conner, conner, 270, 90);
            frmPath.AddArc(newRectangle.X + newRectangle.Width - conner, newRectangle.Y + newRectangle.Height - conner, conner, conner, 0, 90);
            frmPath.AddArc(newRectangle.X, newRectangle.Y + newRectangle.Height - conner, conner, conner, 90, 90);
            frm.Region = new System.Drawing.Region(frmPath);
            */
        }
        #endregion

        #region Custom method
        /// <summary>
        /// Setting for control of form
        /// </summary>
        private void SettingControl()
        {
            //TODO
            //this.BackgroundImage = Common.Utility.GetImageFromService("ShopImg","login3.png");
            ////this.pbImage.BackgroundImage = Common.Config.LOGO;
            //this.lblTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TITLECOLOR);
            //this.lblLanguage.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.LABELCOLOR);
            //this.lblUsername.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.LABELCOLOR);
            //this.lblPassword.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.LABELCOLOR);
            //this.cbbLanguage.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TEXTCOLOR);
            //this.txtUsername.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TEXTCOLOR);
            //this.txtPassword.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TEXTCOLOR);
            //this.btnAgree.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TEXTBUTTONCOLOR);
            //this.btnCancel.ForeColor = System.Drawing.ColorTranslator.FromHtml(Common.Config.TEXTBUTTONCOLOR);
            //this.pbImage.Image = Common.Utility.GetImageFromService("ShopImg", "logo6.png");

        }   

        /// <summary>
        /// Globalize Application
        /// </summary>
        private void GlobalizeApp()
        {
            Languages.SetCulture(Languages.StrCulture);
            Languages.SetResource();
            SetUIChanges();
        }

        /// <summary>
        /// Set text for control
        /// </summary>
        public void SetUIChanges()
        {
            lblTitle.Text = Languages.GetResource("SoftwareTilte");
            lblLanguage.Text = Languages.GetResource("Language");
            cbbLanguage.Items.Clear();
            cbbLanguage.Items.Add(Languages.GetResource("English"));
            cbbLanguage.Items.Add(Languages.GetResource("Japan"));
            cbbLanguage.Items.Add(Languages.GetResource("Vietnam"));
            lblUsername.Text = Languages.GetResource("Username");
            lblPassword.Text = Languages.GetResource("Password");
            btnAgree.Text = Languages.GetResource("Agree");
            btnCancel.Text = Languages.GetResource("Cancel");
            chkAdvanced.Text = Languages.GetResource("Advance");
            lblServer.Text = Languages.GetResource("Server");
            SetSelectedIndexForCombobox();
        }

        /// <summary>
        /// Set strCulture from cbbLanguage
        /// </summary>
        public void SetstrCultureFromCombobox()
        {
            if (cbbLanguage.SelectedIndex == 0)
            {
                Languages.StrCulture = "en-US";
            }

            if (cbbLanguage.SelectedIndex == 1)
            {
                Languages.StrCulture = "ja-JP";
            }

            if (cbbLanguage.SelectedIndex == 2)
            {
                Languages.StrCulture = "vi-VN";
            }
        }

        /// <summary>
        /// Set selected index for cbbLanguge
        /// </summary>
        public void SetSelectedIndexForCombobox()
        {
            if (String.Compare(Languages.StrCulture, "en-US") == 0)
            {
                cbbLanguage.SelectedIndex = 0;
            }

            if (String.Compare(Languages.StrCulture, "ja-JP") == 0)
            {
                cbbLanguage.SelectedIndex = 1;
            }

            if (String.Compare(Languages.StrCulture, "vi-VN") == 0)
            {
                cbbLanguage.SelectedIndex = 2;
            }
        }

        /// <summary>
        /// Change language and save into registry
        /// </summary>
        private void changeLanguage()
        {
            SetstrCultureFromCombobox();
            GlobalizeApp();

            //TODO
            //Registry.SetStringRegistryValue("Language", Languages.StrCulture);
        }
        #endregion

        private void chkAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdvanced.Checked == true)
            {
                lblServer.Visible = true;
                txtServer.Visible = true;
            }
            else
            {
                lblServer.Visible = false;
                txtServer.Visible = false;
            }
        }

        
    }
}
