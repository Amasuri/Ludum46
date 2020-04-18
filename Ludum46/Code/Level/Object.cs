using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class Object
    {
        public enum DrawLevel
        {
            Floor,
            Wall,
            AbovePlayer,
        }
        public DrawLevel drawLevel { private set; get; }
        private Texture2D image;
        public readonly bool isSolid;
        public readonly int id;

        private Ludum46 refGame;

        public Object(Ludum46 game, string imgPath, DrawLevel drawLevel, bool isSolid = true, int id = 0)
        {
            this.refGame = game;
            this.drawLevel = drawLevel;
            this.isSolid = isSolid;
            this.id = id;

            //Debug
            this.image = new Texture2D(game.GraphicsDevice, 12, 12);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 unsCamera, Vector2 unsCoord)
        {
            //Debug
            refGame.screenPool.drawShape.Draw(spriteBatch, Color.Aquamarine, unsCoord - unsCamera, new Vector2(12, 12), Ludum46.Scale);
        }

        public Rectangle GetRect(Vector2 unsCoord)
        {
            return new Rectangle( unsCoord.ToPoint(), image.Bounds.Size);
        }
    }
}
