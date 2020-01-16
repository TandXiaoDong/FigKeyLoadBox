namespace LoadBoxControl
{
    partial class ShowCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowCommand));
            this.tbInput = new Telerik.WinControls.UI.RadTextBox();
            this.office2010BlackTheme1 = new Telerik.WinControls.Themes.Office2010BlackTheme();
            ((System.ComponentModel.ISupportInitialize)(this.tbInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tbInput
            // 
            this.tbInput.AutoScroll = true;
            this.tbInput.BackColor = System.Drawing.Color.Black;
            this.tbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.ForeColor = System.Drawing.Color.White;
            this.tbInput.Location = new System.Drawing.Point(0, 0);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            // 
            // 
            // 
            this.tbInput.RootElement.StretchVertically = true;
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInput.Size = new System.Drawing.Size(893, 470);
            this.tbInput.TabIndex = 0;
            this.tbInput.ThemeName = "Office2010Black";
            // 
            // ShowCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 470);
            this.Controls.Add(this.tbInput);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowCommand";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "自动发送";
            this.ThemeName = "Office2010Black";
            this.Load += new System.EventHandler(this.ShowCommand_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox tbInput;
        private Telerik.WinControls.Themes.Office2010BlackTheme office2010BlackTheme1;
    }
}
