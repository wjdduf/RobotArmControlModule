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
    public partial class WarningReset_Button : Form
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

        private float speed = 0.1f;
        private float acceleration = 1.2f;

        public WarningReset_Button()
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
            RobotArmController.MoveToPreset(new Vector3(0.150f, 0.600f, 0.650f), new Vector3(0, 0, 6));
        }

        
        private void MoveToBack_Click(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(0.150f, 0.300f, 0.650f), new Vector3(0, 0, 6));


        }

        private void MoveToFront2_Click(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(0.133f, 0.524f, 0.650f), new Vector3(4.766f, 0.010f, 0.010f));

        }

        private void MoveToBack_Click2(object sender, EventArgs e)
        {
            RobotArmController.MoveToPreset(new Vector3(0.150f, 0.300f, 0.650f), new Vector3(4.766f, 0.010f, 0.010f));

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
            try
            {
                DataContainer.Instance.RobotArmCurrentData.SetPosition = new Vector3(float.Parse( SetPosX_TextBox.Text), float.Parse( SetPosY_TextBox.Text), float.Parse( SetPosZ_TextBox.Text));
                DataContainer.Instance.RobotArmCurrentData.SetRotation = new Vector3(float.Parse( SetRotX_TextBox.Text), float.Parse( SetRotY_TextBox.Text), float.Parse( SetRotZ_TextBox.Text));



                RobotArmController.Move();
            }
            catch(Exception exception)
            {
                Debug.Log(exception.ToString());
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
            float angle;
            float.TryParse(JointAngle_TextBox.Text, out angle);
            RobotArmController.JointRotation(angle,selectJoint);
        }

        private void SetPivot_Button_Click(object sender, EventArgs e)
        {
            Vector3 pivot = new Vector3();
            pivot.X = float.Parse( SetPivotX_TextBox.Text);
            pivot.Y = float.Parse(SetPivotY_TextBox.Text);
            pivot.Z = float.Parse(SetPivotZ_TextBox.Text);
            //
            RobotArmController.SetPivot(pivot);

        }

        private void PivotReset_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.SetPivot(Vector3.Zero());
        }

        private void ListStop_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.Stop();
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
            RobotArmController.ListPlay(JsonName_textBox.Text);
        }

        

        private void ListAdd_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.AddPlayList(new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text)), new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text)));
            ListUpdate();
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
            RobotArmController.SetHoming();
        }

        private void JointListAdd_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.AddPlayList(new Vector3(float.Parse(SetPosX_TextBox.Text), float.Parse(SetPosY_TextBox.Text), float.Parse(SetPosZ_TextBox.Text)), new Vector3(float.Parse(SetRotX_TextBox.Text), float.Parse(SetRotY_TextBox.Text), float.Parse(SetRotZ_TextBox.Text)), eMoveType.Joint);
            ListUpdate();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(!RobotArmController.GetSafetyMode())
            {
                RobotArmController.UnlockProtectiveStop();
            }
        }

        private void CSVButton_Click(object sender, EventArgs e)
        {
            RobotArmController.LoadCSV(DataContainer.Instance.JsonPath);
        }

        private void GripButton_Click(object sender, EventArgs e)
        {
            RobotArmController.Grip();
        }

        private void Release_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.Release();
        }

        private void WorkPlay_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneGrip01");
        }

        private void PhoneGrip02_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneGrip02");
        }

        private void PhoneGrip03_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneGrip03");
        }

        private void PhoneGrip04_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneGrip04");
        }

        private void PhoneRelease02_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneRelease02");
        }

        private void PhoneRelease01_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneRelease01");
        }

        private void PhoneRelease03_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneRelease03");
        }

        private void PhoneRelease04_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneRelease04");
        }

        private void PhoneReady_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneReady");
        }

        private void PhoneFont_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneFront");
        }

        private void PhoneBack_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_PhoneBack");
        }

        private void Home_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_Home");
        }

        private void StickGrip01_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_StickGrip");
        }

        private void StickReturn_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_StickReturn");
        }

        private void StickSet_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_StickSet");

        }

        private void StickSetGrip_Button_Click(object sender, EventArgs e)
        {
            RobotArmController.PlayWork("Work_StickSetGrip");

        }

        private void Alignment_Button_Click(object sender, EventArgs e)
        {
            Vector3 pivot = new Vector3();
            pivot.X = float.Parse(SetPivotX_TextBox.Text);
            pivot.Y = float.Parse(SetPivotY_TextBox.Text);
            pivot.Z = float.Parse(SetPivotZ_TextBox.Text);

            Vector3 rot = new Vector3();
            rot.X = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignX_TextBox.Text));
            rot.Y = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignX_TextBox.Text));
            rot.Z = (float)RobotArm.DegreesToRadians(double.Parse(SetAlignX_TextBox.Text));

            //
            RobotArmController.SetDeviceAlignment(pivot, rot);
        }
    }
}
