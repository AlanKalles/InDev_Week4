using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerUnityEvent : MonoBehaviour
{
    public UnityEvent OnTrigger;
    void Start()
    {
 
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        OnTrigger.Invoke();
        
    }
}
