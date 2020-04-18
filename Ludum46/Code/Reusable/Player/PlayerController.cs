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

            if (keyState.IsKeyDown(keyUp))
                PlayerDataManager.TryMove(new Vector2(0, -vertVelocity), game.level.currentRoom);
            else if (keyState.IsKeyDown(keyDown))
                PlayerDataManager.TryMove(new Vector2(0, +vertVelocity), game.level.currentRoom);
            else if (keyState.IsKeyDown(keyLeft))
                PlayerDataManager.TryMove(new Vector2(-horzVelocity, 0), game.level.currentRoom);
            else if (keyState.IsKeyDown(keyRight))
                PlayerDataManager.TryMove(new Vector2(horzVelocity, 0), game.level.currentRoom);

            oldKeyState = keyState;
        }

        static private bool oneKeyPress(Keys key)
        {
            return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
        }
    }
}
