//-----------------------------------------------------------------------
// changes size of ruler image when button has been pressed
//
// created by Emilia Pyyny-Polat, 2026
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class ScaleRuler : MonoBehaviour
{
    public Image rulerImage;       // image to be scaled
    public float widthStep = 2f; // How much to change width per press
    public DPICalculator dpiCalculator;

    void Start()
    {
        
        Canvas canvas = GetComponentInParent<Canvas>();

    }

    public void IncreaseWidth()
    {
        RectTransform rt = rulerImage.rectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x + widthStep, rt.sizeDelta.y);
        dpiCalculator.RecalculateDPI();     // recalculate DPI
    }

    public void DecreaseWidth()
    {
        RectTransform rt = rulerImage.rectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x - widthStep, rt.sizeDelta.y);
        dpiCalculator.RecalculateDPI();     // recalculate DPI
    }
}
