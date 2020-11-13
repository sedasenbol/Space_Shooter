using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private int UIScore;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    private Player player;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("Game Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + player.UIScore();
    }
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
    }
    public void GameOver()
    {
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }
    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            _restartText.text = "Press the R key to restart the level";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}