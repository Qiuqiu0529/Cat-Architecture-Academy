using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject[] gameObjects;

    public void SetAllActive(bool flag=true)
    {
        foreach(var gameobj in gameObjects)
        {
            gameobj.SetActive(flag);
        }
    }
}
