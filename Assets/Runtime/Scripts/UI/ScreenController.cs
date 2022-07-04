using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private UIScreen[] _screens;
    [SerializeField] private AudioClip _buttonClip;

    public void ShowScreen<T>() where T : UIScreen
    {
        foreach (var screen in _screens)
        {
            bool isTypeT = screen is T;
            screen.gameObject.SetActive(isTypeT);
            screen.OnShow();
        }
    }

    public void PlayAudioOnButtonPress() => Singleton.Instance.AudioService.PlayAudioCue(_buttonClip);
}
