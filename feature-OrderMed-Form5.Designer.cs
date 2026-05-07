namespace from1INTRO
{
    partial class OrderMed_Form5
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

        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvBatches = new System.Windows.Forms.DataGridView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnButton = new System.Windows.Forms.Button();
            this.InfoBX = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMed = new System.Windows.Forms.ComboBox();
            this.txtMedBatch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).BeginInit();
            this.InfoBX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Medstock";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Medication - Batches - Expiry";
            // 
            // dgvBatches
            // 
            this.dgvBatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBatches.Location = new System.Drawing.Point(452, 101);
            this.dgvBatches.Margin = new System.Windows.Forms.Padding(4);
            this.dgvBatches.Name = "dgvBatches";
            this.dgvBatches.RowHeadersWidth = 51;
            this.dgvBatches.Size = new System.Drawing.Size(315, 330);
            this.dgvBatches.TabIndex = 20;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(452, 65);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(80, 28);
            this.btnCreate.TabIndex = 19;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(694, 65);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 28);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(540, 65);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(73, 28);
            this.btnRead.TabIndex = 17;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            // 
            // btnButton
            // 
            this.btnButton.Location = new System.Drawing.Point(622, 65);
            this.btnButton.Margin = new System.Windows.Forms.Padding(4);
            this.btnButton.Name = "btnButton";
            this.btnButton.Size = new System.Drawing.Size(64, 28);
            this.btnButton.TabIndex = 16;
            this.btnButton.Text = "Update";
            this.btnButton.UseVisualStyleBackColor = true;
            // 
            // InfoBX
            // 
            this.InfoBX.Controls.Add(this.label1);
            this.InfoBX.Controls.Add(this.label3);
            this.InfoBX.Controls.Add(this.label4);
            this.InfoBX.Controls.Add(this.numStock);
            this.InfoBX.Controls.Add(this.dtpExpiry);
            this.InfoBX.Controls.Add(this.label2);
            this.InfoBX.Controls.Add(this.cboMed);
            this.InfoBX.Controls.Add(this.txtMedBatch);
            this.InfoBX.Location = new System.Drawing.Point(38, 101);
            this.InfoBX.Margin = new System.Windows.Forms.Padding(4);
            this.InfoBX.Name = "InfoBX";
            this.InfoBX.Padding = new System.Windows.Forms.Padding(4);
            this.InfoBX.Size = new System.Drawing.Size(379, 330);
            this.InfoBX.TabIndex = 21;
            this.InfoBX.TabStop = false;
            this.InfoBX.Text = "Information ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter the batch number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 260);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Set stock number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pick expiry date";
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(11, 279);
            this.numStock.Margin = new System.Windows.Forms.Padding(4);
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(160, 22);
            this.numStock.TabIndex = 8;
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Location = new System.Drawing.Point(11, 137);
            this.dtpExpiry.Margin = new System.Windows.Forms.Padding(4);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(265, 22);
            this.dtpExpiry.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 193);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Choose medication name";
            // 
            // cboMed
            // 
            this.cboMed.FormattingEnabled = true;
            this.cboMed.Location = new System.Drawing.Point(11, 213);
            this.cboMed.Margin = new System.Windows.Forms.Padding(4);
            this.cboMed.Name = "cboMed";
            this.cboMed.Size = new System.Drawing.Size(160, 24);
            this.cboMed.TabIndex = 6;
            // 
            // txtMedBatch
            // 
            this.txtMedBatch.Location = new System.Drawing.Point(11, 69);
            this.txtMedBatch.Margin = new System.Windows.Forms.Padding(4);
            this.txtMedBatch.Name = "txtMedBatch";
            this.txtMedBatch.Size = new System.Drawing.Size(132, 22);
            this.txtMedBatch.TabIndex = 4;
            // 
            // OrderMed_Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvBatches);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnButton);
            this.Controls.Add(this.InfoBX);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OrderMed_Form5";
            this.Text = "Order Medication";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).EndInit();
            this.InfoBX.ResumeLayout(false);
            this.InfoBX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvBatches;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnButton;
        private System.Windows.Forms.GroupBox InfoBX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numStock;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMed;
        private System.Windows.Forms.TextBox txtMedBatch;
    }
}
