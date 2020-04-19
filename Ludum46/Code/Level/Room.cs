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

            //Load tile data from proper room file
            dataFile = "LevelData/room" + id.ToString() + ".ptdata";
            foreach (var line in File.ReadAllLines(dataFile))
            {
                var splitLine = line.Split(new string[] { " ", "." }, StringSplitOptions.RemoveEmptyEntries);
                var currentObject = game.pool.pool[Convert.ToInt32(splitLine[2])];
                var currentPosition = new Vector2(Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[1]));

                EditorAddObjectAt(currentPosition, currentObject);
            }

            //Debug spawn random enemies
            this.entities = new List<Entity>
            {
                new EntityEnemy(game, "skeleton", new Vector2(130, 29), new Rectangle(new Point(7, 13), new Point(10, 5))),
            };
        }

        public bool PositionCollides(Vector2 pos)
        {
            var futurePlayerRect = new Rectangle(pos.ToPoint() + PlayerDrawer.RectInsideFrame.Location, PlayerDrawer.RectInsideFrame.Size);

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
                    //Object not on screen
                    if (!obj.GetRect(objList.Key).Intersects(PlayerDrawer.UnscaledCameraRect))
                        continue;

                    //Object not on layer
                    if (obj.drawLevel == Object.DrawLevel.AbovePlayer)
                        continue;

                    obj.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset, objList.Key);
                }
            }

            foreach (var entiity in entities)
            {
                entiity.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset);
            }
        }

        public void DrawFront(SpriteBatch batch)
        {
            //Upper level stationary objects
            foreach (var objList in objects)
            {
                foreach (var obj in objList.Value)
                {
                    //Object not on screen
                    if (!obj.GetRect(objList.Key).Intersects(PlayerDrawer.UnscaledCameraRect))
                        continue;

                    //Object not on layer
                    if (obj.drawLevel != Object.DrawLevel.AbovePlayer)
                        continue;

                    obj.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset, objList.Key);
                }
            }
        }

        public void Update(Ludum46 ludum46)
        {
            //Basically this involves checking everythin including bullets and enemies
            foreach (var entiity in entities)
            {
                entiity.Update(ludum46);
            }

            //Enemy hitboxing
            foreach (var item in this.entities.Where(x => x is EntityAttack))
            {
                var attack = (EntityAttack)item;

                //Don't use if...
                if (attack.Used)
                    continue;
                if (attack.targetedTo == EntityAttack.TargetedTo.Player)
                    continue;

                //Compare every attack rect against every enemy
                foreach (var enemy in this.entities.Where(x => !(x is EntityAttack)))
                {
                    foreach (var attackRect in attack.GetRectList())
                    {
                        if(attackRect.Intersects(enemy.GetRectList()[0]))
                        {
                            enemy.Hit();
                            attack.MarkAsSoonToBeUsed();
                        }
                    }
                }

                //Attack is no longer active if it hit something
                if (attack.SoonToBeUsed)
                    attack.MarkAsUsed();
            }

            //Ded attacks
            var dedEntities = new List<Entity>();
            foreach (var item in this.entities)
            {
                if (!(item is EntityAttack))
                    continue;

                var attack = (EntityAttack)item;

                if (attack.readyToDie)
                    dedEntities.Add(item);
            }

            //Ded undead
            foreach (var item in this.entities)
            {
                if (item is EntityAttack)
                    continue;

                if (item.IsDead)
                    dedEntities.Add(item);
            }

            //Delete ded
            foreach (var dedItem in dedEntities)
            {
                this.entities.Remove(dedItem);
            }
        }

        public void SpawnAttackEntity(Ludum46 game, List<Rectangle> rectList, EntityAttack.TargetedTo target, Vector2 unscPos, int timeToLive)
        {
            entities.Add(new EntityAttack(game, "aaaa", unscPos, timeToLive, target, rectList));
        }
    }
}
