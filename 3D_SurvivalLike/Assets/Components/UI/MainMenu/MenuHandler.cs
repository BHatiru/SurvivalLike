using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    //temporary
    public void StartGame()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void Quit(){
        Application.Quit();
    }

}
