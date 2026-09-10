using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonAction : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
