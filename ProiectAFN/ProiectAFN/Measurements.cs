using System;

namespace ProiectAFN
{
    public class Measurements
    {
        public double ConvertCablesToMeters(double cables)
        {
            return cables * 185.2d;
        }

        public double ConvertMetersToCables(double meters)
        {
            return meters * 0.00539957d;
        }

        public double InterpolateAngleFromDistance(string bombType, double distance)
        {
            if (bombType.Contains("1200"))
            {
                return (1.3855375239164 * Math.Pow(10, -13) * Math.Pow(distance, 5)) -
                (5.767824541244 * Math.Pow(10, -10) * Math.Pow(distance, 4)) +
                (9.617365710990 * Math.Pow(10, -7) * Math.Pow(distance, 3)) -
                (0.000786089 * Math.Pow(distance, 2)) +
                (0.332997 * distance - 47.0619);
            } else if (bombType.Contains("2500"))
            {
                return (1.682434515046 * Math.Pow(10, -15) * Math.Pow(distance, 5)) -
                (2.2232170377396 * Math.Pow(10, -12) * Math.Pow(distance, 4)) -
                (3.5042346948847 * Math.Pow(10, -8) * Math.Pow(distance, 3)) +
                (0.000123752 * Math.Pow(distance, 2)) -
                (0.132261 * distance - 56.4);
            } else
            {
                throw new BombNotFoundException();
            }
            
        }

        public double InterpolateTimeFromDistance(string bombType, double distance)
        {
            if (bombType.Contains("1200"))
            {
                return (-8.111183798212 * Math.Pow(10, -14) * Math.Pow(distance, 5)) +
                (3.9292798696923 * Math.Pow(10, -10) * Math.Pow(distance, 4)) -
                (7.261979574818 * Math.Pow(10, -7) * Math.Pow(distance, 3)) +
                (0.000646663 * Math.Pow(distance, 2)) +
                (-0.26819 * distance + 44.2938);
            } else if (bombType.Contains("2500"))
            {
                return (1.2017389393185 * Math.Pow(10, -15) * Math.Pow(distance, 5)) -
                (1.0004476669827 * Math.Pow(10, -11) * Math.Pow(distance, 4)) +
                (3.158746767220 * Math.Pow(10, -8) * Math.Pow(distance, 3)) -
                (0.0000464755 * Math.Pow(distance, 2)) +
                (0.0376802 * distance - 8.5);
            } else
            {
                throw new BombNotFoundException();
            }
            
        }

        public double AngleCorrectionByTemp(double temp)
        {
            double diff = temp - 15;
            return (diff / 10) * 0.1;
        }

        public double AngleCorrectionByPressure(double pressure)
        {
            double diff = pressure - 750;
            return (diff / 10) * 0.1;
        }

        public double AngleCorrectionByWind(double windSpeed)
        {
            return -(windSpeed / 10) * 0.3;
        }

        public double DistanceCorrectionByHeight(string bombType, double distance, double height)
        {
            if (bombType.Contains("1200"))
            {
                if (height == 3)
                {
                    return (-1.4167225943647 * Math.Pow(10, -9) * Math.Pow(distance, 4)) +
                    (5.509917514003 * Math.Pow(10, -6) * Math.Pow(distance, 3)) -
                    (0.00773588 * Math.Pow(distance, 2)) +
                    (4.54104 * distance - 879);
                }
                else if (height == 6)
                {
                    return (-2.868863253588 * Math.Pow(10, -9) * Math.Pow(distance, 4)) +
                    (0.0000114003 * Math.Pow(distance, 3)) -
                    (0.0164181 * Math.Pow(distance, 2)) +
                    (9.96805 * distance - 2050);
                }
                else if (height == 9)
                {
                    return (-3.93140519936 * Math.Pow(10, -9) * Math.Pow(distance, 4)) +
                    (0.0000155458 * Math.Pow(distance, 3)) -
                    (0.0222273 * Math.Pow(distance, 2)) +
                    (13.3383 * distance - 2684);
                } else
                {
                    return (-2.868863253588 * Math.Pow(10, -9) * Math.Pow(distance, 4)) +
                    (0.0000114003 * Math.Pow(distance, 3)) -
                    (0.0164181 * Math.Pow(distance, 2)) +
                    (9.96805 * distance - 2050);
                }
            }
            else if (bombType.Contains("2500"))
            {
                if (height >= 3 && height <= 7)
                {
                    return (-1.593003362892 * Math.Pow(10, -8) * Math.Pow(distance, 3)) +
                    (0.000033841 * Math.Pow(distance, 2)) -
                    (0.0739869 * distance + 252.143);
                }
                else if (height >= 8 && height <= 11)
                {
                    return (-2.904888485273 * Math.Pow(10, -8) * Math.Pow(distance, 3)) +
                    (0.0000702852 * Math.Pow(distance, 2)) -
                    (0.085686 * distance + 222.143);
                } else
                {
                    return (-1.593003362892 * Math.Pow(10, -8) * Math.Pow(distance, 3)) +
                    (0.000033841 * Math.Pow(distance, 2)) -
                    (0.0739869 * distance + 252.143);
                }
            } else
            {
                throw new BombNotFoundException();
            }  
        }
    }
}
