using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tanks.Models;

namespace Tanks.Controller
{
    class PackmanController
    {
        private int tanksValue;
        private int applesValue;
        private Size mapSize;
        private Kolobok kolobok;
        private Lazer lazer;
        private List<Tank> tanks;
        private List<Apple> apples;
        private List<Wall> walls;
        private List<River> rivers;
        private List<Bullet> bullets;
        private List<Explosion> explosions;
        private readonly Random random = new Random();
        private bool GameOver;
        private readonly Timer shotTimer = new Timer();
        private readonly BindingSource infoSource = new BindingSource();
        private List<Entity> allEntities;


        internal void Initial(int tanksValue, int applesValue, Size mapSize)
        {
            this.tanksValue = tanksValue;
            this.applesValue = applesValue;
            this.mapSize = mapSize;

            kolobok = new Kolobok();

            rivers = new List<River>();
            for (var i = 0; i < 5; i++)
            {
                var river = new River();
                SetRandonCoordinates(river);
                while (IsCollize(river, kolobok))
                {
                    SetRandonCoordinates(river);
                }
                rivers.Add(river);
            }

            walls = new List<Wall>();
            for (var i = 0; i < random.Next(10, 30); i++)
            {
                var wall = new Wall();
                SetRandonCoordinates(wall);
                while (IsCollize(wall, kolobok) ||
                       IsCollize(wall, rivers) ||
                       IsCollize(wall, walls))
                {
                    SetRandonCoordinates(wall);
                }
                walls.Add(wall);
            }

            tanks = new List<Tank>();
            var kolobokSpawnTrigger = new Rectangle(0, 0, 50, 50);
            for (var i = 0; i < tanksValue; i++)
            {
                var tank = new Tank();
                SetRandonCoordinates(tank);
                while (IsCollize(tank, kolobok) ||
                       IsCollize(tank, rivers) ||
                       IsCollize(tank, walls) ||
                       IsCollize(tank, tanks) ||
                       IsCollize(tank, kolobokSpawnTrigger))
                {
                    SetRandonCoordinates(tank);
                }
                tanks.Add(tank);
            }

            apples = new List<Apple>();
            for (var i = 0; i < applesValue; i++)
            {
                var apple = new Apple();
                SetRandonCoordinates(apple);
                while (IsCollize(apple, kolobok) ||
                       IsCollize(apple, rivers) ||
                       IsCollize(apple, walls) ||
                       IsCollize(apple, tanks) ||
                       IsCollize(apple, apples))
                {
                    SetRandonCoordinates(apple);
                }
                apples.Add(apple);
            }

            bullets = new List<Bullet>();
            shotTimer.Interval = 5000 / tanks.Count;
            shotTimer.Tick += delegate (object obj, EventArgs args)
            {
                var randomTankIndex = random.Next(0, tanks.Count);
                var randomTank = tanks[randomTankIndex];
                var bullet = new Bullet(randomTank.Direction);
                var spawnCoordinates = new Point
                {
                    X = randomTank.Coordinates.X + randomTank.Sprite.Width / 2 - bullet.Sprite.Width / 2,
                    Y = randomTank.Coordinates.Y + randomTank.Sprite.Height / 2 - bullet.Sprite.Height / 2
                };
                bullet.Coordinates = spawnCoordinates;
                bullets.Add(bullet);
            };
            shotTimer.Start();

            explosions = new List<Explosion>();

            allEntities = new List<Entity>();
            allEntities.Add(kolobok);
            allEntities.AddRange(tanks);
            allEntities.AddRange(apples);
            allEntities.AddRange(walls);
            allEntities.AddRange(rivers);
            allEntities.AddRange(bullets);
            allEntities.AddRange(explosions);
            UpdateInfoSource();
        }

        internal void Initial()
        {
            Initial(tanksValue, applesValue, mapSize);
        }

        internal void ChangePlayerDirection(Direction direction)
        {
            kolobok.Direction = direction;
        }

        internal void KolobokShot()
        {
            var startX = kolobok.Coordinates.X + kolobok.Sprite.Width / 2;
            var startY = kolobok.Coordinates.Y + kolobok.Sprite.Height / 2;
            var startPoint = new Point(startX, startY);

            var target = SearchLazerTarget(startPoint, kolobok.Direction);

            var endPoint = new Point();

            if (target == null)
            {
                switch (kolobok.Direction)
                {
                    case Direction.Left:
                        endPoint.X = 0;
                        endPoint.Y = kolobok.Coordinates.Y + kolobok.Sprite.Height / 2;
                        break;
                    case Direction.Up:
                        endPoint.X = kolobok.Coordinates.X + kolobok.Sprite.Width / 2;
                        endPoint.Y = 0;
                        break;
                    case Direction.Right:
                        endPoint.X = mapSize.Width;
                        endPoint.Y = kolobok.Coordinates.Y + kolobok.Sprite.Height / 2;
                        break;
                    case Direction.Down:
                        endPoint.X = kolobok.Coordinates.X + kolobok.Sprite.Width / 2;
                        endPoint.Y = mapSize.Height;
                        break;
                }
            }
            else
            {
                endPoint.X = target.Coordinates.X + target.Sprite.Width / 2;
                endPoint.Y = target.Coordinates.Y + target.Sprite.Height / 2;
                explosions.Add(new Explosion(target.Coordinates));
                if (target is Tank)
                {
                    var tank = target as Tank;
                    tanks.Remove(tank);
                }
                if (target is Wall)
                {
                    var wall = target as Wall;
                    walls.Remove(wall);
                }
               
            }

            lazer = new Lazer(startPoint, endPoint);


        }

