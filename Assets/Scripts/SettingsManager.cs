using UnityEngine;

//Copy this to make buttons that open a selected canvas, in this case the sound settings

public class SettingsManager : MonoBehaviour
{

    public GameObject openSettingsCanvas;
    public GameObject closeSettingsCanvas;

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
    }

    public void CloseSettings()
    {
        openSettingsCanvas.SetActive(false);
    }

}
