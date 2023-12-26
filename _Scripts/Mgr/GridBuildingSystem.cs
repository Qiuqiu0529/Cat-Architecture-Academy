using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem<GridObj>
{ 
    int length;//x
    int width;//z
    int height;//y
    float cellsize;//一个格子大小,默认正方体
    Vector3 startPos;
    public bool[,,] gridArray;//格子是否被占用
    public GridObj[,,] gridObjects;
   
    public GridBuildingSystem(int length,int width,int height,
        float cellSize,Vector3 startPos)
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.cellsize = cellSize;
        this.startPos = startPos;

        gridArray = new bool[length, width, height];
        gridObjects = new GridObj[length, width, height];
    }

    public Vector3 GetWordPos(int x,int y,int z)
    {
        return new Vector3(x,y,z)*cellsize+startPos;
    }
    public void GetXYZ(Vector3 pos,out int x,out int y,out int z)
    {
        x = Mathf.FloorToInt((pos - startPos).x / cellsize);
        y = Mathf.FloorToInt((pos - startPos).y / cellsize);
        z = Mathf.FloorToInt((pos - startPos).z / cellsize);
    }
    public bool Inside(Vector3 pos)
    {
        int x, y, z;
        GetXYZ(pos, out x, out y, out z);
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            return true;
        }
        return false;
    }
    public void SetValue(Vector3 pos, bool flag=true)
    {
        int x, y, z;
        GetXYZ(pos, out x, out y, out z);
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            gridArray[x, y, z] = flag;
        }
    }
    public void SetGridValue(int x,int y,int z,bool flag=true)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            gridArray[x, y, z] = flag;
        }
    }
    public void SetGridOtherValue(int x, int y, int z, GridObj temp)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            gridObjects[x, y, z] = temp;
        }
    }
    public bool CanBuild(Vector3 pos)
    {
        int x, y, z;
        GetXYZ(pos, out x, out y, out z);
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            if (!gridArray[x, y, z])
                return true;
        }
        return false;
    }
    public bool CanBuildXYZ(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < length && y < height && z < width)
        {
            if (!gridArray[x, y, z])
                return true;
        }
        return false;
    }

   
}
