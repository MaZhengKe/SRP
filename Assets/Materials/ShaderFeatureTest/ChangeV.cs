using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeV : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(mat.name);
        Debug.Log("mat.EnableKeyword(_MKRP_SHOW_TEX)");
        mat.EnableKeyword("_MKRP_SHOW_TEX");
        mat.DisableKeyword("_MKRP_SHOW_TEX");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
