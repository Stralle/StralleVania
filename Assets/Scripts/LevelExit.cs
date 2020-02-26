using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField]
    float m_LevelLoadDelay = 2.0f;

    [SerializeField]
    float m_LevelExitSlowMoFactor = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        Time.timeScale = m_LevelExitSlowMoFactor;
        yield return new WaitForSecondsRealtime(m_LevelLoadDelay);
        Time.timeScale = 1.0f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        FindObjectOfType<ScenePersist>().DestroyMe();
    }

}
