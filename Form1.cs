using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace Reverb
{
    public partial class Form1 : Form
    {
        #region VÁLTOZÓK
        //A playlist igazi List<>je
        BindingList<TAG_INFO> trackList = new BindingList<TAG_INFO>();
        
        //Lejátszáshoz használandó változók
        int _stream = 0,
            _updateInterval = 50,
            _tickCounter = 0;
        BASSTimer _updateTimer = null;
        Visuals _vis = new Visuals();
        SYNCPROC pausefading = null;
        bool paused = false;
        #endregion

        #region SZINKRONIZÁLÓ CALLBACK METHODOK
        //Peerlist szinkronizáló method
        void Synchronize_PeerlistBackend(object sender, PeerEventArgs e)
        {
            switch (e.action)
            {
                //Ha valaki csatlakozott
                case ClientActionType.Connect:
                    peerlist.Invoke((MethodInvoker)delegate() { peerlist.Items.Add(e.address); }); //Adjuk hozzá a listboxhoz
                    break;
                //Ha valaki szétkapcsolt
                case ClientActionType.Disconnect:
                    peerlist.Invoke((MethodInvoker)delegate() { peerlist.Items.Remove(e.address); }); //Távolítsuk el a listboxból
                    break;
            }
        }

        //Playlist szinkronizáló method
        void Synchronize_PlaylistBackend(object sender, ListChangedEventArgs e)
        {
            
            //Ha az eventet hozzáadás vagy változás váltotta ki, akkor kérjük le a dal hosszát is
            int seconds = 0, minutes = 0;
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
            {
                seconds = (int)(trackList[e.NewIndex].duration % 60);
                minutes = (int)(trackList[e.NewIndex].duration / 60);
            }

            switch (e.ListChangedType)
            {
                //Ha elemet adtunk hozzá
                case ListChangedType.ItemAdded:
                    playlist.Rows.Add(trackList[e.NewIndex].ToString(), //Akkor adjuk hozzá a TAG_INFO.ToString()-et (Artist - Title)
                    minutes + ":" + seconds.ToString().PadLeft(2, '0')); //és a szám hosszát (MM:SS)
                    break;
                //Ha elem változott meg
                case ListChangedType.ItemChanged:
                    playlist.Rows[e.NewIndex].Cells[0].Value = trackList[e.NewIndex].ToString(); // -II-
                    playlist.Rows[e.NewIndex].Cells[1].Value = minutes + ":" + seconds.ToString().PadLeft(2, '0'); //-II-
                    break;
                //Ha elem távolítódot el
                case ListChangedType.ItemDeleted:
                    playlist.Rows.RemoveAt(e.NewIndex); //Eltávolítjuk a listából
                    break;
            }
        }
        #endregion

        //Konstruktor
        public Form1()
        {
            //A splash elnyomásához
            BassNet.Registration("nwdozer@gmail.com", "2X14323717152222");
            
            //Lejátszó library inicializálása
            if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_LATENCY, this.Handle))
                MessageBox.Show(this, "Bass_Init error!");

            //Timer a zene streamje és az UI közötti szinkronizációhoz
            _updateTimer = new Un4seen.Bass.BASSTimer(_updateInterval);
            _updateTimer.Tick += new EventHandler(timerUpdate_Tick);

            //Callback a pausekori hangerő fadehez
            pausefading = new SYNCPROC(pausefade);

            //Form controlok inicializálása
            InitializeComponent();

            //trackList és a form playlistje közötti szinktonizációjához event
            trackList.ListChanged += new ListChangedEventHandler(Synchronize_PlaylistBackend);
            //Csatlakozott peerek List<>-je és listBox-a közötti szinkronizáláshoz event
            Program.Net.PeerEvent += new NetBackend.PeerEventHandler(Synchronize_PeerlistBackend);
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
                    }
                }

                //BackgroundWorker inicializálása a fájlinfok háttérben lekérdezéséhez
                BackgroundWorker _bw = new BackgroundWorker();
                _bw.WorkerReportsProgress = true;
                _bw.DoWork += new DoWorkEventHandler(thread_trackparser_DoWork);
                _bw.ProgressChanged += new ProgressChangedEventHandler(_bw_ProgressChanged);

                //ID3 info kiválasztásának elindítása
                _bw.RunWorkerAsync();
            }

        }

        //Ha egy fájl elkészült, akkor azt átírjuk (ebben a methodban nem kell Invoke-ot hívnunk a BW fő methodjával ellentétben
        void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object[] unboxed = e.UserState as object[];
            trackList[(int)unboxed[0]] = (unboxed[1] as TAG_INFO);
        }

        //BackgroundWorker fő methodja
        void thread_trackparser_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _bw = sender as BackgroundWorker;
            for (int i = 0; i < trackList.Count; ++i)
            {
                //Tag info továbbítása _bw_ProgressChanged-hez a lejátszólista elem frissítéséhez
                _bw.ReportProgress(0, new object[2] { i, BassTags.BASS_TAG_GetFromFile(trackList[i].filename) });
            }
        }

        //Dal eltávolítása gomb, Click event
        private void playlist_remove_Click(object sender, EventArgs e)
        {
            trackList.RemoveAt(playlist.SelectedRows[0].Index);
        }

        //Új kapcsolat hozzáadása gomb, Click event
        private void peerlist_add_Click(object sender, EventArgs e)
        {
            IPAddress address;
            //Ha szintaktikailag érvényes az IP cím, akkor csatlakozunk..
            if (IPAddress.TryParse(peertoconnect.Text, out address))
            {
                Program.Net.ConnectPeer(address);
            }
            else
            {
                MessageBox.Show("IPAddress parse unsuccessful!"); //.. különben error
            }
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
                pictureBox1.Image = null;
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
            pictureBox1.Image = _vis.CreateSpectrumLine(_stream, pictureBox1.Width, pictureBox1.Height, Color.Lime, Color.Red, Color.Black, 2, 2, false, true, false);
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
            Program.Net.Shutdown();
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
                playlist_play.Visible = false;
                playlist_pause.Visible = true;
                return;
            }

            _updateTimer.Stop();                
            Bass.BASS_StreamFree(_stream);
            if (playlist.SelectedRows[0].Index != -1)
            {
                // create the stream
                _stream = Bass.BASS_StreamCreateFile(trackList[playlist.SelectedRows[0].Index].filename, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN);

                if (_stream != 0 && Bass.BASS_ChannelPlay(_stream, false))
                {
                    //this.textBox1.Text = "";
                    _updateTimer.Start();

                    // get some channel info
                    BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                    Bass.BASS_ChannelGetInfo(_stream, info);

                    playlist_pause.Visible = true;
                    playlist_play.Visible = false;
                }
                else
                {
                    Console.WriteLine("Error={0}", Bass.BASS_ErrorGetCode());
                }
            }
        }

        //Dal Stop gomb, Click event
        private void playlist_stop_Click(object sender, EventArgs e)
        {
            _updateTimer.Stop();
            //Ha pauseolva vagyunk állítsuk vissza a play gombot
            if (paused)
            {
                playlist_play.Visible = true;
                playlist_pause.Visible = false;
            }
            Bass.BASS_StreamFree(_stream);
            _stream = 0;
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
                Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_SLIDE, 0,
                    pausefading,
                    IntPtr.Zero);
                playlist_pause.Visible = false;
                playlist_play.Visible = true;
            }
        }

        //Pause-kori hangerő fade callback
        void pausefade (int handle, int channel, int data, IntPtr user)
        {
            if (paused)
                Bass.BASS_ChannelPause(_stream);
        }

        //Ha duplakattolunk a playlistre, akkor indítsuk el a kijelölt dalt
        private void playlist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playlist.SelectedRows[0].Index != -1)
                playlist_play_Click(new object(), EventArgs.Empty);
        }        
    }

}
