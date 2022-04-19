using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookItem_BackPack : MonoBehaviour
{
     public Text selected_book_value_txt;
     public Text have_book_value_txt;
     public Text bookName_txt;

     public Book this_book;

     private void Start()
     {
          bookName_txt.text = this_book.BookName;
     }

     private void Update()
     {
          have_book_value_txt.text = this_book.Value.ToString();
     }

}
