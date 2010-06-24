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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.playlist = new System.Windows.Forms.DataGridView();
            this.asstring = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time_position = new System.Windows.Forms.Label();
            this.seekBar = new System.Windows.Forms.ProgressBar();
            this.time_length = new System.Windows.Forms.Label();
            this.open_peerlist = new System.Windows.Forms.Button();
            this.volumeBar = new System.Windows.Forms.ProgressBar();
            this.playlist_add = new System.Windows.Forms.Button();
            this.playlist_remove = new System.Windows.Forms.Button();
            this.playlist_forward = new System.Windows.Forms.Button();
            this.playlist_stop = new System.Windows.Forms.Button();
            this.playlist_play = new System.Windows.Forms.Button();
            this.playlist_back = new System.Windows.Forms.Button();
            this.playlist_pause = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playlist)).BeginInit();
            this.SuspendLayout();
            // 
            // playlist
            // 
            this.playlist.AllowUserToAddRows = false;
            this.playlist.AllowUserToResizeColumns = false;
            this.playlist.AllowUserToResizeRows = false;
            this.playlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.playlist.BackgroundColor = System.Drawing.Color.White;
            this.playlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.playlist.ColumnHeadersVisible = false;
            this.playlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.asstring,
            this.duration});
            this.playlist.Location = new System.Drawing.Point(3, 33);
            this.playlist.MultiSelect = false;
            this.playlist.Name = "playlist";
            this.playlist.ReadOnly = true;
            this.playlist.RowHeadersVisible = false;
            this.playlist.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.playlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.playlist.Size = new System.Drawing.Size(300, 315);
            this.playlist.TabIndex = 23;
            this.playlist.Scroll += new System.Windows.Forms.ScrollEventHandler(this.playlist_Scroll);
            this.playlist.SelectionChanged += new System.EventHandler(this.playlist_SelectionChanged);
            this.playlist.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.playlist_MouseDoubleClick);
            // 
            // asstring
            // 
            this.asstring.HeaderText = "Column1";
            this.asstring.Name = "asstring";
            this.asstring.ReadOnly = true;
            this.asstring.Width = 250;
            // 
            // duration
            // 
            this.duration.HeaderText = "Column1";
            this.duration.Name = "duration";
            this.duration.ReadOnly = true;
            this.duration.Width = 50;
            // 
            // time_position
            // 
            this.time_position.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.time_position.AutoSize = true;
            this.time_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.time_position.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.time_position.Location = new System.Drawing.Point(2, 367);
            this.time_position.Name = "time_position";
            this.time_position.Size = new System.Drawing.Size(54, 20);
            this.time_position.TabIndex = 19;
            this.time_position.Text = "00:00";
            // 
            // seekBar
            // 
            this.seekBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.seekBar.Location = new System.Drawing.Point(3, 354);
            this.seekBar.Name = "seekBar";
            this.seekBar.Size = new System.Drawing.Size(296, 10);
            this.seekBar.TabIndex = 20;
            this.seekBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.progressBar1_MouseClick);
            // 
            // time_length
            // 
            this.time_length.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.time_length.AutoSize = true;
            this.time_length.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.time_length.Location = new System.Drawing.Point(17, 385);
            this.time_length.Name = "time_length";
            this.time_length.Size = new System.Drawing.Size(38, 15);
            this.time_length.TabIndex = 21;
            this.time_length.Text = "00:00";
            // 
            // open_peerlist
            // 
            this.open_peerlist.AutoSize = true;
            this.open_peerlist.FlatAppearance.BorderSize = 0;
            this.open_peerlist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.open_peerlist.Location = new System.Drawing.Point(3, 4);
            this.open_peerlist.Name = "open_peerlist";
            this.open_peerlist.Size = new System.Drawing.Size(107, 23);
            this.open_peerlist.TabIndex = 27;
            this.open_peerlist.Text = "Hallgatók száma: 0";
            this.open_peerlist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.open_peerlist.UseVisualStyleBackColor = true;
            this.open_peerlist.Click += new System.EventHandler(this.open_peerlist_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeBar.Location = new System.Drawing.Point(199, 9);
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(100, 15);
            this.volumeBar.TabIndex = 28;
            this.volumeBar.Value = 50;
            this.volumeBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Click_VolumeBar);
            // 
            // playlist_add
            // 
            this.playlist_add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_add.FlatAppearance.BorderSize = 0;
            this.playlist_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_add.Image = global::Reverb.Properties.Resources.add;
            this.playlist_add.Location = new System.Drawing.Point(227, 366);
            this.playlist_add.Name = "playlist_add";
            this.playlist_add.Size = new System.Drawing.Size(33, 33);
            this.playlist_add.TabIndex = 4;
            this.playlist_add.UseVisualStyleBackColor = true;
            this.playlist_add.Click += new System.EventHandler(this.playlist_add_Click);
            // 
            // playlist_remove
            // 
            this.playlist_remove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_remove.FlatAppearance.BorderSize = 0;
            this.playlist_remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_remove.Image = global::Reverb.Properties.Resources.remove;
            this.playlist_remove.Location = new System.Drawing.Point(266, 367);
            this.playlist_remove.Name = "playlist_remove";
            this.playlist_remove.Size = new System.Drawing.Size(33, 33);
            this.playlist_remove.TabIndex = 11;
            this.playlist_remove.UseVisualStyleBackColor = true;
            this.playlist_remove.Click += new System.EventHandler(this.playlist_remove_Click);
            // 
            // playlist_forward
            // 
            this.playlist_forward.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_forward.FlatAppearance.BorderSize = 0;
            this.playlist_forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_forward.Image = ((System.Drawing.Image)(resources.GetObject("playlist_forward.Image")));
            this.playlist_forward.Location = new System.Drawing.Point(179, 367);
            this.playlist_forward.Name = "playlist_forward";
            this.playlist_forward.Size = new System.Drawing.Size(33, 33);
            this.playlist_forward.TabIndex = 22;
            this.playlist_forward.UseVisualStyleBackColor = true;
            this.playlist_forward.Click += new System.EventHandler(this.playlist_forward_Click);
            // 
            // playlist_stop
            // 
            this.playlist_stop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_stop.FlatAppearance.BorderSize = 0;
            this.playlist_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_stop.Image = ((System.Drawing.Image)(resources.GetObject("playlist_stop.Image")));
            this.playlist_stop.Location = new System.Drawing.Point(103, 367);
            this.playlist_stop.Name = "playlist_stop";
            this.playlist_stop.Size = new System.Drawing.Size(33, 33);
            this.playlist_stop.TabIndex = 13;
            this.playlist_stop.UseVisualStyleBackColor = true;
            this.playlist_stop.Click += new System.EventHandler(this.playlist_stop_Click);
            // 
            // playlist_play
            // 
            this.playlist_play.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_play.FlatAppearance.BorderSize = 0;
            this.playlist_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_play.Image = ((System.Drawing.Image)(resources.GetObject("playlist_play.Image")));
            this.playlist_play.Location = new System.Drawing.Point(141, 367);
            this.playlist_play.Name = "playlist_play";
            this.playlist_play.Size = new System.Drawing.Size(33, 33);
            this.playlist_play.TabIndex = 12;
            this.playlist_play.UseVisualStyleBackColor = true;
            this.playlist_play.Click += new System.EventHandler(this.playlist_play_Click);
            // 
            // playlist_back
            // 
            this.playlist_back.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_back.FlatAppearance.BorderSize = 0;
            this.playlist_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_back.Image = ((System.Drawing.Image)(resources.GetObject("playlist_back.Image")));
            this.playlist_back.Location = new System.Drawing.Point(65, 367);
            this.playlist_back.Name = "playlist_back";
            this.playlist_back.Size = new System.Drawing.Size(33, 33);
            this.playlist_back.TabIndex = 14;
            this.playlist_back.UseVisualStyleBackColor = true;
            this.playlist_back.Click += new System.EventHandler(this.playlist_back_Click);
            // 
            // playlist_pause
            // 
            this.playlist_pause.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playlist_pause.FlatAppearance.BorderSize = 0;
            this.playlist_pause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playlist_pause.Image = ((System.Drawing.Image)(resources.GetObject("playlist_pause.Image")));
            this.playlist_pause.Location = new System.Drawing.Point(141, 367);
            this.playlist_pause.Name = "playlist_pause";
            this.playlist_pause.Size = new System.Drawing.Size(32, 32);
            this.playlist_pause.TabIndex = 25;
            this.playlist_pause.UseVisualStyleBackColor = true;
            this.playlist_pause.Visible = false;
            this.playlist_pause.Click += new System.EventHandler(this.playlist_pause_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Image = global::Reverb.Properties.Resources.sound1;
            this.label1.Location = new System.Drawing.Point(169, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 24);
            this.label1.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 402);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.open_peerlist);
            this.Controls.Add(this.time_position);
            this.Controls.Add(this.playlist);
            this.Controls.Add(this.playlist_add);
            this.Controls.Add(this.playlist_remove);
            this.Controls.Add(this.playlist_forward);
            this.Controls.Add(this.playlist_stop);
            this.Controls.Add(this.seekBar);
            this.Controls.Add(this.playlist_play);
            this.Controls.Add(this.time_length);
            this.Controls.Add(this.playlist_back);
            this.Controls.Add(this.playlist_pause);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Reverb v0.01";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.playlist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playlist_add;
        private Button playlist_remove;
        private Button playlist_play;
        private Button playlist_stop;
        private Button playlist_back;
        private Label time_position;
        private ProgressBar seekBar;
        private Label time_length;
        private Button playlist_forward;
        private DataGridView playlist;
        private Button playlist_pause;
        private Button open_peerlist;
        private ProgressBar volumeBar;
        private Label label1;
        private DataGridViewTextBoxColumn asstring;
        private DataGridViewTextBoxColumn duration;
    }
}

