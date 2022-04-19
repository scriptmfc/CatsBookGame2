using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CurrentHaveAsset : MonoSingleTon<CurrentHaveAsset>
{
     //public List<책장>

     //public delegate void BackPackdelegate(string bookname,int value);
     //public event BackPackdelegate BPAddEvent;
     //public event BackPackdelegate BPRemoveEvent;
     

     //public static Dictionary<string, Book> BookS = new Dictionary<string, Book>();
     public static List<Book> BookS = new List<Book>();

     //public static List<Book> BookSs = new List<Book>();

     public static List<Book> BackPack_BookS = new List<Book>();

     [Header("감시용")]
     [SerializeField]
     private List<Book> BackPack_BookS_observing = new List<Book>();

     private void Update()
     {
          BackPack_BookS_observing = BackPack_BookS;
     }

     private void Start()
     {
          //BPAddEvent += BackPack_Book_Add;
          //BPRemoveEvent += BackPack_Book_Remove;
     }

     public void Book_Collect(string bookname)//책 "회수" ( 책장에서->책가방으로)
     {
          if (BookS.Where(x => x.BookName.Equals(bookname)).First().Value > 0)
          {
               BookS.Where(x => x.BookName.Equals(bookname)).First().Value -= 1;
               BackPack_Book_Add(bookname, 1);
          }
          else
          {
               Debug.Log("Error : 해당 책이 없어요");
          }
     }


     public void BackPack_Book_Add(string bookname, int value)
     {
          if (BackPack_BookS.Where(x => x.BookName.Equals(bookname)).ToList().Count==0)
          {
               Book _book = AllBookDB.Instance.AllBookdb.Where(x => x.BookName.Equals(bookname)).First();

               Book new_book =new Book(_book.BookName,_book.Genre,_book.Contents);

               new_book.Price = _book.Price;
               new_book.Value = value;
               BackPack_BookS.Add(new_book);

               Debug.Log(BackPack_BookS[BackPack_BookS.Count - 1].Value + "개수;;");

               Debug.Log("책가방에" + bookname + "책을" + value + "개 넣었습니다.");
               Debug.Log(CurrentHaveAsset.BackPack_BookS[BackPack_BookS.Count-1].Value + "개수---");

          }
          else
          {
               BackPack_BookS.Where(x => x.BookName.Equals(bookname)).First().Value+=value;
          }
     }
     public void BackPack_Book_Remove(string bookname, int value)
     {
          if (BackPack_BookS.Where(x => x.BookName.Equals(bookname)).ToList().Count == 0)
          {
               Debug.Log("Error!!!! 해당 책은 0개 입니다."+"  ** 책 이름:"+ bookname);
          }
          else
          {
               if(BackPack_BookS.Where(x => x.BookName.Equals(bookname)).First().Value -value<0)
               {
                    Debug.Log("Error!!!! 해당 책을 제거하면 0개미만 입니다." + "  ** 책 이름:" + bookname +
                         "\n해당 책 개수 :"+ BackPack_BookS.Where(x => x.BookName.Equals(bookname)).First().Value);
               }
               else 
                    BackPack_BookS.Where(x => x.BookName.Equals(bookname)).First().Value -= value;
          }
     }


     //public List<BookShelfScriptableObject> BookShelf_List = new List<BookShelfScriptableObject>();

     public List<CatClerk> CatClerk_List = new List<CatClerk>();

}
