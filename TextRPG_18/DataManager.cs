using System;
using System.Text.Json;

public class DataManager
{
	public static DataManager I = new DataManager();

	public void Save(Player player)
	{
        string fileName = $"SaveData_{player.name}.json";
        PlayerJsonModel sD = new PlayerJsonModel(player);
        string jsonStr = sD.SerializeToString();

        //Console.WriteLine(jsonStr);
        File.WriteAllText(fileName, jsonStr);

        // 파일이 존재하는지 확인
        if (File.Exists(fileName))
        {
            string fileContent = File.ReadAllText(fileName);
            if(jsonStr == fileContent)
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
    }

    public Player Load(string playerName)
    {
        // 추후 세이브 데이터 선택 기능 추가 예정
        string fileName = $"SaveData_{playerName}.json";

        Player playerData;
        if (File.Exists(fileName))
        {
            string jsonStr = File.ReadAllText($"SaveData_{playerName}.json");

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
