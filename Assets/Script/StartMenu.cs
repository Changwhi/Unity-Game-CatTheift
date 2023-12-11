using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public void LoadSignUpScene()
    {
        SceneManager.LoadScene("Sign Up");
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login");
    }
}
