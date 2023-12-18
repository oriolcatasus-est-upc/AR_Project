using System.Collections;
using UnityEngine;

public class OpenTrashBin : MonoBehaviour
{
    [SerializeField]
    private float rotationTop;
    [SerializeField]
    private float rotationSpeed;

    private Transform lid;
    private bool opened = false;
    private bool inMotion = false;

    private void Start()
    {
        lid = transform.GetChild(0);
    }

    public void Select()
    {
        if (inMotion)
        {
            return;
        }
        StartCoroutine(OpenLid(!opened));
    }

    IEnumerator OpenLid(bool open)
    {
        inMotion = true;
        float rotation = 0;
        while (rotation < rotationTop)
        {
            if (open)
            {
                lid.Rotate(new Vector3(-rotationSpeed, 0, 0));
            } else
            {
                lid.Rotate(new Vector3(rotationSpeed, 0, 0));
            }
            rotation += rotationSpeed;
            yield return new WaitForFixedUpdate();
        }
        if (open)
        {
            lid.localEulerAngles = new Vector3(-rotationTop, 0, 0);
        } else
        {
            lid.localEulerAngles = new Vector3(0, 0, 0);
        }
        opened = open;
        inMotion = false;
    }
}
