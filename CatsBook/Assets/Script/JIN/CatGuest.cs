using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatGuest : MonoBehaviour
{
     public List<Book> HaveBook;

     public int Schedule_QratingBookValue;
     public int Schedule_BuyBookValue;

     public int Schedule_PayAmount;

     public GameObject QratingBallonPrefab;
     
    // Start is called before the first frame update
    void Start()
    {
          Schedule_BuyBookValue = 2;
          Schedule_QratingBookValue = 2;
          Qrating_Expression();
    }

     public void Qrating_Expression()
     {

          if (Schedule_QratingBookValue > 0)
          {
               var instance = Instantiate(QratingBallonPrefab, gameObject.transform);
               instance.AddComponent<QratingBalloon>();
               QratingBalloon qratingBallon = instance.GetComponent<QratingBalloon>();

          
               qratingBallon.QratingBook_List.Add(Qrating_Book_Select());
               qratingBallon.HostCat = gameObject;
               qratingBallon.offset = new Vector3(0, 2.8f, 0);
               qratingBallon.transform.localPosition = qratingBallon.offset;
               qratingBallon.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
          }
          
     }

     Book Qrating_Book_Select()
     {
          List<Book> _book_list = CurrentHaveAsset.BookS.Where(x => x.Value > 0).ToList<Book>();
          Book _book = _book_list[Random.Range(0, _book_list.Count - 1)];
          _book.Value = 1;
          return _book;
     }

     public void PickBookFromBookShelf()
     {
          GetComponent<CatGuestBehavior_Bookbuy>().process_bookbuy();
     }
}
