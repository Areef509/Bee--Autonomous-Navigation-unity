using UnityEngine;

public class BeeAI : MonoBehaviour
{
    public Transform[] targets;
    public float speed = 3f;
    public float obstacleDistance = 2f;
    public float turnSpeed = 120f;

    private int currentTarget = 0;

    void Update()
    {
        if (targets.Length == 0) return;

        Transform target = targets[currentTarget);

        // ---- OBSTACLE DETECTION ----
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                // Turn left if obstacle detected
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
                return;
            }
        }

        // ---- MOVE TOWARDS TARGET ----
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate towards target
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * 2f
        );

        // Switch target
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentTarget = (currentTarget + 1) % targets.Length;
        }
    }

    // Visualize ray
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * obstacleDistance);
    }
}
