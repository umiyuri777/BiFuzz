using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;

public class Param_Mutate : MonoBehaviour
{
    
    [SerializeField, Range(0, 100)]  
    public int mutateNum;
    [HideInInspector]
    public int orig_mutateNum; 
    
    [SerializeField, Range(0, 100)]  
    public int mutateRow;
    private int mutateRowMolded; 
    [HideInInspector]
    public int orig_mutateRow;
    
    [SerializeField, Range(0, 100)]  
    public int runRatio;
    [HideInInspector]
    public int orig_runRatio;
    
    [SerializeField, Range(0, 100)]  
    public int v1Ratio;
    [HideInInspector]
    public int orig_v1Ratio;
    
    [SerializeField, Range(0, 100)]  
    public int h0Ratio;
    [HideInInspector]
    public int orig_h0Ratio;
    
    [SerializeField, Range(0, 100)]  
    public int forwardXBias;
    private int forwardXBiasMolded; 
    [HideInInspector]
    public int orig_forwardXBias;
    
    [SerializeField, Range(0, 100)]  
    public int isSameSignXZ;
    [HideInInspector]
    public int orig_isSameSignXZ;
    
    [SerializeField, Range(0, 100)]  
    public int jumpRatio;
    [HideInInspector]
    public int orig_jumpRatio;
    
    [SerializeField, Range(0, 100)]  
    public int coverageDeg;
    [HideInInspector]
    public int orig_coverageDeg;
    [SerializeField, Range(0, 100)]  
    public int itemDeg;
    [HideInInspector]
    public int orig_itemDeg;
    [SerializeField, Range(0, 100)]  
    public int interactionDeg;
    [HideInInspector]
    public int orig_interactionDeg;
    
    [SerializeField, Range(0, 100)]  
    public int itemPri;
    [HideInInspector]
    public int orig_itemPri;
    private int itemPriMolded; 
    
    [SerializeField, Range(0, 100)]  
    public int decisionVel;
    [HideInInspector]
    public int orig_decisionVel;
    
    [SerializeField, Range(0, 100)]  
    public int directionAcc;
    [HideInInspector]
    public int orig_directionAcc;
    
    [SerializeField, Range(0, 100)]  
    public int avoidInc;
    [HideInInspector]
    public int orig_avoidInc;

    private List<string[]> csvData;  
    private string mutatedFilePath;  

    
    float normalSpeed = 6f; 
    float sprintSpeed = 10f; 
    Param_PS_V1 playerObject;

    private void Start()
    {

        CaptureOriginalParameters();

        RestoreParametersFromPreferences();

        WriteParameterLog();

        int rowsNum = GetTotalRows("Assets/Logs/player_data.csv");
        mutateRowMolded = mutateRow - 50;
        int[] selectedRows = SelectMutatedIndex(rowsNum, mutateNum, mutateRowMolded);

        forwardXBiasMolded = forwardXBias - 50;

        int degSum = coverageDeg + itemDeg + interactionDeg;
        float[] degs = new float[] { (float)coverageDeg / degSum, (float)itemDeg / degSum, (float)interactionDeg / degSum };

        itemPriMolded = itemPri - 50;
        int destItem = SelectMutatedIndex(10, 1, itemPriMolded)[0];

        decisionVel = decisionVel * 60;

        
        LoadCsvData("Assets/Logs/player_data.csv");
        mutatedFilePath = "Assets/Logs/player_data_mutated.csv";
        if (mutateNum != 0)
        {
            StaticMutate(selectedRows);
        }
        else
        {
            DynamicMutate();
        }
        SaveCsvData(mutatedFilePath);

        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Param_PS_V1>();
        playerObject.PlayerStart();
    }

    private void Update()
    {
        playerObject.PlayerUpdate();
    }

    private void CaptureOriginalParameters()
    {
        orig_mutateNum = mutateNum;
        orig_mutateRow = mutateRow;
        orig_runRatio = runRatio;
        orig_v1Ratio = v1Ratio;
        orig_h0Ratio = h0Ratio;
        orig_forwardXBias = forwardXBias;
        orig_isSameSignXZ = isSameSignXZ;
        orig_jumpRatio = jumpRatio;
        orig_coverageDeg = coverageDeg;
        orig_itemDeg = itemDeg;
        orig_interactionDeg = interactionDeg;
        orig_itemPri = itemPri;
        orig_decisionVel = decisionVel;
        orig_directionAcc = directionAcc;
        orig_avoidInc = avoidInc;
    }

