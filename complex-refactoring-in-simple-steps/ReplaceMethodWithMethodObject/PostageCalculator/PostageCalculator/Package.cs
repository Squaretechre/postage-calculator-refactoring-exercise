using System;

namespace PostageCalculator
{
    public abstract class Package
    {
        public abstract decimal PostageInBaseCurrency();

        public static Package WithDimensions(int weight, int height, int width, int depth)
        {
            if (IsSmall(depth, width, height, weight))
            {
                return new SmallPackage();
            }
            if (IsMedium(depth, width, height, weight))
            {
                return new MediumPackage(weight);
            }
            return new LargePackage(weight, height, width, depth);
        }

        private static bool IsMedium(int depth, int width, int height, int weight)
        {
            return weight <= 500 && height <= 324 && width <= 229 && depth <= 100;
        }

        private static bool IsSmall(int depth, int width, int height, int weight)
        {
            return weight <= 60 && height <= 229 && width <= 162 && depth <= 25;
        }
    }

    public class SmallPackage : Package
    {
        public override decimal PostageInBaseCurrency()
        {
            return 120m;
        }
    }

    public class MediumPackage : Package
    {
        public MediumPackage(int weight)
        {
            _weight = weight;
        }

        private readonly int _weight; 

        public override decimal PostageInBaseCurrency()
        {
            return _weight * 4;
        }
    }

    public class LargePackage : Package
    {
        private readonly int _depth;
        private readonly int _width;
        private readonly int _height;
        private readonly int _weight;

        public LargePackage(int weight, int height, int width, int depth)
        {
            _weight = weight;
            _height = height;
            _width = width;
            _depth = depth;
        }

        public override decimal PostageInBaseCurrency()
        {
            return Math.Max(_weight, _height * _width * _depth / 1000m)*6;
        }
    }
}