using System;
using System.Collections.Generic;
using System.Text;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass;
using System.ComponentModel;

namespace Reverb
{
    class Track : INotifyPropertyChanged
    {
        private TAG_INFO TrackInfo;

        public bool parsed = false;
        public string length { get { return Utils.FixTimespan(TrackInfo.duration, "MMSS"); } }
        public string filename { get { return TrackInfo.filename; } }
        public string asstring { get { return TrackInfo.ToString(); } }
        public override string ToString() { return TrackInfo.ToString(); }

        public event PropertyChangedEventHandler PropertyChanged;

        private Track() { }
        
        public Track(string FileName)
        {
            TrackInfo = new TAG_INFO(FileName);
        }

        public static implicit operator Track(TAG_INFO TrackInfo)
        {
            Track t = new Track();
            t.TrackInfo = TrackInfo;
            return t;
        }

        internal void Init()
        {
            if (!parsed)
            {
                TrackInfo = BassTags.BASS_TAG_GetFromFile(TrackInfo.filename) ?? TrackInfo;
                NotifyPropertyChanged("asstring");
                NotifyPropertyChanged("duration");
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
