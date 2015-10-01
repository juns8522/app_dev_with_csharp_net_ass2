using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_fleet1_Click_1(object sender, EventArgs e)
        {
            string fileName = getFileName();
            textBox_fleet1.Text = fileName;
        }

        private void button_fleet2_Click_1(object sender, EventArgs e)
        {
            string fileName = getFileName();
            textBox_fleet2.Text = fileName;

        }

        private string getFileName()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return null;
        }

        private void button_run_Click(object sender, EventArgs e)
        {
            Fleet fleet1 = null;
            Fleet fleet2 = null;
            int round = 0;
            //string result = "";

            try
            {
                process(out fleet1, out fleet2);
                runBattle(fleet1, fleet2, ref round);
                printBattleReport(fleet1, fleet2, round);
            }
            catch (Exception ex)
            {
                textBox_result.Clear();

                string message = "Program failed with following error\n";
                message += ex.Message;

                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void process(out Fleet fleet1, out Fleet fleet2)
        {
            int seed;
            bool result = Int32.TryParse(textBox_seed.Text, out seed);
            //if (result == false || seed < 0) throw new Exception("Invalid seed value entered");
            Random rand = new Random(seed);

            if (textBox_fleet1.Text == "")
                throw new Exception("No file entered for fleet 1");
            
            fleet1 = new Fleet(rand);
            fleet1.readFleet(textBox_fleet1.Text);

            if (textBox_fleet2.Text == "")
                throw new Exception("No file entered for fleet 2");

            fleet2 = new Fleet(rand);
            fleet2.readFleet(textBox_fleet2.Text);

            if (result == false || seed < 0) throw new Exception("Invalid seed value entered");
        }

        private void runBattle(Fleet fleet1, Fleet fleet2, ref int round)
        {
            textBox_result.Clear();

            while (!fleet1.fleetDestroyed() && !fleet2.fleetDestroyed())
            {
                round++;

                // run fleet battles
                fleet1.attackFleet(fleet2);
                fleet2.attackFleet(fleet1);

                // finalise battle round
                textBox_result.AppendText(fleet1.finaliseBattleRound(round));
                textBox_result.AppendText(fleet2.finaliseBattleRound(round));
            } 
        }

        private void printBattleReport(Fleet fleet1, Fleet fleet2, int round)
        {
            if (fleet1.fleetDestroyed() && fleet2.fleetDestroyed())
            {
                textBox_result.AppendText(Environment.NewLine); 
                textBox_result.AppendText("After round " + round + " the battle has been a draw with both sides destroyed");
            }
            else if (fleet1.fleetDestroyed())
            {
                textBox_result.AppendText(Environment.NewLine);
                textBox_result.AppendText("After round " + round + " the " + fleet2.FleetName + " fleet won");
                textBox_result.AppendText(Environment.NewLine);
                textBox_result.AppendText("  " + fleet1.ShipsDestroyed + " enemy ships destroyed");
                textBox_result.AppendText(fleet2.printDamageReport());
            }
            else if (fleet2.fleetDestroyed())
            {
                textBox_result.AppendText(Environment.NewLine);
                textBox_result.AppendText("After round " + round + " the " + fleet1.FleetName + " fleet won");
                textBox_result.AppendText(Environment.NewLine); 
                textBox_result.AppendText("  " + fleet2.ShipsDestroyed + " enemy ships destroyed");
                textBox_result.AppendText(fleet1.printDamageReport());
            }
            else
            {
                textBox_result.AppendText("ERROR BUG - battle ended but neither fleet is destroyed");
            }
            //textBox_result.Text = result;
            //textBox_result.ap
        }
    }
}
