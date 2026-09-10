using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayButtonAction : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadHowToPlayScene);
    }

    private void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
