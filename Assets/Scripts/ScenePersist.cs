using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    // State
    int m_startingSceneIndex = 0;

    private void Awake()
    {
        int numberOfPersistingScenes = FindObjectsOfType<ScenePersist>().Length;
        if (numberOfPersistingScenes > 1)
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
        m_startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != m_startingSceneIndex)
        {
            Destroy(gameObject);
        }
    }

}
