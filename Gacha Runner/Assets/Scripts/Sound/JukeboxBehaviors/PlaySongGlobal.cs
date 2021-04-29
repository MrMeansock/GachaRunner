using UnityEngine;

public class PlaySongGlobal : MonoBehaviour, IJukeboxBehaviour
{
    public AudioClip song;

    // Version of play song that does not repeat on scene reloads
    public void Behave()
    {
        var soundManager = FindObjectOfType<SoundManager>();
        if (soundManager.musicSource.clip != song)
        {
            soundManager.PlayMusic(song);
        }
    }
}
