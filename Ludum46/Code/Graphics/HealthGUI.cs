using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Graphics
{
    static public class HealthGUI
    {
        static private Texture2D gui;
        static private Texture2D orb;

        static public void InitAsset(Ludum46 game)
        {
            gui = game.Content.Load<Texture2D>("res/gui/hp_bar");
            orb = game.Content.Load<Texture2D>("res/gui/hp");
        }

        static public void Draw(SpriteBatch batch)
        {
            DrawTexture(batch, gui, Vector2.Zero, Ludum46.Scale);
            for (int i = 0; i < PlayerDataManager.hp; i++)
            {
                var pos = new Vector2(6 + 16*i, 1) * Ludum46.Scale;
                DrawTexture(batch, orb, pos, Ludum46.Scale);
            }
        }

        private static void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, int scale, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos, null, Color.White, 0.0f, Vector2.Zero, scale, effects, 0.0f);
        }
    }
}