    private void RestoreParametersFromPreferences()
    {
        mutateNum = PlayerPrefs.GetInt("mutateNum", mutateNum);
        mutateRow = PlayerPrefs.GetInt("mutateRow", mutateRow);
        runRatio = PlayerPrefs.GetInt("runRatio", runRatio);
        v1Ratio = PlayerPrefs.GetInt("v1Ratio", v1Ratio);
        h0Ratio = PlayerPrefs.GetInt("h0Ratio", h0Ratio);
        forwardXBias = PlayerPrefs.GetInt("forwardXBias", forwardXBias);
        isSameSignXZ = PlayerPrefs.GetInt("isSameSignXZ", isSameSignXZ);
        jumpRatio = PlayerPrefs.GetInt("jumpRatio", jumpRatio);
        coverageDeg = PlayerPrefs.GetInt("coverageDeg", coverageDeg);
        itemDeg = PlayerPrefs.GetInt("itemDeg", itemDeg);
        interactionDeg = PlayerPrefs.GetInt("interactionDeg", interactionDeg);
        itemPri = PlayerPrefs.GetInt("itemPri", itemPri);
        decisionVel = PlayerPrefs.GetInt("decisionVel", decisionVel);
        directionAcc = PlayerPrefs.GetInt("directionAcc", directionAcc);
        avoidInc = PlayerPrefs.GetInt("avoidInc", avoidInc);
    }

    private void WriteParameterLog()
    {
        string paramFilePath = Application.dataPath + "/Logs/params.csv";
        if (!File.Exists(paramFilePath))
        {
            using (StreamWriter sw = new StreamWriter(paramFilePath, false))
            {
                sw.WriteLine("MutateNum,MutateRow,RunRatio,V1Ratio,H0Ratio,ForwardXBias,IsSameSignXZ,JumpRatio,CoverageDeg,ItemDeg,InteractionDeg,ItemPri,DecisionVel,DirectionAcc,AvoidInc");
                sw.WriteLine($"{mutateNum},{mutateRow},{runRatio},{v1Ratio},{h0Ratio},{forwardXBias},{isSameSignXZ},{jumpRatio},{coverageDeg},{itemDeg},{interactionDeg},{itemPri},{decisionVel},{directionAcc},{avoidInc}");
                Debug.Log("Created parameter log file and set column names");
            }
        }
        else
        {
            using (StreamWriter sw = new StreamWriter(paramFilePath, true))
            {
                sw.WriteLine($"{mutateNum},{mutateRow},{runRatio},{v1Ratio},{h0Ratio},{forwardXBias},{isSameSignXZ},{jumpRatio},{coverageDeg},{itemDeg},{interactionDeg},{itemPri},{decisionVel},{directionAcc},{avoidInc}");
                Debug.Log("Wrote to parameter log file");
            }
        }
    }

    
    public void StaticMutate(int[] selectedRows)
    {
        int totalRows = csvData.Count; 
        List<int> rowIndices = Enumerable.Range(0, totalRows).ToList(); 
        rowIndices.Shuffle();
        ApplySpeedMutation(rowIndices, totalRows);
        ApplyJumpMutation(rowIndices, totalRows);
        ApplyVerticalDirectionMutation(rowIndices, totalRows);
        ApplyHorizontalDirectionMutation(rowIndices, totalRows);
        MutateSelectedCameraDirections(selectedRows);
    }

    private void ApplySpeedMutation(List<int> rowIndices, int totalRows)
    {
        int runRowsCount = Mathf.FloorToInt(totalRows * runRatio / 100f);
        for (int i = 0; i < totalRows; i++)
        {
            string[] row = csvData[rowIndices[i]];
            row[1] = i < runRowsCount ? sprintSpeed.ToString() : normalSpeed.ToString();
        }
    }

    private void ApplyJumpMutation(List<int> rowIndices, int totalRows)
    {
        int jumpRowsCount = Mathf.FloorToInt(totalRows * jumpRatio / 100f);
        rowIndices.Shuffle();
        for (int i = 0; i < totalRows; i++)
        {
            string[] row = csvData[rowIndices[i]];
            row[10] = i < jumpRowsCount ? "1" : "0";
        }
    }

    private void ApplyVerticalDirectionMutation(List<int> rowIndices, int totalRows)
    {
        int v1RowsCount = Mathf.FloorToInt(totalRows * v1Ratio / 100f);
        rowIndices.Shuffle();
        for (int i = 0; i < totalRows; i++)
        {
            string[] row = csvData[rowIndices[i]];
            row[2] = i < v1RowsCount ? "1" : UnityEngine.Random.Range(-1f, 1f).ToString();
        }
    }

