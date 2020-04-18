using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Reusable
{
    public class PlayerDrawer
    {
        /// <summary>
        /// So it works like that: player's position is the same thing as entities, so collisions etc work without stupid offsets like in that one game i made
        /// But everything is draw with an offset
        /// Player is always drawn in the center
        /// </summary>
        private static Vector2 cameraRelative;

        public static Vector2 unscaledCameraDrawOffset => (PlayerDataManager.unscaledPixelPosition) - cameraRelative;

        public static Rectangle Rect => new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint(), playerSize.ToPoint());

        private static Vector2 playerSize;

        private Ludum46 refGame;
        private Texture2D playerStill;

        public PlayerDrawer(Ludum46 game)
        {
            this.refGame = game;

            //Debug
            playerStill = null;
            playerSize = new Vector2(12, 12);

            cameraRelative = new Vector2(Ludum46.UnscaledWidth - playerSize.X, Ludum46.UnscaledHeight - playerSize.Y) / 2;
        }

        internal void Draw(SpriteBatch batch)
        {
            refGame.screenPool.drawShape.Draw(batch, Color.MediumOrchid, PlayerDataManager.unscaledPixelPosition - unscaledCameraDrawOffset, new Vector2(12, 12), Ludum46.Scale);
        }
    }
}
