using System.Collections;
using UnityEngine;

public class PlayMenuTheme : MonoBehaviour, IJukeboxBehaviour
{
    public void Behave()
    {
        var musicCollection = FindObjectOfType<MusicCollection>();
        var soundManager = FindObjectOfType<SoundManager>();

        soundManager.musicSource.loop = false;
        soundManager.PlayMusic(musicCollection.menuThemeIntro);

        IEnumerator PlayLoopAfterIntro()
        {
            yield return new WaitUntil(() => soundManager.musicSource.isPlaying == false);
            soundManager.musicSource.loop = true;
            soundManager.PlayMusic(musicCollection.menuThemeLoop);
        }
        StartCoroutine(PlayLoopAfterIntro());
    }
}
