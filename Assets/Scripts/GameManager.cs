using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    // Start is called before the first frame update
    private void Update()
    {
        EscButton();
        NextScene();
    }
    public void NextScene()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); // Current Game Scene
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void EscButton()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
