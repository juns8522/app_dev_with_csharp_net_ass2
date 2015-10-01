using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    enum HullDamagePercent
    {
        VERY_HEAVY_DAMAGE = 0,
        HEAVY_DAMAGE = 40,
        MODERATE_DAMAGE = 70,
        LIGHT_DAMAGE = 90,
        UNDAMAGED = 100
    }

    class Hull
    {
        private int hullStrength = -1;
        private int hullDamage = 0;
        private const int HUNDRED = 100;

        private void initHull()
        {
            hullStrength = -1;
            hullDamage = 0;
        }

        public Hull()
        {
            initHull();
        }

        public Hull(int strength)
        {
            initHull();
            hullStrength = strength;
        }

        public void takeDamage(int damage)
        {
            hullDamage += damage;
        }

        public bool unassigned()
        {
            // we have created a ship object but not assigned it any stats
            return hullStrength < 0;
        }

        public bool destroyed()
        {
            return hullDamage >= hullStrength;
        }

        public string damageRating()
        {
            int undamagedPercent = (hullStrength - hullDamage) * HUNDRED / hullStrength;

            if (undamagedPercent == (int) HullDamagePercent.UNDAMAGED) return "undamaged";
            if (undamagedPercent >= (int) HullDamagePercent.LIGHT_DAMAGE) return "lightly damaged";
            if (undamagedPercent >= (int) HullDamagePercent.MODERATE_DAMAGE) return "moderately damaged";
            if (undamagedPercent >= (int) HullDamagePercent.HEAVY_DAMAGE) return "heavily damaged";
            return "very heavily damaged";
        }

        public void readHull(StreamReader fin)
        {
            initHull();

            string hull = fin.ReadLine();

            bool result = (hull != null && Int32.TryParse(hull, out hullStrength));
            if (result == false || hullStrength < 1)
                throw new Exception("Invalid hull strength");
        }
    }
}
