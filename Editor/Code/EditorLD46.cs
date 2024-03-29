﻿using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Editor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EditorLD46 : Ludum46.Code.Ludum46
    {
        private int currentItem = 0;

        private MouseState mouse;
        private MouseState oldMouse;

        private KeyboardState keyboard;
        private KeyboardState oldKeyboard;

        protected override void Update(GameTime gameTime)
        {
            int maxItemRoulette = this.pool.pool.Count - 1;

            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            //Scrolling
            if(mouse.LeftButton == ButtonState.Pressed)
            {
                PlayerDataManager.MoveAsMouse(((oldMouse.Position - mouse.Position).ToVector2() / Ludum46.Code.Ludum46.Scale).ToPoint().ToVector2());
            }

            //Selecting tile
            if(mouse.ScrollWheelValue > oldMouse.ScrollWheelValue)
                currentItem--;
            if (mouse.ScrollWheelValue < oldMouse.ScrollWheelValue)
                currentItem++;

            //Fixing bad selection
            if (currentItem < 0)
                currentItem = maxItemRoulette;
            if (currentItem > maxItemRoulette)
                currentItem = 0;

            //Insertion
            if(mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                this.level.currentRoom.EditorAddObjectAt((this.mouse.Position.ToVector2() / Scale).ToPoint().ToVector2() + PlayerDrawer.unscaledCameraDrawOffset, this.pool.pool[currentItem] );
            }

            //Deletetion
            if (keyboard.IsKeyDown(Keys.D) && oldKeyboard.IsKeyUp(Keys.D))
            {
                this.level.currentRoom.EditorDeleteObjectsAt((this.mouse.Position.ToVector2() / Scale).ToPoint().ToVector2() + PlayerDrawer.unscaledCameraDrawOffset);
            }

            //Saving
            if (keyboard.IsKeyDown(Keys.LeftControl) && keyboard.IsKeyDown(Keys.S) && oldKeyboard.IsKeyUp(Keys.S))
            {
                this.level.currentRoom.EditorSaveRoomData();
            }

            oldMouse = mouse;
            oldKeyboard = keyboard;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(153, 230, 95));
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            this.level.Draw(spriteBatch, this);
            this.level.DrawFront(spriteBatch, this);

            //Draw object at hand
            this.pool.pool[currentItem].Draw(spriteBatch, new Vector2(0, 0), (this.mouse.Position.ToVector2() / Scale).ToPoint().ToVector2());

            spriteBatch.End();
        }
    }
}
