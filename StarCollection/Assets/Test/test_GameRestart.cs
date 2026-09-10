using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test_GameRestart : MonoBehaviour
{
    int count = 0;
    int restartCount = 0;

    // Start is called before the first frame update
    private void Start()
    {
        restartCount = PlayerPrefs.GetInt("RestartCount", 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (count == 300)
        {
            restartCount++;
            Retry();
            Debug.Log(restartCount.ToString());
        }
        count++;
    }

    private void Retry()
    {
        PlayerPrefs.SetInt("RestartCount", restartCount);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
