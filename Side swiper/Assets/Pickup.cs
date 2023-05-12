using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager manager = collision.GetComponent<PlayerManager>();
            if (manager)
            {
                bool pickUp = manager.PickupItem(gameObject);
                if (pickUp)
                {
                    Destroy(gameObject);
                }
            }
        }
     }
}
