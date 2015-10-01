using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class AttackShip : BaseShip
    {
        private const int SHIELD = 50;
        private const int REGEN = 1;
        private const int HULL = 8;
        private const int WBASE = 6;
        private const int WRAND = 3;

        protected override void initShip()
        {
            shipClass = "Attack Ship";

            shipsHull = new Hull(HULL);
            shipsWeapons = new Weapons(rand, WBASE, WRAND);
            shipShields = new Shield(SHIELD, REGEN);
        }

        public AttackShip(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {

        }
    }
}
