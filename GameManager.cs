using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver==true)
        {
            SceneManager.LoadScene(0); // Current Game Scene
        }
    }

    public void isGameOver()
    {
        _isGameOver = true;
    }
}
