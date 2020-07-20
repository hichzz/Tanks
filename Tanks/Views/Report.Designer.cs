namespace Tanks
{
    partial class Report
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
            this.reportGridView = new System.Windows.Forms.DataGridView();
            this.NameObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.reportGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // reportGridView
            // 
            this.reportGridView.AllowUserToAddRows = false;
            this.reportGridView.AllowUserToDeleteRows = false;
            this.reportGridView.AllowUserToResizeColumns = false;
            this.reportGridView.AllowUserToResizeRows = false;
            this.reportGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameObject,
            this.X,
            this.Y});
            this.reportGridView.Enabled = false;
            this.reportGridView.Location = new System.Drawing.Point(12, 12);
            this.reportGridView.Name = "reportGridView";
            this.reportGridView.Size = new System.Drawing.Size(431, 593);
            this.reportGridView.TabIndex = 0;
            // 
            // NameObject
            // 
            this.NameObject.HeaderText = "Name";
            this.NameObject.Name = "NameObject";
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 617);
            this.Controls.Add(this.reportGridView);
            this.Name = "Report";
            this.Text = "Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Report_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.reportGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView reportGridView;
        public System.Windows.Forms.DataGridViewTextBoxColumn NameObject;
        public System.Windows.Forms.DataGridViewTextBoxColumn X;
        public System.Windows.Forms.DataGridViewTextBoxColumn Y;
    }
}