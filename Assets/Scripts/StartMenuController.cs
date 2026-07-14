using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
   
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnExitClick()
    {
        //Delete between lines if doesn't work after exporting
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;


        //delete ends here
#endif
        //exits the game when it's outside of unity
        Application.Quit();
    }
}