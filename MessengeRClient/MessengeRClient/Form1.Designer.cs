namespace MessengeRClient
{
    partial class Form1
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
            this.memoEditLog = new DevExpress.XtraEditors.MemoEdit();
            this.memoEditEnterMsg = new DevExpress.XtraEditors.MemoEdit();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditEnterMsg.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEditLog
            // 
            this.memoEditLog.Location = new System.Drawing.Point(13, 56);
            this.memoEditLog.Name = "memoEditLog";
            this.memoEditLog.Size = new System.Drawing.Size(309, 243);
            this.memoEditLog.TabIndex = 1;
            this.memoEditLog.UseOptimizedRendering = true;
            // 
            // memoEditEnterMsg
            // 
            this.memoEditEnterMsg.Location = new System.Drawing.Point(13, 12);
            this.memoEditEnterMsg.Name = "memoEditEnterMsg";
            this.memoEditEnterMsg.Size = new System.Drawing.Size(253, 38);
            this.memoEditEnterMsg.TabIndex = 2;
            this.memoEditEnterMsg.UseOptimizedRendering = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(273, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.memoEditEnterMsg);
            this.Controls.Add(this.memoEditLog);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximumSize = new System.Drawing.Size(350, 350);
            this.MinimumSize = new System.Drawing.Size(350, 350);
            this.Name = "Form1";
            this.Text = "MessengeR Chat Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditEnterMsg.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEditLog;
        private DevExpress.XtraEditors.MemoEdit memoEditEnterMsg;
        private System.Windows.Forms.Button button1;
    }
}

