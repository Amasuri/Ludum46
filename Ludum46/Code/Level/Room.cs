using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public EntityStone entityStone { get; private set; }

        private string dataFile;

        private double auxSpawnTimer;
        private const double AUX_MAX_SPAWN_TIMER = 7000d;

        public Room(Ludum46 game, int id)
        {
            this.objects = new Dictionary<Vector2, List<Object>>();

            auxSpawnTimer = AUX_MAX_SPAWN_TIMER;

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
            this.entities = new List<Entity>();

            if (id == 1)
            {
                this.entities.Add(SpawnSkeletal(game, new Vector2(130, 29)));
            }
            this.entityStone = new EntityStone(game, "aaaa", new Vector2(0, 29));
        }

        private static EntityEnemy SpawnSkeletal(Ludum46 game, Vector2 pos)
        {
            return new EntityEnemy(game, "skeleton", pos, new Rectangle(new Point(7, 13), new Point(10, 5)), hitPoints: 3);
        }

        public bool PlayerPositionCollides(Vector2 pos)
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

            if (entityStone.GetRectList()[0].Intersects(futurePlayerRect))
                return true;

            return false;
        }

        public void EditorDeleteObjectsAt(Vector2 cursorPos)
        {
            //this.objects.Remove(cursorPos);

            Vector2 posToDelete = new Vector2(-99999, -99999);

            foreach (var position in this.objects.Keys)
                foreach (var obj in this.objects[position])
                {
                    if (obj.GetRect(position).Contains(cursorPos))
                    {
                        posToDelete = position;
                        goto useTwoBreaks;
                    }
                }

            useTwoBreaks:
            if (posToDelete != new Vector2(-99999, -99999))
                this.objects.Remove(posToDelete);
        }

        public bool EntityPositionCollides(Rectangle entityRectFutur)
        {
            foreach (var objList in objects)
            {
                foreach (var obj in objList.Value)
                {
                    if (!obj.isSolid)
                        continue;

                    //Checking whether each obj is inside screen doesn't make sense because it's essentially
                    //The same operation as checking whether the obj is inside the player, so we check for that instead
                    if (obj.GetRect(objList.Key).Intersects(entityRectFutur))
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

            //Stone
            entityStone.Draw(batch, PlayerDrawer.unscaledCameraDrawOffset);
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
            //Chekk spawning conditions
            if (auxSpawnTimer <= 0d)
            {
                var posToDistance = new Dictionary<Vector2, double>();
                foreach (var position in this.objects.Keys)
                {
                    foreach (var obj in this.objects[position].Where(x => x.isSpawning))
                    {
                        posToDistance.Add(position, (PlayerDataManager.unscaledFrameCenterPoint - position).Length());
                    }
                }

                if(posToDistance.Count > 0)
                {
                    var ps = posToDistance.OrderBy(x => x.Value);
                    this.entities.Add(SpawnSkeletal(ludum46, ps.First().Key));
                    auxSpawnTimer = AUX_MAX_SPAWN_TIMER;
                }
            }

            //Or just update the timer
            else
            {
                auxSpawnTimer -= Ludum46.DeltaUpdate;
            }

            //Enemies spawn entities so careful here
            var oomph = entities.Where(x => x is EntityEnemy).ToList();
            foreach (var entiity in oomph)
            {
                entiity.Update(ludum46);
            }

            //Update everything else
            foreach (var entiity in entities.Where(x => !(x is EntityEnemy)))
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

            //Level switch
            var playerRect = new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint() + PlayerDrawer.RectInsideFrame.Location, PlayerDrawer.RectInsideFrame.Size);
            foreach (var position in this.objects.Keys)
            {
                foreach (var obj in this.objects[position])
                {
                    if (!obj.isSwitchingLevels)
                        continue;

                    if (obj.GetRect(position).Intersects(playerRect))
                            ludum46.level.SwitchRoom();
                }
            }
        }

        public void SpawnAttackEntity(Ludum46 game, List<Rectangle> rectList, EntityAttack.TargetedTo target, Vector2 unscPos, int timeToLive)
        {
            entities.Add(new EntityAttack(game, "aaaa", unscPos, timeToLive, target, rectList));
        }
    }
}
