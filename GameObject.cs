using System;
using System.Drawing;

namespace MiniGame
{
    class GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Size { get; set; }
        public bool IsAlive { get; private set; }

        public GameObject(float x, float y)
        {
            X = x;
            Y = y;
            Size = 20;
            IsAlive = true;
        }

        public void Die()
        {
            IsAlive = false;
        }

        public bool Intersect(GameObject obj)
        {
            if (Math.Abs(this.X - obj.X) <= (this.Size + obj.Size)/2 && Math.Abs(this.Y - obj.Y) <= (this.Size + obj.Size)/2) return true;
            return false;
        }

        public virtual void Draw(Graphics graphics)
        {
            if (!IsAlive) return;
            graphics.FillRectangle(Brushes.Red, X - Size / 2, Y - Size / 2, Size, Size);
        }
    }
}