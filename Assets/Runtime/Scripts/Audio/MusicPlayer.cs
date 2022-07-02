using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _mainMusic;

    //private AudioSource _audioSource;
    //private AudioSource AudioSource => _audioSource == null ? _audioSource = GetComponent<AudioSource>() : _audioSource;

    private void PlayMusic(AudioClip clip)
    {
        Singleton.Instance.AudioService.PlayMusic(clip);
    }

    public void StopMusic()
    {
        Singleton.Instance.AudioService.StopMusic();
    }

    public void PlayMenuMusic() => PlayMusic(_menuMusic);

    public void PlayMainMusic() => PlayMusic(_mainMusic);
}
