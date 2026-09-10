using System.IO;
using UnityEngine;

public class PositionLogger : MonoBehaviour
{
    private string positionsPath;

    private void Start()
    {
        
        positionsPath = Path.Combine(Application.dataPath, "Logs", "positions.csv");

        
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Logs"));
        using (StreamWriter writer = new StreamWriter(positionsPath, true))
        {
            
            writer.WriteLine($"Scene started");
        }
    }

    private void FixedUpdate()
    {
        LogPosition(transform.position);
    }

    private void LogPosition(Vector3 position)
    {
        using (StreamWriter writer = new StreamWriter(positionsPath, true))
        {
            
            writer.WriteLine($"{position.x},{position.y},{position.z}");
        }
    }
}
