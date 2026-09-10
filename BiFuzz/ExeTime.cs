using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExeTime : MonoBehaviour
{
    private readonly string filePath = Path.Combine(Application.dataPath, "Logs", "exe_time.csv");
    private readonly int exeTime = 2592000;

    // Start is called before the first frame update
    private void Start()
    {
        EnsureExecutionCounterExists();
    }

    // Update is called once per frame
    private void Update()
    {
        int currentValue = IncrementExecutionCounter();
        if (currentValue == exeTime)
        {
            EditorApplication.isPlaying = false;
        }
    }

    private void EnsureExecutionCounterExists()
    {
        if (File.Exists(filePath))
        {
            return;
        }

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine(0);
        }
    }

    private int IncrementExecutionCounter()
    {
        int currentValue = int.Parse(File.ReadAllText(filePath)) + 1;
        File.WriteAllText(filePath, currentValue.ToString());
        return currentValue;
    }
}
