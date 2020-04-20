using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Ludum46.Code.Reusable
{
    public class MusicPlayer
    {
        private enum MusicMachineState
        {
            Tree,
            Carry,
            Fight,
            HeartCarry,
            HeartFight,
        }

        private Dictionary<MusicMachineState, SoundEffectInstance> dynMusic;

        private MusicMachineState currentState;
        private MusicMachineState goingFromState;

        private const float MAX_VOL = 1f;
        private const float MIN_VOL = 0f;
        private const float CHG_RATE = 0.005f;
        private const float CHG_RATE2 = CHG_RATE * 1.5f;

        static private double AttTrackTimer;
        private const double AttTrackTimerMax = 10000;

        public MusicPlayer(Ludum46 game )
        {
            dynMusic = new Dictionary<MusicMachineState, SoundEffectInstance>();

            dynMusic.Add(MusicMachineState.Carry, game.Content.Load<SoundEffect>("res/music/dynamic/forager").CreateInstance());
            dynMusic.Add(MusicMachineState.Fight, game.Content.Load<SoundEffect>("res/music/dynamic/deadbeat").CreateInstance());
            dynMusic.Add(MusicMachineState.HeartFight, game.Content.Load<SoundEffect>("res/music/dynamic/battle_seeker").CreateInstance());
            dynMusic.Add(MusicMachineState.Tree, game.Content.Load<SoundEffect>("res/music/dynamic/gluttony").CreateInstance());
            dynMusic.Add(MusicMachineState.HeartCarry, game.Content.Load<SoundEffect>("res/music/dynamic/long_haul").CreateInstance());

            foreach (var item in this.dynMusic)
            {
                item.Value.Play();
                item.Value.IsLooped = true;
            }

            //Since we're starting at tree room anyway....
            foreach (var item in this.dynMusic)
            {
                if (item.Key != MusicMachineState.Tree)
                    item.Value.Volume = MIN_VOL;
            }

            currentState = MusicMachineState.Tree;
            goingFromState = MusicMachineState.Tree;
            AttTrackTimer = 0f;
        }

        public void Update(Ludum46 game)
        {
            if (AttTrackTimer >= 0d)
                AttTrackTimer -= Ludum46.DeltaUpdate;

            //State machine switch
            if (game.level.currentRoomType == Level.Level.RoomType.TreeRoom)
                currentState = MusicMachineState.Tree;
            if (game.level.currentRoomType == Level.Level.RoomType.BattleRoom)
                currentState = MusicMachineState.Carry;
            if (game.level.currentRoomType == Level.Level.RoomType.BattleRoom && PlayerDataManager.HasTouchedTheStone)
                currentState = MusicMachineState.HeartCarry;
            if (AttTrackTimer > 0d)
                currentState = MusicMachineState.Fight;
            if (AttTrackTimer > 0d && PlayerDataManager.HasTouchedTheStone)
                currentState = MusicMachineState.HeartFight;

            //Music update
            ShiftMusic();

            //Fade update
            if (this.dynMusic[currentState].Volume == MAX_VOL)
                goingFromState = currentState;
        }

        private void ShiftMusic()
        {
            if (currentState == MusicMachineState.Tree)
            {
                this.IncreaseVolume(this.dynMusic[MusicMachineState.Tree]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Carry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Fight]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.HeartCarry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.HeartFight]);
            }
            else if (currentState == MusicMachineState.Carry)
            {
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Tree]);
                this.IncreaseVolume(this.dynMusic[MusicMachineState.Carry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Fight]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.HeartCarry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.HeartFight]);
            }
            else if (currentState == MusicMachineState.HeartCarry)
            {
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Tree]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Carry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.Fight]);
                this.IncreaseVolume(this.dynMusic[MusicMachineState.HeartCarry]);
                this.DecreaseVolume(this.dynMusic[MusicMachineState.HeartFight]);
            }
            else if (currentState == MusicMachineState.HeartFight)
            {
                this.DecFaster(this.dynMusic[MusicMachineState.Tree]);
                this.DecFaster(this.dynMusic[MusicMachineState.Carry]);
                this.DecFaster(this.dynMusic[MusicMachineState.Fight]);
                this.DecFaster(this.dynMusic[MusicMachineState.HeartCarry]);
                this.IncFaster(this.dynMusic[MusicMachineState.HeartFight]);
            }
            else if (currentState == MusicMachineState.Fight)
            {
                this.DecFaster(this.dynMusic[MusicMachineState.Tree]);
                this.DecFaster(this.dynMusic[MusicMachineState.Carry]);
                this.IncFaster(this.dynMusic[MusicMachineState.Fight]);
                this.DecFaster(this.dynMusic[MusicMachineState.HeartCarry]);
                this.DecFaster(this.dynMusic[MusicMachineState.HeartFight]);
            }
        }

        private void DecreaseVolume(SoundEffectInstance music)
        {
            if (music.Volume - CHG_RATE > MIN_VOL)
                music.Volume -= CHG_RATE;
            else
                music.Volume = MIN_VOL;
        }

        private void IncreaseVolume(SoundEffectInstance music)
        {
            if (music.Volume + CHG_RATE < MAX_VOL)
                music.Volume += CHG_RATE;
            else
                music.Volume = MAX_VOL;
        }

        private void DecFaster(SoundEffectInstance music)
        {
            if (music.Volume - CHG_RATE2 > MIN_VOL)
                music.Volume -= CHG_RATE2;
            else
                music.Volume = MIN_VOL;
        }

        private void IncFaster(SoundEffectInstance music)
        {
            if (music.Volume + CHG_RATE2 < MAX_VOL)
                music.Volume += CHG_RATE2;
            else
                music.Volume = MAX_VOL;
        }

        public static void ReplenishAttMusicTimer()
        {
            AttTrackTimer = AttTrackTimerMax;
        }
    }
}
