namespace Panda.Models
{
    public class AdDto
    {
        public string Adress { get; set; } = string.Empty;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public int minFloor { get; set; } = 0;
        public int maxFloor { get; set; } = int.MaxValue;
        public int minRooms { get; set; } = 1;
        public int maxRooms { get; set; } = int.MaxValue;
        public bool PetsAllowed { get; set; }
        public bool ChilderAllowed { get; set; }
    }
}
