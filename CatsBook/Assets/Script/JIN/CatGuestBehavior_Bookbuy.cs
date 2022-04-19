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
          Debug.Log("�� �մ��� � �մ����� üũ");
          Debug.Log("ex) �� �մ��� ������ �ѹ��̶� ���� ���� �մ��� ���ſ��� : [3�� + !(��ť������)2��  ] �Դϴ�.");
          int normal=0;
          int bookQrating=0;


          process_bookbuy();

     }



     public void process_bookbuy()
     {
          Debug.Log("50%Ȯ���� �����帣���� å����ų� �رݷ����� å�� ��ϴ�.");
          if (Random.Range(0, 2) == 0)
          {
               Debug.Log("�����帣���� å����ϴ�.");

               

               Book find_book = CurrentHaveAsset.BookS[Random.Range(0, CurrentHaveAsset.BookS.Count - 1)];
               //������帣�� ���� �ɷ� ���� �ʿ�

               if (find_book.Value > 0)
               {
                    //find_book.Value-=1;
                    BuyBook(find_book.BookName, BuyBookEnum.BuyFromBookShelf);
                    //CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(find_book.BookName)).First().Value -= 1;
                    //StoreCounter.Gold += find_book.Price;
               }
               else
               {
                    Debug.Log("�����帣���� å������ߴµ� å�� �����ϴ�.");
               }
               
          }
          else
          {
               Debug.Log("�رݷ����� å�� ��ϴ�.");

               string current_UnlockGenre ="��";
               Book find_book;
               find_book=CurrentHaveAsset.BookS.Where(x=>x.Genre.Equals(current_UnlockGenre)).First();
               if (find_book != null)
               {
                    if (find_book.Value > 0)
                    {
                         Debug.Log("�رݷ����� å�� ����ϴ�."+"å�̸� : "+find_book.BookName);
                         //find_book.Value -= 1;
                         BuyBook(find_book.BookName, BuyBookEnum.BuyFromBookShelf);
                         //CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(find_book.BookName)).First().Value -= 1;
                         //StoreCounter.Gold += find_book.Price;
                         
                    }
                    
                    else
                    {
                         Debug.Log("�رݷ����� å�� ��� ������ å�� �����ϴ�.2");
                    }
                         
               }
               else
               {
                    Debug.Log("�رݷ����� å�� ��� ������ å�� �����ϴ�.");
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
                         Debug.Log(this + "�մ��� " + bookname + "å�� " + _price + "��忡 ����ϴ�.(å�忡��)");
                         break;
                    }
               case BuyBookEnum.BuyFromBackPack:
                    {
                         CurrentHaveAsset.Instance.BackPack_Book_Remove(bookname, 1);
                         int _price = AllBookDB.Instance.AllBookdb.Where(m => m.BookName.Equals(bookname)).First().Price;
                         StoreCounter.Gold += _price;
                         Debug.Log(this + "�մ��� " + bookname + "å�� " + _price + "��忡 ����ϴ�.(å���濡��)");
                         break;
                    }
               default:
                    Debug.Log("�߸��� Enum�Դϴ�. :" + buybookenum);
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
