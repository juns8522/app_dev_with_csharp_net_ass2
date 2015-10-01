using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    abstract class BaseShip
    {
        protected string shipClass;
        protected Random rand;
        protected Hull shipsHull;
        protected Weapons shipsWeapons;
        protected Shield shipShields;

        protected abstract void initShip();
        
        public string ShipClass
        {
            get { return shipClass; }
        }

        public abstract void readShip(StreamReader fin);

        public bool unassignedShip()
        {
            // we have created a ship object but not assigned it any stats
            return shipsHull.unassigned();
        }

        public int weaponDamage()
        {
            return shipsWeapons.getDamage();
        }

        public void takeDamage(int damage)
        {
            // the ships hull takes damage minus any absorbed by the shields
            shipsHull.takeDamage(shipShields.absorbDamage(damage));
        }

        public bool shipDestroyed()
        {
            return (shipsHull.destroyed());
        }

        public void regenShield()
        {
            shipShields.regenShield();
        }

        public string damageRating()
        {
            return shipsHull.damageRating();
        }
    }
}
