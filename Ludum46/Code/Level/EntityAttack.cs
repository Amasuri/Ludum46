using Microsoft.Xna.Framework;
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
        private readonly TargetedTo targetedTo;

        /// <summary>
        /// Those die either after timer goes out or collision hits
        /// </summary>
        public float timeToLiveMs;

        public bool readyToDie => timeToLiveMs < 0;

        public EntityAttack(Ludum46 game, string imgPath, Vector2 coord, float timeToLiveMs, TargetedTo targetedTo, List<Rectangle> rectList)
            : base(game, imgPath, coord)
        {
            this.timeToLiveMs = timeToLiveMs;
            this.targetedTo = targetedTo;
        }

        protected override void UpdateCustom(Ludum46 game)
        {
            this.timeToLiveMs -= Ludum46.DeltaUpdate;
        }
    }
}
