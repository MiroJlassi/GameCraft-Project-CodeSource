using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagecircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale *= 1.1f;   
        if(transform.localScale.x >= 1000)
        {
            Destroy(gameObject);
        }
    }
}
