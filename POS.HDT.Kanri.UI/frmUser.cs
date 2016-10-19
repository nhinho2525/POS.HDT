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
    public partial class frmUser : Form
    {
        private UserLogic userLogic;
        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            userLogic = new UserLogic();
            // Load data for dgvUsers
            dgvUsers.DataSource = userLogic.LoadUsers(Program.curUser);
            // Load first row on screen
        }
    }
}
