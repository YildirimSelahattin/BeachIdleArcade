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
    float a = 1;
    float b = 1;
    float c = 1;
    public Color tempColorA;
    public Color tempColorB;
    public Color tempColorC;
    public int i = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    /*
    void OnEnable()
    {
        tempColorA.a = 1;
        tempColorB.a = 1;
        tempColorC.a = 1;
        PlayerManager.Instance.creamSplash[0].GetComponent<MeshRenderer>().material.color = tempColorA;
        PlayerManager.Instance.creamSplash[1].GetComponent<MeshRenderer>().material.color = tempColorB;
        PlayerManager.Instance.creamSplash[2].GetComponent<MeshRenderer>().material.color = tempColorC;
    }
    */

    void Update()
    {
        bool began;
        bool click;
        bool ended;
        click = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        ended = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

        if (click)
        {
            i++;
            handAnim.SetBool("isPour", true);
            Vector3 position;
            position = Input.GetTouch(0).position;
            RaycastHit hit;

            if (Physics.Raycast(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward), out hit, 100.0f))
            {
                                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }

            a -= 0.007f;
            tempColorA.a = a;
            b -= 0.005f;
            tempColorB.a = b;
            c -= 0.004f;
            tempColorC.a = c;

            //PlayerManager.Instance.creamSplash[0].GetComponent<MeshRenderer>().material.color = tempColorA;
            //PlayerManager.Instance.creamSplash[1].GetComponent<MeshRenderer>().material.color = tempColorB;
            //PlayerManager.Instance.creamSplash[2].GetComponent<MeshRenderer>().material.color = tempColorC;
        }

        if (ended)
        {
            handAnim.SetBool("isPour", false);
            //Paintable.Instance.OnClickDebug();
        }
    }


}
