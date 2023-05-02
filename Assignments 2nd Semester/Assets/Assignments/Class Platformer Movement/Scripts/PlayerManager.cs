using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    PlayerMovement playerMovement;
    public int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (currentHealth <= 0) 
        {
            PauseGame();
        }
    }   
    public bool PickupItem(GameObject obj) 
    {
        switch (obj.tag) 
        {
            case "Currency":
                coinCount++;
                Debug.Log("Picked up coin.");
                return true;
            case "Speed+":
                playerMovement.SpeedPowerUp();
                Debug.Log("Picked up powerup.");
                return true;
            default:
                Debug.Log("Item or reference not set.");
                return false;
        }
    }

    //Create function that minuses the player's current health
    public void TakeDamage() 
    {
        currentHealth -= 1;
    }
    //Create function that will pause the game
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    //Create function that will resume game
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
