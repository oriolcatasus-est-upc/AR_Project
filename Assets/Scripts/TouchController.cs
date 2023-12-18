using UnityEngine;

public class TouchController : MonoBehaviour
{

    private Camera arCamera;

    private void Start()
    {
        arCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                var ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var openTrashBin = hit.transform.GetComponent<OpenTrashBin>();
                    if (openTrashBin != null)
                    {
                        openTrashBin.Select();
                    }
                }
            }
        }
    }
}
