using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Start : MonoBehaviour
{
     public bool tutorial_switch;

     private ConversationManager CM;

     public ConversationScriptable CDB;

     

     [Header("���ÿ�")]
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
          CM.SAY_Push_DB(CDB, 3);//������� ���ѷα�//����

          
     }



     void CM_message_check()
     {
          switch (CM.Message)
          {
               case "���ѷα׳�":
                    if(FlowNumbering == 0)
                         
                    {
                         FlowNumbering = 1;
                         CM.SAY_Push_DB(CDB, 4);//��������//�����߰���
                         CM.SAY_Push_DB(CDB, 5);
                         CM.SAY_Push_DB(CDB, 6);

                    }
               
                    
                    break;
               case "û������":
                    if (FlowNumbering == 1)
                    {
                         //������ ��¦��, ������ û���ؾ���
                         FlowNumbering = 2;


                    }
                    break;
               case "û�ҿϷ�":
                    if(FlowNumbering==2)
                    {
                         FlowNumbering = 3;
                         CM.SAY_Push_DB(CDB, 7);
                         CM.SAY_Push_DB(CDB, 8);
                    }
                    break;
               case "å������뱸�Ź�ġ�Ϸ�":
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
               case "�����߰�������":
                    if (FlowNumbering == 4)
                    {
                         FlowNumbering = 5;
                         //���̾ UI ��¦�ϴ� �����
                         //�� UI Ŭ���Ҷ����� ��¦
                    }
                    break;
               case "�����������̵�":
                    if (FlowNumbering == 5)
                    {
                         FlowNumbering = 6;
                         //��������¦
                    }
                    break;
               case "������Ŭ��":
                    if (FlowNumbering == 6)
                    {
                         FlowNumbering = 7;
                         CM.SAY_Push_DB(CDB, 14);
                         CM.SAY_Push_DB(CDB, 15);
                         CM.SAY_Push_DB(CDB, 16);
                         CM.SAY_Push_DB(CDB, 17);
                         //������ å������ ����. Ŭ���ؾ���
                    }
                    break;
               case "å���ſϷ�":
                    if (FlowNumbering == 7)
                    {
                         FlowNumbering = 8;
                         CM.SAY_Push_DB(CDB, 18);
                         CM.SAY_Push_DB(CDB, 19);
                         CM.SAY_Push_DB(CDB, 20);
                         CM.SAY_Push_DB(CDB, 21);
                         //�� ���� ��Ʈ ����//Ŭ���ϸ� å ���â ��
                         //���å 5�� �ޱ� �Ϸ�
                    }
                    break;
               case "���å5�ǹޱ�Ϸ�":
                    if (FlowNumbering == 8)
                    {
                         FlowNumbering = 9;
                         CM.SAY_Push_DB(CDB, 22);
                         CM.SAY_Push_DB(CDB, 23);
                         //���� ����
                         //���� ������ ��¦
                    }
                    break;
               case "�������ƿ�":
                    if (FlowNumbering == 9)
                    {
                         FlowNumbering = 10;
                         CM.SAY_Push_DB(CDB, 24);
                         
                         //å���� UI ��¦> '��μ���'��¦ > 'å�忡 ����'��¦
                         
                    }
                    break;
               case "���å����":
                    if (FlowNumbering == 10)
                    {
                         FlowNumbering = 11;
                         
                        
                        //ù�մ� ����, !��ǳ�� �� 

                         //!Ŭ���ϸ�
                    }
                    break;
               case "��ǳ��Ŭ��":
                    if (FlowNumbering == 11)
                    {
                         FlowNumbering = 12;
                         CM.SAY_Push_DB(CDB, 25);
                         CM.SAY_Push_DB(CDB, 26);
                         //å���� UI ��¦
                         //�ش�å ȸ���ϸ�
                    }
                    break;
               case "åȸ��":
                    if (FlowNumbering == 12)
                    {
                         FlowNumbering = 13;
                         CM.SAY_Push_DB(CDB, 27);
                         
                         //'��'Ŭ�� >å ����
                         //���� ȹ��, ����ġ ȹ��
                    }
                    break;
               case "å����":
                    if (FlowNumbering == 13)
                    {
                         FlowNumbering = 14;
                         CM.SAY_Push_DB(CDB, 28);
                         
                         //�մ�����
                         //Ʃ�丮�� ����
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
                         CM.Message = "û�ҿϷ�";
                    }

               }
          }
     }
}
