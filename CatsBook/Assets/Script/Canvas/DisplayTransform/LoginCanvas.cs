using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginCanvas : MonoBehaviour
{
    [SerializeField] CanvasScaler canvasScaler;

    // Start is called before the first frame update
    void Start()
    {
        int maxBoardSize = 10;
        float zoomLevel = 1f;
        float basicRate = 1920f / 1080f;
        float screenRate = Screen.height / (float)Screen.width;

        //Camera.main.orthographicSize = (_boardSize * zoomLevel) * (Screen.height / (float)Screen.width) * 0.5f;
        float size = (maxBoardSize * zoomLevel) * screenRate / basicRate;
        if (size >= 10f)
        {
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }

    }

}
