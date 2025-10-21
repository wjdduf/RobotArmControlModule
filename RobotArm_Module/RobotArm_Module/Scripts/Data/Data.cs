using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    #region Enum 정의

    public enum eRobotArmType
    {
        UR,
        Rainbow,
    }

    public enum eJointType
    {
        None,
        BASE,
        SHOULDER,
        ELBOW,
        WRIST1,
        WRIST2,
        WRIST3,

    }

    public enum eDirection
    {
        None,      // 방향 없음
        X_Positive,  // X축 양의 방향
        X_Negative,  // X축 음의 방향
        Y_Positive,  // Y축 양의 방향
        Y_Negative,  // Y축 음의 방향
        Z_Positive,  // Z축 양의 방향
        Z_Negative   // Z축 음의 방향
    }
    public enum eRotationAxis
    {
        None,  // 회전축 없음
        X_Axis_Positive, // X축
        X_Axis_Negative,
        Y_Axis_Positive, // Y축
        Y_Axis_Negative,
        Z_Axis_Positive,
        Z_Axis_Negative,  // Z축
    }

    #endregion

    #region RobotArm 관련 데이터

    public class RobotArmCurrentData
    {
        public string ip;
        public string port;
        public Vector3 initPosition = new Vector3();
        public Vector3 initRotateion = new Vector3();

        public Vector3 currentPosition = new Vector3();
        public Vector3 currentRotation = new Vector3();

        public Vector3 SetPosition = new Vector3();
        public Vector3 SetRotation = new Vector3();

        public JointData currentJoinData = new JointData();


        public int moveSpeed;
        public int power;

    }

    [Serializable]
    public class PresetData
    {
        public Vector3 position;
        public Vector3 rotation;
        public List<Vector3> rotations;
        public string presetName;
        public string presetID;
        public eMoveType moveType = eMoveType.Position;
        public bool isLinear = false;

        public PresetData()
        {

        }

        public PresetData(Vector3 vector3, Vector3 rotation)
        {
            this.position = vector3;
            this.rotation = rotation;
        }
        public PresetData(Vector3 vector3, List<Vector3> rotation)
        {
            this.position = vector3;
            this.rotations = rotation;
        }
    }

    public class RobotArmConfig
    {
        public string IP = "192.168.1.40";
    }

    public class URConfig : RobotArmConfig
    {

        public int DASHBOARD_PORT = 29999;
        public int PRIMARY_PORT = 30001;
        public int SECONDARY_PORT = 30002;
        public int INTERPRETER_PORT = 30020;
        public int REALTIMEINTERFACE_PORT = 30003;
        public int RTDE = 30004;
    }

    [Serializable]
    public class RobotWorkList
    {
        public List<WorkStep> WorkList = new List<WorkStep>();
        public string WorkName { get; set; }

    }


    [Serializable]
    public class WorkStep
    {
        public string PresetName { get; set; }
        public string Type { get; set; }
    }


    #endregion



    #region Utill Data Class

    [Serializable]
    public class Vector3
    {
        private float x;
        private float y;
        private float z;


        public float X
        {
            get => x;
            set { x = value; }
        }
        public float Y
        {
            get => y;
            set { y = value; }
            
        }
        public float Z
        {
            get => z;
            set { z = value; }
        }



        public Vector3()
        {
            this.X = 0f;
            this.Y = 0f;
            this.Z = 0f;
        }

        public Vector3(float x,float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3 Zero()
        {
            return new Vector3();
        }

        public static Vector3 One()
        {
            return new Vector3(1f, 1f, 1f);
        }
        
    }

    public class Joint
    {
        public Joint(eJointType type)
        {
            this.Type = type;
        }
        public double Angle;
        public eJointType Type;
        
    }

    public class JointData
    {
        public Dictionary<eJointType, Joint> Joints;

        public JointData()
        {
            Joints = new Dictionary<eJointType, Joint>();
            Joints.Add(eJointType.BASE, new Joint(eJointType.BASE));
            Joints.Add(eJointType.SHOULDER, new Joint(eJointType.SHOULDER));
            Joints.Add(eJointType.ELBOW, new Joint(eJointType.ELBOW));
            Joints.Add(eJointType.WRIST1, new Joint(eJointType.WRIST1));
            Joints.Add(eJointType.WRIST2, new Joint(eJointType.WRIST2));
            Joints.Add(eJointType.WRIST3, new Joint(eJointType.WRIST3));

        }

        public Joint GetJoint(eJointType type)
        {
            return Joints[type];
        }
    }

    [Serializable]
    public class PipeData
    {
        public string Command;
        public string Value;
    }

    #endregion


    #region 기타 데이터

    public class CSVData
    {
        public static readonly int ExpectedFieldCount;

        static CSVData()
        {
            ExpectedFieldCount = typeof(CSVData)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Count(p => p.GetSetMethod() != null);
        }

        public double radianX;
        public double radianY;
        public double radianZ;
        public double velocity;
        public double acceleration;
        public int timeStamp;
    }

    #endregion
}
