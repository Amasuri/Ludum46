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
        protected Texture2D image;
        protected Vector2 unsCoord;

        protected Ludum46 refGame;

        public Entity(Ludum46 game, string imgPath, Vector2 coord)
        {
            this.refGame = game;
            this.unsCoord = coord;

            //Debug
            this.image = new Texture2D(game.GraphicsDevice, 12, 12);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 unsCamera)
        {
            //Debug
            refGame.screenPool.drawShape.Draw(spriteBatch, Color.Black, this.unsCoord - unsCamera, new Vector2(12, 12), Ludum46.Scale);
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

        public Rectangle GetRect()
        {
            return new Rectangle(unsCoord.ToPoint(), image.Bounds.Size);
        }
    }
}
