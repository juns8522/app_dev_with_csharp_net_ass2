using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class Defiant : BaseShip
    {
        private const int SHIELD = 60;
        private const int REGEN = 3;
        private const int HULL = 20;
        private const int WBASE = 7;
        private const int WRAND = 3;

        protected override void initShip()
        {
            shipClass = "Defiant";

            shipsHull = new Hull(HULL);
            shipsWeapons = new Weapons(rand, WBASE, WRAND);
            shipShields = new Shield(SHIELD, REGEN);
        }

        public Defiant(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {
            
        }
    }
}
