using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progressAmount = 0;
        progressSlider.value = 0;
        Gem.OnGemCollect += IncreaseProgressAmount;
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
            Debug.Log("Level Compelte");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
