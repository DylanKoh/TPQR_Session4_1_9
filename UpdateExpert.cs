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
    public partial class UpdateExpert : Form
    {
        public UpdateExpert()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new ExpertMain()).ShowDialog();
            Close();
        }

        private void UpdateExpert_Load(object sender, EventArgs e)
        {
            LoadSkills();
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
            using (var context = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getExpertNames = (from x in context.Users
                                      where x.skillIdFK == getSkillID && x.userTypeIdFK == 2
                                      select x.name).ToArray();
                cbExpertName.Items.AddRange(getExpertNames);
            }
        }

        private void cbExpertName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            using (var context = new Session4Entities())
            {
                var getUserID = (from x in context.Users
                                 where x.name == cbExpertName.SelectedItem.ToString()
                                 select x.userId).FirstOrDefault();
                var getAssigned = (from x in context.Assign_Training
                                   where x.userIdFK == getUserID
                                   select x);
                if (rbName.Checked)
                {
                    var orderedList = getAssigned.OrderBy(x => x.Training_Module.moduleName);
                    foreach (var item in orderedList)
                    {
                        var newRow = new List<string>()
                        {
                            item.Training_Module.moduleName, item.Training_Module.durationDays.ToString(),
                            item.startDate.ToShortDateString(), item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                            item.progress.ToString(), item.trainingId.ToString()
                        };
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                else if (rbProgress.Checked)
                {
                    var orderedList = getAssigned.OrderByDescending(x => x.progress);
                    foreach (var item in orderedList)
                    {
                        var newRow = new List<string>()
                        {
                            item.Training_Module.moduleName, item.Training_Module.durationDays.ToString(),
                            item.startDate.ToShortDateString(), item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                            item.progress.ToString(), item.trainingId.ToString()
                        };
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                else
                {
                    foreach (var item in getAssigned)
                    {
                        var newRow = new List<string>()
                        {
                            item.Training_Module.moduleName, item.Training_Module.durationDays.ToString(),
                            item.startDate.ToShortDateString(), item.startDate.AddDays(item.Training_Module.durationDays).ToShortDateString(),
                            item.progress.ToString(), item.trainingId.ToString()
                        };
                        dataGridView1.Rows.Add(newRow.ToArray());
                    }
                }
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    var getEndDate = Convert.ToDateTime(dataGridView1.Rows[item.Index].Cells[3].Value);
                    if (DateTime.Now > getEndDate || (getEndDate - DateTime.Now).TotalDays <= 5)
                    {
                        dataGridView1.Rows[item.Index].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if ((getEndDate - DateTime.Now).TotalDays <= 14 && (getEndDate - DateTime.Now).TotalDays > 5)
                    {
                        dataGridView1.Rows[item.Index].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
            
        }

        private void rbName_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void rbProgress_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
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
                LoadData();
            }
        }
    }
}
