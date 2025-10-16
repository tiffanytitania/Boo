using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitPopupManager : MonoBehaviour
{
    public GameObject exitPopup; // panel popup
    public Button yesButton;
    public Button noButton;

    void Start()
    {
        exitPopup.SetActive(false);

        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
    }

    public void ShowExitPopup()
    {
        exitPopup.SetActive(true);
    }

    void OnYesClicked()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

#if UNITY_EDITOR
        // biar tetep bisa ngetes di editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnNoClicked()
    {
        exitPopup.SetActive(false);
    }
}
