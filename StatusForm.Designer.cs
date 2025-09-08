namespace MunicipalServiceApp
{
    partial class StatusForm
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
            this.listReports = new System.Windows.Forms.ListBox();
            this.btn_Main = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listReports
            // 
            this.listReports.FormattingEnabled = true;
            this.listReports.ItemHeight = 16;
            this.listReports.Location = new System.Drawing.Point(51, 61);
            this.listReports.Name = "listReports";
            this.listReports.Size = new System.Drawing.Size(713, 308);
            this.listReports.TabIndex = 0;
            // 
            // btn_Main
            // 
            this.btn_Main.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Main.Location = new System.Drawing.Point(770, 152);
            this.btn_Main.Name = "btn_Main";
            this.btn_Main.Size = new System.Drawing.Size(153, 48);
            this.btn_Main.TabIndex = 1;
            this.btn_Main.Text = "Back to Main";
            this.btn_Main.UseVisualStyleBackColor = false;
            this.btn_Main.Click += new System.EventHandler(this.btn_Main_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Red;
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(770, 83);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(142, 47);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "Delete Issue";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // StatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(924, 558);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Main);
            this.Controls.Add(this.listReports);
            this.Name = "StatusForm";
            this.Text = "StatusForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listReports;
        private System.Windows.Forms.Button btn_Main;
        private System.Windows.Forms.Button btn_Delete;
    }
}