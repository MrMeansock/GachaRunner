using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    // Dependencies
    private AudioSource sfxSource = null;
    private AudioSource musicSource = null;

    protected override void Awake()
    {
        base.Awake();

        sfxSource = GetComponentInChildren<SFXSource>().GetComponent<AudioSource>();
        musicSource = GetComponentInChildren<MusicSource>().GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
    }
}
