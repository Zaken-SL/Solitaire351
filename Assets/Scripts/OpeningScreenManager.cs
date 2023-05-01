using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class OpeningScreenManager : MonoBehaviour
{

    //loads the game scene and starts the game
    public void StartButton()
    {

        LoadNextScene(1);

    }

    //Loads the database high score connection
    public void HighScores() {


        LoadNextScene(2);
    
    }

    //Quits the application
    public void QuitGame()
    {

        UnityEngine.Application.Quit();
    }
    //calls a scene depending on its order number
    public void LoadNextScene(int i) {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
    
    }

}
