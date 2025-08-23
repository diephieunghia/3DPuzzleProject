using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButton()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
