using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinTest_Interior : MonoBehaviour
{
     public GameObject testwall;

     public bool wallchange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (wallchange)
          {
               testwall.SetActive(true);


          }
          else
               testwall.SetActive(false);
    }
}
