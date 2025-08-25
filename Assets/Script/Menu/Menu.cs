using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButton(GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
