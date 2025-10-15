using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class JsonManager
    {

        public static void ExportToJsonFile<T>(T data,string fileName, string filePath)
        {
            // 읽기 쉽도록 들여쓰기 옵션 설정 (Formatting.Indented)
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            // JSON 문자열을 파일에 쓰기
            File.WriteAllText(filePath + fileName + ".json", jsonString);

            Console.WriteLine($"[Export 성공] 객체가 JSON 파일로 저장되었습니다: {filePath}");
        }

        public static T ImportFromJsonFile<T>(string name,string filePath)
        {
            string path = filePath + name + ".json";
            if (!File.Exists(path))
            {
                Console.WriteLine($"[Import 실패] 파일을 찾을 수 없습니다: {filePath}");
                return default(T);
            }

            try
            {
                string jsonString = File.ReadAllText(path);

                T data = JsonConvert.DeserializeObject<T>(jsonString);

                Console.WriteLine($"[Import 성공] JSON 파일에서 객체 가져오기 완료: {path}");
                return data;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Import 오류] JSON 역직렬화 오류: {ex.Message}");
                return default(T);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[Import 오류] 파일 읽기 오류: {ex.Message}");
                return default(T);
            }
        }
    }
}
