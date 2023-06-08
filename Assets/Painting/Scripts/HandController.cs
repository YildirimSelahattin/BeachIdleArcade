using UnityEngine;

public class HandController : MonoBehaviour
{
    public Transform joystick; // Joystick objesi
    public Transform top; // Hareket ettirmek istediğiniz top objesi
    public float speed = 5f; // Hareket hızı

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = top.position;
    }

    private void Update()
    {
        Vector3 direction = joystick.position - startPosition;
        direction.z = direction.y;
        direction.y = 0f;
        direction.Normalize();

        top.position += direction * speed * Time.deltaTime;
    }
}