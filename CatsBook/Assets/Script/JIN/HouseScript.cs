using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public enum HouseSortEnum
     {
          HeadOfVillage=0,
          NormalHouse=1,
          Publisher=2
     }

     public GameObject OldBookPopUpObj;

     public HouseSortEnum housesortenum;

     private void Start()
     {
          switch (housesortenum)
          {
               case HouseSortEnum.NormalHouse:
                    StartCoroutine(normalhouse_flow());
                    break;

          }
     }

     IEnumerator normalhouse_flow()
     {
          while (true)
          {
               yield return new WaitForSeconds(20f);

               int r = Random.Range(0, 2);


               OldBookPopUp(r);
               
               
              
          }
     }

     void OldBookPopUp(int FreeOrNot)
     {
          OldBookPopUpObj.SetActive(true);
          if(FreeOrNot==0)
               OldBookPopUpObj.GetComponent<OldBook>().ThisBookIsFree = true;
          else
               OldBookPopUpObj.GetComponent<OldBook>().ThisBookIsFree = false;

          OldBookPopUpObj.GetComponent<OldBook>().Old_book.BookName =
               AllBookDB.Instance.special_situation_book.OldBook_Genre_Book[
                    Random.Range(0, AllBookDB.Instance.special_situation_book.OldBook_Genre_Book.Count - 1)].BookName;

          
     }

}
