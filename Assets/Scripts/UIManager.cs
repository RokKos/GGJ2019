using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] GameObject MainMenuPanel;

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
    }

    public void PauseGame() {
        Cursor.lockState = CursorLockMode.None;
        MainMenuPanel.SetActive(true);
        gamePaused = true;
    }
}
