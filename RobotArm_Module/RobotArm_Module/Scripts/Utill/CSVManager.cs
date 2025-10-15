using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    

    class CSVManager
    {
        public List<CSVData> LoadDataFromCsv(string name,string filePath)
        {
            var dataList = new List<CSVData>();

            int expectedCount = CSVData.ExpectedFieldCount;
            string path = filePath + name + ".csv";

            if (!File.Exists(path))
            {
                Console.WriteLine($"오류: 파일을 찾을 수 없습니다. 경로: {path}");
                return dataList;
            }

            // using을 사용하여 파일 스트림을 안전하게 해제
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 4)
                    {
                        CSVData data = new CSVData();
                        data.radianX = double.Parse(parts[0]);
                        data.radianY = double.Parse(parts[1]);
                        data.radianZ = double.Parse(parts[2]);
                        data.timeStamp = int.Parse(parts[3]);
                        
                        dataList.Add(data);
                    }
                }
            }
            foreach (var item in dataList)
            {
                Console.WriteLine($"X: {item.radianX}, Y: {item.radianY}, Z: {item.radianZ}, Time: {item.timeStamp}");
            }

            return dataList;
        }
    }
}
