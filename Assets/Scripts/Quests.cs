using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Quest
{
    public Quest(string[] row, char listDelimiter)
    {
        this.Title = row[0];
        this.Description = row[1];
        this.SuccessItems = new List<string>(row[2].Split(listDelimiter));
        this.Reward = row[3];
        this.MessageSuccessWithItem = row[4];
        this.MessageSuccessWrongItem = row[5];
        this.MessageSuccessNoItem = row[6];
        this.MessageFailWithItem = row[7];
        this.MessageFailNoItem = row[8];
    }

    public string Title { get; }

    public string Description { get; }

    public List<string> SuccessItems { get; }

    public string Reward { get; }

    public string MessageSuccessWithItem { get; }

    public string MessageSuccessWrongItem { get; }

    public string MessageSuccessNoItem { get; }

    public string MessageFailWithItem { get; }

    public string MessageFailNoItem { get; }

    public override string ToString()
    {
        var properties = typeof(Quest).GetProperties();
        return string.Join(
            "\n",
            properties.Select(p => $"{p.Name}: {p.GetValue(this)}"));
    }
}

public class Quests : MonoBehaviour
{
    public List<Quest> Value { get; } = new List<Quest>();

    private static string Filename = "Assets\\Scripts\\Quests.txt";
    private static char Delimiter = '\t';
    private static char ListDelimiter = ',';
    private static int HeaderLines = 2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Reading quests, Filename: {Filename}, Delimiter: {Delimiter}, ListDelimiter: {ListDelimiter}, HeaderLines: {HeaderLines}");
        this.Value.Clear();
        foreach (var line in File.ReadAllLines(Filename).Skip(HeaderLines))
        {
            var row = line.Split(Delimiter);
            var quest = new Quest(row, ListDelimiter);
            this.Value.Add(quest);
        }
        
        Debug.Log($"Found {this.Value.Count} Quests");
        foreach (var quest in this.Value) 
        {
            Debug.Log(quest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
