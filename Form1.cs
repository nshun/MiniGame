using InoueLab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MiniGame
{
    public partial class Form1 : Form
    {
        private readonly Graphics _graphics;
        private readonly HashSet<Keys> _pressedKeys;
        private GameObject _player;
        private List<Enemy2> _enemies;
        private List<Bullet> _bullets;
        private RandomMT _rand;
        private bool _createBullet;
        private bool _enter;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            BackgroundImage = new Bitmap(1280, 720);
            _graphics = Graphics.FromImage(BackgroundImage);
            _pressedKeys = new HashSet<Keys>();
            timer1.Interval = 10;
            timer1.Start();

            _initialize();
        }

        /// <summary>
        /// ステージ初期化の処理を書く。
        /// </summary>
        private void _initialize()
        {

            _rand = new RandomMT();
            _player = new GameObject(ClientSize.Width / 2, ClientSize.Height * 3 / 4);
            _enemies = new List<Enemy2>();
            _bullets = new List<Bullet>();
            _createBullet = false;
            _enter = false;
            for (var i = 0; i < 20; i++)
            {
                var newEnemy = new Enemy2(_rand.Int(ClientSize.Width - 20), _rand.Int(ClientSize.Height / 2 - 20));
                _enemies.Add(newEnemy);
            }
        }

        /// <summary>
        /// フレームごとに実行する処理(ロジック部分)を書く。
        /// </summary>
        private void _update()
        {
            // エネミーの移動
            foreach (var enemy in _enemies)
            {
                if (!enemy.IsAlive) continue;

                enemy.Update(_rand.Double());
                if (enemy.Intersect(_player))
                {
                    enemy.Die();
                }
                foreach (var bullet in _bullets)
                {
                    if (enemy.Intersect(bullet))
                    {
                        enemy.Die();
                        bullet.Die();
                    }
                }
            }

            // 弾の移動
            foreach (var bullet in _bullets)
            {
                bullet.Update();
            }

            // プレイヤーの移動
            foreach (var key in _pressedKeys)
            {
                switch (key)
                {
                    case Keys.Right:
                        _player.X++;
                        break;

                    case Keys.Left:
                        _player.X--;
                        break;

                    case Keys.Up:
                        _player.Y--;
                        break;

                    case Keys.Down:
                        _player.Y++;
                        break;

                    case Keys.Enter:
                        if (!_createBullet)
                        {
                            _bullets.Add(new Bullet(_player.X, _player.Y, -90 * Math.PI / 180));
                            _createBullet = true;
                        }

                        break;

                    default:

                        break;
                }
            }
        }

        /// <summary>
        /// フレームごとに実行する処理(描画部分)を書く。
        /// </summary>
        private void _draw()
        {
            _graphics.Clear(Color.White);
            _player.Draw(_graphics);
            foreach (var enemy in _enemies)
            {
                enemy.Draw(_graphics);
            }
            foreach (var bullet in _bullets)
            {
                bullet.Draw(_graphics);
            }
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _update();
            _draw();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _pressedKeys.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            _pressedKeys.Remove(e.KeyCode);
            if (e.KeyCode == Keys.Enter) _createBullet = false;
        }
    }
}