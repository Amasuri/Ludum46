﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    /// <summary>
    /// List of all objects available
    /// </summary>
    public class ObjectPool
    {
        public List<Object> pool;

        public ObjectPool(Ludum46 game)
        {
            pool = new List<Object>
            {
                new Object(game, "res/obj/walls/wall0", Object.DrawLevel.Wall, id: 0),
                new Object(game, "res/obj/walls/wall1", Object.DrawLevel.Wall, id: 1),
                new Object(game, "res/obj/walls/wall2", Object.DrawLevel.Wall, id: 2),
                new Object(game, "res/obj/walls/wall3", Object.DrawLevel.Wall, id: 3),
                new Object(game, "res/obj/walls/wall4", Object.DrawLevel.Wall, id: 4),
                new Object(game, "res/obj/walls/wall5", Object.DrawLevel.Wall, id: 5),
                new Object(game, "res/obj/walls/wall6", Object.DrawLevel.Wall, id: 6),
                new Object(game, "res/obj/walls/wall7", Object.DrawLevel.Wall, id: 7),
                new Object(game, "res/obj/walls/wall8", Object.DrawLevel.Wall, id: 8),
                new Object(game, "res/obj/walls/wall9", Object.DrawLevel.Wall, id: 9),
                new Object(game, "res/obj/walls/wall10", Object.DrawLevel.Wall, id: 10),
                new Object(game, "res/obj/walls/wall11", Object.DrawLevel.Wall, id: 11),
                new Object(game, "res/obj/walls/wall12", Object.DrawLevel.Wall, id: 12),
            };
        }
    }
}
