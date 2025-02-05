using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManagerScript
{
    private static string HighScoreFileSnakeJSON => Application.persistentDataPath + "/highScoreSnake.json";
    public static void SaveHighScoreSnakeJSON(int highScore)
    {
        using(var writer = new StreamWriter(new FileStream(HighScoreFileSnakeJSON, FileMode.Create)))
        {
            Debug.Log("Saving high score file");
            var json = JsonUtility.ToJson(new ScoreEntryScript(highScore));
            writer.Write(json);
        }
    }
    public static int LoadHighScoreSnakeJSON()
    {
        if (File.Exists(HighScoreFileSnakeJSON))
        {
            Debug.Log("Loading high score file: "+HighScoreFileSnakeJSON);
            using(var reader = new StreamReader(new FileStream(HighScoreFileSnakeJSON, FileMode.Open)))
            {
                string json = reader.ReadToEnd();
                var entry = JsonUtility.FromJson<ScoreEntryScript>(json);
                Debug.Log(entry.score);
                return entry.score;
            }
        }
        return 0;
    }
}
