using MemoryPack;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/*메모리팩 사용시 주의점
메모리팩에 저장할 데이터는 partial class로 선언되어야 한다.

메모리팩에 저장할 데이터 클래스는 [MemoryPackable], '[Serializable]어트리뷰트가 추가되어있어야 한다.

메모리팩에 저장될 데이터는 [MemoryPackOrder()] 를 추가해주어야 한다.

메모리팩에 저장하지 않을 데이터는 [MemoryPackIgnore] 를 추가해주어야 한다.

메모리팩에 저장할 데이터 클래스는 [MemoryPackConstructor] 어트리뷰트를 지닌 생성자를 갖고 있어야 한다.
생성자는 저장할 데이터들을 모두 매개변수에 동일한 이름으로 추가하고 this.value = value; 형식으로 모두 추가해주어야 한다.

메모리팩 저장할 데이터 중 개발자의 커스텀 클래스가 있으면, 해당 클래스에도 1번부터 5번까지의 작업을 수행해주어야 한다.*/

public class Memory : MonoBehaviour
{
    public void Start()
    {
        // 시리얼라이즈 초기 설정은 필요 없음
        // 시리얼라이즈한 것을 디시리얼라이즈 해본다
        var data = new SaveData(1, "메모리팩 테스트 스트링");

        // 직렬화 함수
        var serialized = MemoryPackSerializer.Serialize(data);
        // 역직렬화 함수
        var deserialized = MemoryPackSerializer.Deserialize<SaveData>(serialized);

        Debug.Log($"Id={deserialized.Id}, Message={deserialized.Message}");

        CreateOrSaveJsonFile(Application.dataPath, "SaveData", serialized);
        SaveData res = LoadJsonFile<SaveData>(Application.dataPath, "SaveData");
        Debug.Log($"Id={res.Id}, Message={res.Message}");
    }

    // 파일 저장 함수
    public void CreateOrSaveJsonFile(string createPath, string fileName, byte[] data)
    {
        string file = string.Format("{0}/{1}.json", createPath, fileName);

        if (File.Exists(file))
        {
            File.Delete(file);
        }

        FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // 파일 불러오기 함수
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        var deserialized = MemoryPackSerializer.Deserialize<T>(data);
        return deserialized;
    }
}
