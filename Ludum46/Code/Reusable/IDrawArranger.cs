using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ludum46.Code.Reusable
{
    /// <summary>
    /// Doesn't draw anything on it's own, but calls scene drawers based on logic.
    /// Same with interface input handling.
    /// </summary>
    public interface IDrawArranger
    {
        void CallDraws(Ludum46 game, SpriteBatch defaultSpriteBatch, GraphicsDevice graphicsDevice);

        void CallGuiControlUpdates(Ludum46 game);
    }
}
