using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform LookAt;
    private Vector3 offSet = new Vector3(0,0,-6.00f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = LookAt.transform.position + offSet;
    }
}
