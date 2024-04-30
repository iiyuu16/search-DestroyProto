using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThruScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerObj;

    [SerializeField]
    private LayerMask wallMask;
    private Camera MainCam;

    private void Awake()
    {
        MainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cutoutPos = MainCam.WorldToViewportPoint(playerObj.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = playerObj.position - transform.position;
        RaycastHit[] hitObjs = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjs.Length; ++i)
        {
            Material[] materials = hitObjs[i].transform.GetComponent<Renderer>().materials;

            for(int m = 0;  m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", 0.1f);
                materials[m].SetFloat("_FalloffSize", 0.05f);
            }
        }

    }
}
