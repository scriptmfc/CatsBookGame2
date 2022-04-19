using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ConversationDB", menuName = "Scriptable Object/ConversationDB", order = 997)]
public class ConversationScriptable : ScriptableObject
{

     [System.Serializable]
     public class Conversation
     {
          public string Speaker_Name;
          public string Conversation_content;

          public Sprite Speaker_sprite;

          public bool ImgIsRight;

          public string Message;
     }

     public List<Conversation> conversation_list = new List<Conversation>();
}
