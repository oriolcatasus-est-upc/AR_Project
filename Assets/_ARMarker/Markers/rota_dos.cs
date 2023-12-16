using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rota_dos : MonoBehaviour
{
    // Start is called before the first frame update
    public float vel = 50f;
    public Transform tapa;

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(tapa.transform.position,Vector3.right,vel*Time.deltaTime);
    }
}
