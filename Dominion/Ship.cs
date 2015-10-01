using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class Ship : BaseShip
    {
        protected override void initShip()
        {
            shipClass = "";

            shipsHull = new Hull();
            shipsWeapons = new Weapons(rand);
            shipShields = new Shield();
        }

        public Ship(Random r)
        {
            rand = r;
            initShip();
        }

        public override void readShip(StreamReader fin)
        {
            initShip();
            shipClass = fin.ReadLine();
            if (shipClass == null || shipClass.Length == 0)
            {
                throw new Exception("Ship class name missing");
            }

            try
            {
                shipShields.readShield(fin);
                shipsHull.readHull(fin);
                shipsWeapons.readWeapon(fin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " in ship class " + shipClass);
            }
        }
    }
}
