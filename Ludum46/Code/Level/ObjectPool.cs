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
                new Object(game, "res/obj/walls/entrance", Object.DrawLevel.Floor, id: 16, isSolid: false, isSwitchingLevels: true),

                //Treeroom objects
                new Object(game, "res/obj/tree_bottom", Object.DrawLevel.Wall, id: 17),
                new Object(game, "res/obj/tree_place", Object.DrawLevel.Floor, id: 18, isSolid: false),
                new Object(game, "res/obj/tree_top", Object.DrawLevel.AbovePlayer, id: 19, isSolid: false),
                new Object(game, "res/obj/water_canal0", Object.DrawLevel.Floor, id: 20),
                new Object(game, "res/obj/water_canal1", Object.DrawLevel.AbovePlayer, id: 21),
                new Object(game, "res/obj/water_grid0", Object.DrawLevel.Floor, id: 22, isSolid: false),
                new Object(game, "res/obj/water_grid1", Object.DrawLevel.Floor, id: 23, isSolid: false),
                new Object(game, "res/obj/waterfall0", Object.DrawLevel.AbovePlayer, id: 24),
                new Object(game, "res/obj/waterfall1", Object.DrawLevel.AbovePlayer, id: 25),
                new Object(game, "res/obj/waterfall2", Object.DrawLevel.AbovePlayer, id: 26),

                //Misc decorations
                new Object(game, "res/obj/column0", Object.DrawLevel.Wall, id: 27),
                new Object(game, "res/obj/column1", Object.DrawLevel.AbovePlayer, id: 28, isSolid: false),
                new Object(game, "res/obj/ruined_wall", Object.DrawLevel.Wall, id: 29),
                new Object(game, "res/obj/slope0", Object.DrawLevel.Wall, id: 30, isSolid: false),
                new Object(game, "res/obj/slope1", Object.DrawLevel.Wall, id: 31, isSolid: false),
                new Object(game, "res/obj/slope2", Object.DrawLevel.Wall, id: 32, isSolid: false),

                //Water
                new Object(game, "res/obj/water/water0", Object.DrawLevel.Wall, id: 33),
                new Object(game, "res/obj/water/water1", Object.DrawLevel.Wall, id: 34),
                new Object(game, "res/obj/water/water2", Object.DrawLevel.Wall, id: 35),
                new Object(game, "res/obj/water/water3", Object.DrawLevel.Wall, id: 36),
                new Object(game, "res/obj/water/water4", Object.DrawLevel.Wall, id: 37),
                new Object(game, "res/obj/water/water5", Object.DrawLevel.Wall, id: 38),
                new Object(game, "res/obj/water/water6", Object.DrawLevel.Wall, id: 39),
                new Object(game, "res/obj/water/water7", Object.DrawLevel.Wall, id: 40),
                new Object(game, "res/obj/water/water8", Object.DrawLevel.Wall, id: 41),
                new Object(game, "res/obj/water/water9", Object.DrawLevel.Wall, id: 42),
                new Object(game, "res/obj/water/water10", Object.DrawLevel.Wall, id: 43),
                new Object(game, "res/obj/water/water11", Object.DrawLevel.Wall, id: 44),

                //Low walls
                new Object(game, "res/obj/low_walls/low_wall0", Object.DrawLevel.Wall, id: 45),
                new Object(game, "res/obj/low_walls/low_wall1", Object.DrawLevel.Wall, id: 46),
                new Object(game, "res/obj/low_walls/low_wall2", Object.DrawLevel.Wall, id: 47),
                new Object(game, "res/obj/low_walls/low_wall3", Object.DrawLevel.Wall, id: 48),
                new Object(game, "res/obj/low_walls/low_wall4", Object.DrawLevel.Wall, id: 49),
                new Object(game, "res/obj/low_walls/low_wall5", Object.DrawLevel.Wall, id: 50),
                new Object(game, "res/obj/low_walls/low_wall6", Object.DrawLevel.Wall, id: 51),
                new Object(game, "res/obj/low_walls/low_wall7", Object.DrawLevel.Wall, id: 52),
                new Object(game, "res/obj/low_walls/low_wall8", Object.DrawLevel.Wall, id: 53),

                //Decorations
                new Object(game, "res/obj/decor/decor0", Object.DrawLevel.Floor, id: 54, isSolid: false),
                new Object(game, "res/obj/decor/decor1", Object.DrawLevel.Floor, id: 55, isSolid: false),
                new Object(game, "res/obj/decor/decor2", Object.DrawLevel.Floor, id: 56, isSolid: false),
                new Object(game, "res/obj/decor/decor3", Object.DrawLevel.Floor, id: 57, isSolid: false),
                new Object(game, "res/obj/decor/decor4", Object.DrawLevel.Floor, id: 58, isSolid: false),
                new Object(game, "res/obj/decor/decor5", Object.DrawLevel.Floor, id: 59, isSolid: false),
                new Object(game, "res/obj/decor/decor6", Object.DrawLevel.Floor, id: 60, isSolid: false),
                new Object(game, "res/obj/decor/decor7", Object.DrawLevel.Floor, id: 61, isSolid: false),
                new Object(game, "res/obj/decor/decor8", Object.DrawLevel.Floor, id: 62, isSolid: false),
                new Object(game, "res/obj/decor/decor9", Object.DrawLevel.Floor, id: 63, isSolid: false),
                new Object(game, "res/obj/decor/decor10", Object.DrawLevel.Floor, id: 64, isSolid: false),
                new Object(game, "res/obj/decor/decor11", Object.DrawLevel.Floor, id: 65, isSolid: false),
                new Object(game, "res/obj/decor/decor12", Object.DrawLevel.Floor, id: 66, isSolid: false),
                new Object(game, "res/obj/decor/decor13", Object.DrawLevel.Floor, id: 67, isSolid: false),
                new Object(game, "res/obj/decor/decor14", Object.DrawLevel.Floor, id: 68, isSolid: false),
                new Object(game, "res/obj/decor/decor15", Object.DrawLevel.Floor, id: 69, isSolid: false),
                new Object(game, "res/obj/decor/decor16", Object.DrawLevel.Floor, id: 70, isSolid: false),
                new Object(game, "res/obj/decor/decor17", Object.DrawLevel.Floor, id: 71, isSolid: false),
                new Object(game, "res/obj/decor/decor18", Object.DrawLevel.Floor, id: 72, isSolid: false),

                //Two more walls for quick-ness
                new Object(game, "res/obj/walls/wall13", Object.DrawLevel.Wall, id: 73),
                new Object(game, "res/obj/walls/wall14", Object.DrawLevel.Wall, id: 74),

                //Monster spawner D
                new Object(game, "res/obj/skeleton_spawn", Object.DrawLevel.Floor, id: 75, isSolid: false, isSpawning: true),

                //Additional low walls
                new Object(game, "res/obj/low_walls/low_wall9", Object.DrawLevel.Wall, id: 76),
                new Object(game, "res/obj/low_walls/low_wall10", Object.DrawLevel.Wall, id: 77),
                new Object(game, "res/obj/low_walls/low_wall11", Object.DrawLevel.Wall, id: 78),
            };

            //Sanity check
            for (int i = 0; i < pool.Count; i++)
                for (int i2 = 0; i2 < pool.Count; i2++)
                {
                    if (pool[i].id == pool[i2].id && pool[i].id != i2)
                    {
                        throw new Exception("Mixed id: " + pool[i].id);
                    }
                }
        }
    }
}
