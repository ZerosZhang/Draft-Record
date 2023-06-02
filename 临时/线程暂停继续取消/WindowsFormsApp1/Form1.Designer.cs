namespace WindowsFormsApp1
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
            this.button_run_stop = new System.Windows.Forms.Button();
            this.button_pause_continue = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_run_stop
            // 
            this.button_run_stop.Location = new System.Drawing.Point(64, 54);
            this.button_run_stop.Name = "button_run_stop";
            this.button_run_stop.Size = new System.Drawing.Size(169, 58);
            this.button_run_stop.TabIndex = 0;
            this.button_run_stop.Text = "自动运行";
            this.button_run_stop.UseVisualStyleBackColor = true;
            // 
            // button_pause_continue
            // 
            this.button_pause_continue.Location = new System.Drawing.Point(64, 209);
            this.button_pause_continue.Name = "button_pause_continue";
            this.button_pause_continue.Size = new System.Drawing.Size(169, 58);
            this.button_pause_continue.TabIndex = 1;
            this.button_pause_continue.Text = "暂停";
            this.button_pause_continue.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(64, 366);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(169, 58);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "停止";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 691);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_pause_continue);
            this.Controls.Add(this.button_run_stop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_run_stop;
        private System.Windows.Forms.Button button_pause_continue;
        private System.Windows.Forms.Button button_cancel;
    }
}

