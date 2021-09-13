namespace MartianRobots.Application.DTOs
{
    public class RobotDTO
    {
        public string Name { get; set; }
        public GridCoordinatesDTO GridCoordinates { get; set; }
        public PositionDTO Position { get; set; }
        public char Orientation { get; set; }
        public char Instruction { get; set; }
        public string Instructions { get; set; }
        public string Status { get; set; }
    }
}
