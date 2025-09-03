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
            InitializeComponent();
            Start();
        }



        private void Start()
        {
            RobotArmController = new RobotArmController();
            RobotArmController.Initialize(eRobotArmType.UR);

            //TestCode
            RobotArmController.Connect("12355", "12355");
            RobotArmController.MoveToPosition(new Vector3(1, 1, 1));

        }
    }
}
