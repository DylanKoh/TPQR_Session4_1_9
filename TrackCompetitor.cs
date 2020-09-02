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
    public partial class TrackCompetitor : Form
    {
        int _skillID = 0;
        public TrackCompetitor(int skillID)
        {
            InitializeComponent();
            _skillID = skillID;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new ExpertMain(_skillID)).ShowDialog();
            Close();
        }

        private void TrackCompetitor_Load(object sender, EventArgs e)
        {
            using (var context = new Session4Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillId == _skillID
                                select x.skillName).FirstOrDefault();
                lblSkill.Text = getSkill;
            }
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new Session4Entities())
            {
                var getModules = (from x in context.Assign_Training
                                  where x.User.skillIdFK == _skillID && x.User.userTypeIdFK == 3
                                  select x.moduleIdFK).Distinct();
                dataGridView1.ColumnCount = getModules.Count() + 1;
                dataGridView1.Columns[0].HeaderText = "Name of Competitor";
                int i = 1;
                var getCompetitors = (from x in context.Users
                                      where x.skillIdFK == _skillID && x.userTypeIdFK == 3
                                      select x.name).Distinct();
                foreach (var modules in getModules)
                {
                    var getModuleName = (from x in context.Training_Module
                                         where x.moduleId == modules
                                         select x.moduleName).FirstOrDefault();
                    dataGridView1.Columns[i].HeaderText = getModuleName;
                    i += 1;
                }
                foreach (var competitors in getCompetitors)
                {
                    var newRow = new List<string>() { competitors };
                    foreach (var modules in getModules)
                    {
                        var getProgress = (from x in context.Assign_Training
                                           where x.moduleIdFK == modules && x.User.name == competitors
                                           select x.progress).FirstOrDefault();
                        newRow.Add(getProgress.ToString());
                    }
                    dataGridView1.Rows.Add(newRow.ToArray());
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewColumn cell in dataGridView1.Columns)
                    {
                        if (dataGridView1[cell.Index, row.Index].Value.ToString() == "0")
                        {
                            dataGridView1[cell.Index, row.Index].Style.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
    }
}
