using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DissolvedEffect : MonoBehaviour
{
    public Material material;
    public Slider controlDissolve;
   

    public void StartControlDissolve()
    {
        controlDissolve.gameObject.SetActive(true);
    }
    public void SetDissolve()
    {
        material.SetFloat("_Fade", controlDissolve.value);
    }
    public void EndControlDissolve()
    {
        controlDissolve.gameObject.SetActive(false);
    }
    public void AddDissolve()
    {
        material.SetFloat("_Fade", controlDissolve.value+0.2f);
    }
    public void MinusDissolve()
    {
        material.SetFloat("_Fade", controlDissolve.value - 0.2f);
    }

    public void ChangeModeToX()
    {
        material.SetFloat("_modeX", 1);
        material.SetFloat("_modeY", 1);
        
    }

    public void ChangeModeToY()
    {
        material.SetFloat("_modeX", 0);
        material.SetFloat("_modeY", 1);
    }
    public void ChangeModeToZ()
    {
        material.SetFloat("_modeX", 0);
        material.SetFloat("_modeY", 0);
    }
}
