using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoSingleTon<ConversationManager>
{
     // Start is called before the first frame update
     public Image Left_Speaker_img;
     public Image Right_Speaker_img;

     public GameObject Conversation_Popup;

     public TextMeshProUGUI Speaker_Name_txt;
     public TextMeshProUGUI Conversation_txt;

     public string Message;

     [SerializeField]
     [Header("°¨½Ã¿ë")]
     private List<ConversationScriptable.Conversation> CDB_List;

     public void SAY_Push_DB(ConversationScriptable conversationscriptable,int number)
     {
          ConversationScriptable.Conversation CC = conversationscriptable.conversation_list[number];
          SAY_Push(CC.Speaker_Name, CC.Conversation_content, CC.Speaker_sprite, CC.ImgIsRight,CC.Message);
          
     }



     public void SAY_Push(string Speaker_Name, string Conversation_Content, Sprite Speaker_sprite, bool imgisRight,string message)
     {
          CDB_List.Add(new ConversationScriptable.Conversation());

          int last = CDB_List.Count - 1;

          CDB_List[last].Speaker_Name = Speaker_Name;
          CDB_List[last].Conversation_content = Conversation_Content;
          CDB_List[last].ImgIsRight = imgisRight;
          CDB_List[last].Speaker_sprite = Speaker_sprite;
          CDB_List[last].Message = message;

     }


     public void SAY_Pop()
     {
          //int last = CDB_List.Count-1;

          string Speaker_Name = CDB_List[0].Speaker_Name;
          string Conversation_Content = CDB_List[0].Conversation_content;
          bool imgisRight = CDB_List[0].ImgIsRight;
          Sprite Speaker_sprite = CDB_List[0].Speaker_sprite;
          Message = CDB_List[0].Message;

          CDB_List.RemoveAt(0);

          Conversation_Popup.SetActive(true);

          Speaker_Name_txt.text = Speaker_Name;
          Conversation_txt.text = Conversation_Content;



          if (imgisRight)
          {
               Right_Speaker_img.sprite = Speaker_sprite;
               Right_Speaker_img.gameObject.SetActive(true);
               Left_Speaker_img.gameObject.SetActive(false);

          }
          else
          {
               Left_Speaker_img.sprite = Speaker_sprite;
               Left_Speaker_img.gameObject.SetActive(true);
               Right_Speaker_img.gameObject.SetActive(false);
          }

          



     }




     private void Update()
     {
          if (Input.GetMouseButtonDown(0)&&CDB_List.Count>0)
          {
               SAY_Pop();
          }
     }


}
