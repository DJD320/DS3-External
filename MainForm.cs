using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;
using static DS3_External.offsetsclass;

namespace DS3_External
{
    public partial class MainForm : Form
    {
        
        
        public bool isAttached = false;
        Mem Memory = new Mem();
        int souls;
        public bool isHPLocked;
        public bool isFPLocked;

        public int flagButton1 = 0;
        

        public MainForm()
        {
            InitializeComponent();
            //slow_MemoryScanner.ProgressChanged += slow_MemoryScanner_ProgressChanged;
        }
        private void siticoneButton1_Click(object sender, EventArgs e) //attach button
        {
            if(flagButton1 == 0)
            {
                if (!Memory.OpenProcess("DarkSoulsIII"))
                {
                    error_DialogLight.Show("Unable to open Dark Souls 3\nmake sure the game is running", "ERROR:");
                }
                else
                {
                    isAttached = true;
                    //siticoneButton1.Visible = false;
                    Console.Beep(500, 300);
                    Tab.Visible = true;
                    siticoneButton1.FillColor = Color.Red;
                    siticoneButton1.Text = "DETACH AND EXIT";
                    flagButton1 = 1;
                    tabPage1.Text = $"STATUS ({Memory.ReadString(__playerName)})";
                    Size = new Size(Width, 553);
                }
            } else if (flagButton1 == 1)
            {
                if (isAttached)
                {
                    Memory.CloseProcess();
                }

                Environment.Exit(0);
            }

        }
        private void siticoneCircleButton1_Click(object sender, EventArgs e) //close button
        {
            
        }
        private void siticoneCheckBox1_CheckedChanged(object sender, EventArgs e) //freeze hp checkbox
        {
           
           if (siticoneCheckBox1.Checked)
           {
                Memory.FreezeValue(__Health, "int", Memory.ReadInt(__Health).ToString());
                isHPLocked = true;
           }
           else
           {
                Memory.UnfreezeValue(__Health);
                isHPLocked = false;
           }
        }
        private void siticoneCheckBox2_CheckedChanged(object sender, EventArgs e) //topmost checkbox
        {
            this.TopMost = siticoneCheckBox2.Checked;
        }
        private void HPTrackBar_Scroll(object sender, ScrollEventArgs e) //HP trackbar
        {
            if (!isHPLocked)
            {
                Memory.WriteMemory(__Health, "int", HPTrackBar.Value.ToString());
            }
            HP_label.Text = "HP SELECTED: "+HPTrackBar.Value.ToString();
            
        }
        private void freezeStaminaCheckBox_CheckedChanged(object sender, EventArgs e) //freeze stamina
        {
            if (freezeStaminaCheckBox.Checked)
            {
                Memory.FreezeValue(__stamina, "int", Memory.ReadInt(__stamina).ToString());
            }
            else
            {
                Memory.UnfreezeValue(__stamina);
            }
        }
        private void VigorLVLTrackBar_Scroll(object sender, ScrollEventArgs e) //vigor lvl 
        {
            Memory.WriteMemory(__vigorLVL, "int", VigorLVLTrackBar.Value.ToString());
            VigorLVL_label.Text = "Vigor: " + VigorLVLTrackBar.Value.ToString();
        }
        private void FPTrackBar_Scroll(object sender, ScrollEventArgs e) //fp trackbar
        {
            labelFP.Text = "FP SELECTED: " + FPTrackBar.Value.ToString();
            if (!isFPLocked)
            {
                Memory.WriteMemory(__fp, "int", FPTrackBar.Value.ToString());
            }
        }
        private void FPCheckBox_CheckedChanged(object sender, EventArgs e) //freeze fp
        {
            if (FPCheckBox.Checked)
            {
                Memory.FreezeValue(__fp, "int", Memory.ReadInt(__fp).ToString());
                isFPLocked = true;
            }
            else
            {
                Memory.UnfreezeValue(__fp);
                isFPLocked = false;
            }
        }
        private void SoulsApplyButton_Click(object sender, EventArgs e) //modify souls button
        {
            try
            {
                souls = Int32.Parse(soulsTextBox.Text);
                Memory.WriteMemory(__souls, "int", souls.ToString());
            } catch 
            {
                MessageBox.Show("Please insert a valid value");            
            }
        }

