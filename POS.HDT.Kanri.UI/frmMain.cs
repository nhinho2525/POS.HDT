using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.HDT.Kanri.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnObject_Click(object sender, EventArgs e)
        {
            if (Program.curUser.RoleId == "admin")
            {
                this.ActivateForm<frmRole>();
            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Truy Cập Tài Nguyên Này!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnUser_Click(object sender, EventArgs e)
        {
            if (Program.curUser.RoleId == "admin")
            {
                this.ActivateForm<frmUser>();
            }
            else
            {
                MessageBox.Show("Bạn Không Có Quyền Truy Cập Tài Nguyên Này!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // custom

        private Dictionary<Type, Form> SingleInstanceForms = new Dictionary<Type, Form>();

        //private frmChangePassword frmChangePassword;

        protected Form ActivateForm<T>() where T : Form, new()
        {
            //Close From
            //CloseForm();
            //
            if (!this.SingleInstanceForms.ContainsKey(typeof(T)))
            {
                T newForm = new T();
                // Set up the necessary properties
                newForm.MdiParent = this;
                newForm.FormClosed += new FormClosedEventHandler(delegate (object sender, FormClosedEventArgs e)
                {
                    this.SingleInstanceForms.Remove(sender.GetType());
                });

                this.SingleInstanceForms.Add(typeof(T), newForm);
            }
            Form formToActivate = this.SingleInstanceForms[typeof(T)];
            formToActivate.Show();
            formToActivate.Activate();

            return formToActivate;
        }
    }
}
