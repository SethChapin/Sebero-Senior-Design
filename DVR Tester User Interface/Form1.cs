using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO.Ports;

namespace DVR_Tester_User_Interface
{

    public partial class Form1 : Form
    {
            public SerialPort SerialObject;
            public bool connected = false;
            public CheckBox[] AudioLines = new CheckBox[16];
            public CheckBox[] VideoLines = new CheckBox[16];
            public CheckBox[] DigitalLines = new CheckBox[16];
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

            AudioLines[0] = checkBox111;
            AudioLines[1] = checkBox110;
            AudioLines[2] = checkBox109;
            AudioLines[3] = checkBox108;
            AudioLines[4] = checkBox107;
            AudioLines[5] = checkBox106;
            AudioLines[6] = checkBox105;
            AudioLines[7] = checkBox93;
            AudioLines[8] = checkBox65;
            AudioLines[9] = checkBox64;
            AudioLines[10] = checkBox63;
            AudioLines[11] = checkBox62;
            AudioLines[12] = checkBox49;
            AudioLines[13] = checkBox48;
            AudioLines[14] = checkBox47;
            AudioLines[15] = checkBox46;

            VideoLines[0] = checkBox45;
            VideoLines[1] = checkBox44;
            VideoLines[2] = checkBox43;
            VideoLines[3] = checkBox42;
            VideoLines[4] = checkBox41;
            VideoLines[5] = checkBox40;
            VideoLines[6] = checkBox39;
            VideoLines[7] = checkBox38;
            VideoLines[8] = checkBox37;
            VideoLines[9] = checkBox36;
            VideoLines[10] = checkBox35;
            VideoLines[11] = checkBox34;
            VideoLines[12] = checkBox33;
            VideoLines[13] = checkBox32;
            VideoLines[14] = checkBox31;
            VideoLines[15] = checkBox30;

            DigitalLines[0] = checkBox29;
            DigitalLines[1] = checkBox28;
            DigitalLines[2] = checkBox27;
            DigitalLines[3] = checkBox26;
            DigitalLines[4] = checkBox25;
            DigitalLines[5] = checkBox24;
            DigitalLines[6] = checkBox23;
            DigitalLines[7] = checkBox22;
            DigitalLines[8] = checkBox21;
            DigitalLines[9] = checkBox20;
            DigitalLines[10] = checkBox19;
            DigitalLines[11] = checkBox18;
            DigitalLines[12] = checkBox15;
            DigitalLines[13] = checkBox14;
            DigitalLines[14] = checkBox13;
            DigitalLines[15] = checkBox12;

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
            comboBox3.SelectedIndex = Com_Port.SelectedIndex;
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

