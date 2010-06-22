using System.Windows.Forms;
using System.Reflection;

namespace Reverb
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
            this.playlist_add = new System.Windows.Forms.Button();
            this.peerlist = new System.Windows.Forms.ListBox();
            this.peerlist_add = new System.Windows.Forms.Button();
            this.playlist_remove = new System.Windows.Forms.Button();
            this.playlist_play = new System.Windows.Forms.Button();
            this.playlist_stop = new System.Windows.Forms.Button();
            this.peertoconnect = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.playlist = new System.Windows.Forms.DataGridView();
            this.tesszt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teszt2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.time_position = new System.Windows.Forms.Label();
            this.seekBar = new System.Windows.Forms.ProgressBar();
            this.time_length = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.playlist_pause = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // playlist_add
            // 
            this.playlist_add.Location = new System.Drawing.Point(5, 339);
            this.playlist_add.Name = "playlist_add";
            this.playlist_add.Size = new System.Drawing.Size(37, 23);
            this.playlist_add.TabIndex = 4;
            this.playlist_add.Text = "Add";
            this.playlist_add.UseVisualStyleBackColor = true;
            this.playlist_add.Click += new System.EventHandler(this.playlist_add_Click);
            // 
            // peerlist
            // 
            this.peerlist.BackColor = System.Drawing.SystemColors.Control;
            this.peerlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peerlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.peerlist.FormattingEnabled = true;
            this.peerlist.ItemHeight = 12;
            this.peerlist.Items.AddRange(new object[] {
            ""});
            this.peerlist.Location = new System.Drawing.Point(6, 13);
            this.peerlist.Name = "peerlist";
            this.peerlist.Size = new System.Drawing.Size(125, 168);
            this.peerlist.TabIndex = 7;
            // 
            // peerlist_add
            // 
            this.peerlist_add.Location = new System.Drawing.Point(24, 39);
            this.peerlist_add.Name = "peerlist_add";
            this.peerlist_add.Size = new System.Drawing.Size(88, 20);
            this.peerlist_add.TabIndex = 8;
            this.peerlist_add.Text = "Connect";
            this.peerlist_add.UseVisualStyleBackColor = true;
            this.peerlist_add.Click += new System.EventHandler(this.peerlist_add_Click);
            // 
            // playlist_remove
            // 
            this.playlist_remove.Location = new System.Drawing.Point(129, 339);
            this.playlist_remove.Name = "playlist_remove";
            this.playlist_remove.Size = new System.Drawing.Size(56, 23);
            this.playlist_remove.TabIndex = 11;
            this.playlist_remove.Text = "Remove";
            this.playlist_remove.UseVisualStyleBackColor = true;
            this.playlist_remove.Click += new System.EventHandler(this.playlist_remove_Click);
            // 
            // playlist_play
            // 
            this.playlist_play.Location = new System.Drawing.Point(181, 84);
            this.playlist_play.Name = "playlist_play";
            this.playlist_play.Size = new System.Drawing.Size(46, 23);
            this.playlist_play.TabIndex = 12;
            this.playlist_play.Text = "Play";
            this.playlist_play.UseVisualStyleBackColor = true;
            this.playlist_play.Click += new System.EventHandler(this.playlist_play_Click);
            // 
            // playlist_stop
            // 
            this.playlist_stop.Location = new System.Drawing.Point(129, 84);
            this.playlist_stop.Name = "playlist_stop";
            this.playlist_stop.Size = new System.Drawing.Size(46, 23);
            this.playlist_stop.TabIndex = 13;
            this.playlist_stop.Text = "Stop";
            this.playlist_stop.UseVisualStyleBackColor = true;
            this.playlist_stop.Click += new System.EventHandler(this.playlist_stop_Click);
            // 
            // peertoconnect
            // 
            this.peertoconnect.Location = new System.Drawing.Point(6, 13);
            this.peertoconnect.Name = "peertoconnect";
            this.peertoconnect.Size = new System.Drawing.Size(125, 20);
            this.peertoconnect.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.peertoconnect);
            this.groupBox1.Controls.Add(this.peerlist_add);
            this.groupBox1.Location = new System.Drawing.Point(201, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 65);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect to IP:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.peerlist);
            this.groupBox2.Location = new System.Drawing.Point(201, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 191);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Peerlist";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.playlist_add);
            this.groupBox3.Controls.Add(this.playlist);
            this.groupBox3.Controls.Add(this.playlist_remove);
            this.groupBox3.Location = new System.Drawing.Point(4, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(191, 368);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Playlist";
            // 
            // playlist
            // 
            this.playlist.AllowUserToAddRows = false;
            this.playlist.AllowUserToResizeColumns = false;
            this.playlist.AllowUserToResizeRows = false;
            this.playlist.BackgroundColor = System.Drawing.Color.White;
            this.playlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.playlist.ColumnHeadersVisible = false;
            this.playlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tesszt,
            this.teszt2});
            this.playlist.Location = new System.Drawing.Point(5, 18);
            this.playlist.MultiSelect = false;
            this.playlist.Name = "playlist";
            this.playlist.ReadOnly = true;
            this.playlist.RowHeadersVisible = false;
            this.playlist.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.playlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.playlist.Size = new System.Drawing.Size(180, 315);
            this.playlist.TabIndex = 23;
            this.playlist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseDoubleClick);
            // 
            // tesszt
            // 
            this.tesszt.Frozen = true;
            this.tesszt.HeaderText = "Column1";
            this.tesszt.Name = "tesszt";
            this.tesszt.ReadOnly = true;
            this.tesszt.Width = 125;
            // 
            // teszt2
            // 
            this.teszt2.Frozen = true;
            this.teszt2.HeaderText = "Column1";
            this.teszt2.Name = "teszt2";
            this.teszt2.ReadOnly = true;
            this.teszt2.Width = 35;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(64, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Previous";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // time_position
            // 
            this.time_position.AutoSize = true;
            this.time_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.time_position.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.time_position.Location = new System.Drawing.Point(-1, 61);
            this.time_position.Name = "time_position";
            this.time_position.Size = new System.Drawing.Size(54, 20);
            this.time_position.TabIndex = 19;
            this.time_position.Text = "00:00";
            // 
            // seekBar
            // 
            this.seekBar.Location = new System.Drawing.Point(52, 62);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(250, 18);
            this.seekBar.TabIndex = 20;
            this.seekBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.progressBar1_MouseClick);
            // 
            // time_length
            // 
            this.time_length.AutoSize = true;
            this.time_length.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.time_length.Location = new System.Drawing.Point(301, 61);
            this.time_length.Name = "time_length";
            this.time_length.Size = new System.Drawing.Size(54, 20);
            this.time_length.TabIndex = 21;
            this.time_length.Text = "00:00";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(233, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Next";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // playlist_pause
            // 
            this.playlist_pause.Location = new System.Drawing.Point(181, 84);
            this.playlist_pause.Name = "playlist_pause";
            this.playlist_pause.Size = new System.Drawing.Size(46, 23);
            this.playlist_pause.TabIndex = 25;
            this.playlist_pause.Text = "Pause";
            this.playlist_pause.UseVisualStyleBackColor = true;
            this.playlist_pause.Visible = false;
            this.playlist_pause.Click += new System.EventHandler(this.playlist_pause_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(7, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(341, 44);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 484);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.playlist_play);
            this.Controls.Add(this.time_length);
            this.Controls.Add(this.playlist_stop);
            this.Controls.Add(this.time_position);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.playlist_pause);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Reverb v0.01";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playlist_add;
        private ListBox peerlist;
        private Button peerlist_add;
        private Button playlist_remove;
        private Button playlist_play;
        private Button playlist_stop;
        private TextBox peertoconnect;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button button1;
        private Label time_position;
        private ProgressBar seekBar;
        private Label time_length;
        private Button button3;
        private DataGridView playlist;
        private DataGridViewTextBoxColumn tesszt;
        private DataGridViewTextBoxColumn teszt2;
        private Button playlist_pause;
        private PictureBox pictureBox1;
    }
}

