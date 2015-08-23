using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{

    public Image filling;

    public void SetFill(float value)
    {
        filling.fillAmount = value;
    }

}
