using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasToActivate;
    public GameObject canvasToSwap;

    public void SwapCrafting()
    {
        canvasToActivate.SetActive(false);
        canvasToSwap.SetActive(true);
    }

    public void CloseCrafting()
    {
        canvasToActivate.SetActive(false);
        Time.timeScale = 1;

    }
}
