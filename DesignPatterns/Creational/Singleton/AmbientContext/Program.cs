using System.Text;

namespace AmbientContext
{
    public sealed class BuildingContext: IDisposable
    {
        public int WallHeight;

        private static Stack<BuildingContext> stack =
            new Stack<BuildingContext>();

        static BuildingContext()
        {
            stack.Push(new BuildingContext(0));
        }

        public BuildingContext(int wallHeight)
        {
            WallHeight = wallHeight;
            stack.Push(this);
        }

        public static BuildingContext Current => stack.Peek();

        public void Dispose()   
        {
            if(stack.Count > 1)
            {
                stack.Pop();
            }
        }
    }

    public class Building
    {
        public List<Wall> Walls = new List<Wall>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            foreach(var wall in Walls)
            {
                sb.AppendLine(wall.ToString());
            }
            
            return sb.ToString();
        }
    }

    public struct Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"(x:{x}, y:{y})";
        }
    }

    public class Wall
    {
        public Point Start, End;
        public int Height;

        public Wall(Point start, Point end/*, int height*/)
        {
            Start = start;
            End = end;
            Height = BuildingContext.Current.WallHeight; /*height*/
        }

        public override string ToString()
        {
            return 
                $"Start: {Start}, End: {End} " +
                $"Height: {Height}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ////Ground floor
            ////var height = 3000;
            //BuildingContext.WallHeight = 3000;
            //var house = new Building();
            //house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0), height));
            //house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000), height));

            //// 1at floor 3500
            ////height = 3500;
            //BuildingContext.WallHeight = 3500;
            //house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0), height));
            //house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000), height));

            ////Ground floor
            ////height = 3000;
            //BuildingContext.WallHeight = 3000;
            //house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000), height));


            //Get rid of parameter height:
            //Ground floor
            //var height = 3000;
            //BuildingContext.WallHeight = 3000; //Ambient Context
            var house = new Building();
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

            // 1at floor 3500
            //height = 3500;
            //BuildingContext.WallHeight = 3500;
            house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
            house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

            //Ground floor
            //height = 3000;
            //BuildingContext.WallHeight = 3000;
            house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));

            var house2 = new Building();
            using (new BuildingContext(3090))
            {
                house2.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                house2.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

                using (new BuildingContext(1515))
                {
                    house2.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
                    house2.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
                }

                house2.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));
            }

            Console.WriteLine(house2);
        }
    }
}