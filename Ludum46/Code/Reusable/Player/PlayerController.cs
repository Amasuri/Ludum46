using Ludum46.Code.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Ludum46.Code.Graphics.AttackEffectPool;
using static Ludum46.Code.Level.EntityAttack;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// A class which takes input and produces action based on current screen and state. Must know what's going on in the game.
    ///
    /// No player information is being hold there - go to PlayerData for that.
    /// </summary>
    static public class PlayerController
    {
        private const Keys keyUp = Keys.W;
        private const Keys keyDown = Keys.S;
        private const Keys keyLeft = Keys.A;
        private const Keys keyRight = Keys.D;

        private const Keys keySlash = Keys.Space;
        private const Keys keyWhack = Keys.LeftShift;

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        private const float horzVelocity = 1f;
        private const float vertVelocity = horzVelocity / 3 * 2;
        private const int attackDelayMs = 200;
        private static double blockAttackForMs = 0;

        //Since attack animations are in shared pool (to reduce overhead)
        //entities track time variables on their own
        public static double currentAnimTime { get; private set; } = AttackEffectPool.MAX_ATT_ANIM_TIME + 1d;
        public static Direction currentAnimDirection { get; private set; }
        public static Type currentAnimType { get; private set; }

        //Needed to know how to place slashes
        private static Vector2 lastNonNullMove = new Vector2(0, +vertVelocity);

        //Attack rectangles
        private static Rectangle leftRectangle =>
            new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint() + new Point(-5, 0), new Point(5, (int)PlayerDrawer.playerFrame.Y));
        private static Rectangle rightRectangle =>
            new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint() + new Point((int)PlayerDrawer.playerFrame.X, 0), new Point(5, (int)PlayerDrawer.playerFrame.Y));
        private static Rectangle upRectangle =>
            new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint() + new Point(0, -5), new Point((int)PlayerDrawer.playerFrame.X, 5));
        private static Rectangle downRectangle =>
            new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint() + new Point(0, (int)PlayerDrawer.playerFrame.Y), new Point((int)PlayerDrawer.playerFrame.X, 5));

        static public void UpdateControls(Ludum46 game)
        {
            keyState = Keyboard.GetState();

            if (blockAttackForMs <= 0)
            {
                UpdateMovement(game);
                UpdateAttack(game);
            }
            else
            {
                blockAttackForMs -= Ludum46.DeltaUpdate;
            }

            //AttackAnimTimers should be updated regardless
            if (currentAnimTime < MAX_ATT_ANIM_TIME)
                currentAnimTime += Ludum46.DeltaUpdate;

            oldKeyState = keyState;
        }

        private static void UpdateAttack(Ludum46 game)
        {
            if (oneKeyPress(keySlash))
            {
                var attackRectList = new List<Rectangle> { };

                if (lastNonNullMove.X < 0)
                    attackRectList.Add(leftRectangle);
                if (lastNonNullMove.X > 0)
                    attackRectList.Add(rightRectangle);
                if (lastNonNullMove.Y < 0)
                    attackRectList.Add(upRectangle);
                if (lastNonNullMove.Y > 0)
                    attackRectList.Add(downRectangle);

                game.level.SpawnAttackEntity(
                    game, attackRectList,
                    TargetedTo.Enemy, PlayerDataManager.unscaledPixelPosition, attackDelayMs);

                blockAttackForMs = attackDelayMs;

                //Attack timers for animation
                currentAnimTime = 0d;
                currentAnimDirection = game.screenPool.playerDrawer.GetCurrentDirection();
                currentAnimType = PlayerDataManager.GetNextAttackType();
            }
            else if (oneKeyPress(keyWhack))
            {
                ;
            }
        }

        private static void UpdateMovement(Ludum46 game)
        {
            //Self movement
            var move = new Vector2(0, 0);

            if (keyState.IsKeyDown(keyUp))
                move = new Vector2(move.X, -vertVelocity);
            else if (keyState.IsKeyDown(keyDown))
                move = new Vector2(move.X, +vertVelocity);
            if (keyState.IsKeyDown(keyLeft))
                move = new Vector2(-horzVelocity, move.Y);
            else if (keyState.IsKeyDown(keyRight))
                move = new Vector2(+horzVelocity, move.Y);

            if (move.X != 0 && move.Y != 0)
                move.Normalize();

            if (move.X != 0 || move.Y != 0)
                lastNonNullMove = move;

            PlayerDataManager.TryMove(move, game.level.currentRoom);

            //Stone movement
            var pRect = new Rectangle((PlayerDataManager.unscaledPixelPosition + move * 2).ToPoint() + PlayerDrawer.RectInsideFrame.Location, PlayerDrawer.RectInsideFrame.Size);

            if (pRect.Intersects(game.level.currentRoom.entityStone.GetRectList()[0]))
                game.level.currentRoom.entityStone.TryPush(move, game.level.currentRoom);
        }

        static private bool oneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
