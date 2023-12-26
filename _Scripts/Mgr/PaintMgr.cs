using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;
using CSharp.UI.ColorBoard;
using MoreMountains.Feedbacks;
public class PaintMgr : MonoBehaviour
{
    public P3dPaintSphere paint;
    public Slider opacity, radius, hardness,colorR,colorB,colorG;
    public Image brush;

    public ColorBoard colorBoard;
    private void Start()
    {
        colorBoard.OnColorChanged += ImageShowColor;
        
    }
    void ImageShowColor(Color color)
    {
        var _rawColor = paint.Color;
        Color newcolor = new Color(color.r, color.g, color.b, _rawColor.a);
        paint.Color = newcolor;
        brush.color = newcolor;
       
    }

    public void SetOpacity()
    {
        //ChooseColor();
        var _rawColor = paint.Color;
        Color color = new Color(_rawColor.r, _rawColor.g, _rawColor.b, opacity.value);
        paint.Color = color;
        brush.color = color;
    }
    public void SetRedius()
    {
        paint.Radius = radius.value;
    }

    public void SetHardness()
    {
        paint.Hardness=hardness.value;
    }

    public void ChooseColor()
    {
        Color color=new Color(colorR.value, colorG.value, colorB.value,opacity.value);
        paint.Color = color;
        brush.color = color;
    }
}
