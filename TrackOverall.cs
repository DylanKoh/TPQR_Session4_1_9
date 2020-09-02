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
    public partial class TrackOverall : Form
    {
        public TrackOverall()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMain()).ShowDialog();
            Close();
        }

        private void TrackOverall_Load(object sender, EventArgs e)
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
            ClearViews();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getAssignedTraining = (from x in context.Assign_Training
                                           select x).ToList();
                var competitorRow = new List<string>() { "Competitor" };
                var expertRow = new List<string>() { "Expert" };

                var getDistinctMonths = (from x in getAssignedTraining
                                         select x.startDate.ToString("MM/yyyy")).Distinct();
                dataGridView1.ColumnCount = getDistinctMonths.Count() + 1;
                int i = 1;
                foreach (var item in getDistinctMonths)
                {
                    dataGridView1.Columns[i].HeaderText = $"No. Of Modules Start in {item}";
                    var getCountExpert = (from x in getAssignedTraining
                                          where x.User.userTypeIdFK == 2 && x.startDate.ToString("MM/yyyy") == item
                                          select x).Count();
                    expertRow.Add(getCountExpert.ToString());
                    var getCountCompetitors = (from x in getAssignedTraining
                                               where x.User.userTypeIdFK == 3 && x.startDate.ToString("MM/yyyy") == item
                                               select x).Count();
                    competitorRow.Add(getCountCompetitors.ToString());
                    i += 1;

                }
                dataGridView1.Rows.Add(competitorRow.ToArray());
                dataGridView1.Rows.Add(expertRow.ToArray());
            }
        }


        private void ClearViews()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            expertProgress.Rows.Clear();
            expertProgress.Columns.Clear();
            competitorProgress.Rows.Clear();
            competitorProgress.Columns.Clear();
        }
    }
}
