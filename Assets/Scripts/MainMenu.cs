using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    void Awake(){ Instance = this; }

    public void PlaySinglePlayer(){
        SceneManager.LoadScene("Single Player Game Scene");
    }

    public void PlayLocalCoop(){
        SceneManager.LoadScene("Coop Game Scene");
    }

    //public void PlayOnline(){}

    //public void OpenMenuSettings(){}

    public void QuitGame(){
        Application.Quit();
    }
}
