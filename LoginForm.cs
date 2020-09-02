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
        DateTime endTime = new DateTime(2020, 10, 1, 09, 00, 00);
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
                MessageBox.Show("Successfully added users!", "Upload Users", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtFilePath.Text = string.Empty;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                if (string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please check your fields!", "Empty Field(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var findUser = (from x in context.Users
                                    where x.userId == txtUserID.Text
                                    select x).FirstOrDefault();
                    if (findUser == null)
                    {
                        MessageBox.Show("User does not exist!", "Invalid User", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else if (findUser.passwd != txtPassword.Text)
                    {
                        MessageBox.Show("User ID or Password does not match our DB!", "Invalid Login", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Welcome {findUser.name}!", "Login", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        if (findUser.userTypeIdFK == 2)
                        {
                            Hide();
                            (new ExpertMain(findUser.skillIdFK)).ShowDialog();
                            Close();
                        }
                        else if (findUser.userTypeIdFK == 3)
                        {
                            Hide();
                            (new UpdateCompetitor()).ShowDialog();
                            Close();
                        }
                        else
                        {
                            Hide();
                            (new AdminMain()).ShowDialog();
                            Close();
                        }
                    }
                }
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

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            TimeSpan ts = endTime.Subtract(DateTime.Now);
            lblTimer.Text = $"{ts.ToString("d' Days 'h' Hours 'm' Minutes 's' Seconds'")} till event starts!";
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = endTime.Subtract(DateTime.Now);
            lblTimer.Text = $"{ts.ToString("d' Days 'h' Hours 'm' Minutes 's' Seconds'")} till event starts!";
        }
    }
}
