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

        #endregion
    }
}