        private void label184_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox78_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox78.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox78.Text = "Audio #10 OFF";
                checkBox78.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					    if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox78.Text = "Audio #10 ON";
                checkBox78.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
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
            byte[] serialBytes, sentSerialBytes;
            if (checkBox87.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox87.Text = "Audio #1 OFF";
                checkBox87.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }

            }
            else
            {
                checkBox87.Text = "Audio #1 ON";
                checkBox87.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox72_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox72.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox72.Text = "Audio #16 OFF";
                checkBox72.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] {0x16, 0x80, 0x00, 0, 0, 0xFF};
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);

                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox72.Text = "Audio #16 ON";
                checkBox72.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x80, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox74_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox74.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox74.Text = "Audio #14 OFF";
                checkBox74.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox74.Text = "Audio #14 ON";
                checkBox74.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox79_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox79.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox79.Text = "Audio #9 OFF";
                checkBox79.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox79.Text = "Audio #9 ON";
                checkBox79.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
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
            byte[] serialBytes, sentSerialBytes;
            if (checkBox86.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox86.Text = "Audio #2 OFF";
                checkBox86.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox86.Text = "Audio #2 ON";
                checkBox86.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox85_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox85.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox85.Text = "Audio #3 OFF";
                checkBox85.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox85.Text = "Audio #3 ON";
                checkBox85.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox84_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox84.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox84.Text = "Audio #4 OFF";
                checkBox84.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox84.Text = "Audio #4 ON";
                checkBox84.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox83_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox83.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox83.Text = "Audio #5 OFF";
                checkBox83.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox83.Text = "Audio #5 ON";
                checkBox83.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox82_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox82.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox82.Text = "Audio #6 OFF";
                checkBox82.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox82.Text = "Audio #6 ON";
                checkBox82.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox81_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox81.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox81.Text = "Audio #7 OFF";
                checkBox81.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox81.Text = "Audio #7 ON";
                checkBox81.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox80_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox80.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox80.Text = "Audio #8 OFF";
                checkBox80.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox80.Text = "Audio #8 ON";
                checkBox80.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox77_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox77.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox77.Text = "Audio #11 OFF";
                checkBox77.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox77.Text = "Audio #11 ON";
                checkBox77.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox76_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox76.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox76.Text = "Audio #12 OFF";
                checkBox76.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox76.Text = "Audio #12 ON";
                checkBox76.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox75_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox75.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox75.Text = "Audio #13 OFF";
                checkBox75.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox75.Text = "Audio #13 ON";
                checkBox75.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox73_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox73.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox73.Text = "Audio #15 OFF";
                checkBox73.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x16, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox73.Text = "Audio #15 ON";
                checkBox73.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x26, 0x40, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox67_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox67.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox67.Text = "Video #1 OFF";
                checkBox67.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox67.Text = "Video #1 ON";
                checkBox67.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox66_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox66.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox66.Text = "Video #2 OFF";
                checkBox66.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox66.Text = "Video #2 ON";
                checkBox66.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox61_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox61.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox61.Text = "Video #3 OFF";
                checkBox61.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox61.Text = "Video #3 ON";
                checkBox61.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox60_CheckedChanged_1(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox60.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox60.Text = "Video #4 OFF";
                checkBox60.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox60.Text = "Video #4 ON";
                checkBox60.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox59_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox59.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox59.Text = "Video #5 OFF";
                checkBox59.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox59.Text = "Video #5 ON";
                checkBox59.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox58_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox58.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox58.Text = "Video #6 OFF";
                checkBox58.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox58.Text = "Video #6 ON";
                checkBox58.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox57_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox57.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox57.Text = "Video #7 OFF";
                checkBox57.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox57.Text = "Video #7 ON";
                checkBox57.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox56_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox56.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox56.Text = "Video #8 OFF";
                checkBox56.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox56.Text = "Video #8 ON";
                checkBox56.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox55_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox55.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox55.Text = "Video #9 OFF";
                checkBox55.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox55.Text = "Video #9 ON";
                checkBox55.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox54_CheckedChanged_1(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox54.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox54.Text = "Video #10 OFF";
                checkBox54.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox54.Text = "Video #10 ON";
                checkBox54.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox53_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox53.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox53.Text = "Video #11 OFF";
                checkBox53.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox53.Text = "Video #11 ON";
                checkBox53.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox52_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox52.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox52.Text = "Video #12 OFF";
                checkBox52.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox52.Text = "Video #12 ON";
                checkBox52.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox51_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox51.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox51.Text = "Video #13 OFF";
                checkBox51.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox51.Text = "Video #13 ON";
                checkBox51.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox50.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox50.Text = "Video #14 OFF";
                checkBox50.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox50.Text = "Video #14 ON";
                checkBox50.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox17.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox17.Text = "Video #15 OFF";
                checkBox17.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x40, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox17.Text = "Video #15 ON";
                checkBox17.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x40, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox16.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox16.Text = "Video #16 OFF";
                checkBox16.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x14, 0x80, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5 ; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox16.Text = "Video #16 ON";
                checkBox16.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x24, 0x80, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
					if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
					j++;
					SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox103_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox103.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox103.Text = "Digital #1 OFF";
                checkBox103.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox103.Text = "Digital #1 ON";
                checkBox103.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox102_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox102.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox102.Text = "Digital #2 OFF";
                checkBox102.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox102.Text = "Digital #2 ON";
                checkBox102.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox101_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox101.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox101.Text = "Digital #3 OFF";
                checkBox101.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox101.Text = "Digital #3 ON";
                checkBox101.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x04, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox100_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox100.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox100.Text = "Digital #4 OFF";
                checkBox100.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox100.Text = "Digital #4 ON";
                checkBox100.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x08, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox99_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox99.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox99.Text = "Digital #5 OFF";
                checkBox99.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox99.Text = "Digital #5 ON";
                checkBox99.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x10, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox98_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox98.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox98.Text = "Digital #6 OFF";
                checkBox98.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox98.Text = "Digital #6 ON";
                checkBox98.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x20, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox95_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox95.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox95.Text = "Digital #7 OFF";
                checkBox95.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox95.Text = "Digital #7 ON";
                checkBox95.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x40, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox92_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox92.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox92.Text = "Digital #8 OFF";
                checkBox92.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox92.Text = "Digital #8 ON";
                checkBox92.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x00, 0x80, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox91_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox91.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox91.Text = "Digital #9 OFF";
                checkBox91.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox91.Text = "Digital #9 ON";
                checkBox91.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x01, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox90_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox90.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox90.Text = "Digital #10 OFF";
                checkBox90.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox90.Text = "Digital #10 ON";
                checkBox90.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x02, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }

        }

        private void checkBox89_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox89.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox89.Text = "Digital #11 OFF";
                checkBox89.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox89.Text = "Digital #11 ON";
                checkBox89.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x04, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox88_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox88.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox88.Text = "Digital #12 OFF";
                checkBox88.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox88.Text = "Digital #12 ON";
                checkBox88.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x08, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox71_CheckedChanged_1(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox71.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox71.Text = "Digital #13 OFF";
                checkBox71.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox71.Text = "Digital #13 ON";
                checkBox71.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x10, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox70_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox70.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox70.Text = "Digital #14 OFF";
                checkBox70.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox70.Text = "Digital #14 ON";
                checkBox70.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x20, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox69_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox69.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox69.Text = "Digital #15 OFF";
                checkBox69.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x40, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox69.Text = "Digital #15 ON";
                checkBox69.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x40, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void checkBox68_CheckedChanged_1(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox68.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox68.Text = "Digital #16 OFF";
                checkBox68.BackColor = Color.Transparent;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x20, 0x80, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox68.Text = "Digital #16 ON";
                checkBox68.BackColor = Color.LightGreen;
                if (connected)
                {
                    serialBytes = new byte[6] { 0x10, 0x80, 0x00, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                    serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[4] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 5; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
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
            byte[] serialBytes, sentSerialBytes;
            if (checkBox104.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox104.Text = "Network OFF";
                checkBox104.BackColor = Color.Transparent; if (connected)
                {
                    serialBytes = new byte[5] {0x18, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox104.Text = "Network ON";
                checkBox104.BackColor = Color.LightGreen; if (connected)
                {
                    serialBytes = new byte[5] { 0x28, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }

        }

        private void checkBox112_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox112.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox112.Text = "Main Power OFF";
                checkBox112.BackColor = Color.Transparent; if (connected)
                {
                    serialBytes = new byte[5] { 0x2E, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox112.Text = "Main Power ON";
                checkBox112.BackColor = Color.LightGreen; if (connected)
                {
                    serialBytes = new byte[5] { 0x0E, 0x02, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }

        }

        private void checkBox113_CheckedChanged(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (checkBox113.Checked == true)             //Need to also send Serial Data to Tester
            {
                checkBox113.Text = "Ignition OFF";
                checkBox113.BackColor = Color.Transparent; if (connected)
                {
                    serialBytes = new byte[5] { 0x2E, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
            else
            {
                checkBox113.Text = "Ignition ON";
                checkBox113.BackColor = Color.LightGreen; if (connected)
                {
                    serialBytes = new byte[5] { 0x0E, 0x01, 0, 0, 0xFF };
                    int checksum = serialBytes[0] + serialBytes[1];
                    serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[3] = (byte)(checksum & 0xFF);
                    int j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 4; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(!connected)
            {
                try
                {
                    SerialObject = new SerialPort(comboBox3.SelectedItem.ToString(), 9600);
                    SerialObject.Open();
                    SerialObject.ReadTimeout = 20;      //1 second timeout
                    SerialObject.WriteTimeout = 20;        //1 second timeout
                    checkBox94.Checked = true;
                    checkBox9.Checked = true;
                    connected = true;
                }
                catch (Exception)
                {

                }
            }
        }
        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            Com_Port.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox3.Items.Add(port);
                Com_Port.Items.Add(port);
            }
        }
        private void Com_Port_DropDown(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            Com_Port.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comboBox3.Items.Add(port);
                Com_Port.Items.Add(port);
            }
        }
        public int readSerialData(SerialPort port, byte[] dataRecieved)
        {
            try
            {
                for (int i = 0; i < 260; ++i)
                {
                    dataRecieved[i] = Convert.ToByte(port.ReadByte());
                    if (dataRecieved[i] == 0xff)
                        return i + 1;
                    if (i > 0 && dataRecieved[i - 1] == 0xCD)
                    {
                        if (dataRecieved[i] == 0x55)
                        {
                            dataRecieved[i - 1] = 0xFF;
                            i--;
                        }
                        if (dataRecieved[i] == 0xAA)
                        {
                            dataRecieved[i - 1] = 0xCD;
                            i--;
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return -1;
        }
        public void sendSerialData(SerialPort port, byte[] dataToSend, int length)
        {
            try
            {
                    port.Write(dataToSend, 0, length);
            }
            catch{}
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                checkBox94.Checked = false;
                checkBox9.Checked = false;
                connected = false;
                SerialObject.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (connected)
            {
                byte volLvl = 0;
                volLvl = Convert.ToByte(textBox21.Text);
                serialBytes = new byte[6] { 0x1A, 0x01, (byte)volLvl, 0, 0, 0xFF };
                int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[4] = (byte)(checksum & 0xFF);
                int j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 5; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            byte[] serialBytes, sentSerialBytes;
            if (connected)
            {
                byte volLvl = 0;
                volLvl = Convert.ToByte(textBox22.Text);
                serialBytes = new byte[6] { 0x1A, 0x02, (byte)volLvl, 0, 0, 0xFF };
                int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[4] = (byte)(checksum & 0xFF);
                int j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 5; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            byte[] serialBytes, sentSerialBytes;
            if (connected)
            {
                byte volLvl = 0;
                volLvl = Convert.ToByte(textBox23.Text);
                serialBytes = new byte[6] { 0x1A, 0x04, (byte)volLvl, 0, 0, 0xFF };
                int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[4] = (byte)(checksum & 0xFF);
                int j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 5; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {

            byte[] serialBytes, sentSerialBytes;
            if (connected)
            {
                byte volLvl = 0;
                volLvl = Convert.ToByte(textBox24.Text);
                serialBytes = new byte[6] { 0x1A, 0x08, (byte)volLvl, 0, 0, 0xFF };
                int checksum = serialBytes[0] + serialBytes[1] + serialBytes[2];
                serialBytes[3] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[4] = (byte)(checksum & 0xFF);
                int j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 5; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] serialBytes, sentSerialBytes;
            if (connected)
            {
                serialBytes = new byte[4] { 0x13, 0, 0, 0xFF };
                int checksum = serialBytes[0];
                serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[2] = (byte)(checksum & 0xFF);
                int j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 3; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
                Recieve_Msg();

                serialBytes = new byte[4] { 0x1C, 0, 0, 0xFF };
                checksum = serialBytes[0];
                serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                serialBytes[2] = (byte)(checksum & 0xFF);
                j = 0;
                sentSerialBytes = new byte[12];
                for (int i = 0; i < 3; ++i)
                {
                    sentSerialBytes[j] = serialBytes[i];
                    if (sentSerialBytes[j] == 0xFF)
                    {
                        sentSerialBytes[j] = 0xCD;
                        j++;
                        sentSerialBytes[j] = 0x55;
                    }
                    else if (sentSerialBytes[j] == 0xCD)
                    {
                        j++;
                        sentSerialBytes[j] = 0xAA;
                    }
                    else
                    {
                        sentSerialBytes[j] = serialBytes[i];
                    }
                    j++;
                }
                sentSerialBytes[j] = 0xFF;
                j++;
                SerialObject.Write(sentSerialBytes, 0, j);
                Recieve_Msg();

                if (UI.SelectedIndex == 0)
                {
                    serialBytes = new byte[4] { 0x17, 0, 0, 0xFF };
                    checksum = serialBytes[0];
                    serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[2] = (byte)(checksum & 0xFF);
                    j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 3; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                    Recieve_Msg();


                    serialBytes = new byte[4] { 0x15, 0, 0, 0xFF };
                    checksum = serialBytes[0];
                    serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[2] = (byte)(checksum & 0xFF);
                    j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 3; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                    Recieve_Msg();


                    serialBytes = new byte[4] { 0x11, 0, 0, 0xFF };
                    checksum = serialBytes[0];
                    serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[2] = (byte)(checksum & 0xFF);
                    j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 3; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                    Recieve_Msg();

                    serialBytes = new byte[4] { 0x19, 0, 0, 0xFF };
                    checksum = serialBytes[0];
                    serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[2] = (byte)(checksum & 0xFF);
                    j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 3; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                    Recieve_Msg();

                    serialBytes = new byte[4] { 0x0F, 0, 0, 0xFF };
                    checksum = serialBytes[0];
                    serialBytes[1] = (byte)((checksum & 0xFF00) >> 8);
                    serialBytes[2] = (byte)(checksum & 0xFF);
                    j = 0;
                    sentSerialBytes = new byte[12];
                    for (int i = 0; i < 3; ++i)
                    {
                        sentSerialBytes[j] = serialBytes[i];
                        if (sentSerialBytes[j] == 0xFF)
                        {
                            sentSerialBytes[j] = 0xCD;
                            j++;
                            sentSerialBytes[j] = 0x55;
                        }
                        else if (sentSerialBytes[j] == 0xCD)
                        {
                            j++;
                            sentSerialBytes[j] = 0xAA;
                        }
                        else
                        {
                            sentSerialBytes[j] = serialBytes[i];
                        }
                        j++;
                    }
                    sentSerialBytes[j] = 0xFF;
                    j++;
                    SerialObject.Write(sentSerialBytes, 0, j);
                    Recieve_Msg();

                    for (int k = 0; k < 4; k++)
                    {
                        serialBytes = new byte[5] { 0x1B, (byte)(1 << k), 0, 0, 0xFF };
                        checksum = serialBytes[0] + serialBytes[1];
                        serialBytes[2] = (byte)((checksum & 0xFF00) >> 8);
                        serialBytes[3] = (byte)(checksum & 0xFF);
                        j = 0;
                        sentSerialBytes = new byte[12];
                        for (int i = 0; i < 4; ++i)
                        {
                            sentSerialBytes[j] = serialBytes[i];
                            if (sentSerialBytes[j] == 0xFF)
                            {
                                sentSerialBytes[j] = 0xCD;
                                j++;
                                sentSerialBytes[j] = 0x55;
                            }
                            else if (sentSerialBytes[j] == 0xCD)
                            {
                                j++;
                                sentSerialBytes[j] = 0xAA;
                            }
                            else
                            {
                                sentSerialBytes[j] = serialBytes[i];
                            }
                            j++;
                        }
                        sentSerialBytes[j] = 0xFF;
                        j++;
                        SerialObject.Write(sentSerialBytes, 0, j);
                        Recieve_Msg();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UI.Controls.RemoveAt(2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Com_Port.SelectedIndex = comboBox3.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                try
                {
                    SerialObject = new SerialPort(Com_Port.SelectedItem.ToString(), 9600);
                    SerialObject.Open();
                    SerialObject.ReadTimeout = 20;      //0.2 second timeout
                    SerialObject.WriteTimeout = 20;        //0.2 second timeout
                    checkBox94.Checked = true;
                    checkBox9.Checked = true;
                    connected = true;
                }
                catch (Exception)
                {

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                checkBox94.Checked = false;
                checkBox9.Checked = false;
                connected = false;
                SerialObject.Close();
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void Recieve_Msg()
        {
            try
            {
                byte[] readBuffer = new byte[260];
                SerialObject.Read(readBuffer, 0, 1);
                int q = 1;
                for (; ; q++)
                {
                    if (readBuffer[q - 1] != 0xFF)
                    {
                        SerialObject.Read(readBuffer, q, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                int length = 0;
                for (int i = 0; i < q; i++)
                {
                    readBuffer[length] = readBuffer[i];
                    if (readBuffer[i] == 0xCA)
                    {
                        if (readBuffer[i + 1] == 0xAA)
                        {
                            readBuffer[length] = 0xCA;
                            ++i;
                        }
                        else
                        {
                            readBuffer[length] = 0xFF;
                            ++i;
                        }
                        i++;
                    }
                    length++;
                }
                UInt16 Lines;
                switch(readBuffer[0])
                {
                case 0x0F:
                        if (((readBuffer[2]) & 0x01) == 0x01)
                        {
                            checkBox6.Checked = false;
                            checkBox6.Text = "Ignition ON";
                            checkBox6.BackColor = Color.LightGreen;

                        }
                        else
                        {
                            checkBox6.Checked = true;
                            checkBox6.Text = "Ignition OFF";
                            checkBox6.BackColor = Color.Transparent;

                        }

                        if (((readBuffer[2]) & 0x02) == 0x02)
                        {
                            checkBox7.Checked = false;
                            checkBox7.Text = "Main Power ON";
                            checkBox7.BackColor = Color.LightGreen;

                        }
                        else
                        {
                            checkBox7.Checked = true;
                            checkBox7.Text = "Main Power OFF";
                            checkBox7.BackColor = Color.Transparent;

                        }
                    break;
                case 0x11:
                    Lines = (UInt16)((readBuffer[2] << 8) + readBuffer[3]);
                    for (int i = 0; i < 16; ++i)
                    {
                        if (((Lines >> i) & 0x0001) == 0x0001)
                        {
                            DigitalLines[i].Checked = false;
                            DigitalLines[i].Text = "Digital #" + (i + 1).ToString() + " ON";
                            DigitalLines[i].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            DigitalLines[i].Checked = true;
                            DigitalLines[i].Text = "Digital #" + (i + 1).ToString() + " OFF";
                            DigitalLines[i].BackColor = Color.Transparent;
                        }
                    }
                    break;
                case 0x13:
                    if ((readBuffer[2] & 0x80) == 0x80)
                    {
                        label106.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                        label23.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                    }
                    else
                    {
                        label106.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label23.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x40) == 0x40)
                    {
                        label107.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                        label24.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                    }
                    else
                    {
                        label107.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label24.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x20) == 0x20)
                    {
                        label104.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                        label25.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                    }
                    else
                    {
                        label104.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label25.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x10) == 0x10)
                    {
                        label105.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                        label26.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                    }
                    else
                    {
                        label105.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label26.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x08) == 0x08)
                    {
                        label102.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                        label27.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                    }
                    else
                    {
                        label102.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label27.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x04) == 0x04)
                    {
                        label103.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                        label32.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                    }
                    else
                    {
                        label103.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label32.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }
                    if ((readBuffer[2] & 0x02) == 0x02)
                    {
                        label100.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                        label33.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Red);
                    }
                    else
                    {
                        label100.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label33.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if ((readBuffer[2] & 0x01) == 0x01)
                    {
                        label98.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                        label34.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Yellow);
                    }
                    else
                    {
                        label98.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label34.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }
                    break;
                case 0x15:
                    Lines = (UInt16)((readBuffer[2] << 8) + readBuffer[3]);
                    for (int i = 0; i < 16; ++i)
                    {
                        if (((Lines >> i) & 0x0001) == 0x0001)
                        {
                            VideoLines[i].Checked = false;
                            VideoLines[i].Text = "Video #" + (i + 1).ToString() + " ON";
                            VideoLines[i].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            VideoLines[i].Checked = true;
                            VideoLines[i].Text = "Video #" + (i + 1).ToString() + " OFF";
                            VideoLines[i].BackColor = Color.Transparent;
                        }
                    }
                    break;
                case 0x17: 
                    Lines = (UInt16)((readBuffer[2] << 8) + readBuffer[3]);
                    for (int i = 0; i < 16; ++i)
                    {
                        if (((Lines >> i) & 0x0001) == 0x0001)
                        {
                            AudioLines[i].Checked = false;
                            AudioLines[i].Text = "Audio #" + (i + 1).ToString() + " ON";
                            AudioLines[i].BackColor = Color.LightGreen;
                        }
                        else
                        {
                            AudioLines[i].Checked = true;
                            AudioLines[i].Text = "Audio #" + (i + 1).ToString() + " OFF";
                            AudioLines[i].BackColor = Color.Transparent;
                        }
                    }
                    break;
                case 0x19:
                    if (((readBuffer[2]) & 0x01) == 0x01)
                    {
                        checkBox10.Checked = false;
                        checkBox10.Text = "Network ON";
                        checkBox10.BackColor = Color.LightGreen;

                    }
                    else
                    {
                        checkBox10.Checked = true;
                        checkBox10.Text = "Network OFF";
                        checkBox10.BackColor = Color.Transparent;

                    }
                    break;
                case 0x1B:

                    switch(readBuffer[2])
                    {
                        case 0x01:
                            textBox28.Text = readBuffer[3].ToString();
                            break;
                        case 0x02:
                            textBox27.Text = readBuffer[3].ToString();
                            break;
                        case 0x04:
                            textBox26.Text = readBuffer[3].ToString();
                            break;
                        case 0x08:
                            textBox25.Text = readBuffer[3].ToString();
                            break;
                    }
                    break;
                case 0x1C:
                    if (((readBuffer[2]) & 0x01) == 0x01)
                    {
                        label29.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Green1);
                        label20.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Green1);
                    }
                    else
                    {
                        label29.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label20.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }

                    if (((readBuffer[2]) & 0x02) == 0x02)
                    {
                        label16.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Green1);
                        label28.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Green1);
                    }
                    else
                    {
                        label16.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                        label28.Image = new Bitmap(DVR_Tester_User_Interface.Properties.Resources.Dark_Brown);
                    }
                    break;
                }
            }
            catch { }
        }

        private void checkBox111_CheckedChanged(object sender, EventArgs e)
        {

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
