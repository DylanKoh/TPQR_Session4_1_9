using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session4_1_9
{
    public partial class UpdateCompetitor : Form
    {
        public UpdateCompetitor()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new LoginForm()).ShowDialog();
            Close();
        }

        private void UpdateCompetitor_Load(object sender, EventArgs e)
        {
            LoadSkills();
            dataGridView1.Rows.Clear();
            cbCompetitorName.Items.Clear();
            txtSearch.Text = string.Empty;
        }
        private void LoadSkills()
        {
            cbSkill.Items.Clear();
            using (var context = new Session4Entities())
            {
                var getSkills = (from x in context.Skills
                                 select x.skillName).Distinct().ToList();
                getSkills.Remove("Admin");
                cbSkill.Items.AddRange(getSkills.ToArray());
            }
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCompetitors();
        }

        private void LoadCompetitors()
        {
            cbCompetitorName.Items.Clear();
            using (var context = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getCompetitors = (from x in context.Users
                                      where x.skillIdFK == getSkillID && x.userTypeIdFK == 3
                                      select x.name).ToArray();
                cbCompetitorName.Items.AddRange(getCompetitors);
            }
        }

        private void cbCompetitorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(0);
        }

        private void LoadData(int mode)
        {
            dataGridView1.Rows.Clear();
            using (var context = new Session4Entities())
            {
                var userID = (from x in context.Users
                              where x.name == cbCompetitorName.SelectedItem.ToString()
                              select x.userId).FirstOrDefault();
                if (mode == 0)
                {
                    var getAssignedModules = (from x in context.Assign_Training
                                              where x.User.userId == userID
                                              select x);
                    foreach (var item in getAssignedModules)
                    {
                        var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                else if (mode == 1)
                {
                    var getAssignedModules = (from x in context.Assign_Training
                                              where x.User.userId == userID
                                              orderby x.Training_Module.moduleName
                                              select x);
                    foreach (var item in getAssignedModules)
                    {
                        var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                else
                {
                    var getAssignedModules = (from x in context.Assign_Training
                                              where x.User.userId == userID
                                              select x).ToList();
                    var getOrdered = (from x in getAssignedModules
                                      orderby x.startDate.AddDays(x.Training_Module.durationDays) descending
                                      select x);
                    foreach (var item in getOrdered)
                    {
                        var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }

            }
        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            LoadData(1);
        }

        private void rbEndDate_CheckedChanged(object sender, EventArgs e)
        {
            LoadData(2);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                if (rbName.Checked)
                {
                    LoadData(1);
                }
                else if (rbEndDate.Checked)
                {
                    LoadData(2);
                }
                else
                {
                    LoadData(0);
                }
            }
            else
            {
                using (var context = new Session4Entities())
                {
                    var userID = (from x in context.Users
                                  where x.name == cbCompetitorName.SelectedItem.ToString()
                                  select x.userId).FirstOrDefault();
                    if (rbName.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.Training_Module.moduleName.ToLower().Contains(txtSearch.Text.ToLower())
                                                  orderby x.Training_Module.moduleName
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else if (rbEndDate.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.Training_Module.moduleName.ToLower().Contains(txtSearch.Text.ToLower())
                                                  select x).ToList();
                        var getOrdered = (from x in getAssignedModules
                                          orderby x.startDate.AddDays(x.Training_Module.durationDays) descending
                                          select x);
                        foreach (var item in getOrdered)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.Training_Module.moduleName.ToLower().Contains(txtSearch.Text.ToLower())
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                }

            }
        }

        private void cbProgress_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                dataGridView1.Rows.Clear();
                var userID = (from x in context.Users
                              where x.name == cbCompetitorName.SelectedItem.ToString()
                              select x.userId).FirstOrDefault();
                if (cbProgress.SelectedItem.ToString() == "No Filter")
                {
                    if (rbName.Checked)
                    {
                        LoadData(1);
                    }
                    else if (rbEndDate.Checked)
                    {
                        LoadData(2);
                    }
                    else
                    {
                        LoadData(0);
                    }
                }
                else if (cbProgress.SelectedItem.ToString() == "Completed")
                {
                    if (rbName.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 100
                                                  orderby x.Training_Module.moduleName
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else if (rbEndDate.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 100
                                                  select x).ToList();
                        var getOrdered = (from x in getAssignedModules
                                          orderby x.startDate.AddDays(x.Training_Module.durationDays) descending
                                          select x);
                        foreach (var item in getOrdered)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 100
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                }
                else if (cbProgress.SelectedItem.ToString() == "In Progress")
                {
                    if (rbName.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID
                                                  where x.progress > 0 && x.progress < 100
                                                  orderby x.Training_Module.moduleName
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else if (rbEndDate.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID
                                                  where x.progress > 0 && x.progress < 100
                                                  select x).ToList();
                        var getOrdered = (from x in getAssignedModules
                                          orderby x.startDate.AddDays(x.Training_Module.durationDays) descending
                                          select x);
                        foreach (var item in getOrdered)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID
                                                  where x.progress > 0 && x.progress < 100
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                }
                else
                {
                    if (rbName.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 0
                                                  orderby x.Training_Module.moduleName
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else if (rbEndDate.Checked)
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 0
                                                  select x).ToList();
                        var getOrdered = (from x in getAssignedModules
                                          orderby x.startDate.AddDays(x.Training_Module.durationDays) descending
                                          select x);
                        foreach (var item in getOrdered)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                    else
                    {
                        var getAssignedModules = (from x in context.Assign_Training
                                                  where x.User.userId == userID && x.progress == 0
                                                  select x);
                        foreach (var item in getAssignedModules)
                        {
                            var newRow = new List<string>() { item.Training_Module.moduleName,
                        item.Training_Module.durationDays.ToString(), item.startDate.ToShortDateString(),
                        item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                        item.progress.ToString(), item.trainingId.ToString()};
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                using (var context = new Session4Entities())
                {
                    var trainingID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    var getOldProgress = (from x in context.Assign_Training
                                          where x.trainingId == trainingID
                                          select x.progress).FirstOrDefault();
                   
                    if (!Int32.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out _) || getOldProgress > Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        MessageBox.Show("Please input a valid number higher than the old progress!",
                            "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = getOldProgress;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var trainingID = Convert.ToInt32(dataGridView1.Rows[row.Index].Cells[5].Value);
                    var getTraining = (from x in context.Assign_Training
                                       where x.trainingId == trainingID
                                       select x).FirstOrDefault();
                    getTraining.progress = Convert.ToInt32(dataGridView1.Rows[row.Index].Cells[4].Value);

                }
                context.SaveChanges();
                MessageBox.Show("Training progress updated successfully!", "Update training progress",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(0);
            }
        }
    }
}
