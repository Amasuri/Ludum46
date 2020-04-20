using System;
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
                //Walls
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

                //Arches, etc
                new Object(game, "res/obj/walls/arch_left", Object.DrawLevel.Wall, id: 13),
                new Object(game, "res/obj/walls/arch_right", Object.DrawLevel.Wall, id: 14),
                new Object(game, "res/obj/walls/arch_top", Object.DrawLevel.AbovePlayer, id: 15),
                new Object(game, "res/obj/walls/entrance", Object.DrawLevel.Floor, id: 16, isSolid: false),

                //Treeroom objects
                new Object(game, "res/obj/tree_bottom", Object.DrawLevel.Wall, id: 17),
                new Object(game, "res/obj/tree_place", Object.DrawLevel.Floor, id: 18, isSolid: false),
                new Object(game, "res/obj/tree_top", Object.DrawLevel.AbovePlayer, id: 19, isSolid: false),
                new Object(game, "res/obj/water_canal0", Object.DrawLevel.Floor, id: 20),
                new Object(game, "res/obj/water_canal1", Object.DrawLevel.Floor, id: 21),
                new Object(game, "res/obj/water_grid0", Object.DrawLevel.Floor, id: 22, isSolid: false),
                new Object(game, "res/obj/water_grid1", Object.DrawLevel.Floor, id: 23, isSolid: false),
                new Object(game, "res/obj/waterfall0", Object.DrawLevel.Wall, id: 24),
                new Object(game, "res/obj/waterfall1", Object.DrawLevel.Wall, id: 25),
                new Object(game, "res/obj/waterfall2", Object.DrawLevel.Wall, id: 26),
            };
        }
    }
}
