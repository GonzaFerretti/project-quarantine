using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
       ResourceManager.ResetResources();
       SceneManager.LoadScene("Demo House");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void ArtQuery()
    {
        SceneManager.LoadScene("ArtQuery");
    }
}
