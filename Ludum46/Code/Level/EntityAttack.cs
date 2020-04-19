using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    public class EntityAttack : Entity
    {
        public enum TargetedTo
        {
            Player,
            Enemy
        }
        public readonly TargetedTo targetedTo;

        /// <summary>
        /// Those die either after timer goes out or collision hits
        /// </summary>
        public float timeToLiveMs;

        /// <summary>
        /// This isn't deleted just yet (to finish animation), but it can't hit anymore.
        /// </summary>
        public bool Used { get; protected set; }

        /// <summary>
        /// This isn't used yet, but it's about to, after one loop of enemy whacking.
        /// </summary>
        public bool SoonToBeUsed { get; protected set; }

        public bool readyToDie => timeToLiveMs < 0;

        public EntityAttack(Ludum46 game, string imgPath, Vector2 coord, float timeToLiveMs, TargetedTo targetedTo, List<Rectangle> rectList)
            : base(game, imgPath, coord, initRectAsImage: false)
        {
            this.timeToLiveMs = timeToLiveMs;
            this.targetedTo = targetedTo;

            this.rectList = rectList;
            this.SoonToBeUsed = false;
            this.Used = false;
        }

        protected override void UpdateCustom(Ludum46 game)
        {
            this.timeToLiveMs -= Ludum46.DeltaUpdate;
        }

        public void MarkAsUsed()
        {
            this.Used = true;
        }

        public void MarkAsSoonToBeUsed()
        {
            this.SoonToBeUsed = true;
        }
    }
}
