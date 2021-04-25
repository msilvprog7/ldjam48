using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

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