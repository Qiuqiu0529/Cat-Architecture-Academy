using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
public class diatest : MonoBehaviour
{
    [TextArea]
    public string text;

    public Text myDia;
    public GameObject Object;
    public CanvasGroup canvas;
    public CanvasGroup one;
    public MMF_Player player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetTextUi());
    }

    public IEnumerator SetTextUi()
    {
        myDia.text = "";
        while (one.alpha < 1)
        {
            one.alpha += 0.01f;
            yield return new WaitForFixedUpdate();
        }
        
        for (int i = 0; i < text.Length; ++i)
        {
            myDia.text += text[i];
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(2f);
        player.PlayFeedbacks();
        while (canvas.alpha > 0)
        {
            canvas.alpha -= 0.02f;
            yield return new WaitForFixedUpdate();
        }
        
        Object.SetActive(false);
       
        //MoveBlock.instance.startTime = Time.time;
    }

}
