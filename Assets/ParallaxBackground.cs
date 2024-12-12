using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        cam = GameObject.Find("Virtual Camera");
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distancToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distancToMove, transform.position.y);
    }
}