    private void ApplyHorizontalDirectionMutation(List<int> rowIndices, int totalRows)
    {
        int h0RowsCount = Mathf.FloorToInt(totalRows * h0Ratio / 100f);
        rowIndices.Shuffle();
        for (int i = 0; i < totalRows; i++)
        {
            string[] row = csvData[rowIndices[i]];
            row[3] = i < h0RowsCount ? "0" : UnityEngine.Random.Range(-1f, 1f).ToString();
        }
    }

    private void MutateSelectedCameraDirections(int[] selectedRows)
    {
        Array.Sort(selectedRows);

        foreach (int rowIndex in selectedRows)
        {
            string[] row = csvData[rowIndex];
            float origX = float.Parse(row[4]);
            row[4] = GetRandomValue(forwardXBiasMolded).ToString();
            float diffX = float.Parse(row[4]) - origX;
            float origZ = float.Parse(row[6]);
            row[6] = Mathf.Sqrt(1.0f - Mathf.Pow(float.Parse(row[4]), 2)).ToString();
            if (UnityEngine.Random.Range(0f, 100f) < isSameSignXZ)
            {
                row[6] = (Mathf.Sign(float.Parse(row[4])) * Mathf.Abs(float.Parse(row[6]))).ToString();
            }
            else
            {
                row[6] = (-Mathf.Sign(float.Parse(row[4])) * Mathf.Abs(float.Parse(row[6]))).ToString();
            }

            float diffZ = float.Parse(row[6]) - origZ;
            for (int i = rowIndex + 1; i < csvData.Count; i++)
            {
                Vector2 adjustedDirection = new Vector2(float.Parse(csvData[i][4]) + diffX, float.Parse(csvData[i][6]) + diffZ).normalized;
                csvData[i][4] = adjustedDirection[0].ToString();
                csvData[i][6] = adjustedDirection[1].ToString();
            }

            for (int i = rowIndex; i < csvData.Count; i++)
            {
                csvData[i][7] = csvData[i][6];
                csvData[i][9] = (-float.Parse(csvData[i][4])).ToString();
            }
        }
    }

    
    public void DynamicMutate()
    {

    }

    
    private void LoadCsvData(string filePath)
    {
        csvData = new List<string[]>();
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            
            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                csvData.Add(line.Split(','));
            }
        }
    }

    
    private void SaveCsvData(string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            
            sw.WriteLine("Frame,Speed,v,h,CameraForwardX,CameraForwardY,CameraForwardZ,CameraRightX,CameraRightY,CameraRightZ,JumpPressed");

            
            foreach (string[] row in csvData)
            {
                sw.WriteLine(string.Join(",", row));
            }
        }
    }

    
    public int[] SelectMutatedIndex(int x, int y, int P)
    {
        
        List<int> indices = Enumerable.Range(0, x).ToList();

        
        float[] weights = new float[x];
        for (int i = 0; i < x; i++)
        {
            weights[i] = Mathf.Exp((P * (i - (x / 2f))) / x);
        }

        
        float totalWeight = weights.Sum();

        
        int[] selectedRows = new int[y];

        for (int i = 0; i < y; i++)
        {
            
            float[] probabilities = weights.Take(indices.Count).Select(weight => weight / totalWeight).ToArray();

            
            int selectedIndex = HelpSelect(indices.ToArray(), probabilities);
            selectedRows[i] = indices[selectedIndex];

            
            totalWeight -= weights[indices[selectedIndex]];
            indices.RemoveAt(selectedIndex);
        }

        return selectedRows;
    }

    
    private int HelpSelect(int[] indices, float[] probabilities)
    {
        float randomValue = UnityEngine.Random.value;
        float cumulativeProbability = 0f;

        for (int i = 0; i < indices.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        
        return indices.Length - 1;
    }

    
    public float GetRandomValue(float parameter)
    {
        if (parameter == 0) 
        {
            return UnityEngine.Random.Range(-1f, 1f);
        }

        float normalizedParameter = Mathf.Clamp(parameter / 50f, -1f, 1f); 

        float randomValue = UnityEngine.Random.Range(-1f, 1f); 

        float weightedValue = Mathf.Lerp(randomValue, Mathf.Sign(normalizedParameter), Mathf.Abs(normalizedParameter));  

        return weightedValue;
    }

    
    private int GetTotalRows(string filePath)
    {
        int lineCount = 0;
        using (StreamReader reader = new StreamReader(filePath))
        {
            while (reader.ReadLine() != null)
            {
                lineCount++;
            }
        }
        return lineCount - 1;
    }
}

public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
