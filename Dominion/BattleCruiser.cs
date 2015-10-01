using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class BattleCruiser : BaseShip
    {
        private const int SHIELD = 90;
        private const int REGEN = 2;
        private const int HULL = 25;
        private const int WBASE = 13;
        private const int WRAND = 5;

        protected override void initShip()
        {
            shipClass = "Battle Cruiser";

            shipsHull = new Hull(HULL);
            shipsWeapons = new Weapons(rand, WBASE, WRAND);
            shipShields = new Shield(SHIELD, REGEN);
        }

        public BattleCruiser(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {

        }
    }
}
