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
        public readonly bool isSwitchingLevels;

        private Ludum46 refGame;

        public Object(Ludum46 game, string imgPath, DrawLevel drawLevel, bool isSolid = true, int id = 0, bool isSwitchingLevels = false)
        {
            this.refGame = game;
            this.drawLevel = drawLevel;
            this.isSolid = isSolid;
            this.id = id;
            this.isSwitchingLevels = isSwitchingLevels;

            this.image = game.Content.Load<Texture2D>(imgPath);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 unsCamera, Vector2 unsCoord)
        {
            spriteBatch.Draw(this.image, (unsCoord - unsCamera) * Ludum46.Scale, null, Color.White, 0.0f, Vector2.Zero, Ludum46.Scale, SpriteEffects.None, 0.0f);
        }

        public Rectangle GetRect(Vector2 unsCoord)
        {
            return new Rectangle( unsCoord.ToPoint(), image.Bounds.Size);
        }
    }
}
