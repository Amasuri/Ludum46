using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Ludum46.Code.Reusable
{
    public class MusicPlayer
    {
        //public enum SongType
        //{
        //}

        //private SongType currentSong;
        //private Dictionary<SongType, Song> songs;

        private SoundEffectInstance dynamic1;
        private SoundEffectInstance dynamic2;

        public MusicPlayer(Ludum46 game )
        {
            dynamic1 = game.Content.Load<SoundEffect>("res/music/dynamic/forager").CreateInstance();
            dynamic2 = game.Content.Load<SoundEffect>("res/music/dynamic/deadbeat").CreateInstance();

            dynamic1.Play();
            dynamic2.Play();

            dynamic1.IsLooped = true;
            dynamic2.IsLooped = true;
        }

        public void Update(Ludum46 game)
        {
        }
    }
}
