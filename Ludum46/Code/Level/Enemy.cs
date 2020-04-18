using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class Enemy : Entity
    {
        public Enemy(Ludum46 game, string imgPath, Vector2 coord)
            : base(game, imgPath, coord)
        {
        }
    }
}
