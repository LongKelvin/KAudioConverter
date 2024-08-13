// Ignore Spelling: Cli

using CommandLine;

namespace K.AudioConverter.Cli.Models
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input audio file path.")]
        public string Input { get; set; } = string.Empty;

        [Option('o', "output", Required = true, HelpText = "Output audio file path.")]
        public string Output { get; set; } = string.Empty;

        [Option('b', "bit-rate", Default = 320, HelpText = "AudioBitRate for MP3 conversion (defaults to 320 kbps).")]
        public int AudioBitRate { get; set; } = 320;

        [Option('v', "version", HelpText = "Show version information.")]
        public bool Version { get; set; }

        [Option('t', "format-type", HelpText = "Format Type for conversion. Currently support .mp3 and .wav", Required = true)]
        public string ConversionType { get; set; } = ".mp3";
    }
}
