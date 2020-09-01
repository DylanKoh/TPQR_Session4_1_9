using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session4_1_9
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                var lines = File.ReadAllLines(txtFilePath.Text);
                foreach (var item in lines.Skip(1))
                {
                    var values = item.Split(',');
                    var newUser = new User()
                    {
                        userId = values[0],
                        skillIdFK = Int32.Parse(values[1]),
                        passwd = values[2],
                        name = values[3],
                        userTypeIdFK = Int32.Parse(values[4])
                    };
                    context.Users.Add(newUser);
                }
                context.SaveChanges();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                
            }
        }

        private void txtFilePath_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }
    }
}
