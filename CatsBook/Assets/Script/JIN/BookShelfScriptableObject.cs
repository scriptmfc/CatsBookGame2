using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BookShelfDB", menuName = "Scriptable Object/BookShelfDB", order = 998)]
public class BookShelfScriptableObject : ScriptableObject
{
     public List<BookScriptable> book_list = new List<BookScriptable>();
    
}
