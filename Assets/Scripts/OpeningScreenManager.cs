using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class OpeningScreenManager : MonoBehaviour
{
    public void StartButton()
    {

        LoadNextScene(1);

    }


    public void HighScores() {


        LoadNextScene(2);
    
    }

    public void QuitGame()
    {

        UnityEngine.Application.Quit();
    }

    public void LoadNextScene(int i) {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
    
    }

}
