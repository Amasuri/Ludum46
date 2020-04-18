using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// A class which takes input and produces action based on current screen and state. Must know what's going on in the game.
    ///
    /// No player information is being hold there - go to PlayerData for that.
    /// </summary>
    static public class PlayerController
    {
        private const Keys keyUp = Keys.W;
        private const Keys keyDown = Keys.S;
        private const Keys keyLeft = Keys.A;
        private const Keys keyRight = Keys.D;

        private static KeyboardState keyState;
        private static KeyboardState oldKeyState;

        private const float horzVelocity = 1f;
        private const float vertVelocity = horzVelocity / 2;

        static public void UpdateMovement(Ludum46 game)
        {
            keyState = Keyboard.GetState();

            var move = new Vector2(0, 0);

            if (keyState.IsKeyDown(keyUp))
                move = new Vector2(move.X, -vertVelocity);
            else if (keyState.IsKeyDown(keyDown))
                move = new Vector2(move.X, +vertVelocity);
            if (keyState.IsKeyDown(keyLeft))
                move = new Vector2(-horzVelocity, move.Y);
            else if (keyState.IsKeyDown(keyRight))
                move = new Vector2(+horzVelocity, move.Y);

            PlayerDataManager.TryMove(move, game.level.currentRoom);

            oldKeyState = keyState;
        }

        static private bool oneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
