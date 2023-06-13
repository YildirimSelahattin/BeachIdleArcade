using UnityEngine;

public class MousePainter : MonoBehaviour
{
    public static MousePainter Instance;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;
    public GameObject Hand;
    public Animator handAnim;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    void Update()
    {
        bool click;
        bool ended;
        click = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        ended = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

        if (click)
        {
            handAnim.SetBool("isPour", true);
            Vector3 position;
            position = Input.GetTouch(0).position;
            RaycastHit hit;

            if (Physics.Raycast(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward), out hit, 100.0f))
            {
                Debug.DrawRay(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);


                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }
        }

        if (ended)
        {
            handAnim.SetBool("isPour", false);
            Paintable.Instance.OnClickDebug();
        }
    }


}
