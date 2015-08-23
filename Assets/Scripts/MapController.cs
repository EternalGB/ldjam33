using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{

    public float hiddenHeight, shownHeight;

    Vector3 hiddenPos, shownPos;

    bool show = false;
    float lerpTimer, lerpSpeed;
    public float showSpeed;

    void Start()
    {
        hiddenPos = new Vector3(transform.localPosition.x, hiddenHeight, transform.localPosition.z);
        shownPos = new Vector3(transform.localPosition.x, shownHeight, transform.localPosition.z);
        lerpTimer = 0;
    }

    void Update()
    {
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            show = true;
        }
        else
        {
            show = false;

        }

        lerpSpeed = show ? showSpeed : -showSpeed;

        lerpTimer = Mathf.Clamp(lerpTimer + lerpSpeed * Time.deltaTime, 0, 1);

        transform.localPosition = Vector3.Lerp(hiddenPos, shownPos, lerpTimer);
    }


}
