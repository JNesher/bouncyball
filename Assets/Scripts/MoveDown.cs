using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float moveDownSpeed;
    public float lowerbound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveDownSpeed);
        if (transform.position.z < lowerbound)
        {
            Destroy(gameObject);
        }
    }
}