        internal Bitmap GetNextBitmap(Size mapSize)
        {
            PerformGameLoop();
            if (!GameOver)
            {
                var bitmap = new Bitmap(mapSize.Width, mapSize.Height);
                var graphics = Graphics.FromImage(bitmap);

                foreach (var river in rivers)
                {
                    graphics.DrawImage(river.Sprite, river.Coordinates);
                }
                foreach (var apple in apples)
                {
                    graphics.DrawImage(apple.Sprite, apple.Coordinates);
                }
                if (lazer != null)
                {
                    var lazerColor = lazer.Color;
                    if (lazerColor != Color.Black)
                    {
                        var pen = new Pen(lazerColor, 3);
                        graphics.DrawLine(pen, lazer.StartPoint, lazer.EndPoint);
                    }
                }
                graphics.DrawImage(kolobok.Sprite, kolobok.Coordinates);
                
                foreach (var tank in tanks)
                {
                    graphics.DrawImage(tank.Sprite, tank.Coordinates);
                }
                
                foreach (var wall in walls)
                {
                    graphics.DrawImage(wall.Sprite, wall.Coordinates);
                }
                foreach (var bullet in bullets)
                {
                    graphics.DrawImage(bullet.Sprite, bullet.Coordinates);
                }
                foreach (var explosion in explosions)
                {
                    var sprite = explosion.Sprite;
                    if (sprite != null)
                    {
                        graphics.DrawImage(sprite, explosion.Coordinates);
                    }
                }
                
                
                return bitmap;
            }
            else
            {
                shotTimer.Stop();
                return null;
            }
        }

        private void PerformGameLoop()
        {
            if (!CheckMapBoundary(kolobok))
            {
                kolobok.MakeAStep();
            }

            foreach (var tank in tanks)
            {
                if (!CheckMapBoundary(tank))
                {
                    
                    tank.MakeAStep();
                }
                else
                {
                    tank.SetRandomDirection();
                }
            }
            if (bullets.Count > 0)
            {
                for (var i = 0; i < bullets.Count; i++)
                {
                    if (!CheckMapBoundary(bullets[i]))
                    {
                        bullets[i].MakeAStep();
                    }
                    else
                    {
                        bullets.Remove(bullets[i]);
                        i--;
                    }
                }
            }

            if (explosions.Count > 10)
            {
                explosions.Remove(explosions[0]);
            }

            CheckCollizions();
            UpdateInfoSource();
        }

        private bool CheckMapBoundary(MobileEntity entity)
        {
            if (entity.Coordinates.X == 0 && entity.Direction == Direction.Left)
            {
                return true;
            }
            if (entity.Coordinates.X == mapSize.Width - entity.Sprite.Width &&
                entity.Direction == Direction.Right)
            {
                return true;
            }
            if (entity.Coordinates.Y == 0 && entity.Direction == Direction.Up)
            {
                return true;
            }
            if (entity.Coordinates.Y == mapSize.Height - entity.Sprite.Height &&
                entity.Direction == Direction.Down)
            {
                return true;
            }
            return false;
        }

