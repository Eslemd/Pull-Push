using System.IO;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public Rigidbody2D coreRigidbody;

    public float gravityStrength = 10f;

    private void Start()
    {
        if (CoreManager.Instance != null)
        {
            coreRigidbody = CoreManager.Instance.GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        if (coreRigidbody != null)
        {
            Attract();
        }
    }

    void Attract()
    {
        Vector2 direction = (Vector2)transform.position - coreRigidbody.position;

        float distance = direction.magnitude;

        if (distance < 0.1f) return;

        float forceMagnitude = gravityStrength / distance;

        Vector2 force = direction.normalized * forceMagnitude;

        coreRigidbody.AddForce(force, ForceMode2D.Force);
    }
}
