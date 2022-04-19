using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BookDB", menuName = "Scriptable Object/BookDB", order = 999)]
public class BookScriptable: ScriptableObject
{
     public string BookName;
     public string Genre;
     public string Contents;

     public int Price;
     public int OrderOfEnter;

     


}
