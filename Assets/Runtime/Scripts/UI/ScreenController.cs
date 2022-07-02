using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private UIScreen[] _screens;

    public void ShowScreen<T>() where T : UIScreen
    {
        foreach (var screen in _screens)
        {
            bool isTypeT = screen is T;
            screen.gameObject.SetActive(isTypeT);
            screen.OnShow();
        }
    }
}
