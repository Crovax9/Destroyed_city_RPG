using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("start");
        SceneManager.LoadScene("destroyed_city");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}