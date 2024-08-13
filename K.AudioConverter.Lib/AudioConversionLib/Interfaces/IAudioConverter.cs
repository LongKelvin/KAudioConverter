using K.AudioConverter.Lib.Models;

using NAudio.Lame;

namespace K.AudioConverter.Lib.AudioConversionLib.Interfaces
{
    /// <summary>
    /// Provides methods for audio format conversion, duration calculation, and trimming.
    /// </summary>
    public interface IAudioConverter
    {
        /// <summary>
        /// Converts an MP3 file to a WAV file asynchronously.
        /// </summary>
        /// <param name="inputPath">The path to the input MP3 file.</param>
        /// <param name="outputPath">The path to the output WAV file.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task Mp3ToWaveAsync(string inputPath, string outputPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// Converts a WAV file to an MP3 file asynchronously.
        /// </summary>
        /// <param name="inputPath">The path to the input WAV file.</param>
        /// <param name="outputPath">The path to the output MP3 file.</param>
        /// <param name="bitRate">The bit rate for the output MP3 file (default: 256).</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task WaveToMp3Async(string inputPath, string outputPath, int bitRate = 256, CancellationToken cancellationToken = default);

        /// <summary>
        /// Converts a WAV file to an MP3 file asynchronously.
        /// </summary>
        /// <param name="inputPath">The path to the input WAV file.</param>
        /// <param name="outputPath">The path to the output MP3 file.</param>
        /// <param name="audioConfig">The KAudioMP3Settings for the output MP3 file.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task WaveToMp3Async(string inputPath, string outputPath, KAudioMP3Settings audioConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Converts a WAV file to an MP3 file asynchronously.
        /// </summary>
        /// <param name="inputPath">The path to the input WAV file.</param>
        /// <param name="outputPath">The path to the output MP3 file.</param>
        /// <param name="audioConfig">LameConfig for the output MP3 file.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task WaveToMp3Async(string inputPath, string outputPath, LameConfig audioConfig, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the duration of an audio file asynchronously.
        /// </summary>
        /// <param name="audioPath">The path to the audio file.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation that returns the audio duration.</returns>
        Task<TimeSpan> GetAudioDurationAsync(string audioPath, CancellationToken cancellationToken = default);

        /// <summary>
        /// Trims an audio file asynchronously.
        /// </summary>
        /// <param name="inputPath">The path to the input audio file.</param>
        /// <param name="outputPath">The path to the output trimmed audio file.</param>
        /// <param name="start">The start time of the trimmed audio.</param>
        /// <param name="duration">The duration of the trimmed audio.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task TrimAudioAsync(string inputPath, string outputPath, TimeSpan start, TimeSpan duration, CancellationToken cancellationToken = default);
    }

}
