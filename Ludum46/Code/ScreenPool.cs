using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code
{
    /// <summary>
    /// Calls other draw things. Doesn't draw anything on it's own.
    /// </summary>
    public class ScreenPool : IDrawArranger
    {
        public enum ScreenState { Start, Playing, EndGame }
        public ScreenState screenState { get; private set; }

        private MouseState _mouse;
        private MouseState _oldMouse;
        private KeyboardState _key;
        private KeyboardState _oldKey;

        public Pixel drawShape;
        private PlayerDrawer playerDrawer;

        private Color bgColor = new Color(121,103,85);

        public ScreenPool(Ludum46 game)
        {
            this.screenState = ScreenState.Playing;

            drawShape = new Pixel(game.GraphicsDevice);
            playerDrawer = new PlayerDrawer(game);
        }

        /// <summary>
        /// Main draw cycle. Calls other drawers.
        /// </summary>
        public void CallDraws(Ludum46 game, SpriteBatch defaultSpriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(bgColor);
            defaultSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (screenState == ScreenState.Start)
            {
            }
            else if (screenState == ScreenState.Playing)
            {
                game.level.Draw(defaultSpriteBatch, game);
                playerDrawer.Draw(defaultSpriteBatch);
            }
            else if (screenState == ScreenState.EndGame)
            {
            }

            defaultSpriteBatch.End();
        }

        public void CallGuiControlUpdates(Ludum46 game)
        {
            this._key = Keyboard.GetState();
            this._mouse = Mouse.GetState();

            //code
            if (screenState == ScreenState.Start)
            {
            }
            else if (screenState == ScreenState.Playing)
            {
                game.level.Update(game);
            }
            else if (screenState == ScreenState.EndGame)
            {
            }

            this._oldKey = this._key;
            this._oldMouse = this._mouse;
        }
    }
}
