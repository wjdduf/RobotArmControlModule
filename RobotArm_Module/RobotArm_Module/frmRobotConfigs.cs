//#define UNITY

using Newtonsoft.Json;
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
    public partial class frmRobotConfigs : Form
    {
        public RobotArmController RobotArmController;

        public CreateProcess CreateProcess;

        public PipeServer PipeServer;

        private bool isPush = false;
        private bool isJointPush = false;
        private bool isJointUp = true;

        private eDirection selectDir;
        private eRotationAxis selectAxis;
        private eJointType selectJoint;

        private bool isPosition = true;

        private float speed = 0.3f;
        private float acceleration = 1.2f;

        public frmRobotConfigs()
        {
            Start();

            InitializeComponent();

            UpdateLabelPeriodicallyAsync();
        }


        private void Start()
        {
            RobotArmController = new RobotArmController();
            RobotArmController.Initialize(eRobotArmType.UR);

            //TestCode
            //RobotArmController.Connect("192.168.1.40", 29999);
            //RobotArmController.MoveToPosition(new Vector3(1, 1, 1));

        }

        private async void UpdateLabelPeriodicallyAsync()
        {
            while (true)
            {
                await Task.Delay(100);

                ConnectCheck();

                SetCurrentData();
                if(isPosition)
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
                    CurPosX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.X.ToString("F3");
                }));
            }
            else
            {
                CurPosX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.X.ToString("F3");
            }
            if (CurPosY_TextBox.InvokeRequired)
            {
                CurPosY_TextBox.Invoke(new Action(() =>
                {
                    CurPosY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Y.ToString("F3");
                }));
            }
            else
            {
                CurPosY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Y.ToString("F3");
            }
            if (CurPosZ_TextBox.InvokeRequired)
            {
                CurPosZ_TextBox.Invoke(new Action(() =>
                {
                    CurPosZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Z.ToString("F3");
                }));
            }
            else
            {
                CurPosZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentPosition.Z.ToString("F3");
            }

            if (CurRotX_TextBox.InvokeRequired)
            {
                CurRotX_TextBox.Invoke(new Action(() =>
                {
                    CurRotX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.X.ToString("F3");
                }));
            }
            else
            {
                CurRotX_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.X.ToString("F3");
            }
            if (CurRotY_TextBox.InvokeRequired)
            {
                CurRotY_TextBox.Invoke(new Action(() =>
                {
                    CurRotY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Y.ToString("F3");
                }));
            }
            else
            {
                CurRotY_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Y.ToString("F3");
            }
            if (CurRotZ_TextBox.InvokeRequired)
            {
                CurRotZ_TextBox.Invoke(new Action(() =>
                {
                    CurRotZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Z.ToString("F3");
                }));
            }
            else
            {
                CurRotZ_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentRotation.Z.ToString("F3");
            }

            //////////////////////////////////////////////////

            if (CurBase_Textbox.InvokeRequired)
            {
                CurBase_Textbox.Invoke(new Action(() =>
                {
                    CurBase_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.BASE].Angle.ToString("F3");
                }));
            }
            else
            {
                CurBase_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.BASE].Angle.ToString("F3");
            }
            if (CurShoulder_TextBox.InvokeRequired)
            {
                CurShoulder_TextBox.Invoke(new Action(() =>
                {
                    CurShoulder_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.SHOULDER].Angle.ToString("F3");
                }));
            }
            else
            {
                CurShoulder_TextBox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.SHOULDER].Angle.ToString("F3");
            }
            if (CurElbow_Textbox.InvokeRequired)
            {
                CurElbow_Textbox.Invoke(new Action(() =>
                {
                    CurElbow_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.ELBOW].Angle.ToString("F3");
                }));
            }
            else
            {
                CurElbow_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.ELBOW].Angle.ToString("F3");
            }
            if (CurWrist1_Textbox.InvokeRequired)
            {
                CurWrist1_Textbox.Invoke(new Action(() =>
                {
                    CurWrist1_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST1].Angle.ToString("F3");
                }));
            }
            else
            {
                CurWrist1_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST1].Angle.ToString("F3");
            }
            if (CurWrist2_Textbox.InvokeRequired)
            {
                CurWrist2_Textbox.Invoke(new Action(() =>
                {
                    CurWrist2_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST2].Angle.ToString("F3");
                }));
            }
            else
            {
                CurWrist2_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST2].Angle.ToString("F3");
            }
            if (CurWrist3_Textbox.InvokeRequired)
            {
                CurWrist3_Textbox.Invoke(new Action(() =>
                {
                    CurWrist3_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST3].Angle.ToString("F3");
                }));
            }
            else
            {
                CurWrist3_Textbox.Text = DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[eJointType.WRIST3].Angle.ToString("F3");
            }

        }

        public void PowerOn_Click(object sender, EventArgs e)
        {
            PowerOn_Click();
        }

        public int PowerOn_Click()
        {
            int isSuccess = 0;

            isSuccess = RobotArmController.Connect(DataContainer.Instance.URConfig.IP) ? 1 : 0;

            return isSuccess;
        }

        private void PowerOff_Click(object sender, EventArgs e)
        {
            PowerOff_Click();
        }

        public int PowerOff_Click()
        {
            int isSuccess = 0;

            isSuccess = RobotArmController.DisConnect() ? 1 : 0;

            return isSuccess;
        }



        private void Float3FormatHandler(object sender, ConvertEventArgs e)
        {
            if (e.Value is float f)
                e.Value = f.ToString("F3");
            else
                e.Value = "0.000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TestCode

            //RobotArmController.MoveToPreset(new Vector3(-0.150f, 0.600f, 0.650f), new Vector3(0, 0, 6));

            RobotArmController.RobotArmBuilder.CurrentRobotArm.TestCode("set_tcp(p[0.1, 0, 0, 0, 0, 0])");
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

                RobotArmController.MoveToPosition(speed,direction);
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
            if(isJointPush)
            {
                RobotArmController.MoveToJoint(speed, isJointUp, type);
            }
        }

        private void SetPositionButton(object sender, EventArgs e)
        {
            SetPositionButton();
        }

        public int SetPositionButton()
        {
            int result = 0;
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetPositionJ();

                result = 1;
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }

            return result;
        }

        private void SetPositionLButton(object sender, EventArgs e)
        {
            SetPositionLButton();
        }

        public int SetPositionLButton()
        {
            int result = 0;
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetPositionL();

                result = 1;
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }

            return result;
        }

        private void SetJoint_Button_Click(object sender, EventArgs e)
        {
            SetJoint_Button_Click();
        }

        public int SetJoint_Button_Click()
        {
            int result = 0;
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text));

                RobotArmController.SetJoint();

                result = 1;
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }

            return result;
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
            else if(button.Name.Contains("XNegative"))
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
            selectJoint = (eJointType)Joint_ComboBox.SelectedIndex + 1 ;
            Debug.Log(selectJoint.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Array jointList = Enum.GetValues(typeof(eJointType));

            foreach (eJointType joint in jointList)
            {
                Console.WriteLine($"Enum 멤버: {joint}");
                if(joint != eJointType.None)
                {
                    Joint_ComboBox.Items.Add(joint);
                }

            }


            //Unity 생성
            CreateProcess = new CreateProcess();
#if UNITY
            CreateProcess.CreateUnity(UnityPanel, "Winform_MergeTest.exe");
#endif
            PipeServer = new PipeServer();
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Debug.Log("Form1_FormClosing");
            PipeServer.DisconnectPipe();
            CreateProcess.ProcessClose();
        }

        private void JointAngle_Button_Down(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;

            if(button.Name.Contains("Up"))
            {
                isJointUp = true;
            }
            else if(button.Name.Contains("Down"))
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
        
        public int RotationJoint_Button_Click()
        {
            float angle;
            float.TryParse(JointAngle_TextBox.Text, out angle);
            
            return RobotArmController.JointRotation(angle, selectJoint);
        }

        private void SetPivot_Button_Click(object sender, EventArgs e)
        {
            SetPivot_Button_Click();
        }
        
        public int SetPivot_Button_Click()
        {
            Vector3 pivot = new Vector3();
            pivot.X = float.Parse(SetPivotX_TextBox.Text);
            pivot.Y = float.Parse(SetPivotY_TextBox.Text);
            pivot.Z = float.Parse(SetPivotZ_TextBox.Text);
            //
            return RobotArmController.SetPivot(pivot);
        }

        private void PivotReset_Button_Click(object sender, EventArgs e)
        {
            PivotReset_Button_Click();
        }

        public int PivotReset_Button_Click()
        {
            return RobotArmController.SetPivot(Vector3.Zero());
        }

        private void ListStop_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.WorkQueueClear();
            RobotArmController.Stop();
        }

        public int ListStop_Button_Click()
        {
            RobotArmController.WorkQueueClear();
            return RobotArmController.Stop();
        }

        private void ListDelete_Button_Click(object sender, EventArgs e)
        {
            //if(WorkQue_ListBox.SelectedIndex != -1)
            //{
            //    string id = RemoveAfterCharacter(WorkQue_ListBox.Items[WorkQue_ListBox.SelectedIndex].ToString(), ':');
            //    
            //    DataContainer.Instance.WorkPreset.Delete(id);
            //
            //    WorkQue_ListBox.Items.RemoveAt(WorkQue_ListBox.SelectedIndex);
            //
            //    foreach(var item in DataContainer.Instance.WorkPreset.WorkList)
            //    {
            //        Debug.Log($"Key: {item.presetID}, Value: {item.presetName}");
            //    }
            //}
        }

        private void ListPlay_Button_Click(object sender, EventArgs e)
        {
            //RobotArmController.ListPlay(JsonName_textBox.Text);

            ListPlay_Button_Click();

        }

        public int ListPlay_Button_Click()
        {
            Vector3 fromPos = new Vector3();

            fromPos.X = float.Parse(LoopFromPosX_TextBox.Text);
            fromPos.Y = float.Parse(LoopFromPosY_TextBox.Text);
            fromPos.Z = float.Parse(LoopFromPosZ_TextBox.Text);

            Vector3 toPos = new Vector3();

            toPos.X = float.Parse(LoopToPosX_TextBox.Text);
            toPos.Y = float.Parse(LoopToPosY_TextBox.Text);
            toPos.Z = float.Parse(LoopToPosZ_TextBox.Text);

            bool isLoop = MoveLoop_CheckBox.Checked;

            float loopTime = float.Parse(MoveLoopTime_TextBox.Text);

            return RobotArmController.PlayLoop(fromPos, toPos, isLoop, loopTime);
        }

        

        

        private void ListUpdate()
        {
            //WorkQue_ListBox.Items.Clear();
            //
            //foreach (var item in DataContainer.Instance.WorkPreset.WorkList)
            //{
            //    Debug.Log($"Key: {item.presetID}, Value: {item.presetName}");
            //    WorkQue_ListBox.Items.Add(item.presetName);
            //    
            //}
        }

        public static string RemoveAfterCharacter(string input, char separator)
        {
            // 1. 기준 문자의 위치를 찾습니다.
            int index = input.IndexOf(separator);

            // 2. 기준 문자가 문자열에 존재하는지 확인합니다.
            if (index != -1)
            {
                // 3. 기준 문자가 있는 경우, 그 위치까지의 문자열을 잘라냅니다.
                return input.Substring(0, index);
            }
            else
            {
                // 4. 기준 문자가 없는 경우, 원본 문자열을 그대로 반환합니다.
                return input;
            }
        }

        private void Homming_Button_Click(object sender, EventArgs e)
        {
            Homming_Button_Click();
        }

        public int Homming_Button_Click()
        {
            return RobotArmController.SetHoming();
        }

        

        private void IPCTest_Button_Click(object sender, EventArgs e)
        {
            float angle;
            float.TryParse(JointAngle_TextBox.Text, out angle);

            PipeData data = new PipeData();
            data.Command = selectJoint.ToString();
            data.Value = angle.ToString();
            string temp = JsonConvert.SerializeObject(data);
            PipeServer.Send(temp);
        }

        private void Export_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.ExportJson(JsonName_textBox.Text);
        }

        private void Import_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.ImportJson(JsonName_textBox.Text);
            ListUpdate();
        }

        private void UnlockProtectiveStop(object sender, EventArgs e)
        {
            UnlockProtectiveStop();
        }

        public int UnlockProtectiveStop()
        {
            if (!RobotArmController.GetSafetyMode())
            {
                RobotArmController.UnlockProtectiveStop();
            }
            return 1;
        }

        private void CSVButton_Click(object sender, EventArgs e)
        {
            RobotArmController.LoadCSV(DataContainer.Instance.JsonPath);
        }

        public int CSVButton_Click()
        {
            var data = RobotArmController.LoadCSV(DataContainer.Instance.JsonPath);
            return RobotArmController.PlayCSV(data);
        }

        public void GripButton_Click(object sender, EventArgs e)
        {
            GripButton_Click();
        }

        public int GripButton_Click()
        {
            return RobotArmController.Grip();
        }

        public void Release_Button_Click(object sender, EventArgs e)
        {
            Release_Button_Click();
        }

        public int Release_Button_Click()
        {
            return RobotArmController.Release();
        }

        public void PhoneGrip01_Button_Click(object sender, EventArgs e)
        {
            PhoneGrip01_Button_Click();
        }

        public int PhoneGrip01_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneGrip01");
        }

        public void PhoneGrip02_Button_Click(object sender, EventArgs e)
        {
            PhoneGrip02_Button_Click();
        }

        public int PhoneGrip02_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneGrip02");
        }

        public void PhoneGrip03_Button_Click(object sender, EventArgs e)
        {
            PhoneGrip03_Button_Click();
        }

        public int PhoneGrip03_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneGrip03");
        }

        public void PhoneGrip04_Button_Click(object sender, EventArgs e)
        {
            PhoneGrip04_Button_Click();
        }

        public int PhoneGrip04_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneGrip04");
        }

        public void PhoneRelease01_Button_Click(object sender, EventArgs e)
        {
            PhoneRelease01_Button_Click();
        }

        public int PhoneRelease01_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneRelease01");
        }

        public void PhoneRelease02_Button_Click(object sender, EventArgs e)
        {
            PhoneRelease02_Button_Click();
        }

        public int PhoneRelease02_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneRelease02");
        }

        public void PhoneRelease03_Button_Click(object sender, EventArgs e)
        {
            PhoneRelease03_Button_Click();
        }

        public int PhoneRelease03_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneRelease03");
        }

        public void PhoneRelease04_Button_Click(object sender, EventArgs e)
        {
            PhoneRelease04_Button_Click();
        }

        public int PhoneRelease04_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneRelease04");
        }

        public void PhoneReady_Button_Click(object sender, EventArgs e)
        {
            PhoneReady_Button_Click();
        }

        public int PhoneReady_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneReady");
        }

        public void PhoneFont_Button_Click(object sender, EventArgs e)
        {
            PhoneFont_Button_Click();
        }

        public int PhoneFont_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneFront");
        }

        public void PhoneBack_Button_Click(object sender, EventArgs e)
        {
            PhoneBack_Button_Click();
        }

        public int PhoneBack_Button_Click()
        {
            return RobotArmController.PlayWork("Work_PhoneBack");
        }

        public void Home_Button_Click(object sender, EventArgs e)
        {
            Home_Button_Click();
        }

        public int Home_Button_Click()
        {
            return RobotArmController.PlayWork("Work_Home");
        }

        public void StickGrip01_Button_Click(object sender, EventArgs e)
        {
            StickGrip01_Button_Click();
        }

        public int StickGrip01_Button_Click()
        {
            return RobotArmController.PlayWork("Work_StickGrip");
        }

        public void StickReturn_Button_Click(object sender, EventArgs e)
        {
            StickReturn_Button_Click();
        }

        public int StickReturn_Button_Click()
        {
            return RobotArmController.PlayWork("Work_StickReturn");
        }

        public void StickSet_Button_Click(object sender, EventArgs e)
        {
            StickSet_Button_Click();

        }

        public int StickSet_Button_Click()
        {
            return RobotArmController.PlayWork("Work_StickSet");
        }

        public void StickSetGrip_Button_Click(object sender, EventArgs e)
        {
            StickSetGrip_Button_Click();

        }

        public int StickSetGrip_Button_Click()
        {
            return RobotArmController.PlayWork("Work_StickSetGrip");
        }

        public void Alignment_Button_Click(object sender, EventArgs e)
        {
            Alignment_Button_Click();
        }

        public int Alignment_Button_Click()
        {
            Vector3 pivot = new Vector3();
            pivot.X = float.Parse(SetPivotX_TextBox.Text);
            pivot.Y = float.Parse(SetPivotY_TextBox.Text);
            pivot.Z = float.Parse(SetPivotZ_TextBox.Text);

            Vector3 rot = new Vector3();
            rot.X = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignX_TextBox.Text));
            rot.Y = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignY_TextBox.Text));
            rot.Z = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignZ_TextBox.Text));

            //
            return RobotArmController.SetDeviceAlignment(pivot, rot);
        }
    }
}
