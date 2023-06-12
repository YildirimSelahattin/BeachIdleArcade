using UnityEngine;

public class MeshDeformerInput : MonoBehaviour
{

    public float force = 10f;
    public float forceOffset = 0.1f;
    public GameObject Hand;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        RaycastHit hit;

        if (Physics.Raycast(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward), out hit, 100.0f))
        {
            Debug.DrawRay(Hand.transform.position, Hand.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);


            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }
    }
}