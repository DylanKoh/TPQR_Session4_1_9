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

                var findRelevantModules = (from x in context.Training_Module
                                             where x.skillIdFK == getSkillID && x.userTypeIdFK == getCategory
                                             select x.moduleId).ToList();
                var getAssignedModules = (from x in context.Assign_Training
                                          where x.User.skillIdFK == getSkillID && x.User.userTypeIdFK == getCategory
                                          select x.moduleIdFK);
                foreach (var item in getAssignedModules)
                {
                    if (findRelevantModules.Contains(item))
                    {
                        findRelevantModules.Remove(item);
                    }
                }
                foreach (var item in findRelevantModules)
                {
                    var getModuleName = (from x in context.Training_Module
                                         where x.moduleId == item
                                         select x.moduleName).FirstOrDefault();
                    cbTrainingModule.Items.Add(getModuleName);
                }

            }
        }
    }
}
