using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
        private readonly Random random;
        private bool gameOver;
        private readonly Timer shotTimer;
        private readonly DataTable infoSource;
        private int tanksScore;
        private int applesScore;
        private bool gameResult;
        private int gameOverDelay;

        public PackmanController()
        {
            shotTimer = new Timer();
            random = new Random();
            infoSource = new DataTable();

            var IdColumn = new DataColumn();
            IdColumn.DataType = typeof(int);
            IdColumn.ColumnName = "Id";
            infoSource.Columns.Add(IdColumn);

            var NameColumn = new DataColumn();
            NameColumn.DataType = typeof(string);
            NameColumn.ColumnName = "Name";
            infoSource.Columns.Add(NameColumn);

            var XColumn = new DataColumn();
            XColumn.DataType = typeof(int);
            XColumn.ColumnName = "X";
            infoSource.Columns.Add(XColumn);

            var YColumn = new DataColumn();
            YColumn.DataType = typeof(int);
            YColumn.ColumnName = "Y";
            infoSource.Columns.Add(YColumn);
        }


        internal bool GetGameResult()
        {
            return gameResult;
        }

        internal void Initial(int tanksValue, int applesValue, Size mapSize)
        {
            infoSource.Rows.Clear();

            gameOver = false;
            gameOverDelay = 0;

            tanksScore = 0;
            applesScore = 0;

            this.tanksValue = tanksValue;
            this.applesValue = applesValue;
            this.mapSize = mapSize;

            kolobok = new Kolobok();
            AddToInfoSource(kolobok);

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
                AddToInfoSource(river);

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
                AddToInfoSource(wall);
            }

            tanks = new List<Tank>();
            var kolobokSpawnAreaTrigger = new Rectangle(0, 0, 50, 50);
            for (var i = 0; i < tanksValue; i++)
            {
                var tank = new Tank();
                SetRandonCoordinates(tank);
                while (IsCollize(tank, kolobok) ||
                       IsCollize(tank, rivers) ||
                       IsCollize(tank, walls) ||
                       IsCollize(tank, tanks) ||
                       IsCollize(tank, kolobokSpawnAreaTrigger))
                {
                    SetRandonCoordinates(tank);
                }
                tanks.Add(tank);
                AddToInfoSource(tank);
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
                AddToInfoSource(apple);
            }

            bullets = new List<Bullet>();
            explosions = new List<Explosion>();

            UpdateInfoSource();
        }

        internal void Initial()
        {
            Initial(tanksValue, applesValue, mapSize);
        }

        internal void ChangePlayerDirection(Direction direction)
        {
            kolobok.ChangeDirection(direction);
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
                var explosion = new Explosion(target.Coordinates);
                explosions.Add(explosion);
                AddToInfoSource(explosion);
                if (target is Tank)
                {
                    tanksScore++;
                    var tank = target as Tank;
                    RemoveFromInfoSource(tank.Id);
                    tanks.Remove(tank);
                }
                if (target is Wall)
                {
                    var wall = target as Wall;
                    RemoveFromInfoSource(wall.Id);
                    walls.Remove(wall);

                }
               
            }

            lazer = new Lazer(startPoint, endPoint);


        }

        internal Bitmap GetNextBitmap(Size mapSize)
        {
            PerformGameLoop();
            if (!gameOver || gameOverDelay < 10)
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
                if (kolobok != null)
                {
                    graphics.DrawImage(kolobok.Sprite, kolobok.Coordinates);
                }
                
                if (tanks.Count > 0)
                {
                    foreach (var tank in tanks)
                    {
                        graphics.DrawImage(tank.Sprite, tank.Coordinates);
                    }
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
            if (gameOver)
            {
                gameOverDelay++;
                return;
            }
            if (tanks.Count == 0)
            {
                gameResult = true;
                gameOver = true;
                gameOverDelay++;
                return;
            }
            if (!CheckMapBoundary(kolobok))
            {
                kolobok.MakeAStep();
            }
            else
            {
                kolobok.GoBack();
                kolobok.TurnAround();
            }

            foreach (var tank in tanks)
            {
                if (tank.ReadyToShot)
                {
                    var bullet = new Bullet(tank.Direction);
                    var spawnCoordinates = new Point
                    {
                        X = tank.Coordinates.X + tank.Sprite.Width / 2 - bullet.Sprite.Width / 2,
                        Y = tank.Coordinates.Y + tank.Sprite.Height / 2 - bullet.Sprite.Height / 2
                    };
                    bullet.Coordinates = spawnCoordinates;
                    bullets.Add(bullet);
                    AddToInfoSource(bullet);
                }

                if (!CheckMapBoundary(tank))
                {
                    
                    tank.MakeAStep();
                }
                else
                {
                    tank.GoBack();
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
                        RemoveFromInfoSource(bullets[i].Id);
                        bullets.Remove(bullets[i]);
                        i--;
                    }
                }
            }

            if (explosions.Count > 5)
            {
                RemoveFromInfoSource(explosions[0].Id);
                explosions.Remove(explosions[0]);
            }

            CheckCollizions();
            UpdateInfoSource();
        }

        private bool CheckMapBoundary(MobileEntity entity)
        {
            if (entity.Coordinates.X < 0 && entity.Direction == Direction.Left)
            {
                return true;
            }
            if (entity.Coordinates.X > mapSize.Width - entity.Sprite.Width &&
                entity.Direction == Direction.Right)
            {
                return true;
            }
            if (entity.Coordinates.Y < 0 && entity.Direction == Direction.Up)
            {
                return true;
            }
            if (entity.Coordinates.Y > mapSize.Height - entity.Sprite.Height &&
                entity.Direction == Direction.Down)
            {
                return true;
            }
            return false;
        }

        private void CheckCollizions()
        {
            if (gameOver)
            {
                return;
            }

            foreach (var tank in tanks)
            {
                if (IsCollize(tank, kolobok))
                {
                    var explosion = new Explosion(kolobok.Coordinates);
                    explosions.Add(explosion);
                    AddToInfoSource(explosion);
                    RemoveFromInfoSource(kolobok.Id);
                    kolobok = null;
                    gameOver = true;
                    gameResult = false;
                    return;
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

                    if (IsCollize(kolobok, wall))
                    {
                        kolobok.GoBack();
                        kolobok.TurnAround();
                    }
                }
                foreach (var river in rivers)
                {
                    if (IsCollize(tank, river))
                    {
                        tank.GoBack();
                        tank.SetRandomDirection();
                    }
                    if (IsCollize(kolobok, river))
                    {
                        kolobok.GoBack();
                        kolobok.TurnAround();
                    }
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
                    applesScore++;
                    RemoveFromInfoSource(apples[i].Id);
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
                    AddToInfoSource(apple);
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
                    var explosion = new Explosion(kolobok.Coordinates);
                    explosions.Add(explosion);
                    AddToInfoSource(explosion);
                    RemoveFromInfoSource(kolobok.Id);
                    kolobok = null;
                    RemoveFromInfoSource(bullets[i].Id);
                    bullets.Remove(bullets[i]);
                    gameResult = false;
                    gameOver = true;
                    return;
                }
                for (var j = 0; j < walls.Count; j++)
                {
                    if (IsCollize(bullets[i], walls[j]))
                    {
                        var explosion = new Explosion(walls[j].Coordinates);
                        explosions.Add(explosion);
                        AddToInfoSource(explosion);
                        RemoveFromInfoSource(bullets[i].Id);
                        bullets.Remove(bullets[i]);
                        RemoveFromInfoSource(walls[j].Id);
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
            randomCoordinates.X -= randomCoordinates.X % 15;
            randomCoordinates.Y -= randomCoordinates.Y % 15;
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
            var potentialTargets = new List<Entity>();
            potentialTargets.AddRange(tanks);
            potentialTargets.AddRange(walls);

            switch (direction)
            {
                case Direction.Left:
                    while (pointer.X > 0)
                    {
                        pointer.X--;
                        result = SearchCollizedEntity(potentialTargets, pointer);
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
                        result = SearchCollizedEntity(potentialTargets, pointer);
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
                        result = SearchCollizedEntity(potentialTargets, pointer);
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
                        result = SearchCollizedEntity(potentialTargets, pointer);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    break;
            }

            return result;
        }

        public DataTable GetInformationSource()
        {
            return infoSource;
        }

        private void UpdateInfoSource()
        {
            UpdateInfoSource(kolobok);
            UpdateInfoSource(tanks);
            UpdateInfoSource(bullets);

        }

        private void UpdateInfoSource(Entity entity)
        {
            if (entity == null)
            {
                return;
            }
            var row = infoSource.Select($"Id = {entity.Id}").FirstOrDefault();
            row["X"] = ((Entity)entity).Coordinates.X;
            row["Y"] = ((Entity)entity).Coordinates.Y;
        }

        private void UpdateInfoSource<T>(List<T> list)
        {
            foreach (var entity in list)
            {
                UpdateInfoSource(entity as Entity);
            }
        }

        private void AddToInfoSource(Entity entity)
        {
            var row = infoSource.NewRow();
            row["Id"] = entity.Id;
            row["Name"] = entity.GetType().Name;
            row["X"] = entity.Coordinates.X;
            row["Y"] = entity.Coordinates.Y;

            infoSource.Rows.Add(row);
        }

        private void RemoveFromInfoSource(int id)
        {
            var row = infoSource.Select($"Id = {id}")[0];
            infoSource.Rows.Remove(row);
        }

        public string GetScores()
        {
            return $"tanks: {tanksScore}    apples: {applesScore}";
        }
    }
}
