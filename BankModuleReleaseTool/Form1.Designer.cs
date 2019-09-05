namespace BankModuleReleaseTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnOpenConfigFile = new System.Windows.Forms.Button();
            this.txtResoursePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(50, 150);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(75, 23);
            this.btnRelease.TabIndex = 0;
            this.btnRelease.Text = "发布";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnOpenConfigFile
            // 
            this.btnOpenConfigFile.Location = new System.Drawing.Point(210, 150);
            this.btnOpenConfigFile.Name = "btnOpenConfigFile";
            this.btnOpenConfigFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenConfigFile.TabIndex = 1;
            this.btnOpenConfigFile.Text = "配置文件";
            this.btnOpenConfigFile.UseVisualStyleBackColor = true;
            // 
            // txtResoursePath
            // 
            this.txtResoursePath.Location = new System.Drawing.Point(126, 39);
            this.txtResoursePath.Name = "txtResoursePath";
            this.txtResoursePath.Size = new System.Drawing.Size(100, 21);
            this.txtResoursePath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "源文件目录：";
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Location = new System.Drawing.Point(126, 85);
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.Size = new System.Drawing.Size(100, 21);
            this.txtReleasePath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "发布文件目录：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 280);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReleasePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResoursePath);
            this.Controls.Add(this.btnOpenConfigFile);
            this.Controls.Add(this.btnRelease);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnOpenConfigFile;
        private System.Windows.Forms.TextBox txtResoursePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReleasePath;
        private System.Windows.Forms.Label label2;
    }
}

