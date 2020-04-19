using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
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

        public EntityEnemy(Ludum46 game, string folderName, Vector2 coord, Rectangle enemyRectRelativeToImg, int hitPoints = 10)
            : base(game, "aaaa", coord, initRectAsImage: false, hitPoints)
        {
            this.relativeRect = enemyRectRelativeToImg;
            this.rectList.Add(new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size));
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

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }
    }
}
