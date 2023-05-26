using UnityEngine;

public class MousePainter : MonoBehaviour
{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    void Update()
    {
        bool click;
        click = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

        if (click)
        {
            Vector3 position;
            position = Input.GetTouch(0).position;

            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                transform.position = hit.point;
                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Paintable.Instance.OnClickDebug();
        }
    }


}
