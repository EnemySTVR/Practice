namespace Tanks.Models
{
    class InfoItem
    {
        public string Name { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public InfoItem(Entity entity)
        {
            Name = entity.GetType().Name;
            XCoord = entity.Coordinates.X;
            YCoord = entity.Coordinates.Y;
        }

    }
}
