using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    static float time;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600)/60).ToString("00");
        string seconds = (time % 60).ToString("00");
        text.text = hours + ":" + minutes + ":" + seconds;
    }
}
