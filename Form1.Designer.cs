namespace 只因了个只因
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            panel_result = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(194, 781);
            button1.Name = "button1";
            button1.Size = new Size(237, 68);
            button1.TabIndex = 0;
            button1.Text = "开始游戏";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel_result
            // 
            panel_result.BorderStyle = BorderStyle.FixedSingle;
            panel_result.Location = new Point(143, 722);
            panel_result.Name = "panel_result";
            panel_result.Size = new Size(337, 53);
            panel_result.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 861);
            Controls.Add(panel_result);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private FlowLayoutPanel panel_result;
    }
}