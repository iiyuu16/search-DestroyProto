using UnityEngine;

public class InitScan : MonoBehaviour
{
    public static InitScan instance;
    public ParticleSystem scanner;
    public float duration = 3;
    public float maxSize = 30;
    public float growthRate = 1f;
    public float startingSize = 0.1f;

    private SphereCollider sphereCollider;
    private bool isExpanding = false;

    void Start()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = startingSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scanner.Play();
            StartExpanding();
        }

        if (isExpanding)
        {
            ExpandCollider();
        }
    }

    void StartExpanding()
    {
        isExpanding = true;
    }

    void ExpandCollider()
    {
        if (sphereCollider.radius < maxSize)
        {
            sphereCollider.radius += growthRate * Time.deltaTime;
        }
        else
        {
            sphereCollider.radius = startingSize;
            isExpanding = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Debug.Log("Target detected in range");
            // Add your code to change the mesh here
        }
    }
}
