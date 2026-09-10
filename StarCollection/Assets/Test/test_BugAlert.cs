//using UnityEngine;

//public class test_BugAlert : MonoBehaviour
//{

//    public bool isPlayerCollidered = false;

//    void OnTriggerEnter(Collider other)
//    {

//        if (other.CompareTag("Player") && isPlayerCollidered == false)
//        {
//            isPlayerCollidered = true;


//        }
//    }

//    void Update()
//    {

//        //Debug.Log("Is Player Colliding: " + isPlayerColliding);
//    }
//}

using System.IO;
using UnityEngine;

public class test_BugAlert : MonoBehaviour
{
    
    public bool isPlayerCollidered = false;
    
    private int collisionFrameCount = 0;
    
    public int requiredFrameCount = 1800;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            collisionFrameCount = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player") && isPlayerCollidered == false)
        {
            
            collisionFrameCount++;
            
            if (collisionFrameCount >= requiredFrameCount)
            {
                isPlayerCollidered = true;
                Debug.Log("Bug_occurred");
                Debug.Log("Name of the parent object attached: " + transform.parent.gameObject.name);
                
                try
                {
                    
                    using (StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, "Logs", "detected_bugs.csv"), true))
                    {
                        writer.WriteLine($"{transform.parent.gameObject.name}");
                    }
                }
                catch (IOException e)
                {
                    Debug.LogError($"Failed to create CSV file: {e.Message}");
                }

                
                if (File.Exists(Path.Combine(Application.dataPath, "Logs", "fuzz_type.csv")))
                {
                    using (StreamWriter writer = new StreamWriter(Path.Combine(Application.dataPath, "Logs", "fuzz_type.csv"), true))
                    {
                        
                        string[] lines = File.ReadAllLines(Path.Combine(Application.dataPath, "Logs", "detected_bugs.csv"));
                        if (lines.Length >= 2 && lines[lines.Length - 1] == lines[lines.Length - 2])
                        {
                            writer.WriteLine("init");
                        }
                        else
                        {
                            writer.WriteLine("local");
                        }
                    }
                    Debug.Log("Wrote to fuzz_type.csv file.");
                }
                else
                {
                    Debug.Log("Error: fuzz_type.csv file does not exist.");
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            collisionFrameCount = 0;
        }
    }
}
