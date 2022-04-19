using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JINtest_book : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
          

    }


    public void get_bookstest()
     {
          for (int i = 0; i < AllBookDB.Instance.AllBookdb.Length; i++)
          {
               string tempbookname = AllBookDB.Instance.AllBookdb[i].BookName;
               Debug.Log(tempbookname);
               if (tempbookname != "")
                    BookStorage.Instance.GatherBook(tempbookname, 1);
          }
     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
