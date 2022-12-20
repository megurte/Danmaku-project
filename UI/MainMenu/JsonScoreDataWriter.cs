using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using System.Linq;
using Character;

namespace UI.MainMenu
{
    public class JsonScoreDataWriter : MonoBehaviour
    {
        private static readonly string FilePath = Application.streamingAssetsPath + "/RecordsData.json";
        private static readonly int MaxDataElements = 10;
        
        public static void SaveJsonData(int points)
        {
            var currentDate = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            currentDate = currentDate.Remove(10, 9);
            var newData = new ScoreData(currentDate, PlayerRunInfo.GetPlayerName(), points.ToString());
            var oldData = JsonUtils.FromJson<ScoreData>(File.ReadAllText(FilePath)).ToList();
            
            if (oldData.Count == 0)
            {
                oldData = new List<ScoreData>();
            }
            
            oldData.Add(newData);
            oldData.Sort(new ScoreDataComparer());
            
            if (oldData.Count > MaxDataElements)
            {
                oldData.RemoveAt(oldData.Count - 1);
            }
            
            File.WriteAllText(FilePath, JsonUtils.ToJson(oldData.ToArray()));
        }
        
        public static ScoreData[] LoadJsonData()
        {
            if (!File.Exists(FilePath))
            {
                Debug.LogError($"LoadJsonData: file RecordsData.json doesn't exist at path {FilePath}");
                return null;
            }
                
            return JsonUtils.FromJson<ScoreData>(File.ReadAllText(FilePath));
        }
    }

    [Serializable]
    public class ScoreData
    {
        public string date;
        public string name;
        public string score;

        public ScoreData(string date, string name, string score)
        {
            this.date = date;
            this.name = name;
            this.score = score;
        }
    }

    internal class ScoreDataComparer : IComparer<ScoreData>
    {
        public int Compare(ScoreData p1, ScoreData p2)
        {
            if (p1 is null || p2 is null)
            {
                throw new ArgumentException("Invalid value");
            } 
            
            return int.Parse(p2.score) - int.Parse(p1.score);
        }
    }
}