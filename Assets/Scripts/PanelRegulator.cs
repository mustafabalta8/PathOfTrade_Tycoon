using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRegulator : MonoBehaviour
{
    GridLayoutGroup glg;
    // Start is called before the first frame update
    void Start()
    {
        glg = gameObject.GetComponent<GridLayoutGroup>();
        
        RectTransform panelRect = (RectTransform)gameObject.transform;
        Debug.Log("Width: " + panelRect.rect.width);
       // if (Resolution.Equals(1366, 768))
            glg.cellSize = new Vector2((panelRect.rect.width / 2) - 80, glg.cellSize.y);
           // Debug.Log("it works...");
        
        
      
    }

}
