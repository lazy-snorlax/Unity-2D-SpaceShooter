using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;

    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }
    }
    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }  
    
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        _gameManager.isGameOver();
        _gameOver.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
