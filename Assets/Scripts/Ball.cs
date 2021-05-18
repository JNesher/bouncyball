using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    public float xBound;
    public float zBound;
    public Camera mainCamera;
    private float _movementForce = 15f;
    private GameManager gameManager;
    private float maxHeight = 20f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void lowerHeight()
    {
        Debug.Log("Lower Ball Max Height!");
        maxHeight /= 1.2f;
    }

    // Update is called once per frame
    void Update()
    {


        /*if (m_Rigidbody.transform.position.x > xBound)
        {
            m_Rigidbody.transform.position = new Vector3(-xBound, m_Rigidbody.transform.position.y, m_Rigidbody.transform.position.z);
        }
        if (m_Rigidbody.transform.position.x < -xBound)
        {
            m_Rigidbody.transform.position = new Vector3(xBound, m_Rigidbody.transform.position.y, m_Rigidbody.transform.position.z);
        }
        if (m_Rigidbody.transform.position.z > zBound)
        {
            m_Rigidbody.transform.position = new Vector3(m_Rigidbody.transform.position.x, m_Rigidbody.transform.position.y, -zBound);
        }
        if (m_Rigidbody.transform.position.z < -zBound)
        {
            m_Rigidbody.transform.position = new Vector3(m_Rigidbody.transform.position.x, m_Rigidbody.transform.position.y, zBound);
        }*/
    }

    private void FixedUpdate()
    {
            //m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, 0);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        m_Rigidbody.AddForce(_movementForce * m_Movement); 
        //m_Rigidbody.transform.Translate(m_Movement * Time.deltaTime * _movementForce, Space.World);
        if (m_Rigidbody.transform.position.y > maxHeight)
        {
            m_Rigidbody.transform.position = new Vector3(m_Rigidbody.transform.position.x, maxHeight, m_Rigidbody.transform.position.z);
        }
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(m_Rigidbody.transform.position);

        if (screenPoint.x > 1)
        {
            screenPoint = new Vector3(1, screenPoint.y, screenPoint.z);
        }
        if (screenPoint.x < 0)
        {
            screenPoint = new Vector3(0, screenPoint.y, screenPoint.z);
        }
        if (screenPoint.y > 1)
        {
            screenPoint = new Vector3(screenPoint.x, 1, screenPoint.z);
        }
        if (screenPoint.y < 0)
        {
            screenPoint = new Vector3(screenPoint.x, 0, screenPoint.z);
        }
        m_Rigidbody.transform.position = mainCamera.ViewportToWorldPoint(screenPoint);

        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        m_Rigidbody.AddForce(_movementForce * m_Movement);*/
        /*if (Input.GetKey(KeyCode.W))
        {
            m_Rigidbody.AddForce(_movementForce * Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Rigidbody.AddForce(_movementForce * Vector3.back);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Rigidbody.AddForce(_movementForce * Vector3.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Rigidbody.AddForce(_movementForce * Vector3.left);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
        {
            gameManager.UpdateScore();
        }
    }
}
