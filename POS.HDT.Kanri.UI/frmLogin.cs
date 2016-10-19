using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.HDT.Kanri.Logic;

namespace POS.HDT.Kanri.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUsername.Text.Trim()) && String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Vui Lòng Nhập Thông Tin Đăng Nhập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Vui Lòng Nhập Username!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Vui Lòng Nhập Mật Khẩu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (chkAdvanced.Checked == true)
            //{
            //    string tss = txtServer.Text;
            //    tss = tss.Replace("http://", "");
            //    tss = tss.Replace("/", "");
            //    Program.urlImage = tss;
            //    Program.destopService.Url = "http://" + tss + "/POSService.asmx";

            //    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //    configuration.AppSettings.Settings["MyLastURL"].Value = tss;
            //    configuration.Save();

            //    ConfigurationManager.RefreshSection("appSettings");

            //    //ConfigurationManager.AppSettings["MyLastURL"] = txtServer.Text;

            //    //ConfigurationManager.AppSettings.
            //}
            try
            {

                LoginLogic loginLogic = new LoginLogic();
                string idOrUsername = txtUsername.Text;
                string password = Program.ConvertStringToMD5(txtPassword.Text.Trim());
                bool ok = loginLogic.CheckLogin(idOrUsername, password);
                if (ok)
                {
                    Program.curUser = loginLogic.user;
                    Program.username = loginLogic.Username;
                    Program.password = loginLogic.Password;
                    if (Program.curUser.RoleId == "admin")
                    {
                        if (string.IsNullOrEmpty(loginLogic.ErrorString))
                        {
                            frmMain frm = new frmMain();
                            frm.Show();
                            txtUsername.Clear();
                            txtPassword.Clear();
                            txtUsername.Focus();
                            this.Hide();
                        }
                        else
                            MessageBox.Show(loginLogic.ErrorString, "Lôi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Bạn Không Có Quyền Truy Xuất!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Lôi Login. Vui Lòng Kiểm Tra Lại!", "Lôi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                string code = System.Runtime.InteropServices.Marshal.GetExceptionCode().ToString();
                if (code == "-532462766")
                {
                    
                }
                return;
            }
        }
    }
}
