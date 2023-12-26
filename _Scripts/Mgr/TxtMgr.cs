using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtMgr : MonoBehaviour
{
    public List<TextAsset> intro;

    public void Inrto(int id)
    {
        DialogueSystem.instance.GetTextFromFile(intro[id]);
    }

}
