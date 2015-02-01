using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DVR_Tester_User_Interface
{

    public partial class Form1 : Form
    {
            Test_Paramater[] Audio_Chnls = new Test_Paramater[16];   //Defines the paramaters for each audio channel
            Test_Paramater[] Video_Chnls = new Test_Paramater[16];   //Defines the paramaters for each Video channel
            Test_Paramater[] Analog_Inpts = new Test_Paramater[4];   //Defines the paramaters for each Analog channel
            Test_Paramater[] Digital_Inpts = new Test_Paramater[16]; //Defines the paramaters for each Digital channel
            Test_Paramater[] Relay_Outpts = new Test_Paramater[4];  //Defines the paramaters for each Relay Output
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
                Audio_Chnls[i] = new Test_Paramater("Audio_Channel_"+i);
                Video_Chnls[i] = new Test_Paramater("Video_Channel_" + i);
                Digital_Inpts[i] = new Test_Paramater("Digital_Input_" + i);
            }
            //Initializing the Analog Inputs
            for( int i = 0; i < 4; ++i)
            {
                Analog_Inpts[i] = new Test_Paramater("Analog_Input_" + i);
                Relay_Outpts[i] = new Test_Paramater("Relay_Output_" + i);
            }

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
                
                //Create the Save File
                string SavePath = "C:\\Users\\Seth_2\\Desktop\\";
                char[] endLine = {'\n'};
                SavePath += SaveName.Text;
                SavePath += ".xml";
                XmlTextWriter XmlFile = new XmlTextWriter(SavePath, null);
                XmlFile.WriteStartDocument();
                string XmlSaveName = SaveName.Text;
                XmlSaveName = XmlSaveName.Replace(' ', '_');
                XmlFile.WriteStartElement(XmlSaveName);

                    XmlFile.WriteChars(endLine, 0, 1);
                    XmlFile.WriteComment("MobileView DVR Automated Test Save");
                    XmlFile.WriteChars(endLine, 0, 1);

                    XmlFile.WriteStartElement("Main_Test_Paramaters");
                    XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("DVR_Model", "");
                        XmlFile.WriteString(DVR_Model.GetItemText(DVR_Model.SelectedIndex));
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("Cycles_To_Run", "");
                        XmlFile.WriteString(textBox6.Text);
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("Main_On_To_Ign_On", "");
                        XmlFile.WriteString(textBox7.Text);
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("Ign_On_To_Ign_Off", "");
                        XmlFile.WriteString(textBox8.Text);
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("Ign_Off_To_Main_Off", "");
                        XmlFile.WriteString(textBox9.Text);
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                        XmlFile.WriteStartElement("Main_Off_To_Main_On", "");
                        XmlFile.WriteString(textBox10.Text);
                        XmlFile.WriteEndElement();
                        XmlFile.WriteChars(endLine, 0, 1);

                    XmlFile.WriteEndElement();
                    XmlFile.WriteChars(endLine, 0, 1);
                    XmlFile.WriteChars(endLine, 0, 1);

                    for (int i = 0; i < 16; ++i)
                    {
                        SaveParamater(Audio_Chnls[i], XmlFile);
                    }
                    for (int i = 0; i < 16; ++i)
                    {
                        SaveParamater(Video_Chnls[i], XmlFile);
                    }
                    for (int i = 0; i < 16; ++i)
                    {
                        SaveParamater(Digital_Inpts[i], XmlFile);
                    }
                    for (int i = 0; i < 4; ++i)
                    {
                        SaveParamater(Relay_Outpts[i], XmlFile);
                    }
                    for (int i = 0; i < 4; ++i)
                    {
                        SaveParamater(Analog_Inpts[i], XmlFile);
                    }
                XmlFile.WriteEndElement();
                XmlFile.WriteEndDocument();
                XmlFile.Close();

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
            if (checkBox78.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox78.Text = "Audio #10 OFF";
                checkBox78.BackColor = Color.Transparent;
            }
            else
            {
                checkBox78.Text = "Audio #10 ON";
                checkBox78.BackColor = Color.LightGreen;
            }
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
                checkBox87.Text = "Audio #1 OFF";
                checkBox87.BackColor = Color.Transparent;
            }
            else
            {
                checkBox87.Text = "Audio #1 ON";
                checkBox87.BackColor = Color.LightGreen;
            }
        }

        private void checkBox72_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox72.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox72.Text = "Audio #16 OFF";
                checkBox72.BackColor = Color.Transparent;
            }
            else
            {
                checkBox72.Text = "Audio #16 ON";
                checkBox72.BackColor = Color.LightGreen;
            }
        }

        private void checkBox74_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox74.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox74.Text = "Audio #14 OFF";
                checkBox74.BackColor = Color.Transparent;
            }
            else
            {
                checkBox74.Text = "Audio #14 ON";
                checkBox74.BackColor = Color.LightGreen;
            }
        }

        private void checkBox79_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox79.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox79.Text = "Audio #9 OFF";
                checkBox79.BackColor = Color.Transparent;
            }
            else
            {
                checkBox79.Text = "Audio #9 ON";
                checkBox79.BackColor = Color.LightGreen;
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

        private void checkBox86_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox86.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox86.Text = "Audio #2 OFF";
                checkBox86.BackColor = Color.Transparent;
            }
            else
            {
                checkBox86.Text = "Audio #2 ON";
                checkBox86.BackColor = Color.LightGreen;
            }
        }

        private void checkBox85_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox85.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox85.Text = "Audio #3 OFF";
                checkBox85.BackColor = Color.Transparent;
            }
            else
            {
                checkBox85.Text = "Audio #3 ON";
                checkBox85.BackColor = Color.LightGreen;
            }
        }

        private void checkBox84_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox84.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox84.Text = "Audio #4 OFF";
                checkBox84.BackColor = Color.Transparent;
            }
            else
            {
                checkBox84.Text = "Audio #4 ON";
                checkBox84.BackColor = Color.LightGreen;
            }
        }

        private void checkBox83_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox83.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox83.Text = "Audio #5 OFF";
                checkBox83.BackColor = Color.Transparent;
            }
            else
            {
                checkBox83.Text = "Audio #5 ON";
                checkBox83.BackColor = Color.LightGreen;
            }
        }

        private void checkBox82_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox82.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox82.Text = "Audio #6 OFF";
                checkBox82.BackColor = Color.Transparent;
            }
            else
            {
                checkBox82.Text = "Audio #6 ON";
                checkBox82.BackColor = Color.LightGreen;
            }
        }

        private void checkBox81_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox81.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox81.Text = "Audio #7 OFF";
                checkBox81.BackColor = Color.Transparent;
            }
            else
            {
                checkBox81.Text = "Audio #7 ON";
                checkBox81.BackColor = Color.LightGreen;
            }
        }

        private void checkBox80_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox80.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox80.Text = "Audio #8 OFF";
                checkBox80.BackColor = Color.Transparent;
            }
            else
            {
                checkBox80.Text = "Audio #8 ON";
                checkBox80.BackColor = Color.LightGreen;
            }
        }

        private void checkBox77_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox77.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox77.Text = "Audio #11 OFF";
                checkBox77.BackColor = Color.Transparent;
            }
            else
            {
                checkBox77.Text = "Audio #11 ON";
                checkBox77.BackColor = Color.LightGreen;
            }
        }

        private void checkBox76_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox76.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox76.Text = "Audio #12 OFF";
                checkBox76.BackColor = Color.Transparent;
            }
            else
            {
                checkBox76.Text = "Audio #12 ON";
                checkBox76.BackColor = Color.LightGreen;
            }
        }

        private void checkBox75_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox75.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox75.Text = "Audio #13 OFF";
                checkBox75.BackColor = Color.Transparent;
            }
            else
            {
                checkBox75.Text = "Audio #13 ON";
                checkBox75.BackColor = Color.LightGreen;
            }
        }

        private void checkBox73_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox73.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox73.Text = "Audio #15 OFF";
                checkBox73.BackColor = Color.Transparent;
            }
            else
            {
                checkBox73.Text = "Audio #15 ON";
                checkBox73.BackColor = Color.LightGreen;
            }
        }

        private void checkBox67_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox67.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox67.Text = "Video #1 OFF";
                checkBox67.BackColor = Color.Transparent;
            }
            else
            {
                checkBox67.Text = "Video #1 ON";
                checkBox67.BackColor = Color.LightGreen;
            }
        }

        private void checkBox66_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox66.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox66.Text = "Video #2 OFF";
                checkBox66.BackColor = Color.Transparent;
            }
            else
            {
                checkBox66.Text = "Video #2 ON";
                checkBox66.BackColor = Color.LightGreen;
            }
        }

        private void checkBox61_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox61.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox61.Text = "Video #3 OFF";
                checkBox61.BackColor = Color.Transparent;
            }
            else
            {
                checkBox61.Text = "Video #3 ON";
                checkBox61.BackColor = Color.LightGreen;
            }
        }

        private void checkBox60_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox60.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox60.Text = "Video #4 OFF";
                checkBox60.BackColor = Color.Transparent;
            }
            else
            {
                checkBox60.Text = "Video #4 ON";
                checkBox60.BackColor = Color.LightGreen;
            }
        }

        private void checkBox59_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox59.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox59.Text = "Video #5 OFF";
                checkBox59.BackColor = Color.Transparent;
            }
            else
            {
                checkBox59.Text = "Video #5 ON";
                checkBox59.BackColor = Color.LightGreen;
            }
        }

        private void checkBox58_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox58.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox58.Text = "Video #6 OFF";
                checkBox58.BackColor = Color.Transparent;
            }
            else
            {
                checkBox58.Text = "Video #6 ON";
                checkBox58.BackColor = Color.LightGreen;
            }
        }

        private void checkBox57_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox57.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox57.Text = "Video #7 OFF";
                checkBox57.BackColor = Color.Transparent;
            }
            else
            {
                checkBox57.Text = "Video #7 ON";
                checkBox57.BackColor = Color.LightGreen;
            }
        }

        private void checkBox56_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox56.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox56.Text = "Video #8 OFF";
                checkBox56.BackColor = Color.Transparent;
            }
            else
            {
                checkBox56.Text = "Video #8 ON";
                checkBox56.BackColor = Color.LightGreen;
            }
        }

        private void checkBox55_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox55.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox55.Text = "Video #9 OFF";
                checkBox55.BackColor = Color.Transparent;
            }
            else
            {
                checkBox55.Text = "Video #9 ON";
                checkBox55.BackColor = Color.LightGreen;
            }
        }

        private void checkBox54_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox54.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox54.Text = "Video #10 OFF";
                checkBox54.BackColor = Color.Transparent;
            }
            else
            {
                checkBox54.Text = "Video #10 ON";
                checkBox54.BackColor = Color.LightGreen;
            }
        }

        private void checkBox53_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox53.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox53.Text = "Video #11 OFF";
                checkBox53.BackColor = Color.Transparent;
            }
            else
            {
                checkBox53.Text = "Video #11 ON";
                checkBox53.BackColor = Color.LightGreen;
            }
        }

        private void checkBox52_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox52.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox52.Text = "Video #12 OFF";
                checkBox52.BackColor = Color.Transparent;
            }
            else
            {
                checkBox52.Text = "Video #12 ON";
                checkBox52.BackColor = Color.LightGreen;
            }
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox51.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox51.Text = "Video #13 OFF";
                checkBox51.BackColor = Color.Transparent;
            }
            else
            {
                checkBox51.Text = "Video #13 ON";
                checkBox51.BackColor = Color.LightGreen;
            }
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox50.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox50.Text = "Video #14 OFF";
                checkBox50.BackColor = Color.Transparent;
            }
            else
            {
                checkBox50.Text = "Video #14 ON";
                checkBox50.BackColor = Color.LightGreen;
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox17.Text = "Video #15 OFF";
                checkBox17.BackColor = Color.Transparent;
            }
            else
            {
                checkBox17.Text = "Video #15 ON";
                checkBox17.BackColor = Color.LightGreen;
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox16.Text = "Video #16 OFF";
                checkBox16.BackColor = Color.Transparent;
            }
            else
            {
                checkBox16.Text = "Video #16 ON";
                checkBox16.BackColor = Color.LightGreen;
            }
        }

        private void checkBox103_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox103.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox103.Text = "Digital #1 OFF";
                checkBox103.BackColor = Color.Transparent;
            }
            else
            {
                checkBox103.Text = "Digital #1 ON";
                checkBox103.BackColor = Color.LightGreen;
            }
        }

        private void checkBox102_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox102.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox102.Text = "Digital #2 OFF";
                checkBox102.BackColor = Color.Transparent;
            }
            else
            {
                checkBox102.Text = "Digital #2 ON";
                checkBox102.BackColor = Color.LightGreen;
            }
        }

        private void checkBox101_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox101.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox101.Text = "Digital #3 OFF";
                checkBox101.BackColor = Color.Transparent;
            }
            else
            {
                checkBox101.Text = "Digital #3 ON";
                checkBox101.BackColor = Color.LightGreen;
            }
        }

        private void checkBox100_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox100.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox100.Text = "Digital #4 OFF";
                checkBox100.BackColor = Color.Transparent;
            }
            else
            {
                checkBox100.Text = "Digital #4 ON";
                checkBox100.BackColor = Color.LightGreen;
            }
        }

        private void checkBox99_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox99.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox99.Text = "Digital #5 OFF";
                checkBox99.BackColor = Color.Transparent;
            }
            else
            {
                checkBox99.Text = "Digital #5 ON";
                checkBox99.BackColor = Color.LightGreen;
            }
        }

        private void checkBox98_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox98.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox98.Text = "Digital #6 OFF";
                checkBox98.BackColor = Color.Transparent;
            }
            else
            {
                checkBox98.Text = "Digital #6 ON";
                checkBox98.BackColor = Color.LightGreen;
            }
        }

        private void checkBox95_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox95.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox95.Text = "Digital #7 OFF";
                checkBox95.BackColor = Color.Transparent;
            }
            else
            {
                checkBox95.Text = "Digital #7 ON";
                checkBox95.BackColor = Color.LightGreen;
            }
        }

        private void checkBox92_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox92.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox92.Text = "Digital #8 OFF";
                checkBox92.BackColor = Color.Transparent;
            }
            else
            {
                checkBox92.Text = "Digital #8 ON";
                checkBox92.BackColor = Color.LightGreen;
            }
        }

        private void checkBox91_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox91.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox91.Text = "Digital #9 OFF";
                checkBox91.BackColor = Color.Transparent;
            }
            else
            {
                checkBox91.Text = "Digital #9 ON";
                checkBox91.BackColor = Color.LightGreen;
            }
        }

        private void checkBox90_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox90.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox90.Text = "Digital #10 OFF";
                checkBox90.BackColor = Color.Transparent;
            }
            else
            {
                checkBox90.Text = "Digital #10 ON";
                checkBox90.BackColor = Color.LightGreen;
            }

        }

        private void checkBox89_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox89.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox89.Text = "Digital #11 OFF";
                checkBox89.BackColor = Color.Transparent;
            }
            else
            {
                checkBox89.Text = "Digital #11 ON";
                checkBox89.BackColor = Color.LightGreen;
            }
        }

        private void checkBox88_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox88.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox88.Text = "Digital #12 OFF";
                checkBox88.BackColor = Color.Transparent;
            }
            else
            {
                checkBox88.Text = "Digital #12 ON";
                checkBox88.BackColor = Color.LightGreen;
            }
        }

        private void checkBox71_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox71.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox71.Text = "Digital #13 OFF";
                checkBox71.BackColor = Color.Transparent;
            }
            else
            {
                checkBox71.Text = "Digital #13 ON";
                checkBox71.BackColor = Color.LightGreen;
            }
        }

        private void checkBox70_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox70.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox70.Text = "Digital #14 OFF";
                checkBox70.BackColor = Color.Transparent;
            }
            else
            {
                checkBox70.Text = "Digital #14 ON";
                checkBox70.BackColor = Color.LightGreen;
            }
        }

        private void checkBox69_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox69.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox69.Text = "Digital #15 OFF";
                checkBox69.BackColor = Color.Transparent;
            }
            else
            {
                checkBox69.Text = "Digital #15 ON";
                checkBox69.BackColor = Color.LightGreen;
            }
        }

        private void checkBox68_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox68.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox68.Text = "Digital #16 OFF";
                checkBox68.BackColor = Color.Transparent;
            }
            else
            {
                checkBox68.Text = "Digital #16 ON";
                checkBox68.BackColor = Color.LightGreen;
            }
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(textBox21.Text) > 30)
                    textBox21.Text = "30";
                if (Convert.ToInt16(textBox21.Text) < 0)
                    textBox21.Text = "0";
            }
            catch
            {
                if (textBox21.Text.Length > 0)
                    textBox21.Text = textBox21.Text.Remove(textBox21.Text.Length - 1);
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(textBox22.Text) > 30)
                    textBox22.Text = "30";
                if (Convert.ToInt16(textBox22.Text) < 0)
                    textBox22.Text = "0";
            }
            catch
            {
                if (textBox22.Text.Length > 0)
                    textBox22.Text = textBox22.Text.Remove(textBox22.Text.Length - 1);
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(textBox23.Text) > 30)
                    textBox23.Text = "30";
                if (Convert.ToInt16(textBox23.Text) < 0)
                    textBox23.Text = "0";
            }
            catch
            {
                if (textBox23.Text.Length > 0)
                    textBox23.Text = textBox23.Text.Remove(textBox23.Text.Length - 1);
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToInt16(textBox24.Text) > 30)
                    textBox24.Text = "30";
                if (Convert.ToInt16(textBox24.Text) < 0)
                    textBox24.Text = "0";
            }
            catch
            {
                if (textBox24.Text.Length > 0)
                    textBox24.Text = textBox24.Text.Remove(textBox24.Text.Length - 1);
            }
        }

        private void SaveParamater(Test_Paramater Param, XmlTextWriter XmlFile)
        {
            char[] endLine = { '\n' };

            XmlFile.WriteStartElement("Test_Paramater");
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteStartElement("Name", "");
            XmlFile.WriteString(Param.name);
            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteStartElement("Enabled", "");
            XmlFile.WriteString(Param.enabled.ToString());
            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteStartElement("Delay", "");
            XmlFile.WriteString(Param.delay.ToString());
            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteStartElement("TimeOn", "");
            XmlFile.WriteString(Param.timeOn.ToString());
            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteStartElement("TimeOff", "");
            XmlFile.WriteString(Param.timeOff.ToString());
            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);

            XmlFile.WriteEndElement();
            XmlFile.WriteChars(endLine, 0, 1);
            XmlFile.WriteChars(endLine, 0, 1);
        }

        private void checkBox104_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox104.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox104.Text = "Network OFF";
                checkBox104.BackColor = Color.Transparent;
            }
            else
            {
                checkBox104.Text = "Network ON";
                checkBox104.BackColor = Color.LightGreen;
            }

        }
    }
    //This class is what holds the definitions for each test input paramater.
    public class Test_Paramater
    {
        public Test_Paramater(string nname)    //Constructor
        {
            enabled = true;
            delay = 0;
            timeOn = 0;
            timeOff = 0;
            name = nname;
        }

        public string name;
        public bool enabled;   //Defines if the test paramater is useable with the DVR Model selected
        public long delay;      //Defines how long before the paramater is toggled
        public long timeOn;     //Defines how long the paramater is on before turning off again.
        public long timeOff;    //Defines how long the paramater is off before beginning a new cycle.
    }
}
