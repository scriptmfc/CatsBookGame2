using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoSingleTon<HouseManager>
{
     

     const string Interactive_Tagname = "Interactive";
     public Camera maincamera;

     Vector3 Village_camera_position;
     Vector3 Room_camera_position;

     public bool Camera_Is_VillagePostion;

    // Start is called before the first frame update
    void Start()
    {
          Village_camera_position = new Vector3(105,0,-10);
          Room_camera_position = new Vector3(0,0,-10);
    }


     public void Camera_position_change()
     {
          if (Camera_Is_VillagePostion)
          {
               maincamera.transform.position = Room_camera_position;
               Camera_Is_VillagePostion = false;
          }
          else
          {
               maincamera.transform.position = Village_camera_position;
               Camera_Is_VillagePostion = true;
          }
     }

    // Update is called once per frame
    void Update()
    {
          process();
    }

     void process()
     {
          Vector3 mouseposition = maincamera.ScreenToWorldPoint(Input.mousePosition);
          RaycastHit2D hit = Physics2D.Raycast(mouseposition, transform.forward, 50f);
          //Debug.DrawLine(mouseposition, transform.forward * 100);
          Debug.DrawRay(mouseposition, transform.forward * 100);
          if (hit&&Input.GetMouseButtonDown(0))
          {
               Debug.Log(hit.transform.gameObject);
               Debug.Log("hit!");
               
               if (hit.transform.CompareTag(Interactive_Tagname)&&(hit.transform.GetComponent<HouseScript>()!=null) )
               {    
                    
                    
                    switch (hit.transform.GetComponent<HouseScript>().housesortenum)
                    {
                         case HouseScript.HouseSortEnum.NormalHouse:
                              Debug.Log(hit.transform.GetComponent<HouseScript>().OldBookPopUpObj.GetComponent<OldBook>().Old_book.BookName);
                              CurrentHaveAsset.Instance.BackPack_Book_Add(
                                   hit.transform.GetComponent<HouseScript>().OldBookPopUpObj.GetComponent<OldBook>().Old_book.BookName,1);
                              break;
                         case HouseScript.HouseSortEnum.HeadOfVillage:
                              Debug.Log("이장집을 클릭");
                              break;

                    }
               }
          }
     }
}
