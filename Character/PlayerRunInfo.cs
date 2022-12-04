using System;
using Stage;
using UI.MainMenu;
using UnityEngine;

namespace Character
{
    public static class PlayerRunInfo
    {
        private static Difficulty _difficultySetting = Difficulty.Normal; // TODO: REMOVE HARDCODE
        private static int _runScore = default;

        public static Difficulty GetRunDifficulty()
        {
            return _difficultySetting;
        }

        public static void SetDifficulty(Difficulty difficulty)
        {
            _difficultySetting = difficulty;
        }
        
        public static int GetRunScore()
        {
            return _runScore > 0 ? _runScore : 0;
        }

        public static void AddRunScore(int points)
        {
            _runScore += points;
            Debug.LogWarning(points + " add to run score: " + _runScore);
        }

        public static void SaveScoreData()
        {            
            Debug.LogWarning("save run score data: " + _runScore);
            JsonScoreDataWriter.SaveJsonData(_runScore);
        }
    }
}