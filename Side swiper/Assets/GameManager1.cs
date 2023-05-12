using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() 
    {
    
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //access our function from our player manager and call it 
            GameManager1 manager = collision.GetComponent<GameManager1>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
