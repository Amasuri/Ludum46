﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// Defines rules that every drawing class should follow.
    /// </summary>
    abstract public class ADrawingClass
    {
        abstract public void Draw(Ludum46 game, SpriteBatch spriteBatch);

        abstract public void Update(Ludum46 game, MouseState mouse, MouseState oldMouse, KeyboardState keys, KeyboardState oldKeys);

        /// <summary>
        /// Wrapper method for quicker draw.
        /// </summary>
        protected void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, int scale, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos, null, Color.White, 0.0f, Vector2.Zero, scale, effects, 0.0f);
        }
    }
}
