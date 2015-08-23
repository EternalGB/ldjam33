using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{

    public Image[] fills;


    public void SetFill(int index, float value)
    {
        fills[index].fillAmount = value;
    }

}
