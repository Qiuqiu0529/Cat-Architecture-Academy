using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new Save",menuName ="_Scripts/Save")]
public class Save : ScriptableObject
{
    public bool[] achieve = new bool[5];
    public bool[] unlockChapter = new bool[5];//Ĭ�Ͽ�����һ�أ���4����������ģʽ
    public int[] stepCount = new int[5];
    public bool unlockCreate;
    public float[] achieveTime = new float[5];
}
