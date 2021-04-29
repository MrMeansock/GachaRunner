using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Dependencies
    [HideInInspector] public AudioSource sfxSource = null;
    [HideInInspector] public AudioSource musicSource = null;

    private void Awake()
    {
        var soundManagers = FindObjectsOfType<SoundManager>();
        if (soundManagers.Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

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
        musicSource.Play();
    }
}
