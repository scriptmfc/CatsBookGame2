using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInteractiveManager : MonoSingleTon<RoomInteractiveManager>
{

     const string Interactive_Tagname = "Interactive";

     public GameObject QratingMessagePopUp;


     // Start is called before the first frame update
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {
          process();
     }

     void process()
     {
          Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          RaycastHit2D hit = Physics2D.Raycast(mouseposition, transform.forward, 50f);
          //Debug.DrawLine(mouseposition, transform.forward * 100);
          
          if (!CPopupManager.Instance.isOpen && !CPopupManager.Instance.block && hit && Input.GetMouseButtonDown(0))
          {
            // isOpen : Popup Open
            // block : Envent Popup Open

               if (hit.transform.CompareTag(Interactive_Tagname))
               {
                    if (hit.transform.GetComponent<QratingBalloon>() != null)
                    {
                        QratingMessagePopUp.GetComponent<GUI_QratingMessagePopUp>().popup.open(); // popup Open

                        QratingBalloon _qratingBalloon = hit.transform.GetComponent<QratingBalloon>();
                        QratingMessagePopUp.GetComponent<GUI_QratingMessagePopUp>().bookname.text
                            = _qratingBalloon.QratingBook_List[0].BookName;
                    }
               }
          }
     }
}
