using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;

    public GameObject player;
    public GameObject LoadCanvas; // load circle
    public GameObject HoldE; //Hold E canvas
    
    public List<GameObject> levels;
    private int currentLevelIndex = 0;


    public GameObject gameOverScreen;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Gem.OnGemCollect += IncreaseProgressAmount;
        HoldToLoadLevel.OnHoldComplete += LoadNextLevel;

        PlayerHealth.OnPlayerDied += GameOverScreen;
        LoadCanvas.SetActive(false); //circle
        HoldE.SetActive(false); // Hold E
        gameOverScreen.SetActive(false); //Game over Screen
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);

    }

    //Increases progress on Gem bar
    //HUOM! This is between start and update even though it should be after. This is where tutorial wanted it.
    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        if(progressAmount >= 100)
        {
            //Level complete!
            LoadCanvas.SetActive(true); //Lets the player load the next level aka show the ability to load next level
            HoldE.SetActive(true); // Hold E appears
            Debug.Log("Level Compelte");
            AudioManager.Instance.musicSource.Stop(); //Stops the game theme music
            AudioManager.Instance.PlaySFX("LevelComplete"); //Playes the the level Complete sound

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex == levels.Count - 1) ? 0 : currentLevelIndex + 1; //Moves the player to the next level. If there are no next levels left, it takes us to the first level, called level 0 in the script.
        LoadCanvas.SetActive(false);
        

        levels[currentLevelIndex].gameObject.SetActive(false);//disables the current level
        levels[nextLevelIndex].gameObject.SetActive(true);//activates the next level

        player.transform.position = new Vector3(0, 0, 0);

        currentLevelIndex = nextLevelIndex;
        progressAmount = 0;
        progressSlider.value = 0;

        AudioManager.Instance.PlayMusic("Theme"); //Plays the main theme when a new level loads

        HoldE.SetActive(false); //hides HoldE
    }

}
