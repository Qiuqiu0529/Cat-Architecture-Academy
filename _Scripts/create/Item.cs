using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Item : MonoBehaviour
{
    public Slot parent;
    
    public int IDType;

    public int SceneItemNum = 0;

    public int x, y, z;//方格位置

    public GameObject cantBuildd;

    public BoxCollider box;
    public bool init;

    public MMFeedbacks initFB;
    public MMFeedbacks deleteFB;
    public MMFeedbacks rotXFB,rotYFB,rotZFB,rotXBack,rotYBack,rotZBack;
    public void Init(int x, int y, int z)//位置固定时
    {
        CanBuild();
        initFB.PlayFeedbacks();
        init = true;
        this.x = x;
        this.y = y;
        this.z = z;
        GridMgr.instance.SetGridValue(x, y, z, true);//格子被占领
        box.enabled = true;
    }

    public void OnMouseDown()
    {
        SceneItemMgr.instance.ChooseItem(this);
    }

    public void Delete()
    {
        deleteFB.PlayFeedbacks();
        parent.AddLimitCount();//
        GridMgr.instance.SetGridValue(x, y, z, false);//置空
        StartCoroutine(Del());
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
    #region Seen
    public void CanBuild()
    {
        //canBuild.SetActive(true);
        cantBuildd.SetActive(false);
    }
    public void CantBuild()
    {
        //canBuild.SetActive(false);
        cantBuildd.SetActive(true);
    }

    #endregion

    #region Change
   
    public void ChangeRotX()
    {
        rotXFB.PlayFeedbacks();
    }
    public void ChangeRotY()
    {
        rotYFB.PlayFeedbacks();
    }
    public void ChangeRotZ()
    {
        rotZFB.PlayFeedbacks();
    }

    public void ChangeRotXBack()
    {
        rotXBack.PlayFeedbacks();
    }
    public void ChangeRotYBack()
    {
        rotYBack.PlayFeedbacks();
    }
    public void ChangeRotZBack()
    {
        rotZBack.PlayFeedbacks();
    }

    #endregion


}
