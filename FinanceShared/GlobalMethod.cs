using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceShared
{
    public class GlobalMethod
    {
        /// <summary>
        ///  Create decimal Random number
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static decimal GetRandom(decimal min, decimal max, int steps = 10000)
        {
            Random rand = new Random();

            if (steps <= 0) throw new ApplicationException("steps must be >= 1");

            int r = rand.Next(0, steps);
            return ((steps - r) * min + r * max) / steps;
        }

    }

}
