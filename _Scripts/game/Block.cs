using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<int> xList = new List<int>();
    public List<int> yList = new List<int>();
    public List<int> zList = new List<int>();

    public int blockID;//??1??????
    public Global.BLOCKTYPE blockType;

    /* public GameObject prefab;
     public Queue<GameObject> grids;*/

    private void Start()
    {
        blockID = MoveBlock.instance.GetBlockCount(this);
        transform.localPosition = GridMgr.instance.GridPos(xList[0], yList[0], zList[0]);
        for (int i = 0; i < xList.Count; ++i)
        {
            AddGrid(xList[i], yList[i], zList[i]);
        }
    }

    public void AddGrid(int x, int y, int z)
    {

        GridMgr.instance.SetGridValue(x, y, z);
        GridMgr.instance.SetGridId(x, y, z, blockID);
    }

    public void ChangeGridSystem(int xCHange, int yChange, int zChange)
    {
        Vector3 des = transform.localPosition + new Vector3(GridMgr.instance.cellSize * xCHange,
           GridMgr.instance.cellSize * yChange, GridMgr.instance.cellSize * zChange);
        for (int i = 0; i < xList.Count; ++i)
        {
            GridMgr.instance.SetGridId(xList[i], yList[i], zList[i], 0);
            GridMgr.instance.SetGridValue(xList[i], yList[i], zList[i], false);
        }
        for (int i = 0; i < xList.Count; ++i)
        {
            xList[i] += xCHange;
            yList[i] += yChange;
            zList[i] += zChange;
            GridMgr.instance.SetGridId(xList[i], yList[i], zList[i], blockID);
            GridMgr.instance.SetGridValue(xList[i], yList[i], zList[i], true);
        }
        MoveBlock.instance.Moving(des, xCHange, yChange, zChange);
    }

    public int CanMove(int dir)
    {
        int change = 1;
        bool test = true;
        for (; test; change++)
        {
            for (int i = 0; i < xList.Count; i++)
            {
                if (!GridMgr.instance.CanMove(xList[i], yList[i], zList[i], dir, change, blockID))
                {
                    return change - 1;
                }
            }
        }
        return 0;
    }

    public int MoveStep(int dir, out int finalDir)
    {
        int moveStep;
        int stepPlus;
        int stepMinus;
        bool plus = true;
        stepPlus = CanMove(dir);
        stepMinus = CanMove(-dir);
        plus = (stepPlus >= stepMinus);
        if (plus)
        {
            finalDir = dir;
            moveStep = stepPlus;
        }
        else
        {
            finalDir = -dir;
            moveStep = stepMinus;
        }
        return moveStep;
    }

    public bool Move()
    {
        int dir = 0;
        int moveStep = 0;
        switch (blockType)
        {
            case Global.BLOCKTYPE.xMOVE:
                moveStep = MoveStep(1, out dir);
                break;
            case Global.BLOCKTYPE.yMOVE:
                moveStep = MoveStep(2, out dir);
                break;
            case Global.BLOCKTYPE.zMOVE:
                moveStep = MoveStep(3, out dir);
                break;
            default:
                break;
        }
        if (moveStep <= 0)
        {
            CantMove();
            return false;
        }
        int xChange=0, yChange=0, zChange=0;
        switch (dir)
        {
            case 1:
                xChange = moveStep;
                break;
            case -1:
                xChange = -moveStep;
                break;
            case 2:
                yChange = moveStep;
                break;
            case -2:
                yChange = -moveStep;
                break;
            case 3:
                zChange = moveStep;
                break;
            case -3:
                zChange = -moveStep;
                break;
            default:
                break;
        }

        MoveBlock.instance.AddAction(blockID - 1, xChange, yChange, zChange);
        ChangeGridSystem(xChange,yChange, zChange);
        return true;
    }

    public bool BeChosen()
    {
        Debug.Log("bechosen");
        return Move();
    }

    public void CantMove()
    {
        MoveBlock.instance.Continue();
    }
    
}
