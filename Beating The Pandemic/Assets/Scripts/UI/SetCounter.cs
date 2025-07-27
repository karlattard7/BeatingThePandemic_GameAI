using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCounter : MonoBehaviour
{

    public TextMeshProUGUI counterText;
    public static int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        counterText.text = counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        counterText.text = counter.ToString();

    }

    /*public void incrementCounter()
    {
        counter++;
        counterText.text = counter.ToString();
    }

    public void decrementCounter()
    {
        if(counter != 0)
        {
            counter--;
            counterText.text = counter.ToString();
        }
    }*/

    public void setCounter(int i)
    {
        counter = i;
        counterText.text = counter.ToString();

    }

}
