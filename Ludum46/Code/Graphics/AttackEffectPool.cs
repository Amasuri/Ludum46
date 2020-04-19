using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Graphics
{
    /// <summary>
    /// Sort of pool so that the attack effect isn't hoarding RAM for each enemy
    /// </summary>
    static public class AttackEffectPool
    {
        /// <summary>
        /// They correspond to list Id
        /// </summary>
        public enum Direction
        {
            Left = 0, Right, Up, Down,
            Upleft, UpRight,
            DownLeft, DownRight
        }

        public enum Type
        {
            Attack1, Attack2, Attack3
        }

        private static List<Animation> attacks1;
        private static List<Animation> attacks2;
        private static List<Animation> attacks3;

        public static void LoadAssets(Ludum46 game)
        {
            LoadAttacksForList(game, attacks1, "attack1/");
            LoadAttacksForList(game, attacks2, "attack2/");
            LoadAttacksForList(game, attacks3, "attack3/");
        }

        private static void LoadAttacksForList(Ludum46 game, List<Animation> list, string attackFolder)
        {
            float frameDelta = 80;
            int frameX = 28;

            list = new List<Animation>
            {
                new Animation(game, "res/effects/" + attackFolder + "Left", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "Right", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "Up", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "Down", frameX, Ludum46.Scale, (int)frameDelta),

                new Animation(game, "res/effects/" + attackFolder + "UpLeft", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "UpRight", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "DownLeft", frameX, Ludum46.Scale, (int)frameDelta),
                new Animation(game, "res/effects/" + attackFolder + "DownRight", frameX, Ludum46.Scale, (int)frameDelta),
            };
        }

        public static void DrawAttackAt(SpriteBatch batch, double atTime, Direction attDirection, Type attType, Vector2 attPos)
        {
            //Selecting appropriate attack type
            var attList = attacks1;
            if (attType == Type.Attack2)
                attList = attacks2;
            else if (attType == Type.Attack3)
                attList = attacks3;

            attList[(int)attDirection].DrawWithExternalCounter(batch, attPos, SpriteEffects.None, atTime);
        }
    }
}
