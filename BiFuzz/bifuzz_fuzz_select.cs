using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class fuzz_select : MonoBehaviour
{
    
    string filePath = Path.Combine(Application.dataPath, "Logs", "fuzz_type.csv");

    private void Start()
    {
        
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath,false))
            {
                writer.WriteLine("init");
            }
            Debug.Log("Created and initialized fuzz_type.csv file.");
        }
        else
        {
            Debug.Log("fuzz_type.csv file already exists.");
        }

        
        string lastLine = ReadLastLine();

        switch (lastLine)
        {
            case "init":
                GetComponent<grad_local>().enabled = false;
                GetComponent<grad_init>().enabled = true;
                Debug.Log("Global fuzzing will be executed this time.");
                break;
            case "local":
                GetComponent<grad_init>().enabled = false;
                GetComponent<grad_local>().enabled = true;
                Debug.Log("Local fuzzing will be executed this time.");
                break;
        }
    }

    private string ReadLastLine()
    {
        string lastLine = null;
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                lastLine = reader.ReadLine();
            }
        }

        return lastLine;
    }
}
