using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AllBookDB :MonoSingleTon<AllBookDB>
{
     [SerializeField]
     public  Book[] AllBookdb = new Book[999];

     [SerializeField]
     public static Dictionary<string, Book> ALLBOOK_DICT = new Dictionary<string, Book>();


     public class Special_Situation_Book
     {
          public List<Book> PreExistence_Genre_Book = new List<Book>();
          public List<Book> OldBook_Genre_Book = new List<Book>();
     }
     public Special_Situation_Book special_situation_book= new Special_Situation_Book();


}

public class JINJsonSave : MonoBehaviour
{
     public string path_str;


     public void testgo()
     {
          
          
          json_write();
     }

     public void json_write()
     {
          //string json = JsonUtility.ToJson(AllBookDB.AllBookdb, true);
          //string json = JsonUtility.ToJson(allbookdb, true);
          //File.WriteAllText(path_str, json);
     }
     public void json_load()
     {
          string json = File.ReadAllText(path_str);
          //allbookdb = JsonUtility.FromJson<AllBookDB>(json);
     }


    // Start is called before the first frame update
    void Start()
    {
          path_str = Path.Combine(Application.dataPath, "DB/save_AllBookDB" + ".json");
          //testgo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
