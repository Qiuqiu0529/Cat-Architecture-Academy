using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Slot Parent;
    bool candrag = true;
    Item go;
    int x = 0, y = 0, z = 0;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Vector2 cursorPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Parent.CanCreate())
        {
            candrag = true;
            Vector3 pos = GridMgr.GetMouseWorldPosition();
            go = GameObject.Instantiate(Parent.prefab, pos, Quaternion.identity) as Item;
            go.transform.SetParent(GridMgr.instance.gameObject.transform);
            go.IDType = Parent.TypeID;
            go.parent = Parent;
            //Cursor.SetCursor(cursorTexture, cursorPos, CursorMode.Auto);
        }
        else
            candrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (candrag)
        {
            Vector3 pos =GridMgr.GetMouseWorldPosition();
            if (GridMgr.instance.CanBuild(pos, out x, out y, out z))
            {
                go.CanBuild();
            }
            else
            {
                go.CantBuild();
            }
            pos = GridMgr.instance.GridPos(x, y, z);
            go.transform.position = pos;
            Debug.Log(pos);
           
        }
    }
    public void OnEndDrag(PointerEventData eventData)//获得坐标生成
    {
        if (candrag)
        {
            Vector3 pos = GridMgr.GetMouseWorldPosition();
            if (GridMgr.instance.CanBuild(pos, out x, out y, out z))
            {
                go.CanBuild();
                go.Init(x, y, z);
                pos = GridMgr.instance.GridPos(x, y, z);
                go.transform.position = pos;
                SceneItemMgr.instance.AddItem(go);
                Parent.CreatItem();
            }
            else
            {
                Destroy(go.gameObject);
            }

            //Cursor.SetCursor(null, cursorPos, CursorMode.Auto);
        }
    }
}
