using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMgr : MonoBehaviour
{
    GridBuildingSystem<int> buildingSystem;
    public LineRenderer line;
    public float cellSize;
    [SerializeField] protected int length;//x
    [SerializeField] protected int width;//z
    [SerializeField] protected int height;
    public static GridMgr instance;

    [SerializeField] LayerMask mousecollider;
    //  [SerializeField] BoxCollider collider;
    Vector3 startPos;

    public GameObject camTarget;
    public bool createBase;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        startPos = transform.position;
        buildingSystem = new GridBuildingSystem<int>(length, width, height, cellSize, startPos);
    }
    private void Start()
    {
        DrawLine();
        //  StartCoroutine(Print());
        if (createBase)
        {
            CreateBase();
        }
    }
    void DrawLine()
    {
        List<Vector3> points = new List<Vector3>();
        for (int y = 0; y <= height; y += height)
        {
            points.Add(new Vector3(0, y, 0) * cellSize);
            points.Add(new Vector3(length, y, 0) * cellSize);
            points.Add(new Vector3(length, y, width) * cellSize);
            points.Add(new Vector3(0, y, width) * cellSize);
            points.Add(new Vector3(0, y, 0) * cellSize);
        }

        for (int z = 0; z <= width; z += width)
        {
            points.Add( new Vector3(0, 0, z) * cellSize);
            points.Add( new Vector3(length, 0, z) * cellSize);
            points.Add( new Vector3(length, height, z) * cellSize);
            points.Add( new Vector3(0, height, z) * cellSize);
            points.Add( new Vector3(0, 0, z) * cellSize);
        }
        for (int x = 0; x <= length; x += length)
        {
            points.Add( new Vector3(x, 0, 0) * cellSize);
            points.Add( new Vector3(x, height, 0) * cellSize);
            points.Add( new Vector3(x, height, width) * cellSize);
            points.Add( new Vector3(x, 0, width) * cellSize);
            points.Add( new Vector3(x, 0, 0) * cellSize);
        }
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
    void CreateBase()
    {
        camTarget.transform.position = new Vector3((float)length / 2, 0.1f, (float)width / 2);
        camTarget.GetComponent<BoxCollider>().size = new Vector3(length, 0.1f, width);
    }

    public void DebugPrint()
    {
        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                for (int k = 0; k < width; ++k)
                {
                    Debug.Log(i.ToString() + j.ToString() + k.ToString() + buildingSystem.gridArray[i, j, k]);
                }
            }
        }
    }
    IEnumerator Print()
    {
        yield return new WaitForSeconds(7f);
        DebugPrint();
    }

    public static Vector3 GetMouseWorldPosition() => instance.GetMouseWorldPosition_Instance();

    Vector3 GetMouseWorldPosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mousecollider))
        {
            return raycastHit.point;
        }
        else
            return Vector3.zero;
    }

    public Vector3 GridPos(int x, int y, int z)
    {
        Vector3 pos = new Vector3();
        pos.x = (x * cellSize) ;
        pos.y = (y * cellSize) ;
        pos.z = (z * cellSize) ;
        return pos;
    }

    public bool CanBuild(Vector3 pos, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((pos - startPos).x / cellSize);
        y = Mathf.FloorToInt((pos - startPos).y / cellSize);
        z = Mathf.FloorToInt((pos - startPos).z / cellSize);
        return buildingSystem.CanBuildXYZ(x, y, z);
    }
    public bool IsIn(Vector3 pos)
    {
        return buildingSystem.Inside(pos);
    }
    public void SetPos(Vector3 pos, bool flag = true)
    {
        buildingSystem.SetValue(pos, flag);
    }
    public void SetGridValue(int x, int y, int z, bool flag = true)
    {
        buildingSystem.SetGridValue(x, y, z, flag);
    }
    public void SetGridId(int x, int y, int z, int id)
    {
        buildingSystem.SetGridOtherValue(x, y, z, id);//????id
    }
    public int GetGridId(int x, int y, int z)
    {
        return buildingSystem.gridObjects[x, y, z];
    }
    public bool GetGridBool(int x, int y, int z)
    {
        return buildingSystem.gridArray[x, y, z];
    }

    public bool CanMove(int x, int y, int z, int dir, int change, int blockId)//1x????-1 x????2y????3z??
    {
        switch (dir)
        {
            case 1:
                if ((x + change < length)&&(!buildingSystem.gridArray[x+change,y,z] ||
                    buildingSystem.gridObjects[x + change, y, z] == blockId))
                    return true;
                break;
            case -1:
                if ((x - change >= 0) && (!buildingSystem.gridArray[x - change, y, z] ||
                    buildingSystem.gridObjects[x - change, y, z] == blockId))
                    return true;
                break;

            case 2:
                if ((y + change < height) &&(!buildingSystem.gridArray[x , y+change, z] ||
                    buildingSystem.gridObjects[x, y + change, z] == blockId))
                    return true;
                break;
            case -2:
                if ((y - change >= 0) && (!buildingSystem.gridArray[x, y - change, z] ||
                    buildingSystem.gridObjects[x, y - change, z] == blockId))
                    return true;
                break;

            case 3:
                if ((z + change < width) && (!buildingSystem.gridArray[x, y , z+ change] ||
                     buildingSystem.gridObjects[x, y, z + change] == blockId))
                    return true;
                break;
            case -3:
                if ((z - change >= 0) &&(!buildingSystem.gridArray[x, y, z - change] ||
                    buildingSystem.gridObjects[x, y, z - change] == blockId))
                    return true;
                break;
            default:
                break;
        }

        return false;
    }

}