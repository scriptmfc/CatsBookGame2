using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMainCamera : MonoBehaviour
{
    //public Transform mapPos;
    //public Transform slotPos;
    void Start()
    {
        Resolution();
    }

    private void Resolution()
    {
        //float zoomLevel = 1.15f; //아이폰 x
        int maxBoardSize = 10;
        float zoomLevel = 1f;
        float basicRate = 1920f / 1080f;
        float screenRate = Screen.height / (float)Screen.width;

        //Camera.main.orthographicSize = (_boardSize * zoomLevel) * (Screen.height / (float)Screen.width) * 0.5f;
        float size = (maxBoardSize * zoomLevel) * screenRate / basicRate;
        if(size > 10f)
        {
            Camera.main.orthographicSize = size;
            //mapPos.localPosition = new Vector3(0f, -size + 10f, 0f);
            //slotPos.localPosition = new Vector3(0f, -size + 9.6f, 0f);


//#if UNITY_ANDROID && !UNITY_EDITOR
//#elif UNITY_IOS && !UNITY_EDITOR
//            if (Screen.height == 2436 && Screen.width == 1125)
//            {
//                mapPos.localPosition = new Vector3(0f, -1, 0f);
//                slotPos.localPosition = new Vector3(0f, -1.6f, 0f);
//            }
//#endif
        }       
    }
}
