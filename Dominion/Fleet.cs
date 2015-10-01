using System;
using System.Collections.Generic;
//using System.Collections.Generic.List;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dominion
{
    class Fleet
    {
        private string fleetName;
        private List<BaseShip> ships;
        private int numShips;
        private int shipsLost;
        private Random rand;

        private void takeShipDamage(int damage)
        {
            // one of the fleet's ships has been hit.
            // randomly select one and apply damage

            int shipHit = rand.Next(numShips);

            ships[shipHit].takeDamage(damage);
        }

        private void removeShip(int i)
        {
            // move all remaining ships to left, filling up array space taken
            // up by destroyed ship
            // this makes sure the unassigned ship at the end is moved left as well
            // thus keeping the sentinal in place

            for (int j = i; j < numShips; j++)
            {
                ships[j] = ships[j + 1];
            }

            // bookkkeeping on ship numbers
            shipsLost++;
            numShips--;
        }

        private void initFleet()
        {
            fleetName = "";
            ships = null;
            shipsLost = numShips = 0;
        }

        public Fleet(Random r)
        {
            rand = r;
            initFleet();
        }

        private void readShips(StreamReader fin)
        {
            // read number of ships in fleet
            bool result = Int32.TryParse(fin.ReadLine(), out numShips);
            if (result == false || numShips < 1)
            {
                throw new Exception("Invalid number of ships");
            }

            // read in ships
            ships = new List<BaseShip>();
            for (int i = 0; i < numShips; i++)
            {
                Ship ship = new Ship(rand);
                ship.readShip(fin);
                ships.Add(ship);

            }

            // place unassigned ship at end of list of ships to act as sentinal
            ships.Add(new Ship(rand));
        }

        private void readShipsV2(StreamReader fin)
        {
            if (fleetName == null || fleetName.Length == 0)
            {
                throw new Exception("Missing fleet name");
            }

            string shipClass = "";
            int numOfShips = 0;
            ships = new List<BaseShip>();

            createShips(fin, ref shipClass, ref numOfShips);
            addUnassignedShip();
        }

        private void createShips(StreamReader fin, ref string shipClass, ref int numOfShips)
        {
            while (!fin.EndOfStream)
            {
                shipClass = fin.ReadLine();

                if (shipClass == null || shipClass.Length == 0)
                {
                    throw new Exception("Invalid number of ships");
                }

                bool result = Int32.TryParse(fin.ReadLine(), out numOfShips);
                if (result == false || numOfShips < 1)
                {
                    throw new Exception("Invalid number of ships");
                }
                createShip(shipClass, numOfShips);
            }
        }

        private void addUnassignedShip()
        {
            ships.Add(new Ship(rand));
            numShips = ships.Count - 1;
        }

        private void createShip(string shipClass, int numOfShips)
        {
            switch (shipClass)
            {
                case "Defiant":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new Defiant(rand));
                    break;
                case "Akira":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new Akira(rand));
                    break;
                case "Galaxy":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new Galaxy(rand));
                    break;
                case "Bird of Prey":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new BirdOfPrey(rand));
                    break;
                case "Vor'cha":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new Vorcha(rand));
                    break;
                case "Attack Ship":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new AttackShip(rand));
                    break;
                case "Battle Cruiser":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new BattleCruiser(rand));
                    break;
                case "Galor":
                    for (int i = 0; i < numOfShips; i++)
                        ships.Add(new Galor(rand));
                    break;
                default:
                    throw new Exception(shipClass + " is not a valid ship class name");
            }
        }

        private bool endOfFleetFile(StreamReader fin)
        {
            string line;

            while (!fin.EndOfStream)
            {
                line = fin.ReadLine().Trim();
                // see if there is any text other than spaces left in file
                if (line.Length > 0) return false;
            }
            return true;
        }

        public void readFleet(string filename)
        {
            // open stream to fleet file
            if (!File.Exists(filename)) throw new Exception(filename + Environment.NewLine + " does not exist");
            StreamReader fin = new StreamReader(filename);
            if (fin == null) throw new Exception("Unable to open " + filename);

            // read fleet name
            try
            {
                initFleet();

                fleetName = fin.ReadLine();
                if (fleetName == null || fleetName.Length == 0)
                {
                    throw new Exception("Missing fleet name");
                }

                if (fleetName == "#2")
                {
                    fleetName = fin.ReadLine();
                    readShipsV2(fin);
                }
                else
                {
                    readShips(fin);
                }

                if (!endOfFleetFile(fin))
                {
                    throw new Exception("More ships than stated");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " in " + filename);
            }
            finally
            {
                fin.Close();
            }
        }

        public int ShipsDestroyed
        {
            get { return shipsLost; }
        }

        public string FleetName
        {
            get { return fleetName; }
        }

        public void attackFleet(Fleet fleet)
        {
            // go through each ship in the fleet, work out how much damage
            // it dishes out and then get the other fleet to apply it to
            // one of its ships

            int damage;

            for (int i = 0; i < numShips; i++)
            {
                damage = ships[i].weaponDamage();
                fleet.takeShipDamage(damage);
            }
        }

        public string finaliseBattleRound(int round)
        {
            bool shipsLost = false;
            int i = 0;
            string result = "";

            while (true)
            {
                if (ships[i].unassignedShip()) break;

                if (ships[i].shipDestroyed())
                {
                    roundResult(round, ref shipsLost, i, ref result);
                }
                else
                {
                    ships[i].regenShield();
                    i++;
                }
            }
            return result;
        }

        private void roundResult(int round, ref bool shipsLost, int i, ref string result)
        {
            if (shipsLost == false)
            {
                result += Environment.NewLine;
                result += "After round " + round + " the " + fleetName + " fleet has lost";
                result += Environment.NewLine;
                shipsLost = true;
            }
            result += "  " + ships[i].ShipClass + " destroyed";
            result += Environment.NewLine;
            removeShip(i);
        }
        
        public bool fleetDestroyed()
        {
            return numShips == 0;
        }

        public string printDamageReport()
        {
            string damageReport = "";

            damageReport += Environment.NewLine + "  " + shipsLost + " ships lost";
            damageReport += Environment.NewLine + "  " + numShips + " ships survived";
            damageReport += Environment.NewLine;

            for (int i = 0; i < numShips; i++)
            {
                damageReport += "    " + ships[i].ShipClass + " - " + ships[i].damageRating();
                damageReport += Environment.NewLine;
            }
            return damageReport;
        }
    }
}
