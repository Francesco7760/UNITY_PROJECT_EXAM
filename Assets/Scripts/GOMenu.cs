using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("ScenaMappa");
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
