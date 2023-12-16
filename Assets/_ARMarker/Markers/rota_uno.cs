using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rota_uno : MonoBehaviour
{
    public float vel = 20f;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, vel, 0)*Time.deltaTime);
    }
}
