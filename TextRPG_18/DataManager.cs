using System;
using System.Text.Json;
using TextRPG;

public class DataManager
{
    public static DataManager I = new DataManager();

    string baseDirectory;
    string saveDirectory;

    DataManager()
    {
        baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        saveDirectory = Path.Combine(baseDirectory, "SaveData");
    }

    public void Save(Player player)
    {
        string fileName = $"{saveDirectory}\\SaveData_{player.name}.json";
        PlayerJsonModel sD = new PlayerJsonModel(player);
        string jsonStr = sD.SerializeToString();

        //Console.WriteLine(jsonStr);
        File.WriteAllText(fileName, jsonStr);

        // 파일이 존재하는지 확인
        if (File.Exists(fileName))
        {
            string fileContent = File.ReadAllText(fileName);
            if (jsonStr == fileContent)
            {
                Console.WriteLine("성공적으로 저장되었습니다.");
            }
            else
            {
                Console.WriteLine("알수없는 이유로 저장에 실패하였습니다.");
            }
        }
        else
        {
            Console.WriteLine("저장에 실패하였습니다.");
        }
        GameManager.PressEnter();
    }

    public string LoadAll()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
        string[] matchedFiles = Directory.GetFiles(saveDirectory, "SaveData_*");

        string filName; int n;
        while (true)
        {
            Console.Clear();
            n = 1;
            foreach (string file in matchedFiles)
            {
                Console.Write($"{n++}. ");
                filName = Path.GetFileName(file); // "SaveData_**.json" 을 잘라서 **부분만 나오게 수정할 예정
                Console.WriteLine($"{filName}");
            }


            Console.WriteLine("불러올 파일을 선택하세요.");
            Console.WriteLine("0. 신규 캐릭터 생성");
            Console.WriteLine();
            Console.Write("입력 : ");
            string str = Console.ReadLine();
            int input;
            int.TryParse(str, out input);

            if (0 < input && input < matchedFiles.Length + 1)
            {
                return Path.GetFileName(matchedFiles[--input]);
            }
            else if (input == 0)
            {
                return null;
            }
            else
            {
                GameManager.printError(str);
            }
        }
    }

    public Player Load(string saveData)
    {
        // 추후 세이브 데이터 선택 기능 추가 예정
        string fileName = $"SaveData\\" + saveData;

        Player playerData;
        if (File.Exists(fileName))
        {
            string jsonStr = File.ReadAllText(fileName);

            PlayerJsonModel sD = PlayerJsonModel.Deserialize(jsonStr);
            playerData = PlayerJsonModel.ModelToPlayer(sD);
        }
        else
        {
            Console.WriteLine("저장된 파일이 없습니다. 새로운 파일을 생성합니다.\n");
            playerData = null;
        }

        return playerData;
    }
}
