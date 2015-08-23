using UnityEngine;
using System.Collections;

public class WallTextureAssigner : MonoBehaviour
{

    public Texture[] albedos;
    public Texture[] normals;

    // Use this for initialization
    void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(var rend in renderers)
        {
            rend.material.SetTexture("_MainTex", albedos[Random.Range(0, albedos.Length)]);
            rend.material.SetTexture("_BumpMap", normals[Random.Range(0, normals.Length)]);
        }
    }


}
