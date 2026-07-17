using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 1;
    private int currentHealth;


    //tästä puuttuu heart toiminto

    public static event Action OnPlayerDied;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        //Tästä puuttuu heart toiminto

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }


    private void TakeDamage(int damage)
    { 
    currentHealth -= damage;
        //tästä puuttuu heart toiminto

        if (currentHealth <= 0)
        {
            Debug.Log("player dead toimii");
            //player dead

            OnPlayerDied.Invoke();







        }
    }

}
