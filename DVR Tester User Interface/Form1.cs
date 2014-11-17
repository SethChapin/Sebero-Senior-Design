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
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
            textBox11.Text = "0";
            textBox12.Text = "0";
            textBox13.Text = "0";
            textBox14.Text = "0";
            textBox15.Text = "0";
            textBox16.Text = "0";
            textBox17.Text = "0";
            textBox18.Text = "0";
            textBox19.Text = "0";
            textBox20.Text = "0";
            for (int i = 0; i < 16; ++i)
            {
                Audio_Chnls[i] = new Test_Paramater();
                Video_Chnls[i] = new Test_Paramater();
                Digital_Inpts[i] = new Test_Paramater();
                Relay_Outpts[i] = new Test_Paramater();
            }
            for( int i = 0; i < 6; ++i)
            { Analog_Inpts[i] = new Test_Paramater(); }

            Digital_Inputs.Tag = 0;
            Analog_Inputs.Tag = 0;
            Audio_Channels.Tag = 0;
            Video_Channels.Tag = 0;
            Relay_Outputs.Tag = 0; 

            Digital_Inputs.SelectedIndex = 0;
            Analog_Inputs.SelectedIndex = 0;
            Audio_Channels.SelectedIndex = 0;
            Video_Channels.SelectedIndex = 0;
            Relay_Outputs.SelectedIndex = 0; 
        }

        private void Audio_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Audio_Chnls[(int)(Audio_Channels.Tag)].delay = long.Parse(textBox2.Text);
            Audio_Chnls[(int)(Audio_Channels.Tag)].timeOn = long.Parse(textBox11.Text);
            Audio_Chnls[(int)(Audio_Channels.Tag)].timeOff = long.Parse(textBox16.Text);

            if (Audio_Channels.SelectedIndex >= 0)
            {
                Audio_Channels.Tag = Audio_Channels.SelectedIndex;
                checkBox1.Checked = !Audio_Chnls[Audio_Channels.SelectedIndex].enabled;
                textBox2.Text = Audio_Chnls[Audio_Channels.SelectedIndex].delay.ToString();
                textBox11.Text = Audio_Chnls[Audio_Channels.SelectedIndex].timeOn.ToString();
                textBox16.Text = Audio_Chnls[Audio_Channels.SelectedIndex].timeOff.ToString();
            }
        }

        private void Video_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Video_Chnls[(int)(Video_Channels.Tag)].delay = long.Parse(textBox3.Text);
            Video_Chnls[(int)(Video_Channels.Tag)].timeOn = long.Parse(textBox12.Text);
            Video_Chnls[(int)(Video_Channels.Tag)].timeOff = long.Parse(textBox17.Text);

            if (Video_Channels.SelectedIndex >= 0)
            {
                Video_Channels.Tag = Video_Channels.SelectedIndex;
                checkBox2.Checked = !Video_Chnls[Video_Channels.SelectedIndex].enabled;
                textBox3.Text = Video_Chnls[Video_Channels.SelectedIndex].delay.ToString();
                textBox12.Text = Video_Chnls[Video_Channels.SelectedIndex].timeOn.ToString();
                textBox17.Text = Video_Chnls[Video_Channels.SelectedIndex].timeOff.ToString();
            }
        }

        private void Analog_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Analog_Inpts[(int)(Analog_Inputs.Tag)].delay = long.Parse(textBox4.Text);
            Analog_Inpts[(int)(Analog_Inputs.Tag)].timeOn = long.Parse(textBox13.Text);
            Analog_Inpts[(int)(Analog_Inputs.Tag)].timeOff = long.Parse(textBox18.Text);

            if (Analog_Inputs.SelectedIndex >= 0)
            {
                Analog_Inputs.Tag = Analog_Inputs.SelectedIndex;

                checkBox3.Checked = !Analog_Inpts[Analog_Inputs.SelectedIndex].enabled;
                textBox4.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].delay.ToString();
                textBox13.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].timeOn.ToString();
                textBox18.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].timeOff.ToString();
            }
        }

        private void Digital_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Digital_Inpts[(int)(Digital_Inputs.Tag)].delay = long.Parse(textBox5.Text);
            Digital_Inpts[(int)(Digital_Inputs.Tag)].timeOn = long.Parse(textBox14.Text);
            Digital_Inpts[(int)(Digital_Inputs.Tag)].timeOff = long.Parse(textBox19.Text);

            if ( Digital_Inputs.SelectedIndex >= 0)
            {
                Digital_Inputs.Tag = Digital_Inputs.SelectedIndex;
                checkBox4.Checked = !Digital_Inpts[Digital_Inputs.SelectedIndex].enabled;
                textBox5.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].delay.ToString();
                textBox14.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].timeOn.ToString();
                textBox19.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].timeOff.ToString();
            }
        }

        private void Relay_Outputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Relay_Outpts[(int)(Relay_Outputs.Tag)].delay = long.Parse(textBox1.Text);
            Relay_Outpts[(int)(Relay_Outputs.Tag)].timeOn = long.Parse(textBox15.Text);
            Relay_Outpts[(int)(Relay_Outputs.Tag)].timeOff = long.Parse(textBox20.Text);

            if (Relay_Outputs.SelectedIndex >= 0)
            {
                Relay_Outputs.Tag = Relay_Outputs.SelectedIndex;
                checkBox5.Checked = !Relay_Outpts[Relay_Outputs.SelectedIndex].enabled;
                textBox1.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].delay.ToString();
                textBox15.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].timeOn.ToString();
                textBox20.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].timeOff.ToString();
            }

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

        private void button3_Click(object sender, EventArgs e)
        {
            if (SaveName.Visible == true)
            {
                SaveName.Visible = false;
                button4.Visible = true;
                SaveList.Items.Add(SaveName.Text);
                label12.Text = "Test Name: " + SaveName.Text;
            }
            else
            {
                SaveName.Visible = true;
                SaveName.Text = "";
                button4.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SaveList.Visible == true)
            {
                SaveList.Visible = false;
                button3.Visible = true;
                if (SaveList.SelectedIndex >= 0)
                {
                    label12.Text = "Test Name: " + SaveList.SelectedItem.ToString();
                }
                else
                {
                    label12.Text = "Test Name: ";
                }
            }
            else
            {
                SaveList.Visible = true;
                button3.Visible = false;
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

    }
    public class Test_Paramater
    {
        public Test_Paramater()
        {
            enabled = true;
            delay = 0;
            timeOn = 0;
            timeOff = 0;
        }
        public bool enabled;
        public long delay;
        public long timeOn;
        public long timeOff;
        public int lastIndex;
    }
}
