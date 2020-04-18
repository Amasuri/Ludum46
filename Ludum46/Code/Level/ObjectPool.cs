using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludum46.Code.Level
{
    /// <summary>
    /// List of all objects available
    /// </summary>
    public class ObjectPool
    {
        public List<Object> pool;

        public ObjectPool(Ludum46 game)
        {
            pool = new List<Object>
            {
                new Object(game, "aaaa", Object.DrawLevel.Floor, id: 0),
            };
        }
    }
}
