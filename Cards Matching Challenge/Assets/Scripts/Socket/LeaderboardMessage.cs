using System.Collections.Generic;

namespace Assets.Scripts.Socket
{
    class LeaderboardMessage
    {
        [System.Serializable]
        public struct ValueArray
        {
            public string name;
            public int moves;
            public int seconds;
            public int score;
        }

        public string type;
        public ValueArray[] value;
    }
}
