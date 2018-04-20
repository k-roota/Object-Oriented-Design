using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots
{
    abstract class RobotDecorator : PaintingRobot
    {
        public string DescriptionTemplate(string name, PaintingRobot r2)
        {
            string result = r2.ToString();
            result = result.Replace("\n", "\n\t");
            return $"{name} {this.GetPaintedLength(1)}\n(\n\t{result}\n)";
        }

        public string DescriptionTemplate(string name, List<PaintingRobot> robots)
        {
            string result = "";
            foreach (var robot in robots)
            {
                result += "\t" + robot.ToString().Replace("\n", "\n\t") + "\n";
            }
            return $"{name} {this.GetPaintedLength(1)}\n(\n{result})";
        }
    }

    class RobotDecoratorAdd : RobotDecorator
    {
        private double n;
        private PaintingRobot robot;

        public RobotDecoratorAdd(PaintingRobot robot, double n)
        {
            this.robot = robot;
            this.n = n;
        }

        public override double GetPaintedLength(double time)
        {
            return robot.GetPaintedLength(time) + n;
        }

        public override string ToString()
        {
            return this.DescriptionTemplate("adding robot", robot);
        }
    }

    class RobotDecoratorMultiply : RobotDecorator
    {
        private double n;
        private PaintingRobot robot;

        public RobotDecoratorMultiply(PaintingRobot robot, double n)
        {
            this.robot = robot;
            this.n = n;
        }

        public override double GetPaintedLength(double time)
        {
            return robot.GetPaintedLength(time) * n;
        }

        public override string ToString()
        {
            return this.DescriptionTemplate("multiplying robot", robot);
        }
    }

    class RobotDecoratorNlogN : RobotDecorator
    {
        private PaintingRobot robot;

        public RobotDecoratorNlogN(PaintingRobot robot)
        {
            this.robot = robot;
        }

        public override double GetPaintedLength(double time)
        {
            double n = robot.GetPaintedLength(time);
            return n * Math.Log(n);
        }

        public override string ToString()
        {
            return this.DescriptionTemplate("NlogN robot", robot);
        }
    }

    class RobotDecoratorMax : RobotDecorator
    {
        private List<PaintingRobot> robots;

        public RobotDecoratorMax(List<PaintingRobot> robots)
        {
            this.robots = robots;
        }

        public override double GetPaintedLength(double time)
        {
            double max = 0;
            foreach(var robot in robots)
            {
                if(robot.GetPaintedLength(time) > max)
                {
                    max = robot.GetPaintedLength(time);
                }
            }
            return max;
        }

        public override string ToString()
        {
            return this.DescriptionTemplate("max robot", robots);
        }
    }

    class RobotDecoratorSum : RobotDecorator
    {
        private List<PaintingRobot> robots;

        public RobotDecoratorSum(List<PaintingRobot> robots)
        {
            this.robots = robots;
        }

        public override double GetPaintedLength(double time)
        {
            double sum = 0;
            foreach (var robot in robots)
            {
                sum += robot.GetPaintedLength(time);
            }
            return sum;
        }

        public override string ToString()
        {
            return this.DescriptionTemplate("sum robot", robots);
        }
    }

    class LazyRobot : PaintingRobot
    {
        double n;

        public LazyRobot(double n)
        {
            this.n = n;
        }

        public override double GetPaintedLength(double time)
        {
            return n * Math.Log(time);
        }

        public override string ToString()
        {
            return "lazy robot";
        }
    }
}
