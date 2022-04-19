using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatClerksDB", menuName = "Scriptable Object/CatClerksDB", order = 997)]
public class CatClerksScriptable : ScriptableObject
{


     public List<CatClerk> CatClerk_List = new List<CatClerk>();
}
