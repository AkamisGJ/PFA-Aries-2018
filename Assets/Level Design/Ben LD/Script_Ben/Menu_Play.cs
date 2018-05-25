
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Play : MonoBehaviour 
{
    public void startButton()
    {
        SceneManager.LoadScene(1);
    }
}
