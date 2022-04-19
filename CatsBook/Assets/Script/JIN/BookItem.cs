using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookItem : MonoBehaviour
{
     public Book book;
     
     private void Start()
     {
          GetComponent<Button>().onClick.AddListener(() => BookSelectButtonClick());
     }

     public void BookSelectButtonClick()
     {
          BookStorage.BookInfomation bi = BookStorage.Instance.bookinformation;

          bi.BookName.text = book.BookName;
          bi.BookGenre.text = book.Genre;
          bi.BookContents.text = book.Contents;
     }
}
