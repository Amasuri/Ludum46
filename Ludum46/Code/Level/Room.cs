using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class Room
    {
        private Dictionary<Vector2, List<Object>> objects;
        private List<Entity> entities;

        private string dataFile;

        public Room(Ludum46 game, int id)
        {
            this.objects = new Dictionary<Vector2, List<Object>>();

            dataFile = "LevelData/room" + id.ToString() + ".ptdata";

            foreach (var line in File.ReadAllLines(dataFile))
            {
                var splitLine = line.Split(new string[] { " ", "." }, StringSplitOptions.RemoveEmptyEntries);
                var currentObject = game.pool.pool[Convert.ToInt32(splitLine[2])];
                var currentPosition = new Vector2(Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[1]));

                EditorAddObjectAt(currentPosition, currentObject);
            }

            this.entities = new List<Entity>
            {
                new Entity(game, "aaa", new Vector2(90, 9)),
            };
        }

        public bool PositionCollides(Vector2 pos)
        {
            var futurePlayerRect = new Rectangle(pos.ToPoint(), PlayerDrawer.Rect.Size);

            foreach (var objList in objects)
            {
                foreach (var obj in objList.Value)
                {
                    if (!obj.isSolid)
                        continue;

                    //Checking whether each obj is inside screen doesn't make sense because it's essentially
                    //The same operation as checking whether the obj is inside the player, so we check for that instead
                    if (obj.GetRect(objList.Key).Intersects(futurePlayerRect))
                        return true;
                }
            }

            return false;
        }

        public void EditorAddObjectAt(Vector2 currentPosition, Object @object)
        {
            if (!this.objects.ContainsKey(currentPosition))
            {
                this.objects.Add(currentPosition, new List<Object>() { @object });
            }
            else
            {
                this.objects[currentPosition].Add(@object);
            }
        }

        public void EditorSaveRoomData()
        {
            var file = "";

            foreach (var objList in this.objects)
            {
                foreach (var obj in objList.Value)
                {
                    var resStr = String.Format("{0}.{1} {2}\n", (int)objList.Key.X, (int)objList.Key.Y, obj.id);
                    file += resStr;
                }
            }

            File.WriteAllText(dataFile, file);
        }

        public void Draw(SpriteBatch batch)
        {
            //Statinary objects that are kinda
            foreach (var objList in objects)
            {
                foreach (var obj in objList.Value)
                {
                    obj.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset, objList.Key);
                }
            }

            foreach (var entiity in entities)
            {
                entiity.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset);
            }
        }

        public void Update(Ludum46 ludum46)
        {
            //Basically this involves checking everythin including bullets and enemies
            foreach (var entiity in entities)
            {
                entiity.Update(ludum46);
            }
        }
    }
}
