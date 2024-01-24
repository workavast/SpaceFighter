namespace Audio
{
    public class SingleAudioSource : PauseableAudioSource
    {
        public void Play() => _audioSource.Play();

        public void Stop() => _audioSource.Stop();
    }
}