using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Linq;






public class BookStorage : MonoSingleTon<BookStorage>
{


     public Dictionary<int, List<Book>> book_Dict = new Dictionary<int, List<Book>>();


     public List<Book> book_list_Poem = new List<Book>();
     public List<Book> book_list_Novel = new List<Book>();
     public List<Book> book_list_Essay = new List<Book>();

     public List<Book> Current_Select_Genre_Books = new List<Book>();

     public Button[] BookShelf_BtnArray;

     public enum Genre
     {
          Poem=0,
          Novel=1,
          Essay=2
     }

     public int Selected_genre_index;
     //0 : 소설
     //1 : 시 ....

     public GameObject BookShelfItemPrefab;
     public GameObject BookItemPrefab;

     public Transform BookShelfParentTr;


     public Transform BookShelfScrollViewContentTr;

     [System.Serializable]
     public class BookInfomation
     {
          public GameObject BookInfo_Obj;
          public Image BookImage;



          public Text BookName;
          public Text BookGenre;
          public Text BookContents;

          

     }
     public BookInfomation bookinformation = new BookInfomation();




     
     [Header("감시용")]
     [SerializeField]
     private Genre SelectedGenre;

     public void SettingBookStorageGUI()
     {
          setting_BookShelf();
     }

     private void Start()
     {
          for(int i = 0; i < BookShelf_BtnArray.Length; i++)
          {
               int num = i;
               BookShelf_BtnArray[i].onClick.AddListener(() => ShelfSelect_Button_Click(num));
          }

          //gameObject.SetActive(false);
     }
     
     void setting_BookShelf_ver02()
     {
          for (int i = 0; i < BookShelfParentTr.childCount; i++)
          {
               Destroy(BookShelfParentTr.GetChild(i).gameObject);
          }

          for (int i = 0; i < Current_Select_Genre_Books.Count; i++) {
               var instance = Instantiate(BookShelfItemPrefab, BookShelfParentTr);
               instance.GetComponent<BookShelfItem>().this_book = Current_Select_Genre_Books[i];
               instance.GetComponent<BookShelfItem>().this_book.Value =
                    CurrentHaveAsset.BookS.Where(x => x.BookName.Equals(Current_Select_Genre_Books[i].BookName)).First().Value;
                    }
     }

     void setting_BookShelf()
     {
          for(int i = 0; i < BookShelfParentTr.childCount; i++)
          {
               Destroy(BookShelfParentTr.GetChild(i).gameObject);
          }

          int temp_bookshelf_number = Current_Select_Genre_Books.Count/10;

          if (temp_bookshelf_number >= 0) {
               for (int i = 0; i < temp_bookshelf_number+1; i++)
               {
                    var instance = Instantiate(BookShelfItemPrefab, BookShelfParentTr);
                    if (i < temp_bookshelf_number)
                    {
                         for (int j = 0; j < 10; j++)
                         {
                              instance.GetComponent<BookShelfItem>().book_list.Add(Current_Select_Genre_Books[i * 10 + j]);
                         }
                    }
                    else if (i == temp_bookshelf_number)
                    {
                         for(int j = 0; j < Current_Select_Genre_Books.Count % 10; j++)
                         {
                              instance.GetComponent<BookShelfItem>().book_list.Add(Current_Select_Genre_Books[i * 10 + j]);
                         }
                    }
               }
          }

     }

     public void SelectGenre(int genre_index)
     {
          Selected_genre_index = genre_index;
          Current_Select_Genre_Books = book_Dict[Selected_genre_index];
          /*
          Current_Select_Genre_Books = book_Dict[genre_index];

          switch (genre_index)
          {
               :
                    
                    break;

               case Genre.Novel:
                    Current_Select_Genre_Books = book_list_Novel;
                    break;
               case Genre.Essay:
                    Current_Select_Genre_Books = book_list_Essay;
                    break;
               default:
                    Debug.Log("ERROR:잘못된 장르선택");
                    break;
               
          }*/
          setting_BookShelf_ver02();
          //setting_BookShelf();
     }

     public void GatherBook(string BookName,int value)
     {

          //var book_ = CurrentHaveAsset.BookSs.Where(m => m.BookName.Equals(BookName)).SingleOrDefault();

          var book_ = CurrentHaveAsset.BookS.Where(m => m.BookName.Equals(BookName)).SingleOrDefault();


          if (book_ != null)
          {
               book_.Value += value;
          }
          else
          {
               var book_new = AllBookDB.Instance.AllBookdb.Where(m => m.BookName.Equals(BookName)).SingleOrDefault();

               if (book_new != null)
               {
                    CurrentHaveAsset.BookS.Add(book_new);

                    //CurrentHaveAsset.BookS.Add(book_new.Genre, AllBookDB.ALLBOOK_DICT[BookName]);
                    //CurrentHaveAsset.BookS.Add(AllBookDB.ALLBOOK_DICT[BookName].Genre, AllBookDB.ALLBOOK_DICT[BookName]);
               }
               
               

               string tempgenre = book_new.Genre;

               Book temp_book = book_new;
               temp_book.Value = 1;
               int index_genre;

               switch (tempgenre)
               {
                    case "시":
                         //book_list_Poem.Add(new Book(BookName));
                         
                         index_genre = 0;
                         book_Dict[index_genre].Add(temp_book);
                         
                         //book_list_Poem.Add(temp_book);
                         
                         break;
                    case "소설":
                         
                         index_genre = 1;
                         book_Dict[index_genre].Add(temp_book);
                         //book_list_Novel.Add(temp_book);
                         //book_list_Novel[book_list_Novel.Count - 1].Value = 1;
                         break;
                    case "수필":
                         
                         index_genre = 2;
                         book_Dict[index_genre].Add(temp_book);
                         //book_list_Essay.Add(temp_book);
                         //book_list_Essay[book_list_Essay.Count - 1].Value = 1;
                         break;
                    default:
                         Debug.Log("ERROR: 잘못된 책 장르입니다.");
                         break;
               }
          }
     }



     public void ShelfSelect_Button_Click(int genre_index)
     {
          SelectGenre(genre_index);
     }

     /*

     public void PoemShelf_ButtonClick()
     {
          SelectGenre(Genre.Poem);

     }

     public void NovelShelf_ButtonClick()
     {

          SelectGenre(Genre.Novel);
     }
     public void EssayShelf_ButtonClick()
     {
          SelectGenre(Genre.Essay);

     }*/

     #region 정렬
     public void OrderOfEnter_ButtonClick()
     {

     }

     #endregion


}
