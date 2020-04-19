using Ludum46.Code.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Reusable
{
    public class PlayerDrawer
    {
        /// <summary>
        /// So it works like that: player's position is the same thing as entities, so collisions etc work without stupid offsets like in that one game i made
        /// But everything is draw with an offset
        /// Player is always drawn in the center
        /// </summary>
        private static Vector2 cameraRelative;

        public static Vector2 unscaledCameraDrawOffset => (PlayerDataManager.unscaledPixelPosition) - cameraRelative;

        static public Rectangle UnscaledCameraRect =>
            new Rectangle((int)unscaledCameraDrawOffset.X, (int)unscaledCameraDrawOffset.Y, Ludum46.UnscaledWidth, Ludum46.UnscaledHeight);

        /// <summary>
        /// Doesn't refer to actual position, but to a rect relative to the frame
        /// </summary>
        public static Rectangle RectInsideFrame => new Rectangle(new Point(7, 13), new Point(10, 5));

        /// <summary>
        /// NOT THE ACTUAL PLAYER SIZE, but the frame thing
        /// </summary>
        public static Vector2 playerFrame { get; private set; }

        private Ludum46 refGame;

        private Texture2D playerStill;
        private Animation sheetLeft;
        private Animation sheetRight;
        private Animation sheetUp;
        private Animation sheetDown;
        private Animation sheetUpLeft;
        private Animation sheetUpRight;
        private Animation sheetDownLeft;
        private Animation sheetDownRight;
        private Animation lastAnimation;

        public PlayerDrawer(Ludum46 game)
        {
            this.refGame = game;

            playerFrame = new Vector2(24, 24);
            cameraRelative = new Vector2(Ludum46.UnscaledWidth - playerFrame.X, Ludum46.UnscaledHeight - playerFrame.Y) / 2;

            //Animation things
            int frameMs = 150;
            sheetLeft = new Animation(game, "res/entity/player/playerLeft", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetRight = new Animation(game, "res/entity/player/playerRight", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUp = new Animation(game, "res/entity/player/playerUp", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDown = new Animation(game, "res/entity/player/playerDown", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUpLeft = new Animation(game, "res/entity/player/playerUpLeft", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUpRight = new Animation(game, "res/entity/player/playerUpRight", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDownLeft = new Animation(game, "res/entity/player/playerDownLeft", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDownRight = new Animation(game, "res/entity/player/playerDownRight", (int)playerFrame.X, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetLeft.EnableDrawing();
            sheetRight.EnableDrawing();
            sheetUp.EnableDrawing();
            sheetDown.EnableDrawing();
            sheetUpLeft.EnableDrawing();
            sheetUpRight.EnableDrawing();
            sheetDownLeft.EnableDrawing();
            sheetDownRight.EnableDrawing();

            lastAnimation = sheetDown;
        }

        public void Draw(SpriteBatch batch)
        {
            var pos = PlayerDataManager.unscaledPixelPosition - unscaledCameraDrawOffset;

            DrawPlayerMovementAnimations(batch, pos);
            DrawPlayerAttackAnimations(batch, pos);
        }

        private void DrawPlayerAttackAnimations(SpriteBatch batch, Vector2 pos)
        {
            if(PlayerController.currentAnimTime < AttackEffectPool.MAX_ATT_ANIM_TIME)
            {
                AttackEffectPool.DrawAttackAt(
                    batch, PlayerController.currentAnimTime, PlayerController.currentAnimDirection,
                    PlayerController.currentAnimType, pos * Ludum46.Scale
                );
            }
        }

        private void DrawPlayerMovementAnimations(SpriteBatch batch, Vector2 pos)
        {
            //Stationary
            if (PlayerDataManager.lastMove == Vector2.Zero)
            {
                lastAnimation.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, 0);
            }

            //Basic directions
            else if (PlayerDataManager.lastMove.X > 0 && PlayerDataManager.lastMove.Y == 0)
            {
                sheetRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetRight;
            }
            else if (PlayerDataManager.lastMove.X < 0 && PlayerDataManager.lastMove.Y == 0)
            {
                sheetLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetLeft;
            }
            else if (PlayerDataManager.lastMove.X == 0 && PlayerDataManager.lastMove.Y < 0)
            {
                sheetUp.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUp;
            }
            else if (PlayerDataManager.lastMove.X == 0 && PlayerDataManager.lastMove.Y > 0)
            {
                sheetDown.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDown;
            }

            //Diagonal
            else if (PlayerDataManager.lastMove.X > 0 && PlayerDataManager.lastMove.Y < 0)
            {
                sheetUpRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUpRight;
            }
            else if (PlayerDataManager.lastMove.X < 0 && PlayerDataManager.lastMove.Y < 0)
            {
                sheetUpLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUpLeft;
            }
            else if (PlayerDataManager.lastMove.X > 0 && PlayerDataManager.lastMove.Y > 0)
            {
                sheetDownRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDownRight;
            }
            else if (PlayerDataManager.lastMove.X < 0 && PlayerDataManager.lastMove.Y > 0)
            {
                sheetDownLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDownLeft;
            }
        }
    }
}
