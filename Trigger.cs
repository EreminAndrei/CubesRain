using System;
using System.Collections;
using UnityEngine;


public class Trigger : MonoBehaviour
{   
    public event Action <GameObject> TriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube")
        {           
            TriggerEntered?.Invoke(other.gameObject);
        }
    }    
}
