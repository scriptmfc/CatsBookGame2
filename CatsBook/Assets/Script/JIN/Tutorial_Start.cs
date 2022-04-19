using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Start : MonoBehaviour
{
     public bool tutorial_switch;

     private ConversationManager CM;

     public ConversationScriptable CDB;

     

     [Header("감시용")]
     private int FlowNumbering=0;



    // Start is called before the first frame update
    void Start()
    {
          CM = ConversationManager.Instance;

          if (tutorial_switch)
               tutorial_go1();
    }

     void tutorial_go1()
     {
          CM.SAY_Push_DB(CDB,0);
          CM.SAY_Push_DB(CDB,1);
          CM.SAY_Push_DB(CDB, 2);
          CM.SAY_Push_DB(CDB, 3);//여기까지 프롤로그//독백

          
     }



     void CM_message_check()
     {
          switch (CM.Message)
          {
               case "프롤로그끝":
                    if(FlowNumbering == 0)
                         
                    {
                         FlowNumbering = 1;
                         CM.SAY_Push_DB(CDB, 4);//서점입장//공인중개사
                         CM.SAY_Push_DB(CDB, 5);
                         CM.SAY_Push_DB(CDB, 6);

                    }
               
                    
                    break;
               case "청소하자":
                    if (FlowNumbering == 1)
                    {
                         //쓰레기 반짝임, 쓰레기 청소해야함
                         FlowNumbering = 2;


                    }
                    break;
               case "청소완료":
                    if(FlowNumbering==2)
                    {
                         FlowNumbering = 3;
                         CM.SAY_Push_DB(CDB, 7);
                         CM.SAY_Push_DB(CDB, 8);
                    }
                    break;
               case "책장과계산대구매배치완료":
                    if (FlowNumbering == 3)
                    {
                         FlowNumbering = 4;
                         CM.SAY_Push_DB(CDB, 9);
                         CM.SAY_Push_DB(CDB, 10);
                         CM.SAY_Push_DB(CDB, 11);
                         CM.SAY_Push_DB(CDB, 12);
                         CM.SAY_Push_DB(CDB, 13);
                    }
                    break;
               case "공인중개사퇴장":
                    if (FlowNumbering == 4)
                    {
                         FlowNumbering = 5;
                         //다이어리 UI 반짝하다 사라짐
                         //맵 UI 클릭할때까지 반짝
                    }
                    break;
               case "마을맵으로이동":
                    if (FlowNumbering == 5)
                    {
                         FlowNumbering = 6;
                         //이장집반짝
                    }
                    break;
               case "이장집클릭":
                    if (FlowNumbering == 6)
                    {
                         FlowNumbering = 7;
                         CM.SAY_Push_DB(CDB, 14);
                         CM.SAY_Push_DB(CDB, 15);
                         CM.SAY_Push_DB(CDB, 16);
                         CM.SAY_Push_DB(CDB, 17);
                         //집위에 책아이콘 생성. 클릭해야함
                    }
                    break;
               case "책구매완료":
                    if (FlowNumbering == 7)
                    {
                         FlowNumbering = 8;
                         CM.SAY_Push_DB(CDB, 18);
                         CM.SAY_Push_DB(CDB, 19);
                         CM.SAY_Push_DB(CDB, 20);
                         CM.SAY_Push_DB(CDB, 21);
                         //집 위에 하트 생성//클릭하면 책 기부창 뜸
                         //기부책 5권 받기 완료
                    }
                    break;
               case "기부책5권받기완료":
                    if (FlowNumbering == 8)
                    {
                         FlowNumbering = 9;
                         CM.SAY_Push_DB(CDB, 22);
                         CM.SAY_Push_DB(CDB, 23);
                         //이장 퇴장
                         //서점 아이콘 반짝
                    }
                    break;
               case "서점돌아옴":
                    if (FlowNumbering == 9)
                    {
                         FlowNumbering = 10;
                         CM.SAY_Push_DB(CDB, 24);
                         
                         //책가방 UI 반짝> '모두선택'반짝 > '책장에 정리'반짝
                         
                    }
                    break;
               case "모든책정리":
                    if (FlowNumbering == 10)
                    {
                         FlowNumbering = 11;
                         
                        
                        //첫손님 등장, !말풍선 뜸 

                         //!클릭하면
                    }
                    break;
               case "말풍선클릭":
                    if (FlowNumbering == 11)
                    {
                         FlowNumbering = 12;
                         CM.SAY_Push_DB(CDB, 25);
                         CM.SAY_Push_DB(CDB, 26);
                         //책꽂이 UI 반짝
                         //해당책 회수하면
                    }
                    break;
               case "책회수":
                    if (FlowNumbering == 12)
                    {
                         FlowNumbering = 13;
                         CM.SAY_Push_DB(CDB, 27);
                         
                         //'예'클릭 >책 제공
                         //코인 획득, 경험치 획득
                    }
                    break;
               case "책제공":
                    if (FlowNumbering == 13)
                    {
                         FlowNumbering = 14;
                         CM.SAY_Push_DB(CDB, 28);
                         
                         //손님퇴장
                         //튜토리얼 종료
                    }
                    break;

          }

          CM.Message = "";

     }

    // Update is called once per frame
    void Update()
    {
          CM_message_check();


          roomclear();
    }

     void roomclear()
     {
          Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          RaycastHit2D hit = Physics2D.Raycast(mouseposition, transform.forward, 50f);

          

               if (hit && Input.GetMouseButtonDown(0))
               {
               if (hit.transform.CompareTag("Interactive"))
               {
                    if (hit.transform.name == "DirtTutorial")
                    {
                         Destroy(hit.transform.gameObject);
                         CM.Message = "청소완료";
                    }

               }
          }
     }
}
