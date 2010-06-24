namespace Reverb
{
    partial class Form2
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.peerlist = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.peertoconnect = new System.Windows.Forms.TextBox();
            this.peerlist_add = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.peerlist);
            this.groupBox2.Location = new System.Drawing.Point(1, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 191);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Peerlist";
            // 
            // peerlist
            // 
            this.peerlist.BackColor = System.Drawing.SystemColors.Control;
            this.peerlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peerlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.peerlist.FormattingEnabled = true;
            this.peerlist.ItemHeight = 12;
            this.peerlist.Location = new System.Drawing.Point(6, 13);
            this.peerlist.Name = "peerlist";
            this.peerlist.Size = new System.Drawing.Size(125, 168);
            this.peerlist.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.peertoconnect);
            this.groupBox1.Controls.Add(this.peerlist_add);
            this.groupBox1.Location = new System.Drawing.Point(1, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 65);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to IP:";
            // 
            // peertoconnect
            // 
            this.peertoconnect.Location = new System.Drawing.Point(6, 13);
            this.peertoconnect.Name = "peertoconnect";
            this.peertoconnect.Size = new System.Drawing.Size(125, 20);
            this.peertoconnect.TabIndex = 14;
            // 
            // peerlist_add
            // 
            this.peerlist_add.Location = new System.Drawing.Point(24, 39);
            this.peerlist_add.Name = "peerlist_add";
            this.peerlist_add.Size = new System.Drawing.Size(88, 20);
            this.peerlist_add.TabIndex = 8;
            this.peerlist_add.Text = "Connect";
            this.peerlist_add.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 267);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Hallgatók";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox peerlist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox peertoconnect;
        private System.Windows.Forms.Button peerlist_add;
    }
}