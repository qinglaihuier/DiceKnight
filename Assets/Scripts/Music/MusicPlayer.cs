using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
   static MusicPlayer _instance;
    
    public static MusicPlayer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MusicPlayer>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(gameObject);
        }
    }
}
