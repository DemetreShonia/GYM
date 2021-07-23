using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyBat
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
        public static T instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                }
                return _instance;
            }
        }
    }
}


