// Ignore Spelling: Cli

using System.ComponentModel;

namespace K.AudioConverter.Cli.Models
{
    public enum ConversionType
    {
        [Description(".mp3")]
        WavToMp3,

        [Description(".wav")]
        Mp3ToWav
    }
}
