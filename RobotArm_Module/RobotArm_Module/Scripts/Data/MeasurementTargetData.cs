using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{

    #region 차량 관련 데이터
    public class CompanyData
    {
        public List<CarData> carData = new List<CarData>();
        public string companyName;
        public string companyID;
    }

    public class CarData
    {
        public List<SeatData> seatData = new List<SeatData>();
        public List<LampData> lampData = new List<LampData>();

        public string carName;
        public string carID;
    }

    public class SeatData
    {
        public string seatName;
        public string seatID;

        public Vector3 position;
        public List<Vector3> rotation;
    }

    public class LampData
    {
        public string lampName;
        public string lampID;
        public string colorCode;
        public int luminosity;
    }

    #endregion
}
