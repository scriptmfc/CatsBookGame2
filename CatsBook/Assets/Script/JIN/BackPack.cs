using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoSingleTon<BackPack>
{
     public GameObject BookItem_BackPack_Prefab;
     public Transform BookItem_BackPack_ParentTr;

     public List<Book> SelectedBook = new List<Book>();

     public GameObject BackPackGUIObj;

     public void BackPackOpenClose()
     {
          if (!BackPackGUIObj.activeSelf)
          {
               BackPackGUIObj.SetActive(true);
               GUI_Update();
          }
          else
               BackPackGUIObj.SetActive(false);
     }
     


     public void All_Organize_IntoBookShelf()
     {
          for(int i = 0; i < CurrentHaveAsset.BackPack_BookS.Count; i++)
          {
               
               BookStorage.Instance.GatherBook(CurrentHaveAsset.BackPack_BookS[i].BookName,
                    CurrentHaveAsset.BackPack_BookS[i].Value);
               CurrentHaveAsset.Instance.BackPack_Book_Remove(CurrentHaveAsset.BackPack_BookS[i].BookName
                    , CurrentHaveAsset.BackPack_BookS[i].Value);

          }
          GUI_Update();
     }


     public void Book_Organize_SelectedBooks()
     {
          GUI_Update();
     }

     void Book_Organize_OneBook(string bookname, int value)//책 "정리하기" (책가방에서 -> 책장으로)
     {
          CurrentHaveAsset.Instance.BackPack_Book_Remove(bookname, value);
          BookStorage.Instance.GatherBook(bookname, value);

          
     }


     public void GUI_Update()
     {
          if(BookItem_BackPack_ParentTr.childCount>0)
          for (int i = 0; i < BookItem_BackPack_ParentTr.childCount; i++)
          {
               Destroy(BookItem_BackPack_ParentTr.GetChild(i).gameObject);
          }


          if (CurrentHaveAsset.BackPack_BookS.Count > 0)
          {
               for (int i = 0; i < CurrentHaveAsset.BackPack_BookS.Count; i++)
               {
                    var instance = Instantiate(BookItem_BackPack_Prefab, BookItem_BackPack_ParentTr);
                    instance.GetComponent<BookItem_BackPack>().this_book = CurrentHaveAsset.BackPack_BookS[i];

                    Debug.Log(CurrentHaveAsset.BackPack_BookS[i].BookName + "개수");
                    Debug.Log(CurrentHaveAsset.BackPack_BookS[i].Value + "개수---");


               }
          }
     }

}
