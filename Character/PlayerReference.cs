using System;

namespace Character
{
    [Serializable]
    public static class PlayerReference
    {
        public static PlayerBase PlayerBase
        {
            get => _playerBase;
            set => _playerBase ??= value;
        }
        private static PlayerBase _playerBase;
    }
}