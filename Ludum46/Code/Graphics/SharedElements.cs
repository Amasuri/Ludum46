using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Graphics
{
    static public class SharedElements
    {
        private static Texture2D shadow;

        static public void LoadAssets(Ludum46 game)
        {
            shadow = game.Content.Load<Texture2D>("res/entity/shadow");
        }

        static public void DrawShadow(SpriteBatch batch, Vector2 pos)
        {
            DrawTexture(batch, shadow, pos, Ludum46.Scale);
        }

        private static void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, int scale, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos, null, Color.White, 0.0f, Vector2.Zero, scale, effects, 0.0f);
        }
    }
}
