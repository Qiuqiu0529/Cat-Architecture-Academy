using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
public class SceneItemMgr : MonoBehaviour
{
    public List<Item> items;
    public static SceneItemMgr instance;
    public Item choseItem;

   
    public GameObject changeSliders;

    [Header("FB")]
    public MMF_Player rotX, rotY, rotZ;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    public void AddItem(Item addItem)
    {
        items.Add(addItem);
        addItem.SceneItemNum = items.Count-1;
    }
    public void DeleteItem()
    {
        if (choseItem != null)
        {
            choseItem.Delete();
           // Destroy(choseItem.gameObject);
            CloseChooseItem();
        }
    }
    public void ResetItemNum()
    {
        for (int i = 0; i < items.Count; ++i)
        {
            items[i].SceneItemNum = i;
        }
    }
    public void ChooseItem(Item chooseItem)
    {
        if (choseItem != chooseItem)
        {
            choseItem = chooseItem;
            changeSliders.SetActive(true);
        }
        else
        {
            CloseChooseItem();
        }
    }
    public void CloseChooseItem()
    {
        changeSliders.SetActive(false);
        choseItem = null;
    }
    #region Change
  
    public void ChangeRotX()
    {
        if (choseItem != null)
            choseItem.ChangeRotX();
    }
    public void ChangeRotY()
    {
        if (choseItem != null)
            choseItem.ChangeRotY();
    }
    public void ChangeRotZ()
    {
        if (choseItem != null)
            choseItem.ChangeRotZ();
    }

    #endregion

    #region Action

    public class Action
    {
        public Item itemNo;
        public virtual void RedoAction() { }
        public virtual void UndoAction() { }
       
    }

      public class ActionRotItem:Action
    {
        public int rotDir;
        public ActionRotItem(Item no, int dir)
        {
            itemNo = no;
            rotDir = dir;
        }

        public override void RedoAction()
        {
            switch (rotDir)
            {
                case Global.xRot:
                    itemNo.ChangeRotX();
                    break;
                case Global.yRot:
                    itemNo.ChangeRotY();
                    break;
                case Global.zRot:
                    itemNo.ChangeRotZ();
                    break;
            }
        }

        public void UndoAction()
        {
            switch (rotDir)
            {
                case Global.xRot:
                    itemNo.ChangeRotXBack();
                    break;
                case Global.yRot:
                    itemNo.ChangeRotYBack();
                    break;
                case Global.zRot:
                    itemNo.ChangeRotZBack();
                    break;
            }
        }


    }

    public List<Action> actions = new List<Action>();
    public int topPointer = -1;

    public void AddAction(int no, int x, int y, int z)
    {
        if (topPointer < actions.Count - 1)
        {
            for (int i = actions.Count - 1; i > topPointer; --i)
            {
                //删除动作
                actions.RemoveAt(i);

            }
        }
        //ActionMoveBlock actionMove = new ActionMoveBlock(no, x, y, z);
        //actions.Add(actionMove);
        topPointer++;
    }

    public void RedoAction()
    {
        if (topPointer < actions.Count - 1 && actions.Count > 0)
        {
            topPointer++;
            //撤回

        }
    }

    public void UndoAction()
    {
       
        if (topPointer >= 0)
        {
            //重置行为
            topPointer--;
        }
    }
    #endregion
}