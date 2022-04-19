using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BuyBookEnum
{
     BuyFromBackPack=0,
     BuyFromBookShelf=1
}

public class CatGuestBehavior_Bookbuy : MonoBehaviour
{
     public GameObject Qrating_Obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

     void process()
     {
          Debug.Log("이 손님은 어떤 손님인지 체크");
          Debug.Log("ex) 이 손님은 음식을 한번이라도 서서 먹은 손님은 구매예정 : [3권 + !(북큐레이팅)2권  ] 입니다.");
          int normal=0;
          int bookQrating=0;


          process_bookbuy();

     }



     public void process_bookbuy()
     {
          Debug.Log("50%확률로 기존장르에서 책을사거나 해금레벨의 책을 삽니다.");
          if (Random.Range(0, 2) == 0)
          {
               Debug.Log("기존장르에서 책을삽니다.");

               

               Book find_book = CurrentHaveAsset.BookS[Random.Range(0, CurrentHaveAsset.BookS.Count - 1)];
               //▲기존장르에 대한 걸로 수정 필요

               if (find_book.Value > 0)
               {
                    //find_book.Value-=1;
                    BuyBook(find_book.BookName, BuyBookEnum.BuyFromBookShelf);
                    //CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(find_book.BookName)).First().Value -= 1;
                    //StoreCounter.Gold += find_book.Price;
               }
               else
               {
                    Debug.Log("기존장르에서 책을사려했는데 책이 없습니다.");
               }
               
          }
          else
          {
               Debug.Log("해금레벨의 책을 삽니다.");

               string current_UnlockGenre ="시";
               Book find_book;
               find_book=CurrentHaveAsset.BookS.Where(x=>x.Genre.Equals(current_UnlockGenre)).First();
               if (find_book != null)
               {
                    if (find_book.Value > 0)
                    {
                         Debug.Log("해금레벨의 책을 샀습니다."+"책이름 : "+find_book.BookName);
                         //find_book.Value -= 1;
                         BuyBook(find_book.BookName, BuyBookEnum.BuyFromBookShelf);
                         //CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(find_book.BookName)).First().Value -= 1;
                         //StoreCounter.Gold += find_book.Price;
                         
                    }
                    
                    else
                    {
                         Debug.Log("해금레벨의 책을 사려 했으나 책이 없습니다.2");
                    }
                         
               }
               else
               {
                    Debug.Log("해금레벨의 책을 사려 했으나 책이 없습니다.");
               }




               
               
               


          }
          /*
          //if (bookQrating > 0)
          {
               Qrating_Obj.SetActive(true);

               int r = Random.Range(0, 100);

               //
          }*/
     }

     public void BuyBook(string bookname,BuyBookEnum buybookenum)
     {

          switch (buybookenum)
          {
               case BuyBookEnum.BuyFromBookShelf:
                    {

                         CurrentHaveAsset.BookS.Where(m => m.BookName.Equals(bookname)).First().Value-=1;
                         int _price = AllBookDB.Instance.AllBookdb.Where(m => m.BookName.Equals(bookname)).First().Price;
                         StoreCounter.Gold += _price;
                         Debug.Log(this + "손님이 " + bookname + "책을 " + _price + "골드에 샀습니다.(책장에서)");
                         break;
                    }
               case BuyBookEnum.BuyFromBackPack:
                    {
                         CurrentHaveAsset.Instance.BackPack_Book_Remove(bookname, 1);
                         int _price = AllBookDB.Instance.AllBookdb.Where(m => m.BookName.Equals(bookname)).First().Price;
                         StoreCounter.Gold += _price;
                         Debug.Log(this + "손님이 " + bookname + "책을 " + _price + "골드에 샀습니다.(책가방에서)");
                         break;
                    }
               default:
                    Debug.Log("잘못된 Enum입니다. :" + buybookenum);
                    break;
          }
          

          //CurrentHaveAsset.BookS[bookname].Value -= 1;

          //Currency.Gold +=CurrentHaveAsset.BookS[bookname].Price;
     }


     // Update is called once per frame
     void Update()
    {
        
    }
}
