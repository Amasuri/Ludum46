﻿using Ludum46.Code.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class EntityStone : Entity
    {
        private Animation stoneAnime;
        private Rectangle relativeRect;

        public EntityStone(Ludum46 game, string imgPath, Vector2 coord, bool initRectAsImage = true)
            : base(game, imgPath, coord, initRectAsImage, Int32.MaxValue)
        {
            this.relativeRect = new Rectangle(0, 0, 18, 25);
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);

            this.stoneAnime = new Animation(game, "res/entity/heart", 18, Ludum46.Scale, 150 );
            this.stoneAnime.EnableDrawing();
        }

        public void TryPush(Vector2 move, Room actionRoom, Ludum46 game)
        {
            if (actionRoom.EntityPositionCollides(new Rectangle((this.unsCoord + move * 5).ToPoint(), this.GetRectList()[0].Size), checkStone: false))
            {
                move = Vector2.Zero;
                return;
            }

            this.unsCoord += move;

            PlayerDataManager.TouchTheStone();
            game.soundPlayer.PlaySound(SoundPlayer.Type.Drag);

            //Updating the rectangle
            this.rectList[0] = new Rectangle(this.unsCoord.ToPoint() + this.relativeRect.Location, this.relativeRect.Size);
        }

        public override void Draw(SpriteBatch batch, Vector2 unsCamera)
        {
            var pos = this.unsCoord - unsCamera;

            stoneAnime.Draw(batch, pos * Ludum46.Scale, SpriteEffects.None, Ludum46.DeltaDraw);

            //Debug
            //base.Draw(batch, unsCamera);
        }
    }
}
