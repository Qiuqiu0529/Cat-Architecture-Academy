using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Star: MonoBehaviour
{
    public int x, y, z;
    public int starID;
    public GameObject starKid;
    public GameObject transparentt;
    public MMF_Player starFB;
   // public ParticleSystem starPa;
    private void Start()
    {
        starID = MoveBlock.instance.GetStarCount(this);
        HideStar();
        transform.localPosition = GridMgr.instance.GridPos(x,y,z);
    }

    public void SeeStar()
    {
        if (!starKid.activeSelf)
            starFB.PlayFeedbacks();
        starKid.SetActive(true);
        transparentt.SetActive(false);
    }

    public void HideStar()
    {
        starKid.SetActive(false);
        transparentt.SetActive(true);
    }
}
