﻿using UnityEngine;

using System.Collections.Generic;

public static class Leaderboard
{
    public const int EntryCount = 10;

    public struct ScoreEntry
    {
        public string name;
        public int score;

        public ScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    private static List<ScoreEntry> s_Entries = new List<ScoreEntry>();

    private static List<ScoreEntry> Entries
    {
        get
        {
            if (s_Entries == null)
            {
                s_Entries = new List<ScoreEntry>();
                LoadScores();
            }
            return s_Entries;
        }
    }

    private const string PlayerPrefsBaseKey = "leaderboard";

    private static void SortScores()
    {
        s_Entries.Sort((a, b) => b.score.CompareTo(a.score));
    }

    /*
    public static List<ScoreEntry> LoadScores()
    {
        s_Entries = new List<ScoreEntry>();
        s_Entries.Clear();

        for (int i = 0; i < EntryCount; ++i)
        {
            ScoreEntry entry;
            entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "");
            entry.score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);
            s_Entries.Add(entry);
        }

        SortScores();
        return s_Entries;
    }

    private static void SaveScores()
    {
        for (int i = 0; i < EntryCount; ++i)
        {
            var entry = s_Entries[i];
            PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.name);
            PlayerPrefs.SetInt(PlayerPrefsBaseKey + "[" + i + "].score", entry.score);
        }
    }
    */

    public static List<ScoreEntry> LoadScores()
    {
        s_Entries = new List<ScoreEntry>();
        s_Entries.Clear();

        for (int i = 0; i < EntryCount; ++i)
        {
            ScoreEntry entry;
            entry.name = PlayerPrefs.GetString(PlayerPrefsBaseKey + i + "name", "");
            entry.score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + i + "score", 0);
            s_Entries.Add(entry);
        }

        SortScores();
        return s_Entries;
    }

    private static void SaveScores()
    {
        for (int i = 0; i < s_Entries.Count; ++i)
        {
            var entry = s_Entries[i];
            PlayerPrefs.SetString(PlayerPrefsBaseKey + i + "name", entry.name);
            PlayerPrefs.SetInt(PlayerPrefsBaseKey + i + "score", entry.score);
        }
    }

    public static ScoreEntry GetEntry(int index)
    {
        return Entries[index];
    }

    public static void Record(string name, int score)
    {
        s_Entries.Add(new ScoreEntry(name, score));
        SortScores();
        //s_Entries.RemoveAt(Entries.Count - 1);
        SaveScores();
    }

    public static string ScoresToString()
    {
        string s = "";
        for (int i = 0; i < s_Entries.Count; i++)
        {
            ScoreEntry item = (ScoreEntry)s_Entries[i];
            
            if (!string.IsNullOrEmpty(item.name))
            {
                s += i + " - " + item.name + " | " + item.score +"\n";
            }
            else
            {
                s += i + " - ---\n";
            }
        }

        return s;
    }
}
