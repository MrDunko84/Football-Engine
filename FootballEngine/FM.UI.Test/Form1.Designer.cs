namespace FM.UI.Test
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
            this.components = new System.ComponentModel.Container();
            this.timerPaint = new System.Windows.Forms.Timer(this.components);
            this.chkFillRegions = new System.Windows.Forms.CheckBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.cmdSet = new System.Windows.Forms.Button();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.cmdSpeed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerPaint
            // 
            this.timerPaint.Enabled = true;
            this.timerPaint.Interval = 500;
            this.timerPaint.Tick += new System.EventHandler(this.timerPaint_Tick);
            // 
            // chkFillRegions
            // 
            this.chkFillRegions.AutoSize = true;
            this.chkFillRegions.Location = new System.Drawing.Point(831, 66);
            this.chkFillRegions.Name = "chkFillRegions";
            this.chkFillRegions.Size = new System.Drawing.Size(117, 24);
            this.chkFillRegions.TabIndex = 0;
            this.chkFillRegions.Text = "Fill Regions";
            this.chkFillRegions.UseVisualStyleBackColor = true;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(831, 112);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(51, 26);
            this.txtX.TabIndex = 1;
            this.txtX.Text = "234";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(888, 112);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(51, 26);
            this.txtY.TabIndex = 2;
            this.txtY.Text = "50";
            // 
            // cmdSet
            // 
            this.cmdSet.Location = new System.Drawing.Point(945, 104);
            this.cmdSet.Name = "cmdSet";
            this.cmdSet.Size = new System.Drawing.Size(75, 42);
            this.cmdSet.TabIndex = 3;
            this.cmdSet.Text = "Set";
            this.cmdSet.UseVisualStyleBackColor = true;
            this.cmdSet.Click += new System.EventHandler(this.cmdSet_Click);
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(831, 187);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(100, 26);
            this.txtSpeed.TabIndex = 4;
            this.txtSpeed.Text = "4";
            // 
            // cmdSpeed
            // 
            this.cmdSpeed.Location = new System.Drawing.Point(945, 179);
            this.cmdSpeed.Name = "cmdSpeed";
            this.cmdSpeed.Size = new System.Drawing.Size(75, 42);
            this.cmdSpeed.TabIndex = 5;
            this.cmdSpeed.Text = "Set";
            this.cmdSpeed.UseVisualStyleBackColor = true;
            this.cmdSpeed.Click += new System.EventHandler(this.cmdSpeed_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 1093);
            this.Controls.Add(this.cmdSpeed);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.cmdSet);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.chkFillRegions);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_OnPaint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerPaint;
        private System.Windows.Forms.CheckBox chkFillRegions;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button cmdSet;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.Button cmdSpeed;
    }
}

