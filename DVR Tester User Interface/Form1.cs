using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DVR_Tester_User_Interface
{

    public partial class Form1 : Form
    {
            Test_Paramater[] Audio_Chnls = new Test_Paramater[16];
            Test_Paramater[] Video_Chnls = new Test_Paramater[16];
            Test_Paramater[] Analog_Inpts = new Test_Paramater[6];
            Test_Paramater[] Digital_Inpts = new Test_Paramater[16];
            Test_Paramater[] Relay_Outpts = new Test_Paramater[16];
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 16; ++i)
            {
                Audio_Chnls[i] = new Test_Paramater();
                Video_Chnls[i] = new Test_Paramater();
                Digital_Inpts[i] = new Test_Paramater();
                Relay_Outpts[i] = new Test_Paramater();
            }
            for( int i = 0; i < 6; ++i)
            {   Analog_Inpts[i] = new Test_Paramater();}
            Digital_Inputs.SelectedIndex = 0;
            Analog_Inputs.SelectedIndex = 0;
            Audio_Channels.SelectedIndex = 0;
            Video_Channels.SelectedIndex = 0;
            Relay_Outputs.SelectedIndex = 0;
        }

        private void Audio_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !Audio_Chnls[Audio_Channels.SelectedIndex].enabled;
            textBox2.Text = Audio_Chnls[Audio_Channels.SelectedIndex].delay.ToString();
        }

        private void Video_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !Video_Chnls[Video_Channels.SelectedIndex].enabled;
            textBox3.Text = Video_Chnls[Video_Channels.SelectedIndex].delay.ToString();
        }

        private void Analog_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {

            checkBox3.Checked = !Analog_Inpts[Analog_Inputs.SelectedIndex].enabled;
            textBox4.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].delay.ToString();
        }

        private void Digital_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox4.Checked = !Digital_Inpts[Digital_Inputs.SelectedIndex].enabled;
            textBox5.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].delay.ToString();
        }

        private void Relay_Outputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox5.Checked = !Relay_Outpts[Relay_Outputs.SelectedIndex].enabled;
            textBox1.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].delay.ToString();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Audio_Chnls[Audio_Channels.SelectedIndex].enabled = !checkBox1.Checked;
            if (checkBox1.Text == "Enabled")
            {
                checkBox1.Text = "Disabled";
            }
            else
            {
                checkBox1.Text = "Enabled";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Video_Chnls[Video_Channels.SelectedIndex].enabled = !checkBox2.Checked;
            if (checkBox2.Text == "Enabled")
            {
                checkBox2.Text = "Disabled";
            }
            else
            {
                checkBox2.Text = "Enabled";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Analog_Inpts[Analog_Inputs.SelectedIndex].enabled = !checkBox3.Checked; 
            
            if (checkBox3.Text == "Enabled")
            {
                checkBox3.Text = "Disabled";
            }
            else
            {
                checkBox3.Text = "Enabled";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Digital_Inpts[Digital_Inputs.SelectedIndex].enabled = !checkBox4.Checked;
            if (checkBox4.Text == "Enabled")
            {
                checkBox4.Text = "Disabled";
            }
            else
            {
                checkBox4.Text = "Enabled";
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Relay_Outpts[Relay_Outputs.SelectedIndex].enabled = !checkBox5.Checked;
            if (checkBox5.Text == "Enabled")
            {
                checkBox5.Text = "Disabled";
            }
            else
            {
                checkBox5.Text = "Enabled";
            }

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Audio_Chnls[Audio_Channels.SelectedIndex].delay = long.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Video_Chnls[Video_Channels.SelectedIndex].delay = long.Parse(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
             Analog_Inpts[Analog_Inputs.SelectedIndex].delay = long.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           Digital_Inpts[Digital_Inputs.SelectedIndex].delay = long.Parse(textBox5.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Relay_Outpts[Relay_Outputs.SelectedIndex].delay = long.Parse(textBox1.Text);
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Settings_Click(object sender, EventArgs e)
        {

        }



    }
    public class Test_Paramater
    {
        public Test_Paramater()
        {
            enabled = true;
            delay = 0;
        }
        public bool enabled;
        public long delay;
    }
}
