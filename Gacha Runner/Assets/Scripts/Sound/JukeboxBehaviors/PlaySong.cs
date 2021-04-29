using UnityEngine;

public class PlaySong : MonoBehaviour, IJukeboxBehaviour
{
    public AudioClip song;

    public void Behave()
    {
        FindObjectOfType<SoundManager>().PlayMusic(song);
    }
}
