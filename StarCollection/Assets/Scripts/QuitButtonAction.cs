using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButtonAction : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(QuitApplication);
    }

    private void QuitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
