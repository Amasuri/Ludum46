﻿using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class Entity
    {
        public bool IsDead => hitPoints <= 0;
        private int hitPoints;

        protected List<Rectangle> rectList;
        protected Texture2D image;
        protected Vector2 unsCoord;

        protected Ludum46 refGame;

        public Entity(Ludum46 game, string imgPath, Vector2 coord, bool initRectAsImage = true, int hitPoints = 10)
        {
            this.refGame = game;
            this.unsCoord = coord;

            this.rectList = new List<Rectangle>();
            this.hitPoints = hitPoints;

            //Debug
            this.image = new Texture2D(game.GraphicsDevice, 12, 12);

            if (initRectAsImage)
                this.rectList.Add(new Rectangle(unsCoord.ToPoint(), image.Bounds.Size));
        }

        virtual public void Draw(SpriteBatch spriteBatch, Vector2 unsCamera)
        {
            ////Debug
            //foreach (var rect in this.rectList)
            //{
            //    refGame.screenPool.drawShape.Draw(spriteBatch, Color.Black, rect.Location.ToVector2() - unsCamera, rect.Size.ToVector2(), Ludum46.Scale);
            //}
        }

        public void Update(Ludum46 game)
        {
            this.UpdateCustom(game);
            this.UpdateMovement(game);
        }

        virtual protected void UpdateMovement(Ludum46 game)
        {
            //For enemies it's based on players (override)
            //For projectiles it's speed probably
        }

        virtual protected void UpdateCustom(Ludum46 game)
        {
            //Some custom things like spawn projectiles etc maybe? idk
        }

        public List<Rectangle> GetRectList()
        {
            return rectList;
        }

        virtual public void Hit(Ludum46 game)
        {
            this.hitPoints--;
        }
    }
}
