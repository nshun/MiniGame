using InoueLab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame
{
    class Enemy2 : Enemy
    {
        public Enemy2(float x, float y) : base(x, y) { }
        
        public override void Update(double rand)
        {
            X += (float)(Math.Sin(rand * 2 * Math.PI) * 3);
            Y += (float)(Math.Cos(rand * 2 * Math.PI) * 3);
        }

        public override void Draw(Graphics graphics)
        {
            if (!IsAlive) return;
            graphics.FillRectangle(Brushes.HotPink, X - 10, Y - 10, 20, 20);
        }
    }
}
