using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] Toggle invertControllsToggle;

    [SerializeField] CharacterController characterController;

    private bool gamePaused = true;

    public bool GetGamePaused() {
        return gamePaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
    }

    public void PlayGame() {
        Cursor.lockState = CursorLockMode.Locked;
        MainMenuPanel.SetActive(false);
        gamePaused = false;
        int vertical = invertControllsToggle.isOn ? 1 : -1;
        characterController.SetVerticalSwitch(vertical);

    }

    public void PauseGame() {
        Cursor.lockState = CursorLockMode.None;
        MainMenuPanel.SetActive(true);
        gamePaused = true;
    }
}
