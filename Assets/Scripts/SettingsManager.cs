using UnityEngine;

//Copy this to make buttons that open a selected canvas, in this case the sound settings

public class SettingsManager : MonoBehaviour
{

    public GameObject openSettingsCanvas;
    public GameObject closeSettingsCanvas;

    public static bool isPaused; //for pausing the game

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenSettings()
    {
        openSettingsCanvas.SetActive(true);
        Time.timeScale = 0f; //pauses the game
        isPaused = true; //tells the code the game paused
    }

    public void CloseSettings()
    {
        openSettingsCanvas.SetActive(false);
        Time.timeScale = 1f; //resumes the game to run
        isPaused = false;//tells the code the game is not paused

    }

}
