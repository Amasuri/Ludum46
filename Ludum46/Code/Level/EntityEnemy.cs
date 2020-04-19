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

        public EntityEnemy(Ludum46 game, string folderName, Vector2 coord, Rectangle enemyRectRelativeToImg, int hitPoints = 10)
            : base(game, "aaaa", coord, initRectAsImage: false, hitPoints)
        {
            this.relativeRect = enemyRectRelativeToImg;
            this.rectList.Add(new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size));
        }

        protected override void UpdateMovement(Ludum46 game)
        {
            this.unsCoord += new Vector2(0f, 0.1f);

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }
    }
}
