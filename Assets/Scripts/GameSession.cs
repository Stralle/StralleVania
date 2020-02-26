using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField]
    int m_playerLives = 3;

    [SerializeField]
    int m_playerScore = 0;

    [SerializeField]
    Text m_livesText;

    [SerializeField]
    Text m_scoreText;

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        RefreshUI();
    }

    public void ProcessPlayerDeath()
    {
        if (m_playerLives > 1)
        {
            TakeLife();
            RefreshUI();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        m_playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd)
    {
        m_playerScore += pointsToAdd;
        RefreshUI();
    }

    private void RefreshUI()
    {
        m_livesText.text = m_playerLives.ToString();
        m_scoreText.text = m_playerScore.ToString();
    }
}
