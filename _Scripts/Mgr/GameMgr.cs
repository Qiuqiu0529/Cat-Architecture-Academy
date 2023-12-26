using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameMgr : MonoBehaviour
{
    public static GameMgr instance;
    public Save save;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Object.Destroy(this.gameObject);
        }
    }
    public void SaveGameData()//���ػ�save����
    {
        if (!Directory.Exists(Application.persistentDataPath + "/gameSave"))
            Directory.CreateDirectory(Application.persistentDataPath + "/gameSave");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameSave/chapter.txt");
        Debug.Log("SaveData");
        var json = JsonUtility.ToJson(save);
        formatter.Serialize(file, json);
    }

    public void LoadSaveData()//��ȡsave����
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gameSave/chapter.txt"))
        {
            Debug.Log("LoadSaveData()");
            FileStream file = File.Open(Application.persistentDataPath + "/gameSave/chapter.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), save);
            file.Close();
        }
    }

    public void AchieveGame(int chapter,float starttime)
    {
        float finishTime = Time.time - starttime;
        if (!save.achieve[chapter])
        {
            save.achieve[chapter] = true;
            save.achieveTime[chapter] = finishTime;
            save.unlockChapter[chapter] = true;//������һ��
            if (save.unlockChapter[4])//��ɵ����
            {
                save.unlockCreate = true;
            }
        }
        else
        {
            if (finishTime < save.achieveTime[chapter])
            {
                save.achieveTime[chapter] = finishTime;//�޸����ʱ��
            }
        }
    }

}
