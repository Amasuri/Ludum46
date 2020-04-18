using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Ludum46.Code.Reusable
{
    public class MusicPlayer
    {
        public enum SongType
        {
        }

        private SongType currentSong;
        private Dictionary<SongType, Song> songs;

        public MusicPlayer(Game game )
        {
        }

        public void Update(Ludum46 game)
        {
        }
    }
}
