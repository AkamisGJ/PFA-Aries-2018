
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public Animator m_camera;
    

    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void introduction()
    {
        SceneManager.LoadScene(1);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    public void Level3()
    {
        SceneManager.LoadScene(4);
    }

    public void Level4()
    {
        SceneManager.LoadScene(5);
    }

    public void ToogleBool(string Parameter)
    {
        bool value = m_camera.GetBool(Parameter);
        m_camera.SetBool(Parameter, !value);
    }
}
