using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this class is for simple menu navigation
public class MenuManager : MonoBehaviour
{
    //start the game
    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    //back to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //back to main menu
    public void HelpScreen()
    {
        SceneManager.LoadScene("HelpScreen");
    }


}