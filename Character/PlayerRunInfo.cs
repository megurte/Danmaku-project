using System;
using Stage;
using UI.MainMenu;
using UnityEngine;

namespace Character
{
    public static class PlayerRunInfo
    {
        private static string _playerName;
        private static Difficulty _difficultySetting = Difficulty.Default;
        private static int _runScore = default;

        public static Difficulty GetRunDifficulty()
        {
            return _difficultySetting;
        }

        public static void SetDifficulty(Difficulty difficulty)
        {
            _difficultySetting = difficulty;
        }
        
        public static string GetPlayerName()
        {
            return _playerName;
        }

        public static void SetPlayerName(string name)
        {
            _playerName = name;
        }
        
        public static int GetRunScore()
        {
            return _runScore > 0 ? _runScore : 0;
        }

        public static void AddRunScore(int points)
        {
            _runScore += points;
        }

        public static void SaveScoreData()
        {            
            JsonScoreDataWriter.SaveJsonData(_runScore);
        }

        public static void ClearRunInfo()
        {
            SetDifficulty(Difficulty.Default);
            SetPlayerName("");
            _runScore = 0;
        }
    }
}