using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogeTigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("triggerwaimian");
        if (other.gameObject.tag == "Player")
        {
            print("triggerwaimian");
            Destroy(other.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
