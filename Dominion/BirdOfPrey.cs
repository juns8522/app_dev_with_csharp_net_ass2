using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class BirdOfPrey : BaseShip
    {
        private const int SHIELD = 60;
        private const int REGEN = 1;
        private const int HULL = 10;
        private const int WBASE = 6;
        private const int WRAND = 3;

        protected override void initShip()
        {
            shipClass = "Bird of Prey";

            shipsHull = new Hull(HULL);
            shipsWeapons = new Weapons(rand, WBASE, WRAND);
            shipShields = new Shield(SHIELD, REGEN);
        }

        public BirdOfPrey(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {

        }
    }
}
