using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreScript.DevilNumber += 1;
        Debug.Log(ScoreScript.DevilNumber);
        Destroy(gameObject);
    }
}
