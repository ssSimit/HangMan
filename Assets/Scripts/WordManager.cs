using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using TMPro;

public class WordManager : MonoBehaviour
{
    const int totalLines = 650;
    private string[] lines;

    private void Awake()
    {
        LoadWords(); 
    }

    private void LoadWords()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("words");
        if (wordFile != null)
        {
            lines = wordFile.text.Split(new[] { "\n", "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        
    }

    public string[] GetRandomWord()
    {
        
        if (lines == null || lines.Length == 0)
        {
            return new string[] { string.Empty, string.Empty };
        }

        int randomLine = Random.Range(0, Mathf.Min(totalLines, lines.Length));
        string[] values = lines[randomLine].Split(", ");
        return values.Length == 2 ? values : new string[] { string.Empty, string.Empty };
    }

}


