using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Show
{
    class MathHelper
    {
        public static double Lerp(double a, double b, double alpha)
        {
            return a * (1.0 - alpha) + b * (alpha);
        }

        public static double GetAngleFromCos(double _cos)
        {
            return Math.Acos(_cos) / (2.0 * Math.PI) * 360.0;
        }

        public static double GetVectorLength(PointF _vector)
        {
            return Math.Sqrt(GetVectorLengthSqr(_vector));
        }
        public static double GetVectorLengthSqr(PointF _vector)
        {
            return _vector.X * _vector.X + _vector.Y * _vector.Y;
        }

        public static double GetAngleOfTwoVector(PointF _V1, PointF _V2)
        {
            return GetAngleFromCos(
                (_V1.X * _V2.X + _V1.Y * _V2.Y) / (GetVectorLength(_V1) * GetVectorLength(_V2))
                );
        }

        public static double[] GetAngleOfTrigle(double _edgeLength0, double _edgeLength1, double _edgeLength2)
        {
            double[] returned = new double[3];

            returned[0] = GetAngleFromCos(
                (_edgeLength1 * _edgeLength1 + _edgeLength2 * _edgeLength2 - _edgeLength0 * _edgeLength0) / (2.0 * _edgeLength1 * _edgeLength2)
                );

            returned[1] = GetAngleFromCos(
                (_edgeLength0 * _edgeLength0 + _edgeLength2 * _edgeLength2 - _edgeLength1 * _edgeLength1) / (2.0 * _edgeLength0 * _edgeLength2)
                );

            returned[2] = GetAngleFromCos(
                (_edgeLength0 * _edgeLength0 + _edgeLength1 * _edgeLength1 - _edgeLength2 * _edgeLength2) / (2.0 * _edgeLength0 * _edgeLength1)
                );

            return returned;
        }
    }
}
