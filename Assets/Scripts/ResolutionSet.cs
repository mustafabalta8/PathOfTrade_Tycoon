using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ResolutionSet : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [SerializeField] GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        ScaleGridOnStart();
        resolutions = Screen.resolutions;  // total 12 resolution options;  24 in PC because of refreshRate
        resolutions = resolutions.OrderByDescending(res => res.refreshRate).ToArray();
        DefineResolutions();

       /* foreach (var res in resolutions)
        {
            Debug.Log("Resolution: "+res.width + "x" + res.height + " : " + res.refreshRate);
        }*/
    }
    private void DefineResolutions()
    {
        List<string> options = new List<string>();
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
      
        for (int i = 0; i < resolutions.Length; i++)
        {
            //if (i != resolutions.Length-1)
            // {
            //if (resolutions[i].width != resolutions[i + 1].width)
            // {
            if (resolutions[i].refreshRate == 60)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                if (i >= 5)
                {
                    options.Add(option);
                   // Debug.Log("option:" + option);
                }
            }     
               // }
           // }
           /* if (i == resolutions.Length - 1)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
                //Debug.Log(option);
            }*/

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                resolutionDropdown.value = currentResolutionIndex;
            }
        }
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int ResolutionIndex)
    {
        Resolution resolution = resolutions[ResolutionIndex+5];//+5)*2
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
       // Debug.Log(resolution.width + "x" + resolution.height);
        if (resolution.width == 1920 || resolution.width == 1680 || resolution.width == 1600)
        {
            GridSizeRegulation(400, 105);

        }
        else if (resolution.width == 1366 || resolution.width == 1280 || resolution.width == 1360 || resolution.width == 1440)
        {
            GridSizeRegulation(300, 80);
        }
    }
    void ScaleGridOnStart()
    {
        if (Screen.width == 1920 || Screen.width == 1680 || Screen.width == 1600)
        {
            GridSizeRegulation(400, 105);

        }
        else if (Screen.width == 1366 || Screen.width == 1280 || Screen.width == 1360 || Screen.width == 1440)
        {
            GridSizeRegulation(300, 80);
        }
    }
    void GridSizeRegulation(int width,int height)
    {
        GridLayoutGroup menu = Menu.GetComponent<GridLayoutGroup>();
        menu.cellSize = new Vector2(width, height);
    }

}
