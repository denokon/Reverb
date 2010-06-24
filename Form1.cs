using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace Reverb
{
    public partial class Form1 : Form
    {
        #region VÁLTOZÓK

        BindingList<Track> trackList = new BindingList<Track>();
        Queue<Track> unparsed = new Queue<Track>();
        //NetBackend Net = new NetBackend();
        
        bool scrolled = false;
        
        //Lejátszáshoz használandó változók
        int _stream = 0,
            _updateInterval = 50,
            _tickCounter = 0,
            NextTrackSyncHandle = 0;
        BASSTimer _updateTimer = null;
        Visuals _vis = new Visuals();
        SYNCPROC pausefading = null, nexttrack = null;
        bool paused = false;
        #endregion

        //Konstruktor
        public Form1()
        {

            //A splash elnyomásához
            BassNet.Registration("nwdozer@gmail.com", "2X14323717152222");

            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, this.Handle))
                MessageBox.Show(this, "Bass_Init error!");

            //Timer a zene streamje és az UI közötti szinkronizációhoz
            _updateTimer = new Un4seen.Bass.BASSTimer(_updateInterval);
            _updateTimer.Tick += new EventHandler(timerUpdate_Tick);

            //Callback a pausekori hangerő fadehez
            pausefading = new SYNCPROC(delegate(int handle, int channel, int data, IntPtr user)
            {
                if (paused)
                    Bass.BASS_ChannelPause(_stream);
            });

            nexttrack = new SYNCPROC(delegate(int handle, int channel, int data, IntPtr user)
            {
                if (playlist.Rows.Count > playlist.CurrentRow.Index + 1)
                {
                    playlist_forward_Click(null, EventArgs.Empty);
                }
            });

            InitializeComponent();
            playlist.AutoGenerateColumns = false;
            
            playlist.Columns[0].DataPropertyName = "asstring";
            playlist.Columns[1].DataPropertyName = "length";
            playlist.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 

            playlist.DataSource = trackList;
        }


        //Dal hozzáadása gomb, Click event
        private void playlist_add_Click(object sender, EventArgs e)
        {
            //OpenFileDialog inicializálása a fájlok kiválasztásához
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "Music (*.WAV; *.MP3; *.FLAC)|*.WAV;*.MP3;*.FLAC|All files (*.*)|*.*";
            opd.Title = "Select Music";
            opd.Multiselect = true;

            //OpenFileDialog megnyitása
            if (DialogResult.OK == opd.ShowDialog())
            {
                
                //Kiválasztott fájlok hozzáadása a playlisthez
                for (int i = 0; opd.FileNames.Length > i; ++i)
                {
                    if (File.Exists(opd.FileNames[i]))
                    {
                        trackList.Add(new TAG_INFO(opd.FileNames[i]));
                        unparsed.Enqueue(trackList[trackList.Count-1]);
                    }
                }

                //BackgroundWorker inicializálása a fájlinfok háttérben lekérdezéséhez
                BackgroundWorker _bw = new BackgroundWorker();
                _bw.DoWork += new DoWorkEventHandler(ProcessFileTags);
                _bw.RunWorkerAsync();
            }

        }

        //BackgroundWorker fő methodja
        void ProcessFileTags(object sender, DoWorkEventArgs e)
        {
            do
            {
                if (scrolled)
                {
                    int LastVisibleRow;
                    
                    GOTO_RestartParsingOfVisible: //GOTO
                    scrolled = false;

                    LastVisibleRow = playlist.FirstDisplayedScrollingRowIndex + playlist.Rows.GetRowCount(DataGridViewElementStates.Displayed);
                    for (int i = playlist.FirstDisplayedScrollingRowIndex; i < LastVisibleRow; ++i)
                    {
                        if (scrolled) goto GOTO_RestartParsingOfVisible;
                        
                        if (null != trackList[i]) playlist.Invoke((MethodInvoker) delegate {trackList[i].Init();});
                    }
                }

                if (null != unparsed.Peek())
                    playlist.Invoke((MethodInvoker) delegate { unparsed.Dequeue().Init(); });
                else
                    unparsed.Dequeue();

            } while (0 != unparsed.Count);
        }

        //Dal eltávolítása gomb, Click event
        private void playlist_remove_Click(object sender, EventArgs e)
        {
            trackList.RemoveAt(playlist.SelectedRows[0].Index);
        }

        //Új kapcsolat hozzáadása gomb, Click event
        private void peerlist_add_Click(object sender, EventArgs e)
        {
            /*IPAddress address;
            //Ha szintaktikailag érvényes az IP cím, akkor csatlakozunk..
            if (IPAddress.TryParse(peertoconnect.Text, out address))
            {
                Program.Net.ConnectPeer(address);
            }
            else
            {
                MessageBox.Show("IPAddress parse unsuccessful!"); //.. különben error
            }*/
        }

        //Lejátszás-UI szinkronizáló method
        private void timerUpdate_Tick(object sender, System.EventArgs e)
        {
            // here we gather info about the stream, when it is playing...
            if (Bass.BASS_ChannelIsActive(_stream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                // the stream is still playing...
            }
            else
            {
                // the stream is NOT playing anymore...
                _updateTimer.Stop();
                /*pictureBox1.Image = null;*/
                playlist_play.Text = "Play";
                return;
            }

            // from here on, the stream is for sure playing...
            _tickCounter++;
            long pos = Bass.BASS_ChannelGetPosition(_stream); // position in bytes
            long len = Bass.BASS_ChannelGetLength(_stream); // length in bytes

            if (_tickCounter == 5)
            {
                // display the position every 250ms (since timer is 50ms)
                _tickCounter = 0;
                double totaltime = Bass.BASS_ChannelBytes2Seconds(_stream, len); // the total time length
                double elapsedtime = Bass.BASS_ChannelBytes2Seconds(_stream, pos); // the elapsed time length

                time_position.Text = Utils.FixTimespan(elapsedtime, "MMSS");
                time_length.Text = Utils.FixTimespan(totaltime, "MMSS");
            }

            // update the wave position
            SetSeekBar(pos, len);
            // update spectrum
            /*pictureBox1.Image = _vis.CreateSpectrumLine(_stream, pictureBox1.Width, pictureBox1.Height, Color.Lime, Color.Red, Color.Black, 2, 2, false, true, false);*/
        }

        //A keresősáv pozícióra állítása
        private void SetSeekBar(long pos, long len)
        {
            seekBar.Maximum = (int)len;
            seekBar.Value = (int)pos;
        }

        //Keresősáv Click eventje, dalban kereséshez
        private void progressBar1_MouseClick(object sender, MouseEventArgs e)
        {
            if (0 == _stream)
                return;

            long teszt = Bass.BASS_ChannelGetLength(_stream);
            long pos = (int)Math.Ceiling(Bass.BASS_ChannelGetLength(_stream)*(e.X/(float)seekBar.Width));
            Bass.BASS_ChannelSetPosition(_stream,pos);
        }

        //Destruktor ha úgy tetszik. Minden program bezárásakori teendő.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Net.Shutdown();
            _updateTimer.Tick -= new EventHandler(timerUpdate_Tick);
            // close bass
            Bass.BASS_Stop();
            Bass.BASS_Free();
        }

        //Lejátszás gomb, Click event
        private void playlist_play_Click(object sender, EventArgs e)
        {
            //Ha már elindult a dal csak Pause-olva vagyunk
            if (paused)
            {
                paused = false;
                _updateTimer.Start();
                Bass.BASS_ChannelPlay(_stream, false);
                Bass.BASS_ChannelSlideAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, 1, 300);

                if (this.InvokeRequired) this.Invoke((MethodInvoker)delegate
                {
                    playlist_play.Visible = false; playlist_pause.Visible = true;
                });
                else
                {
                    playlist_play.Visible = false; playlist_pause.Visible = true;
                }
            }
            else
            {
                if (0 != playlist.RowCount && playlist.SelectedRows[0].Index != -1)
                {
                    // create the stream
                    _stream = Bass.BASS_StreamCreateFile(trackList[playlist.SelectedRows[0].Index].filename, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);

                    if (0 != _stream && Bass.BASS_ChannelPlay(_stream, false))
                    {
                        Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, volumeBar.Value / 100F); //Induljunk a hangerőszabályozó hangerejével
                        NextTrackSyncHandle = Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, nexttrack, IntPtr.Zero); //Következő számra ugrás sync

                        _updateTimer.Start();

                        // get some channel info
                        BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                        Bass.BASS_ChannelGetInfo(_stream, info);

                        if (this.InvokeRequired) this.Invoke((MethodInvoker)delegate
                        {
                            playlist_play.Visible = true; playlist_pause.Visible = false;
                        });
                        else
                        {
                            playlist_play.Visible = true; playlist_pause.Visible = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error={0}", Bass.BASS_ErrorGetCode());
                    }
                }
            }
        }

        //Dal Stop gomb, Click event
        private void playlist_stop_Click(object sender, EventArgs e)
        {
            //Megnézzük van-e mit leállítani
            if (Bass.BASS_ChannelIsActive(_stream) != BASSActive.BASS_ACTIVE_STOPPED)
            {
                Bass.BASS_ChannelStop(_stream);
                _updateTimer.Stop();

                if (this.InvokeRequired) this.Invoke((MethodInvoker)delegate
                {
                    if (!paused)
                    {
                        playlist_play.Visible = true; playlist_pause.Visible = false;
                    }

                    time_position.Text = time_length.Text = "00:00";
                    seekBar.Value = 0;
                });
                else
                {
                    if (!paused)
                    {
                        playlist_play.Visible = true; playlist_pause.Visible = false;
                    }

                    time_position.Text = time_length.Text = "00:00";
                    seekBar.Value = 0;
                }

                Bass.BASS_ChannelRemoveSync(_stream, NextTrackSyncHandle);
                Bass.BASS_StreamFree(_stream);
                _stream = 0;
            }
        }

        //Dal pause gomb, Click event
        private void playlist_pause_Click(object sender, EventArgs e)
        {
            //Tényleg pauseolva vagyunk? (Play/Pause spam védelem)
            if (!paused)
            {
                _updateTimer.Stop();
                Bass.BASS_ChannelSlideAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, 0, 300);
                paused = true;
                Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_SLIDE, 0, pausefading, IntPtr.Zero);
                
                playlist_pause.Visible = false; playlist_play.Visible = true;
            }
        }

        //Ha duplakattolunk a playlistre, akkor indítsuk el a kijelölt dalt
        private void playlist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playlist.SelectedRows[0].Index != -1)
                playlist_play_Click(new object(), EventArgs.Empty);
        }

        private void open_peerlist_Click(object sender, EventArgs e)
        {
            (new Form2()).Show(this);
        }

        private void playlist_back_Click(object sender, EventArgs e)
        {
            playlist_stop_Click(null, EventArgs.Empty);
            playlist.CurrentCell = playlist[0, playlist.CurrentRow.Index - 1];
            playlist_play_Click(null, EventArgs.Empty);
        }

        private void playlist_forward_Click(object sender, EventArgs e)
        {
            playlist_stop_Click(null, EventArgs.Empty);
            if (playlist.InvokeRequired) playlist.Invoke((MethodInvoker)delegate
            {
                playlist.CurrentCell = playlist[0, playlist.CurrentRow.Index + 1];
            });
            else
            {
                playlist.CurrentCell = playlist[0, playlist.CurrentRow.Index + 1];
            }
            playlist_play_Click(null, EventArgs.Empty);
        }

        private void playlist_Scroll(object sender, ScrollEventArgs e)
        {
            scrolled = true;
        }

        private void playlist_SelectionChanged(object sender, EventArgs e)
        {
            if (0 != unparsed.Count && 0 != playlist.SelectedRows.Count) trackList[playlist.SelectedRows[0].Index].Init();
        }

        private void Click_VolumeBar(object sender, MouseEventArgs e)
        {
            if (0 == _stream)
                return;

            if (Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, e.X / 100F))
            {
                volumeBar.Value = e.X;
            }
        }     
    }

}
