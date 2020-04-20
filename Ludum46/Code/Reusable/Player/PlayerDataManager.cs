using Ludum46.Code.Level;
using Microsoft.Xna.Framework;
using System;
using static Ludum46.Code.Graphics.AttackEffectPool;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// Your standard data manager: saving, remembering what have been done, etc.
    /// </summary>
    public static class PlayerDataManager
    {
        private const int MAX_HP = 7;
        public static bool dead => hp <= 0;

        static public Vector2 unscaledPixelPosition { get; private set; }
        static public Vector2 unscaledFrameCenterPoint => unscaledPixelPosition + (PlayerDrawer.playerFrame / 2);
        static public Vector2 lastMove { get; private set; }

        static public Rectangle unscPlayerRect => new Rectangle(unscaledPixelPosition.ToPoint() + PlayerDrawer.RectInsideFrame.Location, PlayerDrawer.RectInsideFrame.Size);

        static public int hp { get; private set; }
        public static bool HasTouchedTheStone { get; private set; }
        public static bool WinCondition { get; private set; }

        static private Graphics.AttackEffectPool.Type attackType;

        static PlayerDataManager()
        {
            unscaledPixelPosition = new Vector2(41, 84);
            hp = MAX_HP;
            attackType = Graphics.AttackEffectPool.Type.Attack1;
            HasTouchedTheStone = false;
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void TryMove(Vector2 move, Room actionRoom)
        {
            if (actionRoom.PlayerPositionCollides(unscaledPixelPosition + move * 2))
            {
                move = Vector2.Zero;
                return;
            }

            unscaledPixelPosition += move;
            lastMove = move;
        }

        /// <summary>
        /// Mode for the editor
        /// </summary>
        static public void MoveAsMouse(Vector2 move)
        {
            unscaledPixelPosition += move;
        }

        static public void Hit(Ludum46 game)
        {
            hp--;
            MusicPlayer.ReplenishAttMusicTimer();
            game.soundPlayer.PlaySound(SoundPlayer.Type.Hit);
        }

        public static void SetPosition(Level.Level.RoomType newRoomType)
        {
            if(newRoomType == Level.Level.RoomType.BattleRoom)
            {
                unscaledPixelPosition = new Vector2(-5, 16);
            }
            else if (newRoomType == Level.Level.RoomType.TreeRoom)
            {
                unscaledPixelPosition = new Vector2(57, - 124);
            }
        }

        static public void Heal()
        {
            hp = MAX_HP;
        }

        static public Graphics.AttackEffectPool.Type GetNextAttackType()
        {
            attackType++;
            if (attackType > Graphics.AttackEffectPool.Type.Attack3)
                attackType = Graphics.AttackEffectPool.Type.Attack1;

            return attackType;
        }

        static public void TouchTheStone()
        {
            HasTouchedTheStone = true;
        }

        public static void Win()
        {
            WinCondition = true;
        }
    }
}
