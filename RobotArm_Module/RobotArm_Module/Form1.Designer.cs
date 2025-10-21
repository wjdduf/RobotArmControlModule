
using System;

namespace RobotArm_Module
{
    partial class WarningReset_Button
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.MoveToFront = new System.Windows.Forms.Button();
            this.MoveToBack = new System.Windows.Forms.Button();
            this.PowerOn = new System.Windows.Forms.Button();
            this.PowerOff = new System.Windows.Forms.Button();
            this.MoveToBack2 = new System.Windows.Forms.Button();
            this.MoveToFront2 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.button1 = new System.Windows.Forms.Button();
            this.CurPosX_TextBox = new System.Windows.Forms.TextBox();
            this.CurrentPosTitle = new System.Windows.Forms.Label();
            this.CurPosX_Text = new System.Windows.Forms.Label();
            this.CurPosY_Text = new System.Windows.Forms.Label();
            this.CurPosY_TextBox = new System.Windows.Forms.TextBox();
            this.CurPosZ_Text = new System.Windows.Forms.Label();
            this.CurPosZ_TextBox = new System.Windows.Forms.TextBox();
            this.CurRotX_Text = new System.Windows.Forms.Label();
            this.CurRotX_TextBox = new System.Windows.Forms.TextBox();
            this.CurRotY_Text = new System.Windows.Forms.Label();
            this.CurRotY_TextBox = new System.Windows.Forms.TextBox();
            this.CurRotZ_Text = new System.Windows.Forms.Label();
            this.CurRotZ_TextBox = new System.Windows.Forms.TextBox();
            this.SetRotZ_Text = new System.Windows.Forms.Label();
            this.SetRotZ_TextBox = new System.Windows.Forms.TextBox();
            this.SetRotY_Text = new System.Windows.Forms.Label();
            this.SetRotY_TextBox = new System.Windows.Forms.TextBox();
            this.SetRotX_Text = new System.Windows.Forms.Label();
            this.SetRotX_TextBox = new System.Windows.Forms.TextBox();
            this.SetPosZ_Text = new System.Windows.Forms.Label();
            this.SetPosZ_TextBox = new System.Windows.Forms.TextBox();
            this.SetPosY_Text = new System.Windows.Forms.Label();
            this.SetPosY_TextBox = new System.Windows.Forms.TextBox();
            this.SetPosX_Text = new System.Windows.Forms.Label();
            this.SetPosTitle = new System.Windows.Forms.Label();
            this.SetPosX_TextBox = new System.Windows.Forms.TextBox();
            this.button2f = new System.Windows.Forms.Button();
            this.ZPositive_Button = new System.Windows.Forms.Button();
            this.ZNegative_Button = new System.Windows.Forms.Button();
            this.XNegative_Button = new System.Windows.Forms.Button();
            this.XPositive_Button = new System.Windows.Forms.Button();
            this.YPositive_Button = new System.Windows.Forms.Button();
            this.YNegative_Button = new System.Windows.Forms.Button();
            this.Position_Radio_Button = new System.Windows.Forms.RadioButton();
            this.Rotation_Radio_Button = new System.Windows.Forms.RadioButton();
            this.ArrowGroupBox = new System.Windows.Forms.GroupBox();
            this.Speed_Textbox = new System.Windows.Forms.TextBox();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.Joint_ComboBox = new System.Windows.Forms.ComboBox();
            this.RotationJoint_Button = new System.Windows.Forms.Button();
            this.JointAngleDown_Button = new System.Windows.Forms.Button();
            this.JointAngleUp_Button = new System.Windows.Forms.Button();
            this.JointAngle_TextBox = new System.Windows.Forms.TextBox();
            this.Wrist3Joint_Label = new System.Windows.Forms.Label();
            this.CurWrist3_Textbox = new System.Windows.Forms.TextBox();
            this.Wrist2Joint_Label = new System.Windows.Forms.Label();
            this.CurWrist2_Textbox = new System.Windows.Forms.TextBox();
            this.Wrist1Joint_Label = new System.Windows.Forms.Label();
            this.CurWrist1_Textbox = new System.Windows.Forms.TextBox();
            this.ElbowJoint_Label = new System.Windows.Forms.Label();
            this.CurElbow_Textbox = new System.Windows.Forms.TextBox();
            this.ShoulderJoint_Label = new System.Windows.Forms.Label();
            this.CurShoulder_TextBox = new System.Windows.Forms.TextBox();
            this.BaseJoint_Label = new System.Windows.Forms.Label();
            this.CurrentJoint_Label = new System.Windows.Forms.Label();
            this.CurBase_Textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SetPivot_Button = new System.Windows.Forms.Button();
            this.SetPivotZ_TextBox = new System.Windows.Forms.TextBox();
            this.SetPivotY_TextBox = new System.Windows.Forms.TextBox();
            this.SetPivotX_TextBox = new System.Windows.Forms.TextBox();
            this.PivotReset_Button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.WorkQue_ListBox = new System.Windows.Forms.ListBox();
            this.ListPlay_Button = new System.Windows.Forms.Button();
            this.ListAdd_Button = new System.Windows.Forms.Button();
            this.ListStop_Button = new System.Windows.Forms.Button();
            this.ListDelete_Button = new System.Windows.Forms.Button();
            this.Homming_Button = new System.Windows.Forms.Button();
            this.JointListAdd_Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Acceleration_TextBox = new System.Windows.Forms.TextBox();
            this.UnityPanel = new System.Windows.Forms.Panel();
            this.IPCTest_Button = new System.Windows.Forms.Button();
            this.Export_Button = new System.Windows.Forms.Button();
            this.Import_Button = new System.Windows.Forms.Button();
            this.JsonName_textBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.CSVButton = new System.Windows.Forms.Button();
            this.GripButton = new System.Windows.Forms.Button();
            this.Release_Button = new System.Windows.Forms.Button();
            this.PhoneGrip01_Button = new System.Windows.Forms.Button();
            this.PhoneGrip02_Button = new System.Windows.Forms.Button();
            this.PhoneGrip03_Button = new System.Windows.Forms.Button();
            this.PhoneGrip04_Button = new System.Windows.Forms.Button();
            this.PhoneRelease04_Button = new System.Windows.Forms.Button();
            this.PhoneRelease03_Button = new System.Windows.Forms.Button();
            this.PhoneRelease02_Button = new System.Windows.Forms.Button();
            this.PhoneRelease01_Button = new System.Windows.Forms.Button();
            this.PhoneReady_Button = new System.Windows.Forms.Button();
            this.PhoneFont_Button = new System.Windows.Forms.Button();
            this.PhoneBack_Button = new System.Windows.Forms.Button();
            this.Home_Button = new System.Windows.Forms.Button();
            this.StickGrip01_Button = new System.Windows.Forms.Button();
            this.StickReturn_Button = new System.Windows.Forms.Button();
            this.StickSet_Button = new System.Windows.Forms.Button();
            this.StickSetGrip_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.ArrowGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoveToFront
            // 
            this.MoveToFront.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.MoveToFront.Location = new System.Drawing.Point(642, 382);
            this.MoveToFront.Name = "MoveToFront";
            this.MoveToFront.Size = new System.Drawing.Size(70, 30);
            this.MoveToFront.TabIndex = 0;
            this.MoveToFront.Text = "앞";
            this.MoveToFront.UseVisualStyleBackColor = false;
            this.MoveToFront.Click += new System.EventHandler(this.MoveToFront_Click);
            // 
            // MoveToBack
            // 
            this.MoveToBack.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.MoveToBack.Location = new System.Drawing.Point(718, 382);
            this.MoveToBack.Name = "MoveToBack";
            this.MoveToBack.Size = new System.Drawing.Size(70, 30);
            this.MoveToBack.TabIndex = 1;
            this.MoveToBack.Text = "뒤";
            this.MoveToBack.UseVisualStyleBackColor = false;
            this.MoveToBack.Click += new System.EventHandler(this.MoveToBack_Click);
            // 
            // PowerOn
            // 
            this.PowerOn.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PowerOn.Location = new System.Drawing.Point(12, 15);
            this.PowerOn.Name = "PowerOn";
            this.PowerOn.Size = new System.Drawing.Size(100, 50);
            this.PowerOn.TabIndex = 2;
            this.PowerOn.Text = "연결";
            this.PowerOn.UseVisualStyleBackColor = false;
            this.PowerOn.Click += new System.EventHandler(this.PowerOn_Click);
            // 
            // PowerOff
            // 
            this.PowerOff.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PowerOff.Location = new System.Drawing.Point(116, 15);
            this.PowerOff.Name = "PowerOff";
            this.PowerOff.Size = new System.Drawing.Size(100, 50);
            this.PowerOff.TabIndex = 3;
            this.PowerOff.Text = "종료";
            this.PowerOff.UseVisualStyleBackColor = false;
            this.PowerOff.Click += new System.EventHandler(this.PowerOff_Click);
            // 
            // MoveToBack2
            // 
            this.MoveToBack2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.MoveToBack2.Location = new System.Drawing.Point(718, 418);
            this.MoveToBack2.Name = "MoveToBack2";
            this.MoveToBack2.Size = new System.Drawing.Size(70, 30);
            this.MoveToBack2.TabIndex = 5;
            this.MoveToBack2.Text = "뒤";
            this.MoveToBack2.UseVisualStyleBackColor = false;
            this.MoveToBack2.Click += new System.EventHandler(this.MoveToBack_Click2);
            // 
            // MoveToFront2
            // 
            this.MoveToFront2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.MoveToFront2.Location = new System.Drawing.Point(642, 418);
            this.MoveToFront2.Name = "MoveToFront2";
            this.MoveToFront2.Size = new System.Drawing.Size(70, 30);
            this.MoveToFront2.TabIndex = 4;
            this.MoveToFront2.Text = "앞";
            this.MoveToFront2.UseVisualStyleBackColor = false;
            this.MoveToFront2.Click += new System.EventHandler(this.MoveToFront2_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button1.Location = new System.Drawing.Point(561, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CurPosX_TextBox
            // 
            this.CurPosX_TextBox.Location = new System.Drawing.Point(86, 98);
            this.CurPosX_TextBox.Name = "CurPosX_TextBox";
            this.CurPosX_TextBox.ReadOnly = true;
            this.CurPosX_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurPosX_TextBox.TabIndex = 7;
            // 
            // CurrentPosTitle
            // 
            this.CurrentPosTitle.AutoSize = true;
            this.CurrentPosTitle.Location = new System.Drawing.Point(26, 83);
            this.CurrentPosTitle.Name = "CurrentPosTitle";
            this.CurrentPosTitle.Size = new System.Drawing.Size(131, 12);
            this.CurrentPosTitle.TabIndex = 8;
            this.CurrentPosTitle.Text = "Current Robot Position";
            // 
            // CurPosX_Text
            // 
            this.CurPosX_Text.AutoSize = true;
            this.CurPosX_Text.BackColor = System.Drawing.SystemColors.Window;
            this.CurPosX_Text.Location = new System.Drawing.Point(25, 106);
            this.CurPosX_Text.Name = "CurPosX_Text";
            this.CurPosX_Text.Size = new System.Drawing.Size(39, 12);
            this.CurPosX_Text.TabIndex = 9;
            this.CurPosX_Text.Text = "Pos X";
            // 
            // CurPosY_Text
            // 
            this.CurPosY_Text.AutoSize = true;
            this.CurPosY_Text.Location = new System.Drawing.Point(25, 133);
            this.CurPosY_Text.Name = "CurPosY_Text";
            this.CurPosY_Text.Size = new System.Drawing.Size(39, 12);
            this.CurPosY_Text.TabIndex = 11;
            this.CurPosY_Text.Text = "Pos Y";
            // 
            // CurPosY_TextBox
            // 
            this.CurPosY_TextBox.Location = new System.Drawing.Point(86, 125);
            this.CurPosY_TextBox.Name = "CurPosY_TextBox";
            this.CurPosY_TextBox.ReadOnly = true;
            this.CurPosY_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurPosY_TextBox.TabIndex = 10;
            // 
            // CurPosZ_Text
            // 
            this.CurPosZ_Text.AutoSize = true;
            this.CurPosZ_Text.Location = new System.Drawing.Point(25, 160);
            this.CurPosZ_Text.Name = "CurPosZ_Text";
            this.CurPosZ_Text.Size = new System.Drawing.Size(39, 12);
            this.CurPosZ_Text.TabIndex = 13;
            this.CurPosZ_Text.Text = "Pos Z";
            // 
            // CurPosZ_TextBox
            // 
            this.CurPosZ_TextBox.Location = new System.Drawing.Point(86, 152);
            this.CurPosZ_TextBox.Name = "CurPosZ_TextBox";
            this.CurPosZ_TextBox.ReadOnly = true;
            this.CurPosZ_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurPosZ_TextBox.TabIndex = 12;
            // 
            // CurRotX_Text
            // 
            this.CurRotX_Text.AutoSize = true;
            this.CurRotX_Text.Location = new System.Drawing.Point(179, 108);
            this.CurRotX_Text.Name = "CurRotX_Text";
            this.CurRotX_Text.Size = new System.Drawing.Size(35, 12);
            this.CurRotX_Text.TabIndex = 15;
            this.CurRotX_Text.Text = "Rot X";
            // 
            // CurRotX_TextBox
            // 
            this.CurRotX_TextBox.BackColor = System.Drawing.SystemColors.Control;
            this.CurRotX_TextBox.Location = new System.Drawing.Point(220, 97);
            this.CurRotX_TextBox.Name = "CurRotX_TextBox";
            this.CurRotX_TextBox.ReadOnly = true;
            this.CurRotX_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurRotX_TextBox.TabIndex = 14;
            // 
            // CurRotY_Text
            // 
            this.CurRotY_Text.AutoSize = true;
            this.CurRotY_Text.Location = new System.Drawing.Point(179, 135);
            this.CurRotY_Text.Name = "CurRotY_Text";
            this.CurRotY_Text.Size = new System.Drawing.Size(35, 12);
            this.CurRotY_Text.TabIndex = 17;
            this.CurRotY_Text.Text = "Rot Y";
            // 
            // CurRotY_TextBox
            // 
            this.CurRotY_TextBox.Location = new System.Drawing.Point(220, 124);
            this.CurRotY_TextBox.Name = "CurRotY_TextBox";
            this.CurRotY_TextBox.ReadOnly = true;
            this.CurRotY_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurRotY_TextBox.TabIndex = 16;
            // 
            // CurRotZ_Text
            // 
            this.CurRotZ_Text.AutoSize = true;
            this.CurRotZ_Text.Location = new System.Drawing.Point(179, 161);
            this.CurRotZ_Text.Name = "CurRotZ_Text";
            this.CurRotZ_Text.Size = new System.Drawing.Size(35, 12);
            this.CurRotZ_Text.TabIndex = 19;
            this.CurRotZ_Text.Text = "Rot Z";
            // 
            // CurRotZ_TextBox
            // 
            this.CurRotZ_TextBox.Location = new System.Drawing.Point(220, 150);
            this.CurRotZ_TextBox.Name = "CurRotZ_TextBox";
            this.CurRotZ_TextBox.ReadOnly = true;
            this.CurRotZ_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurRotZ_TextBox.TabIndex = 18;
            // 
            // SetRotZ_Text
            // 
            this.SetRotZ_Text.AutoSize = true;
            this.SetRotZ_Text.Location = new System.Drawing.Point(179, 382);
            this.SetRotZ_Text.Name = "SetRotZ_Text";
            this.SetRotZ_Text.Size = new System.Drawing.Size(35, 12);
            this.SetRotZ_Text.TabIndex = 32;
            this.SetRotZ_Text.Text = "Rot Z";
            // 
            // SetRotZ_TextBox
            // 
            this.SetRotZ_TextBox.Location = new System.Drawing.Point(220, 371);
            this.SetRotZ_TextBox.Name = "SetRotZ_TextBox";
            this.SetRotZ_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetRotZ_TextBox.TabIndex = 31;
            // 
            // SetRotY_Text
            // 
            this.SetRotY_Text.AutoSize = true;
            this.SetRotY_Text.Location = new System.Drawing.Point(179, 356);
            this.SetRotY_Text.Name = "SetRotY_Text";
            this.SetRotY_Text.Size = new System.Drawing.Size(35, 12);
            this.SetRotY_Text.TabIndex = 30;
            this.SetRotY_Text.Text = "Rot Y";
            // 
            // SetRotY_TextBox
            // 
            this.SetRotY_TextBox.Location = new System.Drawing.Point(220, 345);
            this.SetRotY_TextBox.Name = "SetRotY_TextBox";
            this.SetRotY_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetRotY_TextBox.TabIndex = 29;
            // 
            // SetRotX_Text
            // 
            this.SetRotX_Text.AutoSize = true;
            this.SetRotX_Text.Location = new System.Drawing.Point(179, 329);
            this.SetRotX_Text.Name = "SetRotX_Text";
            this.SetRotX_Text.Size = new System.Drawing.Size(35, 12);
            this.SetRotX_Text.TabIndex = 28;
            this.SetRotX_Text.Text = "Rot X";
            // 
            // SetRotX_TextBox
            // 
            this.SetRotX_TextBox.Location = new System.Drawing.Point(220, 318);
            this.SetRotX_TextBox.Name = "SetRotX_TextBox";
            this.SetRotX_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetRotX_TextBox.TabIndex = 27;
            // 
            // SetPosZ_Text
            // 
            this.SetPosZ_Text.AutoSize = true;
            this.SetPosZ_Text.Location = new System.Drawing.Point(25, 381);
            this.SetPosZ_Text.Name = "SetPosZ_Text";
            this.SetPosZ_Text.Size = new System.Drawing.Size(39, 12);
            this.SetPosZ_Text.TabIndex = 26;
            this.SetPosZ_Text.Text = "Pos Z";
            // 
            // SetPosZ_TextBox
            // 
            this.SetPosZ_TextBox.Location = new System.Drawing.Point(85, 374);
            this.SetPosZ_TextBox.Name = "SetPosZ_TextBox";
            this.SetPosZ_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetPosZ_TextBox.TabIndex = 25;
            // 
            // SetPosY_Text
            // 
            this.SetPosY_Text.AutoSize = true;
            this.SetPosY_Text.Location = new System.Drawing.Point(25, 354);
            this.SetPosY_Text.Name = "SetPosY_Text";
            this.SetPosY_Text.Size = new System.Drawing.Size(39, 12);
            this.SetPosY_Text.TabIndex = 24;
            this.SetPosY_Text.Text = "Pos Y";
            // 
            // SetPosY_TextBox
            // 
            this.SetPosY_TextBox.Location = new System.Drawing.Point(85, 347);
            this.SetPosY_TextBox.Name = "SetPosY_TextBox";
            this.SetPosY_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetPosY_TextBox.TabIndex = 23;
            // 
            // SetPosX_Text
            // 
            this.SetPosX_Text.AutoSize = true;
            this.SetPosX_Text.Location = new System.Drawing.Point(25, 327);
            this.SetPosX_Text.Name = "SetPosX_Text";
            this.SetPosX_Text.Size = new System.Drawing.Size(39, 12);
            this.SetPosX_Text.TabIndex = 22;
            this.SetPosX_Text.Text = "Pos X";
            // 
            // SetPosTitle
            // 
            this.SetPosTitle.AutoSize = true;
            this.SetPosTitle.Location = new System.Drawing.Point(26, 304);
            this.SetPosTitle.Name = "SetPosTitle";
            this.SetPosTitle.Size = new System.Drawing.Size(108, 12);
            this.SetPosTitle.TabIndex = 21;
            this.SetPosTitle.Text = "Set Robot Position";
            // 
            // SetPosX_TextBox
            // 
            this.SetPosX_TextBox.Location = new System.Drawing.Point(85, 320);
            this.SetPosX_TextBox.Name = "SetPosX_TextBox";
            this.SetPosX_TextBox.Size = new System.Drawing.Size(80, 21);
            this.SetPosX_TextBox.TabIndex = 20;
            // 
            // button2f
            // 
            this.button2f.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button2f.Location = new System.Drawing.Point(28, 400);
            this.button2f.Name = "button2f";
            this.button2f.Size = new System.Drawing.Size(100, 23);
            this.button2f.TabIndex = 33;
            this.button2f.Text = "SetPosition";
            this.button2f.UseVisualStyleBackColor = false;
            this.button2f.Click += new System.EventHandler(this.SetPositionButton);
            // 
            // ZPositive_Button
            // 
            this.ZPositive_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ZPositive_Button.Location = new System.Drawing.Point(672, 152);
            this.ZPositive_Button.Name = "ZPositive_Button";
            this.ZPositive_Button.Size = new System.Drawing.Size(50, 30);
            this.ZPositive_Button.TabIndex = 34;
            this.ZPositive_Button.Text = "▲";
            this.ZPositive_Button.UseVisualStyleBackColor = false;
            this.ZPositive_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.ZPositive_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // ZNegative_Button
            // 
            this.ZNegative_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ZNegative_Button.Location = new System.Drawing.Point(672, 212);
            this.ZNegative_Button.Name = "ZNegative_Button";
            this.ZNegative_Button.Size = new System.Drawing.Size(50, 30);
            this.ZNegative_Button.TabIndex = 35;
            this.ZNegative_Button.Text = "▼";
            this.ZNegative_Button.UseVisualStyleBackColor = false;
            this.ZNegative_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.ZNegative_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // XNegative_Button
            // 
            this.XNegative_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.XNegative_Button.Location = new System.Drawing.Point(616, 178);
            this.XNegative_Button.Name = "XNegative_Button";
            this.XNegative_Button.Size = new System.Drawing.Size(50, 30);
            this.XNegative_Button.TabIndex = 36;
            this.XNegative_Button.Text = "◀";
            this.XNegative_Button.UseVisualStyleBackColor = false;
            this.XNegative_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.XNegative_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // XPositive_Button
            // 
            this.XPositive_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.XPositive_Button.Location = new System.Drawing.Point(728, 178);
            this.XPositive_Button.Name = "XPositive_Button";
            this.XPositive_Button.Size = new System.Drawing.Size(50, 30);
            this.XPositive_Button.TabIndex = 37;
            this.XPositive_Button.Text = "▶";
            this.XPositive_Button.UseVisualStyleBackColor = false;
            this.XPositive_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.XPositive_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // YPositive_Button
            // 
            this.YPositive_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.YPositive_Button.Location = new System.Drawing.Point(616, 136);
            this.YPositive_Button.Name = "YPositive_Button";
            this.YPositive_Button.Size = new System.Drawing.Size(50, 30);
            this.YPositive_Button.TabIndex = 38;
            this.YPositive_Button.Text = "▲";
            this.YPositive_Button.UseVisualStyleBackColor = false;
            this.YPositive_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.YPositive_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // YNegative_Button
            // 
            this.YNegative_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.YNegative_Button.Location = new System.Drawing.Point(728, 136);
            this.YNegative_Button.Name = "YNegative_Button";
            this.YNegative_Button.Size = new System.Drawing.Size(50, 30);
            this.YNegative_Button.TabIndex = 39;
            this.YNegative_Button.Text = "▼";
            this.YNegative_Button.UseVisualStyleBackColor = false;
            this.YNegative_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveButtonDown);
            this.YNegative_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveButtonUp);
            // 
            // Position_Radio_Button
            // 
            this.Position_Radio_Button.AutoSize = true;
            this.Position_Radio_Button.Checked = true;
            this.Position_Radio_Button.Location = new System.Drawing.Point(17, 20);
            this.Position_Radio_Button.Name = "Position_Radio_Button";
            this.Position_Radio_Button.Size = new System.Drawing.Size(68, 16);
            this.Position_Radio_Button.TabIndex = 40;
            this.Position_Radio_Button.TabStop = true;
            this.Position_Radio_Button.Text = "Position";
            this.Position_Radio_Button.UseVisualStyleBackColor = true;
            this.Position_Radio_Button.CheckedChanged += new System.EventHandler(this.Arrow_Radio_Button_CheckedChanged);
            // 
            // Rotation_Radio_Button
            // 
            this.Rotation_Radio_Button.AutoSize = true;
            this.Rotation_Radio_Button.Location = new System.Drawing.Point(111, 20);
            this.Rotation_Radio_Button.Name = "Rotation_Radio_Button";
            this.Rotation_Radio_Button.Size = new System.Drawing.Size(68, 16);
            this.Rotation_Radio_Button.TabIndex = 41;
            this.Rotation_Radio_Button.Text = "Rotation";
            this.Rotation_Radio_Button.UseVisualStyleBackColor = true;
            this.Rotation_Radio_Button.CheckedChanged += new System.EventHandler(this.Arrow_Radio_Button_CheckedChanged);
            // 
            // ArrowGroupBox
            // 
            this.ArrowGroupBox.Controls.Add(this.Position_Radio_Button);
            this.ArrowGroupBox.Controls.Add(this.Rotation_Radio_Button);
            this.ArrowGroupBox.Location = new System.Drawing.Point(599, 82);
            this.ArrowGroupBox.Name = "ArrowGroupBox";
            this.ArrowGroupBox.Size = new System.Drawing.Size(190, 49);
            this.ArrowGroupBox.TabIndex = 42;
            this.ArrowGroupBox.TabStop = false;
            // 
            // Speed_Textbox
            // 
            this.Speed_Textbox.Location = new System.Drawing.Point(668, 12);
            this.Speed_Textbox.Name = "Speed_Textbox";
            this.Speed_Textbox.Size = new System.Drawing.Size(100, 21);
            this.Speed_Textbox.TabIndex = 43;
            this.Speed_Textbox.Text = "0.1";
            this.Speed_Textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Speed_Textbox.TextChanged += new System.EventHandler(this.Speed_Textbox_TextChanged);
            this.Speed_Textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(615, 15);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(41, 12);
            this.SpeedLabel.TabIndex = 44;
            this.SpeedLabel.Text = "Speed";
            // 
            // Joint_ComboBox
            // 
            this.Joint_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Joint_ComboBox.FormattingEnabled = true;
            this.Joint_ComboBox.Location = new System.Drawing.Point(627, 262);
            this.Joint_ComboBox.Name = "Joint_ComboBox";
            this.Joint_ComboBox.Size = new System.Drawing.Size(142, 20);
            this.Joint_ComboBox.TabIndex = 45;
            this.Joint_ComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // RotationJoint_Button
            // 
            this.RotationJoint_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.RotationJoint_Button.Location = new System.Drawing.Point(626, 335);
            this.RotationJoint_Button.Name = "RotationJoint_Button";
            this.RotationJoint_Button.Size = new System.Drawing.Size(75, 23);
            this.RotationJoint_Button.TabIndex = 46;
            this.RotationJoint_Button.Text = "SetAngle";
            this.RotationJoint_Button.UseVisualStyleBackColor = false;
            this.RotationJoint_Button.Click += new System.EventHandler(this.RotationJoint_Button_Click);
            // 
            // JointAngleDown_Button
            // 
            this.JointAngleDown_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.JointAngleDown_Button.Location = new System.Drawing.Point(719, 331);
            this.JointAngleDown_Button.Name = "JointAngleDown_Button";
            this.JointAngleDown_Button.Size = new System.Drawing.Size(50, 30);
            this.JointAngleDown_Button.TabIndex = 48;
            this.JointAngleDown_Button.Text = "▼";
            this.JointAngleDown_Button.UseVisualStyleBackColor = false;
            this.JointAngleDown_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JointAngle_Button_Down);
            this.JointAngleDown_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JointAngle_Button_Up);
            // 
            // JointAngleUp_Button
            // 
            this.JointAngleUp_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.JointAngleUp_Button.Location = new System.Drawing.Point(719, 292);
            this.JointAngleUp_Button.Name = "JointAngleUp_Button";
            this.JointAngleUp_Button.Size = new System.Drawing.Size(50, 30);
            this.JointAngleUp_Button.TabIndex = 47;
            this.JointAngleUp_Button.Text = "▲";
            this.JointAngleUp_Button.UseVisualStyleBackColor = false;
            this.JointAngleUp_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JointAngle_Button_Down);
            this.JointAngleUp_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JointAngle_Button_Up);
            // 
            // JointAngle_TextBox
            // 
            this.JointAngle_TextBox.Location = new System.Drawing.Point(626, 298);
            this.JointAngle_TextBox.Name = "JointAngle_TextBox";
            this.JointAngle_TextBox.Size = new System.Drawing.Size(75, 21);
            this.JointAngle_TextBox.TabIndex = 49;
            this.JointAngle_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.JointAngle_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // Wrist3Joint_Label
            // 
            this.Wrist3Joint_Label.AutoSize = true;
            this.Wrist3Joint_Label.Location = new System.Drawing.Point(178, 265);
            this.Wrist3Joint_Label.Name = "Wrist3Joint_Label";
            this.Wrist3Joint_Label.Size = new System.Drawing.Size(38, 12);
            this.Wrist3Joint_Label.TabIndex = 62;
            this.Wrist3Joint_Label.Text = "Wrist3";
            // 
            // CurWrist3_Textbox
            // 
            this.CurWrist3_Textbox.Location = new System.Drawing.Point(219, 254);
            this.CurWrist3_Textbox.Name = "CurWrist3_Textbox";
            this.CurWrist3_Textbox.ReadOnly = true;
            this.CurWrist3_Textbox.Size = new System.Drawing.Size(80, 21);
            this.CurWrist3_Textbox.TabIndex = 61;
            // 
            // Wrist2Joint_Label
            // 
            this.Wrist2Joint_Label.AutoSize = true;
            this.Wrist2Joint_Label.Location = new System.Drawing.Point(178, 239);
            this.Wrist2Joint_Label.Name = "Wrist2Joint_Label";
            this.Wrist2Joint_Label.Size = new System.Drawing.Size(38, 12);
            this.Wrist2Joint_Label.TabIndex = 60;
            this.Wrist2Joint_Label.Text = "Wrist2";
            // 
            // CurWrist2_Textbox
            // 
            this.CurWrist2_Textbox.Location = new System.Drawing.Point(219, 228);
            this.CurWrist2_Textbox.Name = "CurWrist2_Textbox";
            this.CurWrist2_Textbox.ReadOnly = true;
            this.CurWrist2_Textbox.Size = new System.Drawing.Size(80, 21);
            this.CurWrist2_Textbox.TabIndex = 59;
            // 
            // Wrist1Joint_Label
            // 
            this.Wrist1Joint_Label.AutoSize = true;
            this.Wrist1Joint_Label.Location = new System.Drawing.Point(178, 212);
            this.Wrist1Joint_Label.Name = "Wrist1Joint_Label";
            this.Wrist1Joint_Label.Size = new System.Drawing.Size(38, 12);
            this.Wrist1Joint_Label.TabIndex = 58;
            this.Wrist1Joint_Label.Text = "Wrist1";
            // 
            // CurWrist1_Textbox
            // 
            this.CurWrist1_Textbox.BackColor = System.Drawing.SystemColors.Control;
            this.CurWrist1_Textbox.Location = new System.Drawing.Point(219, 201);
            this.CurWrist1_Textbox.Name = "CurWrist1_Textbox";
            this.CurWrist1_Textbox.ReadOnly = true;
            this.CurWrist1_Textbox.Size = new System.Drawing.Size(80, 21);
            this.CurWrist1_Textbox.TabIndex = 57;
            // 
            // ElbowJoint_Label
            // 
            this.ElbowJoint_Label.AutoSize = true;
            this.ElbowJoint_Label.Location = new System.Drawing.Point(24, 264);
            this.ElbowJoint_Label.Name = "ElbowJoint_Label";
            this.ElbowJoint_Label.Size = new System.Drawing.Size(40, 12);
            this.ElbowJoint_Label.TabIndex = 56;
            this.ElbowJoint_Label.Text = "Elbow";
            // 
            // CurElbow_Textbox
            // 
            this.CurElbow_Textbox.Location = new System.Drawing.Point(85, 256);
            this.CurElbow_Textbox.Name = "CurElbow_Textbox";
            this.CurElbow_Textbox.ReadOnly = true;
            this.CurElbow_Textbox.Size = new System.Drawing.Size(80, 21);
            this.CurElbow_Textbox.TabIndex = 55;
            // 
            // ShoulderJoint_Label
            // 
            this.ShoulderJoint_Label.AutoSize = true;
            this.ShoulderJoint_Label.Location = new System.Drawing.Point(24, 237);
            this.ShoulderJoint_Label.Name = "ShoulderJoint_Label";
            this.ShoulderJoint_Label.Size = new System.Drawing.Size(55, 12);
            this.ShoulderJoint_Label.TabIndex = 54;
            this.ShoulderJoint_Label.Text = "Shoulder";
            // 
            // CurShoulder_TextBox
            // 
            this.CurShoulder_TextBox.Location = new System.Drawing.Point(85, 229);
            this.CurShoulder_TextBox.Name = "CurShoulder_TextBox";
            this.CurShoulder_TextBox.ReadOnly = true;
            this.CurShoulder_TextBox.Size = new System.Drawing.Size(80, 21);
            this.CurShoulder_TextBox.TabIndex = 53;
            // 
            // BaseJoint_Label
            // 
            this.BaseJoint_Label.AutoSize = true;
            this.BaseJoint_Label.BackColor = System.Drawing.SystemColors.Window;
            this.BaseJoint_Label.Location = new System.Drawing.Point(24, 210);
            this.BaseJoint_Label.Name = "BaseJoint_Label";
            this.BaseJoint_Label.Size = new System.Drawing.Size(34, 12);
            this.BaseJoint_Label.TabIndex = 52;
            this.BaseJoint_Label.Text = "Base";
            // 
            // CurrentJoint_Label
            // 
            this.CurrentJoint_Label.AutoSize = true;
            this.CurrentJoint_Label.Location = new System.Drawing.Point(25, 187);
            this.CurrentJoint_Label.Name = "CurrentJoint_Label";
            this.CurrentJoint_Label.Size = new System.Drawing.Size(120, 12);
            this.CurrentJoint_Label.TabIndex = 51;
            this.CurrentJoint_Label.Text = "Current Joint Degree";
            // 
            // CurBase_Textbox
            // 
            this.CurBase_Textbox.Location = new System.Drawing.Point(85, 202);
            this.CurBase_Textbox.Name = "CurBase_Textbox";
            this.CurBase_Textbox.ReadOnly = true;
            this.CurBase_Textbox.Size = new System.Drawing.Size(80, 21);
            this.CurBase_Textbox.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(579, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 63;
            this.label1.Text = "Joint";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(579, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 12);
            this.label2.TabIndex = 64;
            this.label2.Text = "Angle";
            // 
            // SetPivot_Button
            // 
            this.SetPivot_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.SetPivot_Button.Location = new System.Drawing.Point(465, 418);
            this.SetPivot_Button.Name = "SetPivot_Button";
            this.SetPivot_Button.Size = new System.Drawing.Size(84, 23);
            this.SetPivot_Button.TabIndex = 65;
            this.SetPivot_Button.Text = "Set Pivot";
            this.SetPivot_Button.UseVisualStyleBackColor = false;
            this.SetPivot_Button.Click += new System.EventHandler(this.SetPivot_Button_Click);
            // 
            // SetPivotZ_TextBox
            // 
            this.SetPivotZ_TextBox.Location = new System.Drawing.Point(381, 418);
            this.SetPivotZ_TextBox.Name = "SetPivotZ_TextBox";
            this.SetPivotZ_TextBox.Size = new System.Drawing.Size(78, 21);
            this.SetPivotZ_TextBox.TabIndex = 66;
            this.SetPivotZ_TextBox.Text = "0.000";
            this.SetPivotZ_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // SetPivotY_TextBox
            // 
            this.SetPivotY_TextBox.Location = new System.Drawing.Point(381, 389);
            this.SetPivotY_TextBox.Name = "SetPivotY_TextBox";
            this.SetPivotY_TextBox.Size = new System.Drawing.Size(78, 21);
            this.SetPivotY_TextBox.TabIndex = 67;
            this.SetPivotY_TextBox.Text = "0.000";
            this.SetPivotY_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // SetPivotX_TextBox
            // 
            this.SetPivotX_TextBox.Location = new System.Drawing.Point(381, 357);
            this.SetPivotX_TextBox.Name = "SetPivotX_TextBox";
            this.SetPivotX_TextBox.Size = new System.Drawing.Size(78, 21);
            this.SetPivotX_TextBox.TabIndex = 68;
            this.SetPivotX_TextBox.Text = "0.000";
            this.SetPivotX_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // PivotReset_Button
            // 
            this.PivotReset_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PivotReset_Button.Location = new System.Drawing.Point(465, 387);
            this.PivotReset_Button.Name = "PivotReset_Button";
            this.PivotReset_Button.Size = new System.Drawing.Size(84, 23);
            this.PivotReset_Button.TabIndex = 69;
            this.PivotReset_Button.Text = "Pivot Reset";
            this.PivotReset_Button.UseVisualStyleBackColor = false;
            this.PivotReset_Button.Click += new System.EventHandler(this.PivotReset_Button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 360);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 70;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(362, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 71;
            this.label4.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "Z";
            // 
            // WorkQue_ListBox
            // 
            this.WorkQue_ListBox.FormattingEnabled = true;
            this.WorkQue_ListBox.ItemHeight = 12;
            this.WorkQue_ListBox.Location = new System.Drawing.Point(339, 39);
            this.WorkQue_ListBox.Name = "WorkQue_ListBox";
            this.WorkQue_ListBox.Size = new System.Drawing.Size(223, 160);
            this.WorkQue_ListBox.TabIndex = 73;
            // 
            // ListPlay_Button
            // 
            this.ListPlay_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ListPlay_Button.Location = new System.Drawing.Point(339, 205);
            this.ListPlay_Button.Name = "ListPlay_Button";
            this.ListPlay_Button.Size = new System.Drawing.Size(60, 25);
            this.ListPlay_Button.TabIndex = 74;
            this.ListPlay_Button.Text = "Play";
            this.ListPlay_Button.UseVisualStyleBackColor = false;
            this.ListPlay_Button.Click += new System.EventHandler(this.ListPlay_Button_Click);
            // 
            // ListAdd_Button
            // 
            this.ListAdd_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ListAdd_Button.Location = new System.Drawing.Point(141, 400);
            this.ListAdd_Button.Name = "ListAdd_Button";
            this.ListAdd_Button.Size = new System.Drawing.Size(75, 23);
            this.ListAdd_Button.TabIndex = 75;
            this.ListAdd_Button.Text = "Add";
            this.ListAdd_Button.UseVisualStyleBackColor = false;
            this.ListAdd_Button.Click += new System.EventHandler(this.ListAdd_Button_Click);
            // 
            // ListStop_Button
            // 
            this.ListStop_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ListStop_Button.Location = new System.Drawing.Point(420, 205);
            this.ListStop_Button.Name = "ListStop_Button";
            this.ListStop_Button.Size = new System.Drawing.Size(60, 25);
            this.ListStop_Button.TabIndex = 76;
            this.ListStop_Button.Text = "Stop";
            this.ListStop_Button.UseVisualStyleBackColor = false;
            this.ListStop_Button.Click += new System.EventHandler(this.ListStop_Button_Click);
            // 
            // ListDelete_Button
            // 
            this.ListDelete_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ListDelete_Button.Location = new System.Drawing.Point(501, 205);
            this.ListDelete_Button.Name = "ListDelete_Button";
            this.ListDelete_Button.Size = new System.Drawing.Size(60, 25);
            this.ListDelete_Button.TabIndex = 77;
            this.ListDelete_Button.Text = "Delete";
            this.ListDelete_Button.UseVisualStyleBackColor = false;
            this.ListDelete_Button.Click += new System.EventHandler(this.ListDelete_Button_Click);
            // 
            // Homming_Button
            // 
            this.Homming_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Homming_Button.Location = new System.Drawing.Point(219, 15);
            this.Homming_Button.Name = "Homming_Button";
            this.Homming_Button.Size = new System.Drawing.Size(100, 50);
            this.Homming_Button.TabIndex = 78;
            this.Homming_Button.Text = "초기화";
            this.Homming_Button.UseVisualStyleBackColor = false;
            this.Homming_Button.Click += new System.EventHandler(this.Homming_Button_Click);
            // 
            // JointListAdd_Button
            // 
            this.JointListAdd_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.JointListAdd_Button.Location = new System.Drawing.Point(225, 400);
            this.JointListAdd_Button.Name = "JointListAdd_Button";
            this.JointListAdd_Button.Size = new System.Drawing.Size(75, 23);
            this.JointListAdd_Button.TabIndex = 79;
            this.JointListAdd_Button.Text = "Add(Joint)";
            this.JointListAdd_Button.UseVisualStyleBackColor = false;
            this.JointListAdd_Button.Click += new System.EventHandler(this.JointListAdd_Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(579, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 12);
            this.label6.TabIndex = 81;
            this.label6.Text = "Acceleration";
            // 
            // Acceleration_TextBox
            // 
            this.Acceleration_TextBox.Location = new System.Drawing.Point(668, 44);
            this.Acceleration_TextBox.Name = "Acceleration_TextBox";
            this.Acceleration_TextBox.Size = new System.Drawing.Size(100, 21);
            this.Acceleration_TextBox.TabIndex = 80;
            this.Acceleration_TextBox.Text = "1.2";
            this.Acceleration_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Acceleration_TextBox.TextChanged += new System.EventHandler(this.Acceleration_TextBox_TextChanged);
            this.Acceleration_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.myTextBox_KeyPress);
            // 
            // UnityPanel
            // 
            this.UnityPanel.BackColor = System.Drawing.SystemColors.Desktop;
            this.UnityPanel.Location = new System.Drawing.Point(865, 15);
            this.UnityPanel.Name = "UnityPanel";
            this.UnityPanel.Size = new System.Drawing.Size(960, 540);
            this.UnityPanel.TabIndex = 82;
            // 
            // IPCTest_Button
            // 
            this.IPCTest_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.IPCTest_Button.Location = new System.Drawing.Point(905, 578);
            this.IPCTest_Button.Name = "IPCTest_Button";
            this.IPCTest_Button.Size = new System.Drawing.Size(75, 23);
            this.IPCTest_Button.TabIndex = 83;
            this.IPCTest_Button.Text = "ICP Test";
            this.IPCTest_Button.UseVisualStyleBackColor = false;
            this.IPCTest_Button.Click += new System.EventHandler(this.IPCTest_Button_Click);
            // 
            // Export_Button
            // 
            this.Export_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Export_Button.Location = new System.Drawing.Point(116, 445);
            this.Export_Button.Name = "Export_Button";
            this.Export_Button.Size = new System.Drawing.Size(75, 23);
            this.Export_Button.TabIndex = 84;
            this.Export_Button.Text = "Export";
            this.Export_Button.UseVisualStyleBackColor = false;
            this.Export_Button.Click += new System.EventHandler(this.Export_Button_Click);
            // 
            // Import_Button
            // 
            this.Import_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Import_Button.Location = new System.Drawing.Point(116, 474);
            this.Import_Button.Name = "Import_Button";
            this.Import_Button.Size = new System.Drawing.Size(75, 23);
            this.Import_Button.TabIndex = 85;
            this.Import_Button.Text = "Import";
            this.Import_Button.UseVisualStyleBackColor = false;
            this.Import_Button.Click += new System.EventHandler(this.Import_Button_Click);
            // 
            // JsonName_textBox
            // 
            this.JsonName_textBox.Location = new System.Drawing.Point(26, 447);
            this.JsonName_textBox.Name = "JsonName_textBox";
            this.JsonName_textBox.Size = new System.Drawing.Size(80, 21);
            this.JsonName_textBox.TabIndex = 86;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button2.Location = new System.Drawing.Point(15, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 52);
            this.button2.TabIndex = 87;
            this.button2.Text = "WarningReset";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CSVButton
            // 
            this.CSVButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.CSVButton.Location = new System.Drawing.Point(116, 503);
            this.CSVButton.Name = "CSVButton";
            this.CSVButton.Size = new System.Drawing.Size(75, 23);
            this.CSVButton.TabIndex = 88;
            this.CSVButton.Text = "CSV";
            this.CSVButton.UseVisualStyleBackColor = false;
            this.CSVButton.Click += new System.EventHandler(this.CSVButton_Click);
            // 
            // GripButton
            // 
            this.GripButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.GripButton.Location = new System.Drawing.Point(339, 256);
            this.GripButton.Name = "GripButton";
            this.GripButton.Size = new System.Drawing.Size(97, 52);
            this.GripButton.TabIndex = 89;
            this.GripButton.Text = "Grip";
            this.GripButton.UseVisualStyleBackColor = false;
            this.GripButton.Click += new System.EventHandler(this.GripButton_Click);
            // 
            // Release_Button
            // 
            this.Release_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Release_Button.Location = new System.Drawing.Point(452, 256);
            this.Release_Button.Name = "Release_Button";
            this.Release_Button.Size = new System.Drawing.Size(97, 52);
            this.Release_Button.TabIndex = 90;
            this.Release_Button.Text = "Release";
            this.Release_Button.UseVisualStyleBackColor = false;
            this.Release_Button.Click += new System.EventHandler(this.Release_Button_Click);
            // 
            // PhoneGrip01_Button
            // 
            this.PhoneGrip01_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneGrip01_Button.Location = new System.Drawing.Point(214, 488);
            this.PhoneGrip01_Button.Name = "PhoneGrip01_Button";
            this.PhoneGrip01_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneGrip01_Button.TabIndex = 91;
            this.PhoneGrip01_Button.Text = "PhoneGrip01";
            this.PhoneGrip01_Button.UseVisualStyleBackColor = false;
            this.PhoneGrip01_Button.Click += new System.EventHandler(this.WorkPlay_Button_Click);
            // 
            // PhoneGrip02_Button
            // 
            this.PhoneGrip02_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneGrip02_Button.Location = new System.Drawing.Point(317, 488);
            this.PhoneGrip02_Button.Name = "PhoneGrip02_Button";
            this.PhoneGrip02_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneGrip02_Button.TabIndex = 92;
            this.PhoneGrip02_Button.Text = "PhoneGrip02";
            this.PhoneGrip02_Button.UseVisualStyleBackColor = false;
            this.PhoneGrip02_Button.Click += new System.EventHandler(this.PhoneGrip02_Button_Click);
            // 
            // PhoneGrip03_Button
            // 
            this.PhoneGrip03_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneGrip03_Button.Location = new System.Drawing.Point(420, 488);
            this.PhoneGrip03_Button.Name = "PhoneGrip03_Button";
            this.PhoneGrip03_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneGrip03_Button.TabIndex = 93;
            this.PhoneGrip03_Button.Text = "PhoneGrip03";
            this.PhoneGrip03_Button.UseVisualStyleBackColor = false;
            this.PhoneGrip03_Button.Click += new System.EventHandler(this.PhoneGrip03_Button_Click);
            // 
            // PhoneGrip04_Button
            // 
            this.PhoneGrip04_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneGrip04_Button.Location = new System.Drawing.Point(523, 488);
            this.PhoneGrip04_Button.Name = "PhoneGrip04_Button";
            this.PhoneGrip04_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneGrip04_Button.TabIndex = 94;
            this.PhoneGrip04_Button.Text = "PhoneGrip04";
            this.PhoneGrip04_Button.UseVisualStyleBackColor = false;
            this.PhoneGrip04_Button.Click += new System.EventHandler(this.PhoneGrip04_Button_Click);
            // 
            // PhoneRelease04_Button
            // 
            this.PhoneRelease04_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneRelease04_Button.Location = new System.Drawing.Point(523, 546);
            this.PhoneRelease04_Button.Name = "PhoneRelease04_Button";
            this.PhoneRelease04_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneRelease04_Button.TabIndex = 98;
            this.PhoneRelease04_Button.Text = "PhoneRelease04";
            this.PhoneRelease04_Button.UseVisualStyleBackColor = false;
            this.PhoneRelease04_Button.Click += new System.EventHandler(this.PhoneRelease04_Button_Click);
            // 
            // PhoneRelease03_Button
            // 
            this.PhoneRelease03_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneRelease03_Button.Location = new System.Drawing.Point(420, 546);
            this.PhoneRelease03_Button.Name = "PhoneRelease03_Button";
            this.PhoneRelease03_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneRelease03_Button.TabIndex = 97;
            this.PhoneRelease03_Button.Text = "PhoneRelease03";
            this.PhoneRelease03_Button.UseVisualStyleBackColor = false;
            this.PhoneRelease03_Button.Click += new System.EventHandler(this.PhoneRelease03_Button_Click);
            // 
            // PhoneRelease02_Button
            // 
            this.PhoneRelease02_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneRelease02_Button.Location = new System.Drawing.Point(317, 546);
            this.PhoneRelease02_Button.Name = "PhoneRelease02_Button";
            this.PhoneRelease02_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneRelease02_Button.TabIndex = 96;
            this.PhoneRelease02_Button.Text = "PhoneRelease02";
            this.PhoneRelease02_Button.UseVisualStyleBackColor = false;
            this.PhoneRelease02_Button.Click += new System.EventHandler(this.PhoneRelease02_Button_Click);
            // 
            // PhoneRelease01_Button
            // 
            this.PhoneRelease01_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneRelease01_Button.Location = new System.Drawing.Point(214, 546);
            this.PhoneRelease01_Button.Name = "PhoneRelease01_Button";
            this.PhoneRelease01_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneRelease01_Button.TabIndex = 95;
            this.PhoneRelease01_Button.Text = "PhoneRelease01";
            this.PhoneRelease01_Button.UseVisualStyleBackColor = false;
            this.PhoneRelease01_Button.Click += new System.EventHandler(this.PhoneRelease01_Button_Click);
            // 
            // PhoneReady_Button
            // 
            this.PhoneReady_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneReady_Button.Location = new System.Drawing.Point(642, 488);
            this.PhoneReady_Button.Name = "PhoneReady_Button";
            this.PhoneReady_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneReady_Button.TabIndex = 99;
            this.PhoneReady_Button.Text = "PhoneReady";
            this.PhoneReady_Button.UseVisualStyleBackColor = false;
            this.PhoneReady_Button.Click += new System.EventHandler(this.PhoneReady_Button_Click);
            // 
            // PhoneFont_Button
            // 
            this.PhoneFont_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneFont_Button.Location = new System.Drawing.Point(642, 549);
            this.PhoneFont_Button.Name = "PhoneFont_Button";
            this.PhoneFont_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneFont_Button.TabIndex = 100;
            this.PhoneFont_Button.Text = "PhoneFont";
            this.PhoneFont_Button.UseVisualStyleBackColor = false;
            this.PhoneFont_Button.Click += new System.EventHandler(this.PhoneFont_Button_Click);
            // 
            // PhoneBack_Button
            // 
            this.PhoneBack_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.PhoneBack_Button.Location = new System.Drawing.Point(745, 549);
            this.PhoneBack_Button.Name = "PhoneBack_Button";
            this.PhoneBack_Button.Size = new System.Drawing.Size(97, 52);
            this.PhoneBack_Button.TabIndex = 101;
            this.PhoneBack_Button.Text = "PhoneBack";
            this.PhoneBack_Button.UseVisualStyleBackColor = false;
            this.PhoneBack_Button.Click += new System.EventHandler(this.PhoneBack_Button_Click);
            // 
            // Home_Button
            // 
            this.Home_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Home_Button.Location = new System.Drawing.Point(745, 488);
            this.Home_Button.Name = "Home_Button";
            this.Home_Button.Size = new System.Drawing.Size(97, 52);
            this.Home_Button.TabIndex = 102;
            this.Home_Button.Text = "Home";
            this.Home_Button.UseVisualStyleBackColor = false;
            this.Home_Button.Click += new System.EventHandler(this.Home_Button_Click);
            // 
            // StickGrip01_Button
            // 
            this.StickGrip01_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.StickGrip01_Button.Location = new System.Drawing.Point(214, 633);
            this.StickGrip01_Button.Name = "StickGrip01_Button";
            this.StickGrip01_Button.Size = new System.Drawing.Size(97, 52);
            this.StickGrip01_Button.TabIndex = 103;
            this.StickGrip01_Button.Text = "StickGrip01";
            this.StickGrip01_Button.UseVisualStyleBackColor = false;
            this.StickGrip01_Button.Click += new System.EventHandler(this.StickGrip01_Button_Click);
            // 
            // StickReturn_Button
            // 
            this.StickReturn_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.StickReturn_Button.Location = new System.Drawing.Point(317, 633);
            this.StickReturn_Button.Name = "StickReturn_Button";
            this.StickReturn_Button.Size = new System.Drawing.Size(97, 52);
            this.StickReturn_Button.TabIndex = 104;
            this.StickReturn_Button.Text = "StickReturn";
            this.StickReturn_Button.UseVisualStyleBackColor = false;
            this.StickReturn_Button.Click += new System.EventHandler(this.StickReturn_Button_Click);
            // 
            // StickSet_Button
            // 
            this.StickSet_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.StickSet_Button.Location = new System.Drawing.Point(420, 633);
            this.StickSet_Button.Name = "StickSet_Button";
            this.StickSet_Button.Size = new System.Drawing.Size(97, 52);
            this.StickSet_Button.TabIndex = 105;
            this.StickSet_Button.Text = "StickSet";
            this.StickSet_Button.UseVisualStyleBackColor = false;
            this.StickSet_Button.Click += new System.EventHandler(this.StickSet_Button_Click);
            // 
            // StickSetGrip_Button
            // 
            this.StickSetGrip_Button.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.StickSetGrip_Button.Location = new System.Drawing.Point(523, 633);
            this.StickSetGrip_Button.Name = "StickSetGrip_Button";
            this.StickSetGrip_Button.Size = new System.Drawing.Size(97, 52);
            this.StickSetGrip_Button.TabIndex = 106;
            this.StickSetGrip_Button.Text = "StickSetGrip";
            this.StickSetGrip_Button.UseVisualStyleBackColor = false;
            this.StickSetGrip_Button.Click += new System.EventHandler(this.StickSetGrip_Button_Click);
            // 
            // WarningReset_Button
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.StickSetGrip_Button);
            this.Controls.Add(this.StickSet_Button);
            this.Controls.Add(this.StickReturn_Button);
            this.Controls.Add(this.StickGrip01_Button);
            this.Controls.Add(this.Home_Button);
            this.Controls.Add(this.PhoneBack_Button);
            this.Controls.Add(this.PhoneFont_Button);
            this.Controls.Add(this.PhoneReady_Button);
            this.Controls.Add(this.PhoneRelease04_Button);
            this.Controls.Add(this.PhoneRelease03_Button);
            this.Controls.Add(this.PhoneRelease02_Button);
            this.Controls.Add(this.PhoneRelease01_Button);
            this.Controls.Add(this.PhoneGrip04_Button);
            this.Controls.Add(this.PhoneGrip03_Button);
            this.Controls.Add(this.PhoneGrip02_Button);
            this.Controls.Add(this.PhoneGrip01_Button);
            this.Controls.Add(this.Release_Button);
            this.Controls.Add(this.GripButton);
            this.Controls.Add(this.CSVButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.JsonName_textBox);
            this.Controls.Add(this.Import_Button);
            this.Controls.Add(this.Export_Button);
            this.Controls.Add(this.IPCTest_Button);
            this.Controls.Add(this.UnityPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Acceleration_TextBox);
            this.Controls.Add(this.JointListAdd_Button);
            this.Controls.Add(this.Homming_Button);
            this.Controls.Add(this.ListDelete_Button);
            this.Controls.Add(this.ListStop_Button);
            this.Controls.Add(this.ListAdd_Button);
            this.Controls.Add(this.ListPlay_Button);
            this.Controls.Add(this.WorkQue_ListBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PivotReset_Button);
            this.Controls.Add(this.SetPivotX_TextBox);
            this.Controls.Add(this.SetPivotY_TextBox);
            this.Controls.Add(this.SetPivotZ_TextBox);
            this.Controls.Add(this.SetPivot_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Wrist3Joint_Label);
            this.Controls.Add(this.CurWrist3_Textbox);
            this.Controls.Add(this.Wrist2Joint_Label);
            this.Controls.Add(this.CurWrist2_Textbox);
            this.Controls.Add(this.Wrist1Joint_Label);
            this.Controls.Add(this.CurWrist1_Textbox);
            this.Controls.Add(this.ElbowJoint_Label);
            this.Controls.Add(this.CurElbow_Textbox);
            this.Controls.Add(this.ShoulderJoint_Label);
            this.Controls.Add(this.CurShoulder_TextBox);
            this.Controls.Add(this.BaseJoint_Label);
            this.Controls.Add(this.CurrentJoint_Label);
            this.Controls.Add(this.CurBase_Textbox);
            this.Controls.Add(this.JointAngle_TextBox);
            this.Controls.Add(this.JointAngleDown_Button);
            this.Controls.Add(this.JointAngleUp_Button);
            this.Controls.Add(this.RotationJoint_Button);
            this.Controls.Add(this.Joint_ComboBox);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.Speed_Textbox);
            this.Controls.Add(this.ArrowGroupBox);
            this.Controls.Add(this.YNegative_Button);
            this.Controls.Add(this.YPositive_Button);
            this.Controls.Add(this.XPositive_Button);
            this.Controls.Add(this.XNegative_Button);
            this.Controls.Add(this.ZNegative_Button);
            this.Controls.Add(this.ZPositive_Button);
            this.Controls.Add(this.button2f);
            this.Controls.Add(this.SetRotZ_Text);
            this.Controls.Add(this.SetRotZ_TextBox);
            this.Controls.Add(this.SetRotY_Text);
            this.Controls.Add(this.SetRotY_TextBox);
            this.Controls.Add(this.SetRotX_Text);
            this.Controls.Add(this.SetRotX_TextBox);
            this.Controls.Add(this.SetPosZ_Text);
            this.Controls.Add(this.SetPosZ_TextBox);
            this.Controls.Add(this.SetPosY_Text);
            this.Controls.Add(this.SetPosY_TextBox);
            this.Controls.Add(this.SetPosX_Text);
            this.Controls.Add(this.SetPosTitle);
            this.Controls.Add(this.SetPosX_TextBox);
            this.Controls.Add(this.CurRotZ_Text);
            this.Controls.Add(this.CurRotZ_TextBox);
            this.Controls.Add(this.CurRotY_Text);
            this.Controls.Add(this.CurRotY_TextBox);
            this.Controls.Add(this.CurRotX_Text);
            this.Controls.Add(this.CurRotX_TextBox);
            this.Controls.Add(this.CurPosZ_Text);
            this.Controls.Add(this.CurPosZ_TextBox);
            this.Controls.Add(this.CurPosY_Text);
            this.Controls.Add(this.CurPosY_TextBox);
            this.Controls.Add(this.CurPosX_Text);
            this.Controls.Add(this.CurrentPosTitle);
            this.Controls.Add(this.CurPosX_TextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MoveToBack2);
            this.Controls.Add(this.MoveToFront2);
            this.Controls.Add(this.PowerOff);
            this.Controls.Add(this.PowerOn);
            this.Controls.Add(this.MoveToBack);
            this.Controls.Add(this.MoveToFront);
            this.Name = "WarningReset_Button";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ArrowGroupBox.ResumeLayout(false);
            this.ArrowGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.Button MoveToFront;
        private System.Windows.Forms.Button MoveToBack;
        private System.Windows.Forms.Button PowerOn;
        private System.Windows.Forms.Button PowerOff;
        private System.Windows.Forms.Button MoveToBack2;
        private System.Windows.Forms.Button MoveToFront2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label CurPosX_Text;
        private System.Windows.Forms.Label CurrentPosTitle;
        private System.Windows.Forms.TextBox CurPosX_TextBox;
        private System.Windows.Forms.Label CurRotZ_Text;
        private System.Windows.Forms.TextBox CurRotZ_TextBox;
        private System.Windows.Forms.Label CurRotY_Text;
        private System.Windows.Forms.TextBox CurRotY_TextBox;
        private System.Windows.Forms.Label CurRotX_Text;
        private System.Windows.Forms.TextBox CurRotX_TextBox;
        private System.Windows.Forms.Label CurPosZ_Text;
        private System.Windows.Forms.TextBox CurPosZ_TextBox;
        private System.Windows.Forms.Label CurPosY_Text;
        private System.Windows.Forms.TextBox CurPosY_TextBox;
        private System.Windows.Forms.Label SetRotZ_Text;
        private System.Windows.Forms.TextBox SetRotZ_TextBox;
        private System.Windows.Forms.Label SetRotY_Text;
        private System.Windows.Forms.TextBox SetRotY_TextBox;
        private System.Windows.Forms.Label SetRotX_Text;
        private System.Windows.Forms.TextBox SetRotX_TextBox;
        private System.Windows.Forms.Label SetPosZ_Text;
        private System.Windows.Forms.TextBox SetPosZ_TextBox;
        private System.Windows.Forms.Label SetPosY_Text;
        private System.Windows.Forms.TextBox SetPosY_TextBox;
        private System.Windows.Forms.Label SetPosX_Text;
        private System.Windows.Forms.Label SetPosTitle;
        private System.Windows.Forms.TextBox SetPosX_TextBox;
        private System.Windows.Forms.Button button2f;
        private System.Windows.Forms.Button YNegative_Button;
        private System.Windows.Forms.Button YPositive_Button;
        private System.Windows.Forms.Button XPositive_Button;
        private System.Windows.Forms.Button XNegative_Button;
        private System.Windows.Forms.Button ZNegative_Button;
        private System.Windows.Forms.Button ZPositive_Button;
        private System.Windows.Forms.RadioButton Rotation_Radio_Button;
        private System.Windows.Forms.RadioButton Position_Radio_Button;
        private System.Windows.Forms.GroupBox ArrowGroupBox;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.TextBox Speed_Textbox;
        private System.Windows.Forms.ComboBox Joint_ComboBox;
        private System.Windows.Forms.Button JointAngleDown_Button;
        private System.Windows.Forms.Button JointAngleUp_Button;
        private System.Windows.Forms.Button RotationJoint_Button;
        private System.Windows.Forms.TextBox JointAngle_TextBox;
        private System.Windows.Forms.Label Wrist3Joint_Label;
        private System.Windows.Forms.TextBox CurWrist3_Textbox;
        private System.Windows.Forms.Label Wrist2Joint_Label;
        private System.Windows.Forms.TextBox CurWrist2_Textbox;
        private System.Windows.Forms.Label Wrist1Joint_Label;
        private System.Windows.Forms.TextBox CurWrist1_Textbox;
        private System.Windows.Forms.Label ElbowJoint_Label;
        private System.Windows.Forms.TextBox CurElbow_Textbox;
        private System.Windows.Forms.Label ShoulderJoint_Label;
        private System.Windows.Forms.TextBox CurShoulder_TextBox;
        private System.Windows.Forms.Label BaseJoint_Label;
        private System.Windows.Forms.Label CurrentJoint_Label;
        private System.Windows.Forms.TextBox CurBase_Textbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetPivot_Button;
        private System.Windows.Forms.Button PivotReset_Button;
        private System.Windows.Forms.TextBox SetPivotX_TextBox;
        private System.Windows.Forms.TextBox SetPivotY_TextBox;
        private System.Windows.Forms.TextBox SetPivotZ_TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox WorkQue_ListBox;
        private System.Windows.Forms.Button ListDelete_Button;
        private System.Windows.Forms.Button ListStop_Button;
        private System.Windows.Forms.Button ListAdd_Button;
        private System.Windows.Forms.Button ListPlay_Button;
        private System.Windows.Forms.Button Homming_Button;
        private System.Windows.Forms.Button JointListAdd_Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Acceleration_TextBox;
        private System.Windows.Forms.Panel UnityPanel;
        private System.Windows.Forms.Button IPCTest_Button;
        private System.Windows.Forms.Button Import_Button;
        private System.Windows.Forms.Button Export_Button;
        private System.Windows.Forms.TextBox JsonName_textBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button CSVButton;
        private System.Windows.Forms.Button GripButton;
        private System.Windows.Forms.Button Release_Button;
        private System.Windows.Forms.Button PhoneGrip01_Button;
        private System.Windows.Forms.Button PhoneGrip04_Button;
        private System.Windows.Forms.Button PhoneGrip03_Button;
        private System.Windows.Forms.Button PhoneGrip02_Button;
        private System.Windows.Forms.Button PhoneRelease04_Button;
        private System.Windows.Forms.Button PhoneRelease03_Button;
        private System.Windows.Forms.Button PhoneRelease02_Button;
        private System.Windows.Forms.Button PhoneRelease01_Button;
        private System.Windows.Forms.Button PhoneReady_Button;
        private System.Windows.Forms.Button Home_Button;
        private System.Windows.Forms.Button PhoneBack_Button;
        private System.Windows.Forms.Button PhoneFont_Button;
        private System.Windows.Forms.Button StickGrip01_Button;
        private System.Windows.Forms.Button StickReturn_Button;
        private System.Windows.Forms.Button StickSet_Button;
        private System.Windows.Forms.Button StickSetGrip_Button;
    }
}

