namespace from1INTRO
{
    partial class Reports_Form8
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reports_Form8));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chartAlerts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartMedUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chartResidentsRoom = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAlerts)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMedUsage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResidentsRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.groupBoxControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.splitContainer);
            this.pnlContent.Controls.Add(this.groupBoxControls);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1502, 800);
            this.pnlContent.TabIndex = 2;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 80);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvReport);
            this.splitContainer.Size = new System.Drawing.Size(1502, 720);
            this.splitContainer.SplitterDistance = 511;
            this.splitContainer.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chartAlerts);
            this.groupBox3.Location = new System.Drawing.Point(922, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(446, 410);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Emergency Alerts";
            // 
            // chartAlerts
            // 
            this.chartAlerts.Location = new System.Drawing.Point(6, 19);
            this.chartAlerts.Name = "chartAlerts";
            this.chartAlerts.Size = new System.Drawing.Size(420, 376);
            this.chartAlerts.TabIndex = 2;
            this.chartAlerts.Text = "AlertsByDate";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartMedUsage);
            this.groupBox2.Location = new System.Drawing.Point(470, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(446, 410);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Medication Stock ";
            // 
            // chartMedUsage
            // 
            this.chartMedUsage.Location = new System.Drawing.Point(6, 19);
            this.chartMedUsage.Name = "chartMedUsage";
            this.chartMedUsage.Size = new System.Drawing.Size(420, 376);
            this.chartMedUsage.TabIndex = 1;
            this.chartMedUsage.Text = "MedStock";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chartResidentsRoom);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 410);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Residents Per Room";
            // 
            // chartResidentsRoom
            // 
            this.chartResidentsRoom.Location = new System.Drawing.Point(6, 19);
            this.chartResidentsRoom.Name = "chartResidentsRoom";
            this.chartResidentsRoom.Size = new System.Drawing.Size(420, 376);
            this.chartResidentsRoom.TabIndex = 0;
            this.chartResidentsRoom.Text = "ResidentsPerRoom";
            // 
            // dgvReport
            // 
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.ColumnHeadersHeight = 29;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgvReport.Location = new System.Drawing.Point(0, 0);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1502, 205);
            this.dgvReport.TabIndex = 0;
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.cmbTable);
            this.groupBoxControls.Controls.Add(this.lblTable);
            this.groupBoxControls.Controls.Add(this.btnLoad);
            this.groupBoxControls.Controls.Add(this.btnExportCsv);
            this.groupBoxControls.Controls.Add(this.btnPrintPreview);
            this.groupBoxControls.Controls.Add(this.btnPrint);
            this.groupBoxControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBoxControls.Location = new System.Drawing.Point(0, 0);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(1502, 80);
            this.groupBoxControls.TabIndex = 1;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Reports & Actions";
            // 
            // cmbTable
            // 
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.Location = new System.Drawing.Point(100, 30);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(220, 28);
            this.cmbTable.TabIndex = 0;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(20, 33);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(101, 20);
            this.lblTable.TabIndex = 1;
            this.lblTable.Text = "Show Table:";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(340, 28);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 28);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load Reports";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(470, 28);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(120, 28);
            this.btnExportCsv.TabIndex = 3;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(720, 28);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(120, 28);
            this.btnPrintPreview.TabIndex = 4;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.BtnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(850, 28);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 28);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(800, 600);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // printDocument
            // 
            this.printDocument.DocumentName = "Reports Print";
            // 
            // Reports_Form8
            // 
            this.ClientSize = new System.Drawing.Size(1502, 800);
            this.Controls.Add(this.pnlContent);
            this.Name = "Reports_Form8";
            this.Text = "System Reports";
            this.pnlContent.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAlerts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMedUsage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResidentsRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResidentsRoom;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMedUsage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAlerts;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

        // designer helper to satisfy ResumeLayout calls (no-op)
        private System.Windows.Forms.GroupBox groupBox3;
    }
}


