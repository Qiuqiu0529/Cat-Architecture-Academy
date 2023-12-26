using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInvisibal : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    private void OnDisable()
    {
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
