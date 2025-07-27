using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeNickname : MonoBehaviour
{

    //public Text nicknameText;
    public TextMeshProUGUI nicknameText;
    public TMP_InputField inputField;
    //private TextMeshProUGUI[] inputFields;

    // Start is called before the first frame update
    void Start()
    {
        //nicknameText = GetComponent<Text>();
        nicknameText = GetComponent<TextMeshProUGUI>();
        inputField = GetComponent<TMP_InputField>();
        if(FindInActiveObjectByTag("StartCanvas"))
            inputField = FindInActiveObjectByTag("StartCanvas").GetComponentInChildren<TMP_InputField>();

        /* inputFields = FindInActiveObjectByTag("StartCanvas").GetComponentsInChildren<TextMeshProUGUI>();

        int index = -99;
        for(int i = 0; i < inputFields.Length; i++)
        {
            if(inputFields[i].CompareTag("NicknameField"))
            {
                index = i;
            }
        } */


        //nicknameText.text = inputFields[index].text;
        if(nicknameText && inputField)
            nicknameText.text = inputField.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
