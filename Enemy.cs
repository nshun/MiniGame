using InoueLab;
using System;
using System.Drawing;

namespace MiniGame
{
    class Enemy : GameObject
    {
        public Enemy(float x, float y) : base(x, y) { }

        public virtual void Update(double rand)
        {
            Y += 1;
        }

        public override void Draw(Graphics graphics)
        {
            if (!IsAlive) return;
            graphics.FillRectangle(Brushes.Blue, X - 10, Y - 10, 20, 20);
        }
    }
}