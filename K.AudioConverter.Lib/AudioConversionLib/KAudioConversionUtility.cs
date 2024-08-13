using K.AudioConverter.Lib.AudioConversionLib.Interfaces;
using K.AudioConverter.Lib.Models;

using Microsoft.Extensions.DependencyInjection;

using NAudio.Lame;

namespace K.AudioConverter.Lib.AudioConversionLib
{
    public class KAudioConversionUtility
    {
        private readonly IAudioConverter _audioConverter;

        public KAudioConversionUtility()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            _audioConverter = serviceProvider.GetRequiredService<IAudioConverter>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAudioConverter, AudioConverter>();
        }

        public Task Mp3ToWaveAsync(string inputPath, string outputPath, CancellationToken cancellationToken = default)
        {
            return _audioConverter.Mp3ToWaveAsync(inputPath, outputPath, cancellationToken);
        }

        public Task WaveToMp3Async(string inputPath, string outputPath, int bitRate = 256, CancellationToken cancellationToken = default)
        {
            return _audioConverter.WaveToMp3Async(inputPath, outputPath, bitRate, cancellationToken);
        }

        public Task WaveToMp3Async(string inputPath, string outputPath, KAudioMP3Settings audioMP3Settings, CancellationToken cancellationToken = default)
        {
            return _audioConverter.WaveToMp3Async(inputPath, outputPath, audioMP3Settings, cancellationToken);
        }

        public Task WaveToMp3Async(string inputPath, string outputPath, LameConfig audioConfig, CancellationToken cancellationToken = default)
        {
            return _audioConverter.WaveToMp3Async(inputPath, outputPath, audioConfig, cancellationToken);
        }

        public Task<TimeSpan> GetAudioDurationAsync(string audioPath, CancellationToken cancellationToken = default)
        {
            return _audioConverter.GetAudioDurationAsync(audioPath, cancellationToken);
        }

        public Task TrimAudioAsync(string inputPath, string outputPath, TimeSpan start, TimeSpan duration, CancellationToken cancellationToken = default)
        {
            return _audioConverter.TrimAudioAsync(inputPath, outputPath, start, duration, cancellationToken);
        }
    }
}