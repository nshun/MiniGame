using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame
{
    class Bullet : GameObject
    {
        private double direction;
        public Bullet(float x, float y, double dir) : base(x, y)
        {
            direction = dir;
            Size = 5;
        }


        public virtual void Update()
        {
            X += (float)(Math.Cos(direction));
            Y += (float)(Math.Sin(direction));
        }

    }
}
