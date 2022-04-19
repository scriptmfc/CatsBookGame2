using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Book
{
     public string BookName;
     public string Genre;
     public string Contents;


     public string image_path;

     public int Price;
     public int Value;

     public Book(string _BookName)
     {
          this.BookName = _BookName;
     }
     
     public Book(string _BookName,string _Genre, string _Contents)
     {
          this.BookName = _BookName;
          this.Genre = _Genre;
          this.Contents = _Contents;
     }

}
