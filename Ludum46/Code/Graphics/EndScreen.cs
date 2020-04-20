using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Graphics
{
    public class EndScreen : ADrawingClass
    {
        private Pixel pixel;
        private int pixAlpha = 0;

        private const string EndBadText = "You have fallen in battle,\n and therefore, your tree has died.";
        private const string EndGoodText = "You have saved your dear friend. \n Your life together may continue for more.";

        public EndScreen(Ludum46 game)
        {
            pixel = new Pixel(game.GraphicsDevice);
        }

        public override void Draw(Ludum46 game, SpriteBatch spriteBatch)
        {
            //Bad end
            if (PlayerDataManager.dead)
            {
                pixel.Draw(spriteBatch, new Color(5, 5, 5, pixAlpha), Vector2.Zero, new Vector2(Ludum46.ScaledWidth, Ludum46.ScaledHeight));

                var size = ScreenPool.fontBig.MeasureString(EndBadText);
                var pos = (new Vector2(Ludum46.ScaledWidth, Ludum46.ScaledHeight) - size) / 2;

                spriteBatch.DrawString(ScreenPool.fontBig, EndBadText, pos, new Color(200, 2, 50, pixAlpha));
            }

            //Good end
            else
            {
                pixel.Draw(spriteBatch, new Color(20, 16, 19, pixAlpha), Vector2.Zero, new Vector2(Ludum46.ScaledWidth, Ludum46.ScaledHeight));

                //var size = ScreenPool.fontBig.MeasureString(EndGoodText);
                //var pos = (new Vector2(Ludum46.ScaledWidth, Ludum46.ScaledHeight) - size) / 2;

                //spriteBatch.DrawString(ScreenPool.fontBig, EndGoodText, pos, new Color(200, 200, 200, pixAlpha));
            }
        }

        public override void Update(Ludum46 game, MouseState mouse, MouseState oldMouse, KeyboardState keys, KeyboardState oldKeys)
        {
            pixAlpha++;
            if (pixAlpha > 255)
                pixAlpha = 255;
        }
    }
}
