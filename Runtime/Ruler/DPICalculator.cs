//-----------------------------------------------------------------------
// calculates DPI of user device based off of scaled ruler image
// loads saved DPI from PlayerPrefs if it exists, otherwise calculates it on startup and saves it to PlayerPrefs
// created by Emilia Pyyny-Polat, 2026
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

public class DPICalculator : MonoBehaviour
{
    public Image rulerImage;   // ruler image
    public Text dpiText;       // UI text
    float DPI; 
    float rulerWidthPixels;     
    float rulerWidthInches = 0.124f/0.0254f; // 124mm in inches (real world size of ruler image)
    public static float CalculatedDPI;      // calculated DPI value to be accessed by other scripts

    private Canvas canvas;
    //private float scaleFactor;  // canvas scale factor
    
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Canvas canvas = GetComponentInParent<Canvas>();
        canvas = GetComponentInParent<Canvas>();
        //scaleFactor = canvas.scaleFactor;

        // Load saved ruler width if it exists
        if (PlayerPrefs.HasKey("RulerWidth"))
        {
            float savedWidth = PlayerPrefs.GetFloat("RulerWidth");
            RectTransform rt = rulerImage.rectTransform;
            rt.sizeDelta = new Vector2(savedWidth, rt.sizeDelta.y);
        }

        // Load saved DPI if it exists
        if (PlayerPrefs.HasKey("SavedDPI"))
        {
            CalculatedDPI = PlayerPrefs.GetFloat("SavedDPI");
        }

        UpdateDPIText();

    }

    public void RecalculateDPI()
    {
        rulerWidthPixels = rulerImage.rectTransform.sizeDelta.x; 
        //CalculatedDPI = (rulerWidthPixels * scaleFactor) / rulerWidthInches;
        CalculatedDPI = (rulerWidthPixels * canvas.scaleFactor) / rulerWidthInches;
        // Save DPI and ruler width
        PlayerPrefs.SetFloat("SavedDPI", CalculatedDPI);
        PlayerPrefs.SetFloat("RulerWidth", rulerWidthPixels);
        PlayerPrefs.Save();

        UpdateDPIText();
    }

    private void UpdateDPIText()
    {
        if (dpiText != null)
            dpiText.text = "DPI: " + CalculatedDPI.ToString("F1");
    }
}