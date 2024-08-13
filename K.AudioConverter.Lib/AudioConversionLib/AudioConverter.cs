using K.AudioConverter.Lib.AudioConversionLib.Interfaces;
using K.AudioConverter.Lib.Models;

using NAudio.Lame;
using NAudio.Wave;

namespace K.AudioConverter.Lib.AudioConversionLib
{
    internal class AudioConverter : IAudioConverter
    {
        private const int BufferSize = 4096;

        public async Task Mp3ToWaveAsync(string inputPath, string outputPath, CancellationToken cancellationToken = default)
        {
            using var reader = new Mp3FileReader(inputPath);
            using var writer = new WaveFileWriter(outputPath, reader.WaveFormat);
            await CopyStreamAsync(reader, writer, cancellationToken);
        }

        public async Task WaveToMp3Async(string inputPath, string outputPath, int bitRate = 256, CancellationToken cancellationToken = default)
        {
            using var reader = new AudioFileReader(inputPath);

            using var writer = new LameMP3FileWriter(outputPath, reader.WaveFormat, bitRate);
            await CopyStreamAsync(reader, writer, cancellationToken);
        }

        public async Task WaveToMp3Async(string inputPath, string outputPath, LameConfig audioConfig, CancellationToken cancellationToken = default)
        {
            using var reader = new AudioFileReader(inputPath);
            using var writer = new LameMP3FileWriter(outputPath, reader.WaveFormat, audioConfig);
            await CopyStreamAsync(reader, writer, cancellationToken);
        }

        public async Task WaveToMp3Async(string inputPath, string outputPath, KAudioMP3Settings audioConfig, CancellationToken cancellationToken = default)
        {
            using var reader = new AudioFileReader(inputPath);

            var lameConfig = new LameConfig
            {
                BitRate = audioConfig.BitRate,
                Preset = audioConfig.Preset,
                Mode = audioConfig.Mode,
            };

            using var writer = new LameMP3FileWriter(outputPath, reader.WaveFormat, lameConfig);
            await CopyStreamAsync(reader, writer, cancellationToken);
        }

        public Task<TimeSpan> GetAudioDurationAsync(string audioPath, CancellationToken cancellationToken = default)
        {
            using var reader = new AudioFileReader(audioPath);
            return Task.FromResult(reader.TotalTime);
        }

        public async Task TrimAudioAsync(string inputPath, string outputPath, TimeSpan start, TimeSpan duration, CancellationToken cancellationToken = default)
        {
            using var reader = new AudioFileReader(inputPath);
            reader.CurrentTime = start;
            using var writer = new WaveFileWriter(outputPath, reader.WaveFormat);

            var buffer = new byte[BufferSize];
            var endPosition = reader.CurrentTime + duration;

            while (reader.CurrentTime < endPosition)
            {
                int bytesRead = await reader.ReadAsync(buffer, cancellationToken);
                if (bytesRead == 0)
                    break;

                var remainingTime = endPosition - reader.CurrentTime;
                var bytesToWrite = Math.Min(bytesRead, (int)(remainingTime.TotalSeconds * reader.WaveFormat.AverageBytesPerSecond));

                await writer.WriteAsync(buffer.AsMemory(0, bytesToWrite), cancellationToken);
            }
        }

        private static async Task CopyStreamAsync(WaveStream source, Stream destination, CancellationToken cancellationToken)
        {
            var buffer = new byte[BufferSize];
            int read;
            while ((read = await source.ReadAsync(buffer, cancellationToken)) > 0)
            {
                await destination.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            }
        }
    }
}