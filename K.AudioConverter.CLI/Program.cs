// Ignore Spelling: Cli

using System.Diagnostics;

using CommandLine;

using K.AudioConverter.Cli.Extensions;
using K.AudioConverter.Cli.Models;
using K.AudioConverter.Lib.AudioConversionLib;

namespace K.AudioConverter.Cli
{
    internal class Program
    {
        protected Program()
        {
        }

        private static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    async (opts) => await RunOptionsAndReturnExitCode(opts),
                    errs => Task.FromResult(1));
        }

        private static async Task<int> RunOptionsAndReturnExitCode(Options opts)
        {
            if (opts.Version)
            {
                Console.WriteLine("KAudioConversion CLI Version 1.0.0.0");
                return 0;
            }

            Console.WriteLine($"Converting [{opts.Input}] to [{opts.Output}] with bit-rate {opts.AudioBitRate}kbps");

            var kAudioUtility = new KAudioConversionUtility();
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            // Perform the conversion logic
            ConversionType conversionFormat = ConversionType.WavToMp3;

            var conversionType = StringExtensions.EnsurePeriodPrefix(opts.ConversionType);
            if (!string.IsNullOrEmpty(conversionType))
            {
                try
                {
                    conversionFormat = conversionType.GetEnumValueFromDescription<ConversionType>();
                }
                catch
                {
                    Console.WriteLine("Unsupported file format conversion. Supported formats: WAV to MP3 and MP3 to WAV.");
                    return 1;
                }
            }

            switch (conversionFormat)
            {
                case ConversionType.WavToMp3:
                    await kAudioUtility.WaveToMp3Async(opts.Input, opts.Output, opts.AudioBitRate);
                    break;

                case ConversionType.Mp3ToWav:
                    await kAudioUtility.Mp3ToWaveAsync(opts.Input, opts.Output);
                    break;

                default:
                    Console.WriteLine("Unsupported file format conversion. Supported formats: WAV to MP3 and MP3 to WAV.");
                    return 1;
            }

            stopWatch.Stop();
            Console.WriteLine($"Finished conversion in {stopWatch.Elapsed.TotalSeconds}s");
            return 0;
        }
    }
}