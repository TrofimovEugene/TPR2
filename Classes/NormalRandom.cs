﻿using System;

namespace TPR2
{
    // класс для задания случайных чисел по нормальному закону
    public class NormalRandom: Random
    {
        double _prevSample = double.NaN;
        protected override double Sample()
        {
            if (!double.IsNaN(_prevSample))
            {
                double result = _prevSample;
                _prevSample = double.NaN;
                return result;
            }

            double u, v, s;
            do
            {
                u = 2 * base.Sample() - 1;
                v = 2 * base.Sample() - 1;
                s = u * u + v * v;
            } while (u <= -1 || v <= -1 || s >= 1 || s == 0);
            double r = Math.Sqrt(-2 * Math.Log(s) / s);
            _prevSample = r * v;
            return r * u;
        }
    }
}
