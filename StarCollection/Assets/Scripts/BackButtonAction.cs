using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonAction : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadPreviousScene);
    }

    private void LoadPreviousScene()
    {
        
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousSceneIndex);
    }
}
