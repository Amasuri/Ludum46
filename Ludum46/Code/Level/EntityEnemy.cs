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
                move = new Vector2(0, 0.1f);

            this.unsCoord += move;

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }
    }
}
