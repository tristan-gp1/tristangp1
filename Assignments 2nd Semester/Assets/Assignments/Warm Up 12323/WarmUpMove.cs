using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUpMove : MonoBehaviour
{
    public bool amongus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(amongus == true) 
        {
            transform.position += Vector3.right * 2 * Time.deltaTime;      
        }
    }
}
