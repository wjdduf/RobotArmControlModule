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
    public partial class RBUI : Form
    {
        public RobotArmController RobotArmController;

        private bool isPush = false;
        private bool isJointPush = false;
        private bool isJointUp = true;

        private eDirection selectDir;
        private eRotationAxis selectAxis;
        private eJointType selectJoint;

        private bool isPosition = true;

        private float speed = 0.3f;
        private float acceleration = 1.2f;

        public double m_dCurPosRobotX = 0, m_dCurPosRobotY = 0, m_dCurPosRobotZ = 0, m_dCurPosRobotRotX = 0, m_dCurPosRobotRotY = 0, m_dCurPosRobotRotZ = 0;
        public double m_dCurDegreeBase = 0, m_dCurDegreeShoulder = 0, m_dCurDegreeElbow = 0, m_dCurDegreeWrist1 = 0, m_dCurDegreeWrist2 = 0, m_dCurDegreeWrist3 = 0;


        public RBUI()
        {
            Start();
            InitializeComponent();
            UpdateLabelPeriodicallyAsync();
        }

        private void Start()
        {
            RobotArmController = new RobotArmController();
            RobotArmController.Initialize(eRobotArmType.RB);
        }

        private async void UpdateLabelPeriodicallyAsync()
        {
            while (true)
            {
                await Task.Delay(100);

                ConnectCheck();
                SetCurrentData();

                if (isPosition)
                {
                    MoveToPosition(speed, selectDir);
                }
                else
                {
                    MoveToRotation(speed, selectAxis);
                }

                MoveToJoint(speed, selectJoint);
            }
        }

        private void ConnectCheck()
        {
            if (DataContainer.Instance.RobotArmCurrentData.isConnect)
            {
                ConnectCheck_Text.Text = "Connect";
            }
            else
            {
                ConnectCheck_Text.Text = "Disconnect";

            }
        }

        private void SetCurrentData()
        {
            // UI 스레드에서 label1.Text 갱신
            if (CurPosX_TextBox.InvokeRequired)
            {
                CurPosX_TextBox.Invoke(new Action(() =>
                {
                    CurPosX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.X.ToString("F4");
                }));
            }
            else
            {
                CurPosX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.X.ToString("F4");
            }
            if (CurPosY_TextBox.InvokeRequired)
            {
                CurPosY_TextBox.Invoke(new Action(() =>
                {
                    CurPosY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Y.ToString("F4");
                }));
            }
            else
            {
                CurPosY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Y.ToString("F4");
            }
            if (CurPosZ_TextBox.InvokeRequired)
            {
                CurPosZ_TextBox.Invoke(new Action(() =>
                {
                    CurPosZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Z.ToString("F4");
                }));
            }
            else
            {
                CurPosZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Z.ToString("F4");
            }

            if (CurRotX_TextBox.InvokeRequired)
            {
                CurRotX_TextBox.Invoke(new Action(() =>
                {
                    CurRotX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.X.ToString("F4");
                }));
            }
            else
            {
                CurRotX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.X.ToString("F4");
            }
            if (CurRotY_TextBox.InvokeRequired)
            {
                CurRotY_TextBox.Invoke(new Action(() =>
                {
                    CurRotY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Y.ToString("F4");
                }));
            }
            else
            {
                CurRotY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Y.ToString("F4");
            }
            if (CurRotZ_TextBox.InvokeRequired)
            {
                CurRotZ_TextBox.Invoke(new Action(() =>
                {
                    CurRotZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Z.ToString("F4");
                }));
            }
            else
            {
                CurRotZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Z.ToString("F4");
            }

            //////////////////////////////////////////////////

            if (CurBase_Textbox.InvokeRequired)
            {
                CurBase_Textbox.Invoke(new Action(() =>
                {
                    CurBase_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.BASE].Angle.ToString("F4");
                }));
            }
            else
            {
                CurBase_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.BASE].Angle.ToString("F4");
            }
            if (CurShoulder_TextBox.InvokeRequired)
            {
                CurShoulder_TextBox.Invoke(new Action(() =>
                {
                    CurShoulder_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.SHOULDER].Angle.ToString("F4");
                }));
            }
            else
            {
                CurShoulder_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.SHOULDER].Angle.ToString("F4");
            }
            if (CurElbow_Textbox.InvokeRequired)
            {
                CurElbow_Textbox.Invoke(new Action(() =>
                {
                    CurElbow_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.ELBOW].Angle.ToString("F4");
                }));
            }
            else
            {
                CurElbow_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.ELBOW].Angle.ToString("F4");
            }
            if (CurWrist1_Textbox.InvokeRequired)
            {
                CurWrist1_Textbox.Invoke(new Action(() =>
                {
                    CurWrist1_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST1].Angle.ToString("F4");
                }));
            }
            else
            {
                CurWrist1_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST1].Angle.ToString("F4");
            }
            if (CurWrist2_Textbox.InvokeRequired)
            {
                CurWrist2_Textbox.Invoke(new Action(() =>
                {
                    CurWrist2_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST2].Angle.ToString("F4");
                }));
            }
            else
            {
                CurWrist2_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST2].Angle.ToString("F4");
            }
            if (CurWrist3_Textbox.InvokeRequired)
            {
                CurWrist3_Textbox.Invoke(new Action(() =>
                {
                    CurWrist3_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST3].Angle.ToString("F4");
                }));
            }
            else
            {
                CurWrist3_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST3].Angle.ToString("F4");
            }

            //현재위치 정보 공유
            m_dCurPosRobotX = Convert.ToDouble(CurPosX_TextBox.Text);
            m_dCurPosRobotY = Convert.ToDouble(CurPosY_TextBox.Text);
            m_dCurPosRobotZ = Convert.ToDouble(CurPosZ_TextBox.Text);
            m_dCurPosRobotRotX = Convert.ToDouble(CurRotX_TextBox.Text);
            m_dCurPosRobotRotY = Convert.ToDouble(CurRotY_TextBox.Text);
            m_dCurPosRobotRotZ = Convert.ToDouble(CurRotZ_TextBox.Text);

            m_dCurDegreeBase = Convert.ToDouble(CurBase_Textbox.Text);
            m_dCurDegreeShoulder = Convert.ToDouble(CurShoulder_TextBox.Text);
            m_dCurDegreeElbow = Convert.ToDouble(CurElbow_Textbox.Text);
            m_dCurDegreeWrist1 = Convert.ToDouble(CurWrist1_Textbox.Text);
            m_dCurDegreeWrist2 = Convert.ToDouble(CurWrist2_Textbox.Text);
            m_dCurDegreeWrist3 = Convert.ToDouble(CurWrist3_Textbox.Text);
        }

        public void PowerOn_Click(object sender, EventArgs e)
        {
            PowerOn_Click();
        }

        public void PowerOn_Click(Action<bool> onComplete = null)
        {

            RobotArmController.Connect(DataContainer.Instance.URConfig.IP, onComplete);

        }

        private void PowerOff_Click(object sender, EventArgs e)
        {
            PowerOff_Click();
        }

        public void PowerOff_Click(Action<bool> onComplete = null)
        {
            RobotArmController.DisConnect(onComplete);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TestCode

            //RobotArmController.MoveToPreset(new Vector3(-0.150f, 0.600f, 0.650f), new Vector3(0, 0, 6));

            //RobotArmController.RobotArmBuilder.CurrentRobotArm.TestCode("set_tcp(p[0.1, 0, 0, 0, 0, 0])");
            RobotArmController.PlayWork("Work_Test");

        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

            Debug.Log("button1_Click Down");
            isPush = true;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            Debug.Log("button1_Click Up");

            isPush = false;

            RobotArmController.Stop();
        }
        private void MoveToPosition(float speed, eDirection direction)
        {
            if (isPush)
            {
                //speed = 0.1f;

                RobotArmController.MoveToPosition(speed, direction);
            }
        }

        private void MoveToRotation(float speed, eRotationAxis axis)
        {
            if (isPush)
            {
                //speed = 0.1f;

                RobotArmController.MoveToRotation(speed, axis);
            }
        }

        private void MoveToJoint(float speed, eJointType type)
        {
            if (isJointPush)
            {
                RobotArmController.MoveToJoint(speed, isJointUp, type);
            }
        }

        private void SetPositionButton(object sender, EventArgs e)
        {
            SetPositionButton();
        }

        public void SetPositionButton(Action<bool> onComplete = null)
        {
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetPositionJ(onComplete);

            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
                onComplete?.Invoke(false);
            }

        }

        private void SetPositionLButton(object sender, EventArgs e)
        {
            SetPositionLButton();
        }

        public void SetPositionLButton(Action<bool> onComplete = null)
        {
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetPositionL(onComplete);

            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
                onComplete?.Invoke(false);

            }

        }

        private void SetJoint_Button_Click(object sender, EventArgs e)
        {
            SetJoint_Button_Click();
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.Stop();
        }

        public void SetJoint_Button_Click(Action<bool> onComplete = null)
        {
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetJoint(onComplete);

            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
                onComplete?.Invoke(false);

            }

        }

        private void MoveButtonDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            Debug.Log(button.Name);


            if (button.Name.Contains("XPositive"))
            {
                selectDir = eDirection.X_Positive;
                selectAxis = eRotationAxis.X_Axis_Positive;
            }
            else if (button.Name.Contains("XNegative"))
            {
                selectDir = eDirection.X_Negative;
                selectAxis = eRotationAxis.X_Axis_Negative;

            }
            if (button.Name.Contains("YPositive"))
            {
                selectDir = eDirection.Y_Positive;
                selectAxis = eRotationAxis.Y_Axis_Positive;

            }
            else if (button.Name.Contains("YNegative"))
            {
                selectDir = eDirection.Y_Negative;
                selectAxis = eRotationAxis.Y_Axis_Negative;

            }
            if (button.Name.Contains("ZPositive"))
            {
                selectDir = eDirection.Z_Positive;
                selectAxis = eRotationAxis.Z_Axis_Positive;

            }
            else if (button.Name.Contains("ZNegative"))
            {
                selectDir = eDirection.Z_Negative;
                selectAxis = eRotationAxis.Z_Axis_Negative;

            }
            isPush = true;
        }

        

        private void MoveButtonUp(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            Debug.Log(button.Name);

            isPush = false;
            RobotArmController.Stop();
        }

        private void Arrow_Radio_Button_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;

            if (button.Checked)
            {
                // 선택된 라디오 버튼의 텍스트를 기준으로 작업 수행
                switch (button.Text)
                {
                    case "Position":
                        Console.WriteLine("Position.");
                        isPosition = true;
                        break;
                    case "Rotation":
                        Console.WriteLine("Rotation.");
                        isPosition = false;

                        break;

                }
            }
        }

        private void Speed_Textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            float temp;

            if (float.TryParse(Speed_Textbox.Text.ToString(), out temp))
            {
                this.speed = temp;
                RobotArmController.SetSpeed(speed);
            }
        }

        private void Acceleration_TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            float temp;

            if (float.TryParse(Acceleration_TextBox.Text.ToString(), out temp))
            {
                this.acceleration = temp;
                RobotArmController.SetAcceleration(acceleration);
            }
        }

        private void myTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 1. 숫자, 백스페이스, 그리고 제어 키는 허용
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || char.IsControl(e.KeyChar))
            {
                return;
            }

            // 2. 음수 부호(-)는 맨 앞에서 한 번만 허용
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '-' && textBox.SelectionStart == 0 && !textBox.Text.Contains("-"))
            {
                return;
            }

            // 3. 소수점(.)은 한 번만 허용
            if (e.KeyChar == '.' && !textBox.Text.Contains("."))
            {
                return;
            }

            // 4. 위에 해당하지 않는 모든 문자는 차단
            e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectJoint = (eJointType)Joint_ComboBox.SelectedIndex + 1;
            Debug.Log(selectJoint.ToString());
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Array jointList = Enum.GetValues(typeof(eJointType));

            foreach (eJointType joint in jointList)
            {
                Console.WriteLine($"Enum 멤버: {joint}");
                if (joint != eJointType.None)
                {
                    Joint_ComboBox.Items.Add(joint);
                }

            }
        }

        private void JointAngle_Button_Down(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            if (button.Name.Contains("Up"))
            {
                isJointUp = true;
            }
            else if (button.Name.Contains("Down"))
            {
                isJointUp = false;
            }

            isJointPush = true;
        }

        private void JointAngle_Button_Up(object sender, MouseEventArgs e)
        {
            isJointPush = false;
            RobotArmController.Stop();

        }

        private void RotationJoint_Button_Click(object sender, EventArgs e)
        {
            RotationJoint_Button_Click();
        }

        public void RotationJoint_Button_Click(Action<bool> onComplete = null)
        {
            float angle;
            float.TryParse(JointAngle_TextBox.Text, out angle);

            RobotArmController.JointRotation(angle, selectJoint, onComplete);
        }
    }
}
