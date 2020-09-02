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
            LoadGraph();
        }

        private void LoadGraph()
        {
            chart1.Series.Clear();
            chart1.Series.Add("Completed");
            chart1.Series.Add("In Progress");
            chart1.Series.Add("Not Started");
            using (var context = new Session4Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getAssignedTraining = (from x in context.Assign_Training
                                           select x).ToList();
                var getDistinctCompetitorModule = (from x in getAssignedTraining
                                                   where x.User.skillIdFK == getSkillID
                                                   where x.User.userTypeIdFK == 3
                                                   select x.moduleIdFK).Distinct();
                foreach (var item in getDistinctCompetitorModule)
                {
                    var getModuleName = (from x in context.Training_Module
                                         where x.moduleId == item
                                         select x.moduleName).FirstOrDefault();
                    var getCompleted = (from x in context.Assign_Training
                                        where x.moduleIdFK == item && x.progress == 100
                                        select x).Count();
                    var getInProgress = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress > 0 && x.progress < 100
                                         select x).Count();
                    var getNotStarted = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress == 0
                                         select x).Count();
                    chart1.Series["Completed"].Points.AddXY(getModuleName, getCompleted);
                    chart1.Series["In Progress"].Points.AddXY(getModuleName, getInProgress);
                    chart1.Series["Not Started"].Points.AddXY(getModuleName, getNotStarted);
                }
            }
           
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
                var expertCompleted = new List<string>() { "Completed" };
                var expertInProcess = new List<string>() { "In Process" };
                var expertNotStarted = new List<string>() { "Not Started" };
                var competitorCompleted = new List<string>() { "Completed" };
                var competitorInProcess = new List<string>() { "In Process" };
                var competitorNotStarted = new List<string>() { "Not Started" };
                var getDistinctMonths = (from x in getAssignedTraining
                                         where x.User.skillIdFK == getSkillID
                                         orderby x.startDate
                                         select x.startDate.ToString("MM/yyyy")).Distinct();
                var getDistinctExpertModule = (from x in getAssignedTraining
                                               where x.User.skillIdFK == getSkillID
                                               where x.User.userTypeIdFK == 2
                                               select x.moduleIdFK).Distinct();
                var getDistinctCompetitorModule = (from x in getAssignedTraining
                                                   where x.User.skillIdFK == getSkillID
                                                   where x.User.userTypeIdFK == 3
                                                   select x.moduleIdFK).Distinct();

                dataGridView1.ColumnCount = getDistinctMonths.Count() + 1;
                expertProgress.ColumnCount = getDistinctExpertModule.Count() + 1;
                competitorProgress.ColumnCount = getDistinctCompetitorModule.Count() + 1;
                dataGridView1.Columns[0].HeaderText = "Trainee Category";
                expertProgress.Columns[0].HeaderText = "Status (Expert)";
                competitorProgress.Columns[0].HeaderText = "Status (Competitor)";
                int i = 1;
                int ie = 1;
                int ic = 1;
                foreach (var item in getDistinctMonths)
                {
                    dataGridView1.Columns[i].HeaderText = $"No. Of Modules Start in {item}";
                    var getCountExpert = (from x in getAssignedTraining
                                          where x.User.userTypeIdFK == 2 && x.startDate.ToString("MM/yyyy") == item && x.User.skillIdFK == getSkillID
                                          select x.Training_Module).Distinct().Count();
                    expertRow.Add(getCountExpert.ToString());
                    var getCountCompetitors = (from x in getAssignedTraining
                                               where x.User.userTypeIdFK == 3 && x.startDate.ToString("MM/yyyy") == item && x.User.skillIdFK == getSkillID
                                               select x.Training_Module).Distinct().Count();
                    competitorRow.Add(getCountCompetitors.ToString());
                    i += 1;

                }
                foreach (var item in getDistinctExpertModule)
                {
                    var getModuleName = (from x in context.Training_Module
                                         where x.moduleId == item
                                         select x.moduleName).FirstOrDefault();
                    var getCompleted = (from x in context.Assign_Training
                                        where x.moduleIdFK == item && x.progress == 100
                                        select x).Count();
                    var getInProgress = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress > 0 && x.progress < 100
                                         select x).Count();
                    var getNotStarted = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress == 0
                                         select x).Count();
                    expertProgress.Columns[ie].HeaderText = getModuleName;

                    expertCompleted.Add(getCompleted.ToString());
                    expertInProcess.Add(getInProgress.ToString());
                    expertNotStarted.Add(getNotStarted.ToString());
                    ie += 1;

                }
                foreach (var item in getDistinctCompetitorModule)
                {
                    var getModuleName = (from x in context.Training_Module
                                         where x.moduleId == item
                                         select x.moduleName).FirstOrDefault();
                    var getCompleted = (from x in context.Assign_Training
                                        where x.moduleIdFK == item && x.progress == 100
                                        select x).Count();
                    var getInProgress = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress > 0 && x.progress < 100
                                         select x).Count();
                    var getNotStarted = (from x in context.Assign_Training
                                         where x.moduleIdFK == item && x.progress == 0
                                         select x).Count();
                    competitorProgress.Columns[ic].HeaderText = getModuleName;

                    competitorCompleted.Add(getCompleted.ToString());
                    competitorInProcess.Add(getInProgress.ToString());
                    competitorNotStarted.Add(getNotStarted.ToString());
                    ic += 1;

                }
                dataGridView1.Rows.Add(competitorRow.ToArray());
                dataGridView1.Rows.Add(expertRow.ToArray());
                expertProgress.Rows.Add(expertCompleted.ToArray());
                expertProgress.Rows.Add(expertInProcess.ToArray());
                expertProgress.Rows.Add(expertNotStarted.ToArray());
                competitorProgress.Rows.Add(competitorCompleted.ToArray());
                competitorProgress.Rows.Add(competitorInProcess.ToArray());
                competitorProgress.Rows.Add(competitorNotStarted.ToArray());
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
