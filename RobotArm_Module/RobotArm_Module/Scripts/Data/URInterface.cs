using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public enum eDangerType
    {
        REDUCED, PROTECTIVE_STOP, RECOVERY, SAFEGUARD_STOP, 
        SYSTEM_EMERGENCY_STOP, ROBOT_EMERGENCY_STOP, VIOLATION, FAULT,
        AUTOMATIC_MODE_SAFEGUARD_STOP, SYSTEM_THREE_POSITION_ENABLING_STOP
    }

    public static class URInterface
    {

        #region 전원 관련
        //전원 On
        public const string PowerOn = "power on";
        
        //잠금 해제
        public const string BrakeRelease = "brake release";

        //로봇 전원 종료
        public const string PowerOff = "power off";

        //컨트롤 박스 종료
        public const string ShutDown = "shutdown";

        #endregion



        #region 상태 관련

        //현재 상태
        public const string RobotMode = "robotmode";

        //버전 정보 요청
        public const string version = "version";

        public const string EndInterpreter = "end_interpreter()";

        public const string GetSafetyStatus = "safetystatus";

        public const string ClosePopup = "close popup";

        public const string UnlockProtectiveStop = "Unlock Protective Stop";

        public const string ProgramState = "programState";

        #endregion


        #region Gripper

        public const string ConnectGrip = "load connectgrip.urp\n" + "play";

        public const string Grip = "load grip.urp\n" + "play";

        public const string Release = "load release.urp\n" + "play";

        public const string gripTest =
            //"def my_sequence():\n" +
            "lehr_modbus_xmlrpc = rpc_factory(\"xmlrpc\", \"http://127.0.0.1:31000/RPC2\")\n" +
            "lehr_isConnected = lehr_modbus_xmlrpc.reachable()\n" +
            //"if(lehr_isConnected == True):\n" +
            //"popup(\"lehr_Modbus xmlrpc is not available!\")\n" +
            //"else:\n" +
            //"popup(\"asdfsadfsd!\")\n" +
            //"end\n";
            //stopmotion
            "lehr_modbus_xmlrpc.tool_modbus_write5(1408, 0)\n" +
            "lehr_modbus_xmlrpc.tool_modbus_write5(1408, 1)";
            //"end";
            //setCM3DataPoint
            //"lehr_modbus_xmlrpc.tool_modbus_write(640 + 2 * 2, 303)\n" +
            //"lehr_modbus_xmlrpc.tool_modbus_write(672 + 2 * 2, 60)\n" +
            ////setCM3Force
            //"lehr_modbus_xmlrpc.tool_modbus_write(736 + 2 * 2, 32)\n" +
            //"lehr_modbus_xmlrpc.tool_modbus_write(768 + 2 * 2, 100)\n" +
            ////startMotion
            //"lehr_modbus_xmlrpc.tool_modbus_write5(1417 + 3, 1)\n" +
            //"lehr_modbus_xmlrpc.tool_modbus_write5(1417 + 3, 0)\n" +
            //"lehr_modbus_xmlrpc.tool_modbus_close()";



        #endregion
    }
}

