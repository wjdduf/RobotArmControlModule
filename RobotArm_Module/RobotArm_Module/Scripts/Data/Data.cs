using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    #endregion

    #region RobotArm 관련 데이터

    public class RobotArmConfigData
    {
        public string ip;
        public string port;
        public Vector3 initPosition;
        public Vector3 initRotateion;

        public Vector3 currentPosition;
        public Vector3 currentRotation;

        public int moveSpeed;
        public int power;
    }

    public class PresetData
    {
        public Vector3 position;
        public List<Vector3> rotation;
        public string presetName;
        public string presetID;

        public string carID;
        public string seatID;
    }

    public class RobotArmConfig
    {
        public string IP = "192.168.1.40";
    }

    public class URConfig : RobotArmConfig
    {

        public int DASHBOARD_PORT = 29999;
        public int SECONDARY_PORT = 30002;
        public int INTERPRETER_PORT = 30020;
    }


    #endregion



    #region Utill Data Class

    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

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


    #endregion
}
