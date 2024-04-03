namespace Audio
{
    public class SingleAudioSource : PauseableAudioSource
    {
        public void Play() => AudioSource.Play();

        public void Stop() => AudioSource.Stop();
    }
}