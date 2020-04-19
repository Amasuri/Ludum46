using Ludum46.Code.Level;
using Microsoft.Xna.Framework;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// Your standard data manager: saving, remembering what have been done, etc.
    /// </summary>
    public static class PlayerDataManager
    {
        static public Vector2 unscaledPixelPosition { get; private set; }
        static public Vector2 unscaledFrameCenterPoint => unscaledPixelPosition + (PlayerDrawer.playerFrame / 2);
        static public Vector2 lastMove { get; private set; }

        static PlayerDataManager()
        {
            unscaledPixelPosition = new Vector2(-10, 0);
        }

        /// <summary>
        /// Is internal because only PlayerController can do this.
        /// </summary>
        static internal void TryMove(Vector2 move, Room actionRoom)
        {
            if (actionRoom.PositionCollides(unscaledPixelPosition + move * 2))
            {
                move = Vector2.Zero;
                return;
            }

            unscaledPixelPosition += move;
            lastMove = move;
        }

        /// <summary>
        /// Mode for the editor
        /// </summary>
        static public void MoveAsMouse(Vector2 move)
        {
            unscaledPixelPosition += move;
        }
    }
}
