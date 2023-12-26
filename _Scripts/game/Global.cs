using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Color transparent=new Color(1,1,1,0);
    public static Color normal= new Color(1, 1, 1, 1);

    public const int tenNormalWall = 0;
    public const int tweNormalWall = 1;
    public const int tenRoundWall = 2;
    public const int twnRoundWall = 3;

    public enum BLOCKTYPE { xMOVE,yMOVE,zMOVE};
   
    public const int xRot = 0;
    public const int yRot = 1;
    public const int zRot = 2;
}
