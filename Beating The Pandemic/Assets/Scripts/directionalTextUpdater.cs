using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class directionalTextUpdater : MonoBehaviour
{
    TextMeshProUGUI textObj;
    void Start()
    {
        textObj = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string s)
    {
        textObj.text = s;
    }
    public string getText(string s)
    {
        return textObj.text;
    }

    public void setColor(Color c)
    {
        textObj.color = c;
    }
}
