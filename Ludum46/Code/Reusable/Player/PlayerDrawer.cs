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

        public static Rectangle Rect => new Rectangle(PlayerDataManager.unscaledPixelPosition.ToPoint(), playerFrame.ToPoint());

        private static Vector2 playerFrame;

        private Ludum46 refGame;

        private Texture2D playerStill;
        private Animation sheetLeft;
        private Animation sheetRight;
        private Animation sheetUp;
        private Animation sheetDown;
        private Animation sheetUpLeft;
        private Animation sheetUpRight;
        private Animation sheetDownLeft;
        private Animation sheetDownRight;

        public PlayerDrawer(Ludum46 game)
        {
            this.refGame = game;

            playerFrame = new Vector2(24, 24);
            cameraRelative = new Vector2(Ludum46.UnscaledWidth - playerFrame.X, Ludum46.UnscaledHeight - playerFrame.Y) / 2;

            int frameMs = 150;
            sheetLeft = new Animation(game, "res/entity/player/playerLeft", (int)playerFrame.X, frameMs);
            sheetRight = new Animation(game, "res/entity/player/playerRight", (int)playerFrame.X, frameMs);
            sheetUp = new Animation(game, "res/entity/player/playerUp", (int)playerFrame.X, frameMs);
            sheetDown = new Animation(game, "res/entity/player/playerDown", (int)playerFrame.X, frameMs);
            sheetUpLeft = new Animation(game, "res/entity/player/playerUpLeft", (int)playerFrame.X, frameMs);
            sheetUpRight = new Animation(game, "res/entity/player/playerUpRight", (int)playerFrame.X, frameMs);
            sheetDownLeft = new Animation(game, "res/entity/player/playerDownLeft", (int)playerFrame.X, frameMs);
            sheetDownRight = new Animation(game, "res/entity/player/playerDownRight", (int)playerFrame.X, frameMs);
        }

        public void Draw(SpriteBatch batch)
        {
            refGame.screenPool.drawShape.Draw(batch, Color.MediumOrchid, PlayerDataManager.unscaledPixelPosition - unscaledCameraDrawOffset, playerFrame, Ludum46.Scale);
        }
    }
}
