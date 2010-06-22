using System;
using System.Collections.Generic;
using System.Text;
using Un4seen.Bass.AddOn.Tags;

namespace Reverb
{
    class Track
    {
        public bool Parsed { get; private set; }
        public TAG_INFO TrackInfo { get; private set; }

        public Track() {}

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

        public override string ToString()
        {
            return TrackInfo.ToString();
        }
    }
}
