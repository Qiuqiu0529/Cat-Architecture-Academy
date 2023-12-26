using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiImgTrack : MonoBehaviour
{
    public ARTrackedImageManager trackmgr;
    private Dictionary<string, GameObject> model = new Dictionary<string, GameObject>();
    public GameObject[] models;
    public GameObject BG;
    private void Awake()
    {
    }

    void Start()
    {
        model.Add("iland", models[1]);
        model.Add("dalitang", models[0]);
    }
    private void Update()
    {
        Debug.Log(ARSession.state);
    }
    void OnEnable() => trackmgr.trackedImagesChanged += OnChanged;

    void OnDisable() => trackmgr.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            BG.SetActive(false);
            model[newImage.referenceImage.name].SetActive(true);
            model[newImage.referenceImage.name].transform.position = newImage.transform.position;
            model[newImage.referenceImage.name].transform.rotation = newImage.transform.rotation;
            model[newImage.referenceImage.name].transform.localScale = newImage.transform.localScale;
            // Handle added event
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            model[updatedImage.referenceImage.name].transform.position = updatedImage.transform.position;
            model[updatedImage.referenceImage.name].transform.rotation = updatedImage.transform.rotation;
            model[updatedImage.referenceImage.name].transform.localScale = updatedImage.transform.localScale;
            // Handle updated event
        }

        foreach (var removedImage in eventArgs.removed)
        {
            BG.SetActive(true);
            model[removedImage.referenceImage.name].SetActive(false);
            // Handle removed event
        }
    }


}

