using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm_Module
{
    public partial class Form1 : Form
    {
        public RobotArmController RobotArmController;

        public Form1()
        {
            Start();

            InitializeComponent();
        }



        private void Start()
        {
            RobotArmController = new RobotArmController();
            RobotArmController.Initialize(eRobotArmType.UR);

            //TestCode
            //RobotArmController.Connect("192.168.1.40", 29999);
            //RobotArmController.MoveToPosition(new Vector3(1, 1, 1));

        }

        private void PowerOn_Click(object sender, EventArgs e)
        {
            RobotArmController.Connect(DataContainer.Instance.URConfig.IP);

        }

        private void PowerOff_Click(object sender, EventArgs e)
        {
            RobotArmController.DisConnect();
        }


        private void MoveToFront_Click(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(-0.151f, 0.601f, 0.651f), new Vector3(0, 0, 6));
        }

        
        private void MoveToBack_Click(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(-0.151f, 0.301f, 0.651f), new Vector3(0, 0, 6));


        }

        private void MoveToFront2_Click(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(-0.151f, 0.601f, 0.651f), new Vector3(0.4f, -2.3f, -2.3f));

        }

        private void MoveToBack_Click2(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(-0.151f, 0.301f, 0.651f), new Vector3(0.4f, -2.3f, -2.3f));

        }


    }
}
