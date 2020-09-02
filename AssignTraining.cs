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
    public partial class AssignTraining : Form
    {
        DateTime endDate = new DateTime(2020, 10, 1, 09, 00, 00);
        public AssignTraining()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMain()).ShowDialog();
            Close();
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTrainee();
        }

        private void LoadTrainee()
        {
            cbTrainingCat.Items.Clear();
            using (var context = new Session4Entities())
            {
                var getTraineeCat = (from x in context.User_Type
                                     select x.userTypeName).Distinct().ToList();
                getTraineeCat.Remove("Admin");
                cbTrainingCat.Items.AddRange(getTraineeCat.ToArray());
            }
        }

        private void AssignTraining_Load(object sender, EventArgs e)
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

        private void cbTrainingCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTrainingModules();
        }

        private void LoadTrainingModules()
        {
            cbTrainingModule.Items.Clear();
            using (var context = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getCategory = (from x in context.User_Type
                                   where x.userTypeName == cbTrainingCat.SelectedItem.ToString()
                                   select x.userTypeId).FirstOrDefault();

                var findUnassignedModules = (from x in context.Training_Module
                                             where x.skillIdFK == getSkillID && x.userTypeIdFK == getCategory && x.Assign_Training.Count == 0
                                             select x);
                foreach (var item in findUnassignedModules)
                {
                    cbTrainingModule.Items.Add($"{item.moduleName} ({item.durationDays} days)");
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var context  = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getCategory = (from x in context.User_Type
                                   where x.userTypeName == cbTrainingCat.SelectedItem.ToString()
                                   select x.userTypeId).FirstOrDefault();
                var getModule = (from x in context.Training_Module
                                 where x.skillIdFK == getSkillID && x.userTypeIdFK == getCategory
                                 where x.moduleName + " ("  + x.durationDays + " days)" == cbTrainingModule.SelectedItem.ToString()
                                 select x).FirstOrDefault();
                var endDuration = dtpStart.Value.AddDays(getModule.durationDays);
                var boolCheck = true;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(dataGridView1[4, row.Index].Value) == getModule.moduleId)
                    {
                        boolCheck = false;
                    }
                }
                if (endDuration > endDate)
                {
                    MessageBox.Show("Unable to assign training as date will be after start of competition!",
                        "Assign Training", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (boolCheck == false)
                {
                    MessageBox.Show("Unable to add duplicate!",
                        "Assign Training", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var newRow = new List<string>()
                    {
                        cbSkill.SelectedItem.ToString(), cbTrainingCat.SelectedItem.ToString(), getModule.moduleName, dtpStart.Value.ToShortDateString(), getModule.moduleId.ToString()
                    };
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a training assignment to remove!",
                        "Delete Assignment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var skill = dataGridView1.Rows[row.Index].Cells[0].Value.ToString();
                    var category = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();

                    var getSkillID = (from x in context.Skills
                                      where x.skillName == skill
                                      select x.skillId).FirstOrDefault();
                    var getUserTypeID = (from x in context.User_Type
                                         where x.userTypeName == category
                                         select x.userTypeId).FirstOrDefault();
                    var getTrainees = (from x in context.Users
                                       where x.skillIdFK == getSkillID && x.userTypeIdFK == getUserTypeID
                                       select x);
                    foreach (var item in getTrainees)
                    {
                        var newTraining = new Assign_Training()
                        {
                            moduleIdFK = Convert.ToInt32(dataGridView1.Rows[row.Index].Cells[4].Value),
                            progress = 0,
                            startDate = Convert.ToDateTime(dataGridView1.Rows[row.Index].Cells[3].Value),
                            userIdFK = item.userId
                        };
                        context.Assign_Training.Add(newTraining);
                    }
                }
                context.SaveChanges();
                MessageBox.Show("Trainings assigned successfully!", "Assign Training",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                (new AdminMain()).ShowDialog();
                Close();
            }
        }
    }
}