        private void HP_label_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void attunementTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__attunementLVL, "int", attunementTrackBar.Value.ToString());
            attunementLabel.Text = "Attunement: "+attunementTrackBar.Value.ToString();
        }

        private void enduranceTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__enduranceLVL, "int", enduranceTrackBar.Value.ToString());
            enduranceLabel.Text = "Endurance: " + enduranceTrackBar.Value.ToString();
        }

        private void vitalityTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__vitalityLVL, "int", vitalityTrackBar.Value.ToString());
            vitalityLabel.Text = "Vitality: " + vitalityTrackBar.Value.ToString();
        }

        private void strengthTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__strenghtLVL, "int", strengthTrackBar.Value.ToString());
            strengthLabel.Text = "Strength: " + strengthTrackBar.Value.ToString();
        }

        private void dexterityTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__dexterityLVL, "int", dexterityTrackBar.Value.ToString());
            dexterityLabel.Text = "Dexterity: " + dexterityTrackBar.Value.ToString();
        }

        private void intelligenceTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__intelligenceLVL, "int", intelligenceTrackBar.Value.ToString());
            intLabel.Text = "Intelligence: " + intelligenceTrackBar.Value.ToString();
        }

        private void faithTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__faithLVL, "int", faithTrackBar.Value.ToString());
            faithLabel.Text = "Faith: " + faithTrackBar.Value.ToString();
        }

        private void luckTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__luckLVL, "int", luckTrackBar.Value.ToString());
            luckLabel.Text = "Luck: " + luckTrackBar.Value.ToString();
        }

       
        private void slowMemoryScanner()
        {
            while (true)
            {
                deathCounterLabel.Text = "Deaths: " + Memory.ReadInt(__deathCounter);
                Thread.Sleep(1000);
            }
            
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(Size.Width, 120);
            Control.CheckForIllegalCrossThreadCalls = false;
            await Task.Run(() => {
               slowMemoryScanner();
            });
        }

        private void engineSpeedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (engineSpeedComboBox.Text)
            {
                case "0 (Freeze)":
                    Memory.WriteMemory(__engineSpeed, "float", "0.0");
                    break;
                case "0.25":
                    Memory.WriteMemory(__engineSpeed, "float", "0.25");
                    break;
                case "0.5":
                    Memory.WriteMemory(__engineSpeed, "float", "0.5");
                    break;
                case "0.75":
                    Memory.WriteMemory(__engineSpeed, "float", "0.75");
                    break;
                case "1 (Default)":
                    Memory.WriteMemory(__engineSpeed, "float", "1.0");
                    break;
                case "2":
                    Memory.WriteMemory(__engineSpeed, "float", "2.0");
                    break;
                case "4":
                    Memory.WriteMemory(__engineSpeed, "float", "4.0");
                    break;
                case "8":
                    Memory.WriteMemory(__engineSpeed, "float", "8.0");
                    break;
                case "10":
                    Memory.WriteMemory(__engineSpeed, "float", "10.0");
                    break;
                default:
                    Memory.WriteMemory(__engineSpeed, "float", "1.0");
                    break;
            }
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void slTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
            Memory.WriteMemory(__SL, "int", slTrackBar.Value.ToString());
            label4.Text = "Soul Level: " + slTrackBar.Value.ToString();
        }

        private void siticoneButton2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(@"This tool has been developed and created by smooth#9999 & xor#2407 for Breeze Solutions.

Too fast engine speed, may cause game instability and crashes.
In order to fully mod the max health value, you would need to increase STATS too.
            ");
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            tabPage1.Text = $"STATUS ({Memory.ReadString(__playerName)})";
        }
    }
}