        private void CheckCollizions()
        {
            foreach (var tank in tanks)
            {
                if (IsCollize(tank, kolobok))
                {
                    GameOver = true;
                }

                foreach (var otherTank in tanks)
                {
                    if (otherTank.Equals(tank))
                    {
                        continue;
                    }
                    if (IsCollize(tank, otherTank))
                    {
                        tank.GoBack();
                        tank.TurnAround();
                        otherTank.GoBack();
                        otherTank.TurnAround();
                    }
                }
                foreach (var wall in walls)
                {
                    if (IsCollize(tank, wall))
                    {
                        tank.GoBack();
                        tank.SetRandomDirection();
                    }
                }
                foreach (var river in rivers)
                {
                    if (IsCollize(tank, river))
                    {
                        tank.GoBack();
                        tank.SetRandomDirection();
                    }
                }
                
            }
            foreach (var wall in walls)
            {
                if (IsCollize(kolobok, wall))
                {
                    kolobok.GoBack();
                }
            }
            foreach (var river in rivers)
            {
                if (IsCollize(kolobok, river))
                {
                    kolobok.GoBack();

                }
            }
            for (var i = 0; i < apples.Count; i++)
            {
                if (i < 0)
                {
                    i = 0;
                }
                if (IsCollize(kolobok, apples[i]))
                {
                    apples.Remove(apples[i]);
                    i--;
                    var apple = new Apple();
                    SetRandonCoordinates(apple);
                    while (IsCollize(apple, kolobok) ||
                       IsCollize(apple, rivers) ||
                       IsCollize(apple, walls) ||
                       IsCollize(apple, tanks) ||
                       IsCollize(apple, apples))
                    {
                        SetRandonCoordinates(apple);
                    }
                    apples.Add(apple);
                    continue;
                }
            }
            for (var i = 0; i < bullets.Count; i++)
            {
                if (i < 0)
                {
                    i = 0;
                }
                if (IsCollize(bullets[i], kolobok))
                {
                    GameOver = true;
                }
                for (var j = 0; j < walls.Count; j++)
                {
                    if (IsCollize(bullets[i], walls[j]))
                    {
                        var explosion = new Explosion(walls[j].Coordinates);
                        explosions.Add(explosion);
                        bullets.Remove(bullets[i]);
                        walls.Remove(walls[j]);
                        i--;
                        break;
                    }
                }
            }
        }

        private static bool IsCollize(Entity entity1, Entity entity2)
        {
            var rectanhle1 = new Rectangle(entity1.Coordinates.X, entity1.Coordinates.Y, entity1.Sprite.Width, entity1.Sprite.Height);
            var rectanhle2 = new Rectangle(entity2.Coordinates.X, entity2.Coordinates.Y, entity2.Sprite.Width, entity2.Sprite.Height);
            return rectanhle1.IntersectsWith(rectanhle2);
        }

        private static bool IsCollize(Entity entity1, Rectangle rectangle2)
        {
            var rectanhle1 = new Rectangle(entity1.Coordinates.X, entity1.Coordinates.Y, entity1.Sprite.Width, entity1.Sprite.Height);
            return rectanhle1.IntersectsWith(rectangle2);
        }

        private static bool IsCollize<T>(Entity entity1, List<T> entityList)
        {
            var rectanhle1 = new Rectangle(entity1.Coordinates.X, entity1.Coordinates.Y, entity1.Sprite.Width, entity1.Sprite.Height);
            foreach (var obj in entityList)
            {
                var entity2 = obj as Entity;
                var rectanhle2 = new Rectangle(entity2.Coordinates.X, entity2.Coordinates.Y, entity2.Sprite.Width, entity2.Sprite.Height);
                if (rectanhle1.IntersectsWith(rectanhle2))
                {
                    return true;
                }
            }
            return false;
        }

        private void SetRandonCoordinates(Entity entity)
        {
            var randomCoordinates = new Point
            {
                X = random.Next(0, mapSize.Width - entity.Sprite.Width),
                Y = random.Next(0, mapSize.Height - entity.Sprite.Height)
            };
            entity.Coordinates = randomCoordinates;
        }

        private static Entity SearchCollizedEntity<T>(List<T> entityList, Rectangle rectangle)
        {

            foreach (var temp in entityList)
            {
                var entity = temp as Entity;
                if (IsCollize(entity, rectangle))
                {
                    return entity;
                }
            }
            return null;
        }
        private Entity SearchLazerTarget(Point startPoint, Direction direction)
        {
            Entity result = null;
            var pointer = new Rectangle(startPoint, new Size(1, 1));


            switch (direction)
            {
                case Direction.Left:
                    while (pointer.X > 0)
                    {
                        pointer.X--;
                        result = SearchCollizedEntity(tanks, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                        result = SearchCollizedEntity(walls, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    break;
                case Direction.Up:
                    while (pointer.Y > 0)
                    {
                        pointer.Y--;
                        result = SearchCollizedEntity(tanks, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                        result = SearchCollizedEntity(walls, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    break;
                case Direction.Right:
                    while (pointer.X < mapSize.Width)
                    {
                        pointer.X++;
                        result = SearchCollizedEntity(tanks, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                        result = SearchCollizedEntity(walls, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    break;
                case Direction.Down:
                    while (pointer.Y < mapSize.Height)
                    {
                        pointer.Y++;
                        result = SearchCollizedEntity(tanks, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                        result = SearchCollizedEntity(walls, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    break;
            }

            return result;
        }

        internal void Reset()
        {
            kolobok = null;
            lazer = null;
            tanks = null;
            apples = null;
            walls = null;
            rivers = null;
            bullets = null;
            explosions = null;
            GameOver = false;
        }

        public BindingSource GetInformationSource()
        {
            return infoSource;
        }

        private void UpdateInfoSource()
        {
            var infoList = new List<InfoItem>();
            foreach (var entity in allEntities)
            {
                infoList.Add(new InfoItem(entity));
            }
            infoSource.DataSource = infoList;
        }
    }
}
