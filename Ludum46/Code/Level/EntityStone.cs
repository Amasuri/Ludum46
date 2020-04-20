using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class EntityStone : Entity
    {
        public EntityStone(Ludum46 game, string imgPath, Vector2 coord, bool initRectAsImage = true)
            : base(game, imgPath, coord, initRectAsImage, Int32.MaxValue)
        {
        }
    }
}
