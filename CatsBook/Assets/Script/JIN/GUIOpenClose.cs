using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIOpenClose : MonoBehaviour
{
     public GameObject WindowObj;
     public void WindowOpenCloseToggle()
     {
          if (WindowObj.activeSelf)
               WindowObj.SetActive(false);
          else
               WindowObj.SetActive(true);

     }

}
