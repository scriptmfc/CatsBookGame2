using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BookShelfItem : MonoBehaviour
{
     //public BookShelfScriptableObject bookshelfDB;

     public Book this_book;

     public Text BookValue_txt;

     public Text BookName_txt;


     public List<Book> book_list = new List<Book>();

     //public Text bookvalue_inshelf_txt;

     private void Start()
     {
          //GetComponent<Button>().onClick.AddListener(() => BookShelfSelectButtonClick());
          GetComponent<Button>().onClick.AddListener(() => ButtonClick());
          BookValue_txt.text = this_book.Value.ToString();
          BookName_txt.text = this_book.BookName;
     }

     public void ButtonClick()
     {
          collectBook();
     }

     void collectBook()//회수하기   (책장에서 책가방으로)
     {

          if (CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(this_book.BookName)).First().Value > 0)
          {
               CurrentHaveAsset.Instance.BackPack_Book_Add(this_book.BookName, 1);
               CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(this_book.BookName)).First().Value -= 1;
          }
     }

     private void Update()
     {
          BookValue_txt.text = this_book.Value.ToString();
     }

     public void BookShelfSelectButtonClick()
     {
          
          BookItemSetting();

     }

     void BookItemSetting()
     {
          for(int i = 0; i < BookStorage.Instance.BookShelfScrollViewContentTr.childCount; i++)
          {
               Destroy(BookStorage.Instance.BookShelfScrollViewContentTr.GetChild(i).gameObject);
          }

          for(int i = 0; i < book_list.Count; i++)
          {
               var instance =Instantiate(BookStorage.Instance.BookItemPrefab, BookStorage.Instance.BookShelfScrollViewContentTr);
               //instance.GetComponent<BookItem>().bookDB = bookshelfDB.book_list[i];
               instance.GetComponent<BookItem>().book = book_list[i];


               instance.GetComponentInChildren<Text>().text =book_list[i].BookName;

               
          }
     }

}
