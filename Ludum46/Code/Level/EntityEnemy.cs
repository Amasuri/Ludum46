using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ludum46.Code.Graphics.AttackEffectPool;
using static Ludum46.Code.Level.EntityAttack;
using Ludum46.Code.Graphics;

namespace Ludum46.Code.Level
{
    public class EntityEnemy : Entity
    {
        private readonly Rectangle relativeRect;

        private Vector2 unscaledFrameCenterPoint => unsCoord + (this.relativeRect.Size.ToVector2() / 2);

        private readonly int SIGHT_RANGE = 60;
        private const float HOR_MOVE = 0.6f;
        private const float VER_MOVE = HOR_MOVE / 3 * 2;
        private const int DIFF_THRESHOLD = 2; //5
        private const int ATT_COOLDOWN = 200;
        private const int ATT_DELAY = 500;
        private const int ATT_RANGE = 13; //10

        private double blockAfterAttackForMs = 0;
        private double blockBeforeAttackForMs = ATT_DELAY;

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
        private Vector2 lastNonNullMove = new Vector2(0, HOR_MOVE);

        //Attack rects
        private Rectangle leftRectangle =>
            new Rectangle(this.unsCoord.ToPoint() + new Point(-5, 0), new Point(10, (int)PlayerDrawer.playerFrame.Y));
        private Rectangle rightRectangle =>
            new Rectangle(this.unsCoord.ToPoint() + new Point((int)PlayerDrawer.playerFrame.X - 5, 0), new Point(10, (int)PlayerDrawer.playerFrame.Y));
        private Rectangle upRectangle =>
            new Rectangle(this.unsCoord.ToPoint() + new Point(0, -5), new Point((int)PlayerDrawer.playerFrame.X, 10));
        private Rectangle downRectangle =>
            new Rectangle(this.unsCoord.ToPoint() + new Point(0, (int)PlayerDrawer.playerFrame.Y -5), new Point((int)PlayerDrawer.playerFrame.X, 10));

        //Since attack animations are in shared pool (to reduce overhead)
        //entities track time variables on their own
        protected double currentAnimTime { get; private set; } = AttackEffectPool.MAX_ATT_ANIM_TIME + 1d;
        protected Direction currentAnimDirection { get; private set; }
        protected AttackEffectPool.Type currentAnimType { get; private set; }
        protected AttackEffectPool.Type attackType;
        protected Vector2 animSwipeUnscOffest = new Vector2(-2, -2);

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

            SharedElements.DrawShadow(batch, pos * Ludum46.Scale);

            DrawMovement(batch, pos);
            DrawAttack(batch, pos);

            base.Draw(batch, unsCamera);
        }

        private void DrawAttack(SpriteBatch batch, Vector2 pos)
        {
            if (currentAnimTime < MAX_ATT_ANIM_TIME)
            {
                AttackEffectPool.DrawAttackAt(
                    batch, currentAnimTime, currentAnimDirection,
                    currentAnimType, (pos + animSwipeUnscOffest) * Ludum46.Scale
                );
            }
        }

        private void DrawMovement(SpriteBatch batch, Vector2 pos)
        {
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
        }

        protected override void UpdateMovement(Ludum46 game)
        {
            var move = Vector2.Zero;

            if (blockAfterAttackForMs >= 0f)
                return;

            var posDiff = PlayerDataManager.unscaledFrameCenterPoint - this.unscaledFrameCenterPoint;
            if (posDiff.Length() < SIGHT_RANGE && posDiff.Length() >= ATT_RANGE - 1.1f)
            {
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

            //Doesn't make sense to do updates in case there's nothing to update
            if (move == Vector2.Zero && lastMove == Vector2.Zero)
                return;

            //Check legitness (collisions)
            var rectFutur = new Rectangle(this.unsCoord.ToPoint() + (move * 5).ToPoint(), this.relativeRect.Size);
            if (game.level.currentRoom.EntityPositionCollides(rectFutur))
            {
                move = Vector2.Zero;
            }

            this.unsCoord += move;
            this.lastMove = move;
            if (move != Vector2.Zero)
                this.lastNonNullMove = move;

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }

        protected override void UpdateCustom(Ludum46 game)
        {
            //AttackAnimTimers should be updated regardless
            if (currentAnimTime < MAX_ATT_ANIM_TIME)
                currentAnimTime += Ludum46.DeltaUpdate;

            //If attack cooldown is still in the effect
            if (blockAfterAttackForMs >= 0f)
            {
                blockAfterAttackForMs -= Ludum46.DeltaUpdate;
                return;
            }

            //If not at range
            var posDiff = (PlayerDataManager.unscaledFrameCenterPoint - this.unscaledFrameCenterPoint).Length();
            if(posDiff > ATT_RANGE)
            {
                blockBeforeAttackForMs = ATT_DELAY;
                return;
            }

            //If not ready to attack yet
            blockBeforeAttackForMs -= Ludum46.DeltaUpdate;
            if(blockBeforeAttackForMs > 0f)
            {
                return;
            }

            //Replenish the delay before the next attack
            blockBeforeAttackForMs = ATT_DELAY;

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
                TargetedTo.Player, PlayerDataManager.unscaledPixelPosition, ATT_COOLDOWN);

            blockAfterAttackForMs = ATT_COOLDOWN;

            //Attack timers for animation
            currentAnimTime = 0d;
            currentAnimDirection = this.GetCurrentDirection();
            currentAnimType = this.GetNextAttackType();
        }

        protected AttackEffectPool.Direction GetCurrentDirection()
        {
            if (lastAnimation == sheetLeft)
                return AttackEffectPool.Direction.Left;
            else if (lastAnimation == sheetRight)
                return AttackEffectPool.Direction.Right;
            else if (lastAnimation == sheetUp)
                return AttackEffectPool.Direction.Up;
            else if (lastAnimation == sheetDown)
                return AttackEffectPool.Direction.Down;
            else if (lastAnimation == sheetUpLeft)
                return AttackEffectPool.Direction.Upleft;
            else if (lastAnimation == sheetUpRight)
                return AttackEffectPool.Direction.UpRight;
            else if (lastAnimation == sheetDownLeft)
                return AttackEffectPool.Direction.DownLeft;
            else
                return AttackEffectPool.Direction.DownRight;
        }

        protected AttackEffectPool.Type GetNextAttackType()
        {
            attackType++;
            if (attackType > AttackEffectPool.Type.Attack2)
                attackType = AttackEffectPool.Type.Attack1;

            return attackType;
        }

        public override void Hit(Ludum46 game)
        {
            MusicPlayer.ReplenishAttMusicTimer();

            game.soundPlayer.PlaySound(SoundPlayer.Type.Hit);
            base.Hit(game);
        }
    }
}
