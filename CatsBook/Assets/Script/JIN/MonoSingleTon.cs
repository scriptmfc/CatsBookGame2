using System.Collections;
using System.Collections.Generic;
using UnityEngine;



     public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
     {
          /*
          public static T Instance { get; private set; }

          void Awake() => Instance = FindObjectOfType(typeof(T)) as T;
          */

          private static T instance;

          public static T Instance
          {
               get
               {
                    if (instance == null)
                    {
                         var obj = (T)FindObjectOfType(typeof(T));
                         if (obj != null)
                         {
                              instance = obj;
                         }
                         else
                         {

                              Debug.Log("SingleTon instance를 부르는데 실패했습니다.");
                         Debug.Log("1. 이유 : 이 Script를 달아놓은 오브젝트가 없을 가능성이 큽니다.");
                              


                         }

                    }
                    return instance;
               }
          }

     }
