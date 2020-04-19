﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class Level
    {
        //Some sorta current room idk and a room list
        public Room currentRoom { private set; get; }

        public Level(Ludum46 game)
        {
            currentRoom = new Room(game, 1);
        }

        public void Draw(SpriteBatch batch, Ludum46 game)
        {
            currentRoom.Draw(batch);
        }

        public void DrawFront(SpriteBatch batch, Ludum46 game)
        {
            currentRoom.DrawFront(batch);
        }

        public void Update(Ludum46 ludum46)
        {
            currentRoom.Update(ludum46);
        }

        public void SpawnAttackEntity(Ludum46 game, List<Rectangle> rectList, EntityAttack.TargetedTo target, Vector2 unscPos, int timeToLive)
        {
            currentRoom.SpawnAttackEntity(game, rectList, target, unscPos, timeToLive);
        }
    }
}
