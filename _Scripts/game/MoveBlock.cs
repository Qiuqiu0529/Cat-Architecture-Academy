using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.UI;
public class MoveBlock : MonoBehaviour
{
    public static MoveBlock instance;
    Block chooseBlock;
    int dir;
    Vector3 oldPos1;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] bool isMoving;
    int blockCount;
    int starCount;
    List<Block> blockMgr = new List<Block>();
    List<Star> starMgr = new List<Star>();

    [SerializeField] int chapter;
    public float startTime;
    public MMFeedbacks achieveEffect;
    [SerializeField] bool achieve;
    [SerializeField] int achieveStarCount = 0;
    int step = 0;

    public GameObject moveTarget;
    public MMF_SquashAndStretch stretch;
    public MMF_Position position;
    public MMF_Player moveFB;


    public Text secondUI;
    public Text minuteUI;
    public Text achieveStarUI;
    public MMFeedbacks starUpFB;
    public MMFeedbacks cantmoveFB;

    public CanvasGroup undocanvas, redocanvas;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void Start()
    {
        stretch = moveFB.GetFeedbackOfType<MMF_SquashAndStretch>();
        position = moveFB.GetFeedbackOfType<MMF_Position>();
        achieve=false;
        isMoving = false;
        startTime = Time.time;
        topPointer = -1;
        StartCoroutine(StartCheck());
    } 
    public void ResetStartTime()
    {
        startTime=Time.time;
    }
    IEnumerator StartCheck()
    {
        yield return new WaitForSeconds(0.3f);
        CheckStar();
    }


    public int GetStarCount(Star star)
    {
        starMgr.Add(star);
        return starCount++;
    }

    public int GetBlockCount(Block block)
    {
        blockMgr.Add(block);
        return ++blockCount;
    }

    void Update()
    {
        if (!achieve)
        {
            secondUI.text = Mathf.FloorToInt((Time.time - startTime) % 60).ToString();
            minuteUI.text = Mathf.FloorToInt((Time.time - startTime) / 60).ToString();
            if(Input.GetMouseButtonDown(0) ||(Input.touchCount==1&&Input.GetTouch(0).phase == TouchPhase.Began))
            {
                MoveControl();
            }
        }
    }

    public void StopWatch()
    {
        achieve = true;
    }
    public void StartWatch()
    {
        startTime = Time.time;
    }

    public void CheckStar()
    {
        int count = 0;
        int preCount = achieveStarCount;
        foreach (var star in starMgr)
        {
            if (GridMgr.instance.GetGridBool(star.x, star.y, star.z))
            {
                star.HideStar();
            }
            else
            {
                ++count;
                star.SeeStar();
            }
        }
        ++step;
        achieveStarCount = count;
        achieveStarUI.text = achieveStarCount.ToString();
        if (achieveStarCount > preCount)
        {
            starUpFB.PlayFeedbacks();
        }
        if (count == starMgr.Count)
        {
            achieve = true;
            // achieveEffect.PlayFeedbacks();
            GameMgr.instance.AchieveGame(chapter, startTime);
        }
    }

    public void Moving(Vector3 des, int xChange, int yChange, int zChange)
    {
        Debug.Log(chooseBlock.name+"Moving");
        
        moveTarget.transform.localPosition = des;
        position.AnimatePositionTarget = chooseBlock.gameObject;
        stretch.SquashAndStretchTarget = chooseBlock.transform;
        switch (chooseBlock.blockType)
        {
            case Global.BLOCKTYPE.xMOVE:
                stretch.Axis = MMF_SquashAndStretch.PossibleAxis.XtoYZ;
                break;
            case Global.BLOCKTYPE.yMOVE:
                stretch.Axis = MMF_SquashAndStretch.PossibleAxis.YtoXZ;
                break;
            case Global.BLOCKTYPE.zMOVE:
                stretch.Axis = MMF_SquashAndStretch.PossibleAxis.ZtoXZ;
                break;
            default:
                break;
        }

        moveFB.PlayFeedbacks();
        StartCoroutine(Recover());
        //moveblock.
    }
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.3f);
        Continue();
    }

    public void Continue()
    {
        CheckStar();
        chooseBlock = null;
        isMoving = false;

    }
    public void MoveControl()
    {
        if (isMoving)
            return;
        if (achieve)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; //���߼�ⷵ�ص���Ϣ
        if (Physics.Raycast(ray, out hit, 1000, blockLayer))
        {
            if (hit.collider.CompareTag("block"))
            {
                oldPos1 = hit.point;
                chooseBlock = hit.collider.GetComponent<Block>();
                Debug.Log(chooseBlock.name);
                isMoving = true;
                bool ifcanmove = chooseBlock.BeChosen();
                if (!ifcanmove)
                {
                    cantmoveFB.PlayFeedbacks();
                }

            }
        }
        //}

    }


    public class ActionMoveBlock
    {
        public int blockNo;
        public int xChange, yChange, zChange;
        public ActionMoveBlock(int no, int x, int y, int z)
        {
            blockNo = no;
            xChange = x;
            yChange = y;
            zChange = z;
        }
    }

    public List<ActionMoveBlock> moveAction = new List<ActionMoveBlock>();
    public int topPointer = -1;

    public void AddAction(int no, int x, int y, int z)
    {
        if (topPointer < moveAction.Count - 1)
        {
            for (int i = moveAction.Count - 1; i > topPointer; --i)
            {
                moveAction.RemoveAt(i);
            }
        }
        ActionMoveBlock actionMove = new ActionMoveBlock(no, x, y, z);
        moveAction.Add(actionMove);
        topPointer++;
        redocanvas.alpha = 0.5f;
        undocanvas.alpha = 1f;
    }

    public void RedoAction()
    {
        if (achieve)
            return;
       
        if (topPointer == moveAction.Count - 2)
        {
            redocanvas.alpha = 0.5f;
        }
        if (topPointer < moveAction.Count - 1 && moveAction.Count > 0)
        {
            undocanvas.alpha = 1f;
            topPointer++;
            chooseBlock = null;
            StopCoroutine(Recover());
            chooseBlock = blockMgr[moveAction[topPointer].blockNo];
            chooseBlock.ChangeGridSystem(moveAction[topPointer].xChange,
               moveAction[topPointer].yChange, moveAction[topPointer].zChange);
        }

    }

    public void UndoAction()
    {
        if (achieve)
            return;
       
        if (topPointer == 0)
        {
            undocanvas.alpha = 0.5f;
        }
        if (topPointer >= 0)
        {
            redocanvas.alpha = 1f;
            chooseBlock = null;
            StopCoroutine(Recover());
            chooseBlock = blockMgr[moveAction[topPointer].blockNo];
            chooseBlock.ChangeGridSystem(-moveAction[topPointer].xChange,
                -moveAction[topPointer].yChange, -moveAction[topPointer].zChange);
            topPointer--;
        }
    }
}
