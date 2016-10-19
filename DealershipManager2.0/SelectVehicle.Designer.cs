namespace DealershipManager2._0
{
    partial class SelectVehicle
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
            this.gpbox = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddVehicle = new System.Windows.Forms.Button();
            this.lbxVehicleHistory = new System.Windows.Forms.ListBox();
            this.gpbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbox
            // 
            this.gpbox.Controls.Add(this.btnCancel);
            this.gpbox.Controls.Add(this.btnAddVehicle);
            this.gpbox.Controls.Add(this.lbxVehicleHistory);
            this.gpbox.Location = new System.Drawing.Point(12, 12);
            this.gpbox.Name = "gpbox";
            this.gpbox.Size = new System.Drawing.Size(410, 456);
            this.gpbox.TabIndex = 0;
            this.gpbox.TabStop = false;
            this.gpbox.Text = "Vehicle History";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(268, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddVehicle
            // 
            this.btnAddVehicle.Enabled = false;
            this.btnAddVehicle.Location = new System.Drawing.Point(16, 393);
            this.btnAddVehicle.Name = "btnAddVehicle";
            this.btnAddVehicle.Size = new System.Drawing.Size(118, 34);
            this.btnAddVehicle.TabIndex = 1;
            this.btnAddVehicle.Text = "Add vehicle";
            this.btnAddVehicle.UseVisualStyleBackColor = true;
            this.btnAddVehicle.Click += new System.EventHandler(this.btnAddVehicle_Click);
            // 
            // lbxVehicleHistory
            // 
            this.lbxVehicleHistory.FormattingEnabled = true;
            this.lbxVehicleHistory.Location = new System.Drawing.Point(16, 32);
            this.lbxVehicleHistory.Name = "lbxVehicleHistory";
            this.lbxVehicleHistory.Size = new System.Drawing.Size(370, 355);
            this.lbxVehicleHistory.TabIndex = 0;
            this.lbxVehicleHistory.SelectedIndexChanged += new System.EventHandler(this.lbxVehicleHistory_SelectedIndexChanged);
            this.lbxVehicleHistory.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lbxVehicleHistory_Format);
            // 
            // SelectVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 480);
            this.Controls.Add(this.gpbox);
            this.Name = "SelectVehicle";
            this.Text = "SelectVehicle";
            this.gpbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddVehicle;
        private System.Windows.Forms.ListBox lbxVehicleHistory;
    }
}