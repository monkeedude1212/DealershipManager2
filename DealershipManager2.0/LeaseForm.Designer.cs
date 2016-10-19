namespace DealershipManager2._0
{
    partial class LeaseForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIncludeGST = new System.Windows.Forms.CheckBox();
            this.txtNumberOfMonths = new System.Windows.Forms.TextBox();
            this.txtInterestRate = new System.Windows.Forms.TextBox();
            this.txtCapitalizedCost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbAnnually = new System.Windows.Forms.RadioButton();
            this.rdbMonthly = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtResidualValue = new System.Windows.Forms.TextBox();
            this.txtMonthlyPayment = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCalculateResidualValue = new System.Windows.Forms.Button();
            this.btnCalculateMonthlyPayment = new System.Windows.Forms.Button();
            this.dgvLeaseInfo = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalInterest = new System.Windows.Forms.TextBox();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.btnSavePaymentInfo = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaseInfo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkIncludeGST);
            this.groupBox1.Controls.Add(this.txtNumberOfMonths);
            this.groupBox1.Controls.Add(this.txtInterestRate);
            this.groupBox1.Controls.Add(this.txtCapitalizedCost);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdbAnnually);
            this.groupBox1.Controls.Add(this.rdbMonthly);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 291);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculate Monthly Payment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Number of Months:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(69, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Capitalized Cost:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Interest Rate:";
            // 
            // chkIncludeGST
            // 
            this.chkIncludeGST.AutoSize = true;
            this.chkIncludeGST.Checked = true;
            this.chkIncludeGST.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeGST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeGST.Location = new System.Drawing.Point(160, 199);
            this.chkIncludeGST.Name = "chkIncludeGST";
            this.chkIncludeGST.Size = new System.Drawing.Size(86, 17);
            this.chkIncludeGST.TabIndex = 6;
            this.chkIncludeGST.Text = "Include GST";
            this.chkIncludeGST.UseVisualStyleBackColor = true;
            // 
            // txtNumberOfMonths
            // 
            this.txtNumberOfMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfMonths.Location = new System.Drawing.Point(160, 147);
            this.txtNumberOfMonths.Name = "txtNumberOfMonths";
            this.txtNumberOfMonths.Size = new System.Drawing.Size(123, 20);
            this.txtNumberOfMonths.TabIndex = 5;
            // 
            // txtInterestRate
            // 
            this.txtInterestRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterestRate.Location = new System.Drawing.Point(160, 121);
            this.txtInterestRate.Name = "txtInterestRate";
            this.txtInterestRate.Size = new System.Drawing.Size(123, 20);
            this.txtInterestRate.TabIndex = 4;
            // 
            // txtCapitalizedCost
            // 
            this.txtCapitalizedCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapitalizedCost.Location = new System.Drawing.Point(160, 95);
            this.txtCapitalizedCost.Name = "txtCapitalizedCost";
            this.txtCapitalizedCost.Size = new System.Drawing.Size(123, 20);
            this.txtCapitalizedCost.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Calculate Interest";
            // 
            // rdbAnnually
            // 
            this.rdbAnnually.AutoSize = true;
            this.rdbAnnually.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbAnnually.Location = new System.Drawing.Point(160, 42);
            this.rdbAnnually.Name = "rdbAnnually";
            this.rdbAnnually.Size = new System.Drawing.Size(65, 17);
            this.rdbAnnually.TabIndex = 1;
            this.rdbAnnually.Text = "Annually";
            this.rdbAnnually.UseVisualStyleBackColor = true;
            // 
            // rdbMonthly
            // 
            this.rdbMonthly.AutoSize = true;
            this.rdbMonthly.Checked = true;
            this.rdbMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMonthly.Location = new System.Drawing.Point(160, 19);
            this.rdbMonthly.Name = "rdbMonthly";
            this.rdbMonthly.Size = new System.Drawing.Size(62, 17);
            this.rdbMonthly.TabIndex = 0;
            this.rdbMonthly.TabStop = true;
            this.rdbMonthly.Text = "Monthly";
            this.rdbMonthly.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtResidualValue);
            this.groupBox2.Controls.Add(this.txtMonthlyPayment);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 243);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtResidualValue
            // 
            this.txtResidualValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResidualValue.Location = new System.Drawing.Point(160, 96);
            this.txtResidualValue.Name = "txtResidualValue";
            this.txtResidualValue.Size = new System.Drawing.Size(123, 20);
            this.txtResidualValue.TabIndex = 11;
            // 
            // txtMonthlyPayment
            // 
            this.txtMonthlyPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonthlyPayment.Location = new System.Drawing.Point(160, 54);
            this.txtMonthlyPayment.Name = "txtMonthlyPayment";
            this.txtMonthlyPayment.Size = new System.Drawing.Size(123, 20);
            this.txtMonthlyPayment.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(73, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Residual Value:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(63, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Monthly Payment:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCalculateResidualValue);
            this.groupBox3.Controls.Add(this.btnCalculateMonthlyPayment);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 575);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(337, 134);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnCalculateResidualValue
            // 
            this.btnCalculateResidualValue.Location = new System.Drawing.Point(168, 46);
            this.btnCalculateResidualValue.Name = "btnCalculateResidualValue";
            this.btnCalculateResidualValue.Size = new System.Drawing.Size(115, 39);
            this.btnCalculateResidualValue.TabIndex = 1;
            this.btnCalculateResidualValue.Text = "Calculate Residual Value";
            this.btnCalculateResidualValue.UseVisualStyleBackColor = true;
            this.btnCalculateResidualValue.Click += new System.EventHandler(this.btnCalculateResidualValue_Click);
            // 
            // btnCalculateMonthlyPayment
            // 
            this.btnCalculateMonthlyPayment.Location = new System.Drawing.Point(6, 46);
            this.btnCalculateMonthlyPayment.Name = "btnCalculateMonthlyPayment";
            this.btnCalculateMonthlyPayment.Size = new System.Drawing.Size(115, 39);
            this.btnCalculateMonthlyPayment.TabIndex = 0;
            this.btnCalculateMonthlyPayment.Text = "Calculate Monthly Payment";
            this.btnCalculateMonthlyPayment.UseVisualStyleBackColor = true;
            this.btnCalculateMonthlyPayment.Click += new System.EventHandler(this.btnCalculateMonthlyPayment_Click);
            // 
            // dgvLeaseInfo
            // 
            this.dgvLeaseInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeaseInfo.Location = new System.Drawing.Point(355, 30);
            this.dgvLeaseInfo.Name = "dgvLeaseInfo";
            this.dgvLeaseInfo.Size = new System.Drawing.Size(613, 533);
            this.dgvLeaseInfo.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtTotalInterest);
            this.groupBox4.Controls.Add(this.btnPrintReport);
            this.groupBox4.Controls.Add(this.btnSavePaymentInfo);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(355, 575);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(613, 134);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(150, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total Interest:";
            // 
            // txtTotalInterest
            // 
            this.txtTotalInterest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalInterest.Location = new System.Drawing.Point(228, 56);
            this.txtTotalInterest.Name = "txtTotalInterest";
            this.txtTotalInterest.Size = new System.Drawing.Size(124, 20);
            this.txtTotalInterest.TabIndex = 5;
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.Location = new System.Drawing.Point(371, 46);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(115, 39);
            this.btnPrintReport.TabIndex = 4;
            this.btnPrintReport.Text = "Print Report";
            this.btnPrintReport.UseVisualStyleBackColor = true;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // btnSavePaymentInfo
            // 
            this.btnSavePaymentInfo.Location = new System.Drawing.Point(492, 46);
            this.btnSavePaymentInfo.Name = "btnSavePaymentInfo";
            this.btnSavePaymentInfo.Size = new System.Drawing.Size(115, 39);
            this.btnSavePaymentInfo.TabIndex = 3;
            this.btnSavePaymentInfo.Text = "Save Payment Info";
            this.btnSavePaymentInfo.UseVisualStyleBackColor = true;
            this.btnSavePaymentInfo.Click += new System.EventHandler(this.btnSavePaymentInfo_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 46);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 39);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // LeaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dgvLeaseInfo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LeaseForm";
            this.Text = "LeaseForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LeaseForm_FormClosing);
            this.Load += new System.EventHandler(this.LeaseForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaseInfo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvLeaseInfo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIncludeGST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResidualValue;
        private System.Windows.Forms.Button btnCalculateResidualValue;
        private System.Windows.Forms.Button btnCalculateMonthlyPayment;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSavePaymentInfo;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.TextBox txtTotalInterest;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.RadioButton rdbMonthly;
        public System.Windows.Forms.RadioButton rdbAnnually;
        public System.Windows.Forms.TextBox txtNumberOfMonths;
        public System.Windows.Forms.TextBox txtInterestRate;
        public System.Windows.Forms.TextBox txtCapitalizedCost;
        public System.Windows.Forms.TextBox txtMonthlyPayment;
    }
}