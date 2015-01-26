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
            Test_Paramater[] Audio_Chnls = new Test_Paramater[16];   //Defines the paramaters for each audio channel
            Test_Paramater[] Video_Chnls = new Test_Paramater[16];   //Defines the paramaters for each Video channel
            Test_Paramater[] Analog_Inpts = new Test_Paramater[6];   //Defines the paramaters for each Analog channel
            Test_Paramater[] Digital_Inpts = new Test_Paramater[16]; //Defines the paramaters for each Digital channel
            Test_Paramater[] Relay_Outpts = new Test_Paramater[16];  //Defines the paramaters for each Relay Output
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

            //Initializing each of the channels
            for (int i = 0; i < 16; ++i)
            {
                Audio_Chnls[i] = new Test_Paramater();
                Video_Chnls[i] = new Test_Paramater();
                Digital_Inpts[i] = new Test_Paramater();
                Relay_Outpts[i] = new Test_Paramater();
            }
            //Initializing the Analog Inputs
            for( int i = 0; i < 6; ++i)
            { Analog_Inpts[i] = new Test_Paramater(); }

            Digital_Inputs.Tag = 0;  //initialize the last used index to the first channel so that it isn't null when used the first time.
            Analog_Inputs.Tag = 0;
            Audio_Channels.Tag = 0;
            Video_Channels.Tag = 0;
            Relay_Outputs.Tag = 0; 

            Digital_Inputs.SelectedIndex = 0;//Start each drop down list on the first channel
            Analog_Inputs.SelectedIndex = 0;
            Audio_Channels.SelectedIndex = 0;
            Video_Channels.SelectedIndex = 0;
            Relay_Outputs.SelectedIndex = 0; 
        }

        private void Audio_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sets the last user inputs for the correct paramater before changing to the new paramater.
            Audio_Chnls[(int)(Audio_Channels.Tag)].delay = long.Parse(textBox2.Text);
            Audio_Chnls[(int)(Audio_Channels.Tag)].timeOn = long.Parse(textBox11.Text);
            Audio_Chnls[(int)(Audio_Channels.Tag)].timeOff = long.Parse(textBox16.Text);

            if (Audio_Channels.SelectedIndex >= 0)
            {
                Audio_Channels.Tag = Audio_Channels.SelectedIndex;                              //Index of the last used paramater
                checkBox1.Checked = !Audio_Chnls[Audio_Channels.SelectedIndex].enabled;         //sets the enabled button to the saved paramater
                textBox2.Text = Audio_Chnls[Audio_Channels.SelectedIndex].delay.ToString();     //sets the delay text to the saved value
                textBox11.Text = Audio_Chnls[Audio_Channels.SelectedIndex].timeOn.ToString();   //sets the timeOn text to the saved value
                textBox16.Text = Audio_Chnls[Audio_Channels.SelectedIndex].timeOff.ToString();  //sets the timeOff text to the saved value
            }
        }

        private void Video_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sets the last user inputs for the correct paramater before changing to the new paramater.
            Video_Chnls[(int)(Video_Channels.Tag)].delay = long.Parse(textBox3.Text);
            Video_Chnls[(int)(Video_Channels.Tag)].timeOn = long.Parse(textBox12.Text);
            Video_Chnls[(int)(Video_Channels.Tag)].timeOff = long.Parse(textBox17.Text);

            if (Video_Channels.SelectedIndex >= 0)
            {
                Video_Channels.Tag = Video_Channels.SelectedIndex;                              //Index of the last used paramater
                checkBox2.Checked = !Video_Chnls[Video_Channels.SelectedIndex].enabled;         //sets the enabled button to the saved paramater
                textBox3.Text = Video_Chnls[Video_Channels.SelectedIndex].delay.ToString();     //sets the delay text to the saved value
                textBox12.Text = Video_Chnls[Video_Channels.SelectedIndex].timeOn.ToString();   //sets the timeOn text to the saved value
                textBox17.Text = Video_Chnls[Video_Channels.SelectedIndex].timeOff.ToString();  //sets the timeOff text to the saved value
            }
        }

        private void Analog_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sets the last user inputs for the correct paramater before changing to the new paramater.
            Analog_Inpts[(int)(Analog_Inputs.Tag)].delay = long.Parse(textBox4.Text);
            Analog_Inpts[(int)(Analog_Inputs.Tag)].timeOn = long.Parse(textBox13.Text);
            Analog_Inpts[(int)(Analog_Inputs.Tag)].timeOff = long.Parse(textBox18.Text);

            if (Analog_Inputs.SelectedIndex >= 0)
            {
                Analog_Inputs.Tag = Analog_Inputs.SelectedIndex;                                //Index of the last used paramater
                checkBox3.Checked = !Analog_Inpts[Analog_Inputs.SelectedIndex].enabled;         //sets the enabled button to the saved paramater
                textBox4.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].delay.ToString();     //sets the delay text to the saved value
                textBox13.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].timeOn.ToString();   //sets the timeOn text to the saved value
                textBox18.Text = Analog_Inpts[Analog_Inputs.SelectedIndex].timeOff.ToString();  //sets the timeOff text to the saved value
            }
        }

        private void Digital_Inputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sets the last user inputs for the correct paramater before changing to the new paramater.
            Digital_Inpts[(int)(Digital_Inputs.Tag)].delay = long.Parse(textBox5.Text);
            Digital_Inpts[(int)(Digital_Inputs.Tag)].timeOn = long.Parse(textBox14.Text);
            Digital_Inpts[(int)(Digital_Inputs.Tag)].timeOff = long.Parse(textBox19.Text);

            if ( Digital_Inputs.SelectedIndex >= 0)
            {
                Digital_Inputs.Tag = Digital_Inputs.SelectedIndex;                               //Index of the last used paramater
                checkBox4.Checked = !Digital_Inpts[Digital_Inputs.SelectedIndex].enabled;        //sets the enabled button to the saved paramater
                textBox5.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].delay.ToString();    //sets the delay text to the saved value
                textBox14.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].timeOn.ToString();  //sets the timeOn text to the saved value
                textBox19.Text = Digital_Inpts[Digital_Inputs.SelectedIndex].timeOff.ToString(); //sets the timeOff text to the saved value
            }
        }

        private void Relay_Outputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sets the last user inputs for the correct paramater before changing to the new paramater.
            Relay_Outpts[(int)(Relay_Outputs.Tag)].delay = long.Parse(textBox1.Text);
            Relay_Outpts[(int)(Relay_Outputs.Tag)].timeOn = long.Parse(textBox15.Text);
            Relay_Outpts[(int)(Relay_Outputs.Tag)].timeOff = long.Parse(textBox20.Text);

            if (Relay_Outputs.SelectedIndex >= 0)
            {
                Relay_Outputs.Tag = Relay_Outputs.SelectedIndex;                                 //Index of the last used paramater
                checkBox5.Checked = !Relay_Outpts[Relay_Outputs.SelectedIndex].enabled;          //sets the enabled button to the saved paramater
                textBox1.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].delay.ToString();      //sets the delay text to the saved value
                textBox15.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].timeOn.ToString();    //sets the timeOn text to the saved value
                textBox20.Text = Relay_Outpts[Relay_Outputs.SelectedIndex].timeOff.ToString();   //sets the timeOff text to the saved value
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Audio_Chnls[Audio_Channels.SelectedIndex].enabled = !checkBox1.Checked;  //Save the changes to the test paramater

            //Toggle between Enabled and Disabled
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
            Video_Chnls[Video_Channels.SelectedIndex].enabled = !checkBox2.Checked;  //Save the changes to the test paramater

            //Toggle between Enabled and Disabled
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
            Analog_Inpts[Analog_Inputs.SelectedIndex].enabled = !checkBox3.Checked; //Save the changes to the test paramater

            //Toggle between Enabled and Disabled            
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
            Digital_Inpts[Digital_Inputs.SelectedIndex].enabled = !checkBox4.Checked;   //Save the changes to the test paramater

            //Toggle between Enabled and Disabled
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
            Relay_Outpts[Relay_Outputs.SelectedIndex].enabled = !checkBox5.Checked;     //Save the changes to the test paramater

            //Toggle between Enabled and Disabled
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
            if (SaveName.Visible == true)                       //Completing the save process
            {
                SaveName.Visible = false;                       //Remove the save name text box
                button4.Visible = true;                         //Reenable the load button
                SaveList.Items.Add(SaveName.Text);              //Add the save name to the list of Loadable Tests
                label12.Text = "Test Name: " + SaveName.Text;   //Set the test name label to equal the last save name
            }
            else                                                //Starting the save process
            {
                SaveName.Visible = true;                        //Enable the save name text box
                SaveName.Text = "";                             //Clear any previous save names in the text box
                button4.Visible = false;                        //temporarily remove the Load button
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SaveList.Visible == true)                   //Completing the load process
            {
                SaveList.Visible = false;                   //Remove the saved tests list
                button3.Visible = true;                     //Reenable the save button
                if (SaveList.SelectedIndex >= 0)            //if a save has been selected
                {
                    label12.Text = "Test Name: " + SaveList.SelectedItem.ToString();        //Add the loaded save name to the Test name label
                }
                else                                        //If no save has been selected
                {
                    label12.Text = "Test Name: ";
                }
            }
            else                                            //Starting the save process
            {
                SaveList.Visible = true;                    //Display the save drop down list
                button3.Visible = false;                    //Temporarily remove the save button
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void Com_Port_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Real_Time_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label184_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox78_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox71_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox68_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox87_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox87.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox87.Text = "Low";
            }
            else
            {
                checkBox87.Text = "High";
            }
        }

        private void checkBox72_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox74_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox74.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox74.Text = "Low";
            }
            else
            {
                checkBox74.Text = "High";
            }
        }

        private void checkBox79_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox79.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox79.Text = "Low";
            }
            else
            {
                checkBox79.Text = "High";
            }
        }

        private void checkBox60_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox60.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox60.Text = "Low";
            }
            else
            {
                checkBox60.Text = "High";
            }
        }

        private void checkBox54_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox54.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox54.Text = "Low";
            }
            else
            {
                checkBox54.Text = "High";
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

    }
    //This class is what holds the definitions for each test input paramater.
    public class Test_Paramater
    {
        public Test_Paramater()    //Constructor
        {
            enabled = true;
            delay = 0;
            timeOn = 0;
            timeOff = 0;
        }

        public bool enabled;   //Defines if the test paramater is useable with the DVR Model selected
        public long delay;      //Defines how long before the paramater is toggled
        public long timeOn;     //Defines how long the paramater is on before turning off again.
        public long timeOff;    //Defines how long the paramater is off before beginning a new cycle.
    }
}
