using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class EntityEnemy : Entity
    {
        private readonly Rectangle relativeRect;

        private Vector2 unscaledFrameCenterPoint => unsCoord + (this.relativeRect.Size.ToVector2() / 2);

        private readonly int SIGHT_RANGE = 60;
        private const float HOR_MOVE = 0.3f;
        private const float VER_MOVE = HOR_MOVE / 3 * 2;
        private const int DIFF_THRESHOLD = 5;

        //Visual
        private Animation sheetLeft;
        private Animation sheetRight;
        private Animation sheetUp;
        private Animation sheetDown;
        private Animation sheetUpLeft;
        private Animation sheetUpRight;
        private Animation sheetDownLeft;
        private Animation sheetDownRight;
        private Animation lastAnimation;

        private Vector2 lastMove;

        public EntityEnemy(Ludum46 game, string folderName, Vector2 coord, Rectangle enemyRectRelativeToImg, int hitPoints = 10)
            : base(game, "aaaa", coord, initRectAsImage: false, hitPoints)
        {
            this.relativeRect = enemyRectRelativeToImg;
            this.rectList.Add(new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size));

            //Visual
            int frameMs = 150;
            sheetLeft = new Animation(game, "res/entity/" + folderName +"/Left", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetRight = new Animation(game, "res/entity/" + folderName + "/Right", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUp = new Animation(game, "res/entity/" + folderName + "/Up", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDown = new Animation(game, "res/entity/" + folderName + "/Down", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUpLeft = new Animation(game, "res/entity/" + folderName + "/UpLeft", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetUpRight = new Animation(game, "res/entity/" + folderName + "/UpRight", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDownLeft = new Animation(game, "res/entity/" + folderName + "/DownLeft", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
            sheetDownRight = new Animation(game, "res/entity/" + folderName + "/DownRight", 24, msBetweenFrames: frameMs, imgScale: Ludum46.Scale);
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

        public override void Draw(SpriteBatch batch, Vector2 unsCamera)
        {
            var pos = this.unsCoord - unsCamera;

            //Stationary
            if (this.lastMove == Vector2.Zero)
            {
                lastAnimation.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, 0);
            }

            //Basic directions
            else if (this.lastMove.X > 0 && this.lastMove.Y == 0)
            {
                sheetRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetRight;
            }
            else if (this.lastMove.X < 0 && this.lastMove.Y == 0)
            {
                sheetLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetLeft;
            }
            else if (this.lastMove.X == 0 && this.lastMove.Y < 0)
            {
                sheetUp.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUp;
            }
            else if (this.lastMove.X == 0 && this.lastMove.Y > 0)
            {
                sheetDown.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDown;
            }

            //Diagonal
            else if (this.lastMove.X > 0 && this.lastMove.Y < 0)
            {
                sheetUpRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUpRight;
            }
            else if (this.lastMove.X < 0 && this.lastMove.Y < 0)
            {
                sheetUpLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetUpLeft;
            }
            else if (this.lastMove.X > 0 && this.lastMove.Y > 0)
            {
                sheetDownRight.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDownRight;
            }
            else if (this.lastMove.X < 0 && this.lastMove.Y > 0)
            {
                sheetDownLeft.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);
                lastAnimation = sheetDownLeft;
            }

            base.Draw(batch, unsCamera);
        }

        protected override void UpdateMovement(Ludum46 game)
        {
            var move = Vector2.Zero;

            if (Math.Abs(PlayerDataManager.unscaledFrameCenterPoint.Length() - this.unscaledFrameCenterPoint.Length()) < SIGHT_RANGE)
            {
                var posDiff = PlayerDataManager.unscaledFrameCenterPoint - this.unscaledFrameCenterPoint;

                //X axis
                if(posDiff.X > DIFF_THRESHOLD)
                    move = new Vector2(HOR_MOVE, 0f);
                else if (posDiff.X < -DIFF_THRESHOLD)
                    move = new Vector2(-HOR_MOVE, 0f);

                //Y axis
                if (posDiff.Y > DIFF_THRESHOLD)
                    move = new Vector2(move.X, VER_MOVE);
                else if (posDiff.Y < -DIFF_THRESHOLD)
                    move = new Vector2(move.X, -VER_MOVE);
            }

            this.unsCoord += move;
            this.lastMove = move;

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }
    }
}
