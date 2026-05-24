using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    private AudioClip currentClip;

    public void PlayPageAudio(Page page)
    {
        if (page == null)
            return;

        AudioClip newClip = page.audioClip;

        if (newClip == null)
            return;

        if (musicSource.clip == newClip && musicSource.isPlaying)
            return;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.loop = page.loopAudio;
        musicSource.Play();

        currentClip = newClip;
    }

    public void StopAudio()
    {
        if (musicSource == null)
            return;

        musicSource.Stop();
        musicSource.clip = null;
        currentClip = null;
    }

    public void ToggleMute()
    {
        if (musicSource == null)
            return;

        musicSource.mute = !musicSource.mute;
    }

    public bool IsMuted()
    {
        return musicSource != null && musicSource.mute;
    }
}