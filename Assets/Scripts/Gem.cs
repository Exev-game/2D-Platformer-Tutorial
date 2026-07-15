using UnityEngine;
using System;

public class Gem : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int worth = 5;

  
    public void Collect()
    {
        OnGemCollect.Invoke(worth);
        Destroy(gameObject);  // collects (in code it destroys) the gems
        AudioManager.Instance.PlaySFX("PickUp"); //Playes the pick up sound
    }


}
