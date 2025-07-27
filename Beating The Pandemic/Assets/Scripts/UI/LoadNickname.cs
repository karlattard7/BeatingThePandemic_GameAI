using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadNickname : MonoBehaviour
{
    //public InputField inputField;
    //public TextMeshProUGUI inputField;
    public TMP_InputField inputField;
    private GameObject obj;

    private void Start()
    {
        obj = GameObject.FindGameObjectWithTag("StartCanvas");
        

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            obj.SetActive(false);
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
