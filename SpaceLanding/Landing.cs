using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceLanding
{
    class Landing
    {
        public SpaceLand SpaceLand { get; set; }
        public SpaceCraft SpaceCraft { get; set; }

        public double TimePassed { get; set; } = 0.0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">Simulation step in seconds.</param>
        /// <returns>Returns true if spacecraft is above surface, false if it's not.</returns>
        public bool Simulate(double dt)
        {
            //At each step we perform the following:

            //Decrease height according to speed: H = H - V * dt.
            //Determine dM - how much fuel is burned during dt (depending on burning rate and whether we have fuel at all).
            //Calculate speed change due to exhaust: dV = Vexhaust * dM / M.
            //Decrement mass by dM.
            //Calculate gravity acceleration g at the current height as described below.
            //Increase speed according to g and decrease according to dV: V = V + g * dt - dV.
            TimePassed += dt;

            SpaceCraft.High -= SpaceCraft.Velocity * dt;

            if (SpaceCraft.High <= 0)
                return false;

            var dV = 0.0;

            if (SpaceCraft.FuelMass > 0)
            {
                var dM = SpaceCraft.FuelBurningRate * dt;

                if (SpaceCraft.FuelMass < dM)
                    dM = SpaceCraft.FuelMass;

                dV = SpaceCraft.ExhaustSpeed * dM / SpaceCraft.FullMass;
                SpaceCraft.FuelMass -= dM;
            }

            var g = GetGravityChange();

            SpaceCraft.Velocity += g * dt - dV;

            return true;
        }

        public double GetGravityChange()
        {
            return SpaceLand.G0 * Math.Pow(SpaceLand.Radius, 2) / Math.Pow(SpaceLand.Radius + SpaceCraft.High, 2);
        }
    }

    class SpaceCraft
    {
        public string Name { get; set; }
        public double Mass { get; set; } // [kg]
        public double FuelMass { get; set; } // [kg]
        public double FuelBurningRate { get; set; } // [kg/s]
        public double High { get; set; } // [m]
        public double Velocity { get; set; } // [m/s]
        public double ExhaustSpeed { get; set; } // [m/s]

        public double FullMass
        {
            get
            {
                return this.Mass + this.FuelMass;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }


    class SpaceLand
    {
        public string Name { get; set; }
        public double Radius { get; set; } // [m]
        public double G0 { get; set; } // [m/s*s] - gravity acceleration at its surface

        public override string ToString()
        {
            return this.Name;
        }
    }
}
