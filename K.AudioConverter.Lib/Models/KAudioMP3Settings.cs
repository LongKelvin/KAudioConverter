using NAudio.Lame;

namespace K.AudioConverter.Lib.Models
{
    public class KAudioMP3Settings
    {
        public int BitRate { get; set; }
        public MPEGMode Mode { get; set; }
        public LAMEPreset Preset { get; set; }

        public KAudioMP3Settings()
        {
        }

        public KAudioMP3Settings(int bitRate, MPEGMode mode, LAMEPreset preset)
        {
            BitRate = bitRate;
            Mode = mode;
            Preset = preset;
        }
    }
}