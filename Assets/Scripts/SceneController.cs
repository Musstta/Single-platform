using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
   public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
}
