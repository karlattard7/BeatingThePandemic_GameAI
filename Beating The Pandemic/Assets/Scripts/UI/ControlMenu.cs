using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public static bool isClosed = false;
    public GameObject controlMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isClosed)
            {
                openMenu();
            }
            else
            {
                closeMenu();
            }
        }
    }

    public void closeMenu()
    {
        controlMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isClosed = true;
    }

    public void openMenu()
    {
        controlMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isClosed = false;
    }

}
