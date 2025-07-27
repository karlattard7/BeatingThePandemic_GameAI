using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    void Awake()
    {
        // Finding gameobjects with the following tag
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        // If there are more than one gameobject with the same tag, destroy it to only have 1 song
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject); // function used to play background music along all the scenes
        
    }
}
