using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillbarUI : MonoBehaviour
{
    [SerializeField]private Image bar;
    public RectTransform rectTransform;
    ///<summary>
    ///Set fill level of bar, expressed as a float from 0 to 1
    ///</summary>
    public void SetFillLevel(float ratio){
        bar.fillAmount = ratio;
    }
}
