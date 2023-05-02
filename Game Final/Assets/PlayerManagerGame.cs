using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerGame : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int artifactCount;
    public bool isVisible;

    SpriteRenderer playerColor;

    private void Start()
    {
        playerColor = GetComponent<SpriteRenderer>();
        playerColor.color = Color.white;
        isVisible = true;
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
            case "Artifact":
                artifactCount++;
                Debug.Log("Picked up artifact.");
                return true;
            case "Invisibility":
                StartCoroutine(PowerUpInvisibility());
                Debug.Log("Invisible");
                return true;
            case "PowerUpHealing":
                currentHealth += 1;
                Debug.Log("Healed");
                return true;
            default:
                Debug.Log("Item or reference not set.");
                return false;
        }
    }

    IEnumerator PowerUpInvisibility()
    {
        isVisible = false;
        playerColor.color = Color.gray;
        yield return new WaitForSeconds(5);
        isVisible = true;
        playerColor.color = Color.white;
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
