using MemoryPack;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/*�޸��� ���� ������
�޸��ѿ� ������ �����ʹ� partial class�� ����Ǿ�� �Ѵ�.

�޸��ѿ� ������ ������ Ŭ������ [MemoryPackable], '[Serializable]��Ʈ����Ʈ�� �߰��Ǿ��־�� �Ѵ�.

�޸��ѿ� ����� �����ʹ� [MemoryPackOrder()] �� �߰����־�� �Ѵ�.

�޸��ѿ� �������� ���� �����ʹ� [MemoryPackIgnore] �� �߰����־�� �Ѵ�.

�޸��ѿ� ������ ������ Ŭ������ [MemoryPackConstructor] ��Ʈ����Ʈ�� ���� �����ڸ� ���� �־�� �Ѵ�.
�����ڴ� ������ �����͵��� ��� �Ű������� ������ �̸����� �߰��ϰ� this.value = value; �������� ��� �߰����־�� �Ѵ�.

�޸��� ������ ������ �� �������� Ŀ���� Ŭ������ ������, �ش� Ŭ�������� 1������ 5�������� �۾��� �������־�� �Ѵ�.*/

public class Memory : MonoBehaviour
{
    public void Start()
    {
        // �ø�������� �ʱ� ������ �ʿ� ����
        // �ø���������� ���� ��ø�������� �غ���
        var data = new SaveData(1, "�޸��� �׽�Ʈ ��Ʈ��");

        // ����ȭ �Լ�
        var serialized = MemoryPackSerializer.Serialize(data);
        // ������ȭ �Լ�
        var deserialized = MemoryPackSerializer.Deserialize<SaveData>(serialized);

        Debug.Log($"Id={deserialized.Id}, Message={deserialized.Message}");

        CreateOrSaveJsonFile(Application.dataPath, "SaveData", serialized);
        SaveData res = LoadJsonFile<SaveData>(Application.dataPath, "SaveData");
        Debug.Log($"Id={res.Id}, Message={res.Message}");
    }

    // ���� ���� �Լ�
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

    // ���� �ҷ����� �Լ�
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
