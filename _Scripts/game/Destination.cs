using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class Destination : MonoBehaviour
{
    [SerializeField] int chapter;
    public float startTime;
    public MMFeedbacks achieveEffect;
    bool achieve;
    private void Start()
    {
        startTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!achieve)
        {
            achieve= true;
            achieveEffect.PlayFeedbacks();
            GameMgr.instance.AchieveGame(chapter,startTime);
        }
    }
}
