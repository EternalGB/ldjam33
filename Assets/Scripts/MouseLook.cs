using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{

    public Vector2 sensitivity, xMinMaxAngle, yMinMaxAngle;

    Quaternion origRotation;
    Vector2 rot;

    // Use this for initialization
    void Start()
    {
        origRotation = transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rot.x += Input.GetAxis("Mouse X") * sensitivity.x;
        rot.y += Input.GetAxis("Mouse Y") * sensitivity.y;

        rot.x = ClampAngle(rot.x, xMinMaxAngle);
        rot.y = ClampAngle(rot.y, yMinMaxAngle);

        transform.localRotation = origRotation * 
            Quaternion.AngleAxis(rot.x, Vector3.up) * 
            Quaternion.AngleAxis(rot.y, -Vector3.right);
    }

    public float ClampAngle(float angle, Vector2 minMax)
    {
        return ClampAngle(angle, minMax.x, minMax.y);
    }

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
