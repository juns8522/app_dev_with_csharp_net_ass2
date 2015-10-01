using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class Akira : BaseShip
    {
        private const int SHIELD = 90;
        private const int REGEN = 2;
        private const int HULL = 20;
        private const int WBASE = 8;
        private const int WRAND = 7;

        protected override void initShip()
        {
            shipClass = "Akira";

            shipsHull = new Hull(HULL);
            shipsWeapons = new Weapons(rand, WBASE, WRAND);
            shipShields = new Shield(SHIELD, REGEN);
        }

        public Akira(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {

        }
    }
}
