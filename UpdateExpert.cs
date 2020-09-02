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

        }
    }
}
