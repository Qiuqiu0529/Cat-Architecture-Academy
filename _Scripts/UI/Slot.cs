using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    public Image image;
    public Item prefab;
    public int limitCount = 2;
    public int TypeID = 0;
    public void CreatItem()
    {
        --limitCount;
    }

    public void AddLimitCount()
    {
        ++limitCount;
    }

    public bool CanCreate()
    {
        return true;
       /* if (limitCount > 0)
        {
            return true;
        }
        return false;*/
    }

   /* public void ImageCanSeen()
    {
        image.color = Global.normal;
    }

    public void ImageCanNotSeen()
    {
        image.color = Global.transparent;
    }*/
}
