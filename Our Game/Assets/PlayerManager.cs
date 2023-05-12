using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    PlayerMovement playerMovement;
    public int coinCount;
    //Start is called before the first fame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        currentHealth = maxHealth;
    }


    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Currency":
                coinCount++;
                return true;
            case "Speed+":
                //playerMovement.SpeedPowerUp();
                return true;
            default:
                Debug.Log("Item or referance not set.");
                return false;
        }
    }
    private void Update()
    {
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {

            PauseGame();
        }
    }

    //Create a function that minusis the players current health.
    public void TakeDamage()
    {
        currentHealth -= 1;
    }
    //create a function that will pause the game
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    //create a function that will remuse the game
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
