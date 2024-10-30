namespace SoundAnomalyDetection
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
            btnDetect = new Button();
            SuspendLayout();
            // 
            // btnDetect
            // 
            btnDetect.Location = new Point(655, 30);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new Size(105, 36);
            btnDetect.TabIndex = 0;
            btnDetect.Text = "Detect";
            btnDetect.UseVisualStyleBackColor = true;
            btnDetect.Click += btnDetect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDetect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnDetect;
    }
}
