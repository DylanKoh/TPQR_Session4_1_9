namespace TPQR_Session4_1_9
{
    partial class UpdateExpert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbName = new System.Windows.Forms.RadioButton();
            this.rbProgress = new System.Windows.Forms.RadioButton();
            this.cbSkill = new System.Windows.Forms.ComboBox();
            this.cbExpertName = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1010, 74);
            this.panel1.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 16);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(92, 42);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(767, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASEAN Skills 2020\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 627);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 59);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14F);
            this.label2.Location = new System.Drawing.Point(304, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(389, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Update Expert Training Records";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Skill: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Expert\'s Name: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sort By: ";
            // 
            // rbName
            // 
            this.rbName.AutoSize = true;
            this.rbName.Location = new System.Drawing.Point(366, 278);
            this.rbName.Name = "rbName";
            this.rbName.Size = new System.Drawing.Size(91, 29);
            this.rbName.TabIndex = 11;
            this.rbName.TabStop = true;
            this.rbName.Text = "Name";
            this.rbName.UseVisualStyleBackColor = true;
            this.rbName.CheckedChanged += new System.EventHandler(this.rbName_CheckedChanged);
            // 
            // rbProgress
            // 
            this.rbProgress.AutoSize = true;
            this.rbProgress.Location = new System.Drawing.Point(463, 278);
            this.rbProgress.Name = "rbProgress";
            this.rbProgress.Size = new System.Drawing.Size(119, 29);
            this.rbProgress.TabIndex = 12;
            this.rbProgress.TabStop = true;
            this.rbProgress.Text = "Progress";
            this.rbProgress.UseVisualStyleBackColor = true;
            this.rbProgress.CheckedChanged += new System.EventHandler(this.rbProgress_CheckedChanged);
            // 
            // cbSkill
            // 
            this.cbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkill.FormattingEnabled = true;
            this.cbSkill.Location = new System.Drawing.Point(366, 157);
            this.cbSkill.Name = "cbSkill";
            this.cbSkill.Size = new System.Drawing.Size(293, 33);
            this.cbSkill.TabIndex = 13;
            this.cbSkill.SelectedIndexChanged += new System.EventHandler(this.cbSkill_SelectedIndexChanged);
            // 
            // cbExpertName
            // 
            this.cbExpertName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExpertName.FormattingEnabled = true;
            this.cbExpertName.Location = new System.Drawing.Point(366, 221);
            this.cbExpertName.Name = "cbExpertName";
            this.cbExpertName.Size = new System.Drawing.Size(367, 33);
            this.cbExpertName.TabIndex = 14;
            this.cbExpertName.SelectedIndexChanged += new System.EventHandler(this.cbExpertName_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(12, 323);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(986, 220);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(859, 563);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(139, 57);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Training Module";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 183;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Duration (Days)";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 185;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Start Date";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 134;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Latest End Date";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 122;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Progress (%)";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.Width = 159;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "TrainingID";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 125;
            // 
            // UpdateExpert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 686);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbExpertName);
            this.Controls.Add(this.cbSkill);
            this.Controls.Add(this.rbProgress);
            this.Controls.Add(this.rbName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "UpdateExpert";
            this.Text = "Update Expert";
            this.Load += new System.EventHandler(this.UpdateExpert_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbName;
        private System.Windows.Forms.RadioButton rbProgress;
        private System.Windows.Forms.ComboBox cbSkill;
        private System.Windows.Forms.ComboBox cbExpertName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}