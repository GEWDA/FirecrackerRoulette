namespace FirecrackerRoulette
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
            this.testButton1 = new System.Windows.Forms.Button();
            this.testButton2 = new System.Windows.Forms.Button();
            this.cbxLethal = new System.Windows.Forms.ComboBox();
            this.cbxFirecrackers = new System.Windows.Forms.ComboBox();
            this.labelExplosives = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSpeedUp = new System.Windows.Forms.Button();
            this.btnAvoid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testButton1
            // 
            this.testButton1.Location = new System.Drawing.Point(799, 12);
            this.testButton1.Name = "testButton1";
            this.testButton1.Size = new System.Drawing.Size(75, 23);
            this.testButton1.TabIndex = 0;
            this.testButton1.Text = "Explosion";
            this.testButton1.UseVisualStyleBackColor = true;
            this.testButton1.Click += new System.EventHandler(this.testButtons_Click);
            // 
            // testButton2
            // 
            this.testButton2.Location = new System.Drawing.Point(799, 41);
            this.testButton2.Name = "testButton2";
            this.testButton2.Size = new System.Drawing.Size(75, 23);
            this.testButton2.TabIndex = 1;
            this.testButton2.Text = "Firecracker";
            this.testButton2.UseVisualStyleBackColor = true;
            this.testButton2.Click += new System.EventHandler(this.testButtons_Click);
            // 
            // cbxLethal
            // 
            this.cbxLethal.FormattingEnabled = true;
            this.cbxLethal.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbxLethal.Location = new System.Drawing.Point(30, 26);
            this.cbxLethal.Name = "cbxLethal";
            this.cbxLethal.Size = new System.Drawing.Size(43, 21);
            this.cbxLethal.TabIndex = 2;
            // 
            // cbxFirecrackers
            // 
            this.cbxFirecrackers.FormattingEnabled = true;
            this.cbxFirecrackers.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbxFirecrackers.Location = new System.Drawing.Point(131, 26);
            this.cbxFirecrackers.Name = "cbxFirecrackers";
            this.cbxFirecrackers.Size = new System.Drawing.Size(43, 21);
            this.cbxFirecrackers.TabIndex = 3;
            // 
            // labelExplosives
            // 
            this.labelExplosives.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelExplosives.Location = new System.Drawing.Point(-7, 0);
            this.labelExplosives.Name = "labelExplosives";
            this.labelExplosives.Size = new System.Drawing.Size(121, 23);
            this.labelExplosives.TabIndex = 4;
            this.labelExplosives.Text = "Explosives";
            this.labelExplosives.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(91, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Firecrackers";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(200, 0);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 49);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(281, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 49);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSpeedUp
            // 
            this.btnSpeedUp.Location = new System.Drawing.Point(38, 0);
            this.btnSpeedUp.Name = "btnSpeedUp";
            this.btnSpeedUp.Size = new System.Drawing.Size(75, 49);
            this.btnSpeedUp.TabIndex = 8;
            this.btnSpeedUp.TabStop = false;
            this.btnSpeedUp.Text = ">>";
            this.btnSpeedUp.UseVisualStyleBackColor = true;
            // 
            // btnAvoid
            // 
            this.btnAvoid.Location = new System.Drawing.Point(119, 0);
            this.btnAvoid.Name = "btnAvoid";
            this.btnAvoid.Size = new System.Drawing.Size(75, 49);
            this.btnAvoid.TabIndex = 9;
            this.btnAvoid.TabStop = false;
            this.btnAvoid.Text = "Drop";
            this.btnAvoid.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelExplosives);
            this.Controls.Add(this.cbxFirecrackers);
            this.Controls.Add(this.cbxLethal);
            this.Controls.Add(this.testButton2);
            this.Controls.Add(this.testButton1);
            this.Controls.Add(this.btnSpeedUp);
            this.Controls.Add(this.btnAvoid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testButton1;
        private System.Windows.Forms.Button testButton2;
        private System.Windows.Forms.ComboBox cbxLethal;
        private System.Windows.Forms.ComboBox cbxFirecrackers;
        private System.Windows.Forms.Label labelExplosives;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSpeedUp;
        private System.Windows.Forms.Button btnAvoid;
    }
}

