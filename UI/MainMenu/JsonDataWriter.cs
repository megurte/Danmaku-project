using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;

namespace UI.MainMenu
{
    public class JsonDataWriter : MonoBehaviour
    {
        private static string _filePath = Application.streamingAssetsPath + "/RecordsData.json";
        
        public static void SaveJsonData(int points)
        {
            var currentDate = DateTime.Now;
            var newData = new ScoreData(currentDate
                .ToString().Remove(10, 9), "MEGURT", points.ToString()); // TODO: change name
            var oldData = JsonHelper.FromJson<ScoreData>(File.ReadAllText(_filePath)).ToList();
            
            if (oldData.Count == 0)
            {
                oldData = new List<ScoreData>();
            }
            
            oldData.Add(newData);
            oldData.Sort(new ScoreDataComparer());
            
            if (oldData.Count > 10)
            {
                oldData.RemoveAt(oldData.Count - 1);
            }
            File.WriteAllText(_filePath, JsonHelper.ToJson(oldData.ToArray()));
        }
        
        public static ScoreData[] LoadJsonData()
        {
            if (!File.Exists(_filePath))
            {
                Debug.LogError("LoadJsonData: file RecordsData.json doesn't exist at path ${_filePath}");
                return null;
            }
                
            return JsonHelper.FromJson<ScoreData>(File
                .ReadAllText(_filePath));
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
    
    class ScoreDataComparer : IComparer<ScoreData>
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