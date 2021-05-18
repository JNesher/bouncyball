using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject platform;
    private float setMoveDownSpeed = 2.5f;
    public float timeLeft = 5f;
    private float resetTime = 5f;
    private Vector3 setScale = new Vector3(10, 0.3f, 10);
    private float edge1 = -2.5f;
    private float edge2 = 2.5f;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SpeedUpPlatform()
    {
        Debug.Log("Speed Up!");
        setMoveDownSpeed *= 1.25f;
        resetTime /= 1.25f;
        timeLeft /= 1.25f;
        MoveDown[] moveList = FindObjectsOfType<MoveDown>();
        foreach (MoveDown md in moveList)
        {
            md.moveDownSpeed = setMoveDownSpeed;
        }
    }

    public void makePlatformSmaller()
    {
        Debug.Log("Make Platform Smaller!");
        setScale /= 1.1f;
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            platform.transform.localScale = setScale;
        }
    }

    public void spacePlatformsOut()
    {
        Debug.Log("Space Platforms Out!");
        edge1 *= 1.7f;
        edge2 *= 1.7f;
    }

    
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && gameManager.gameRunning)
        {
            timeLeft = resetTime;
            SpawnPlatform();
        }
    }
    void SpawnPlatform()
    {
        GameObject newPlat = Instantiate(platform, new Vector3(Random.Range(edge1, edge2), Random.Range(-5f, 0f), 20), new Quaternion(180, 0, 0, 0));
        newPlat.GetComponent<MoveDown>().moveDownSpeed = setMoveDownSpeed;
        newPlat.tag = "Platform";
        newPlat.transform.localScale = setScale;
    }

    public void ResetDefaults()
    {
        setMoveDownSpeed = 2.5f;
        timeLeft = 5f;
        resetTime = 5f;
        setScale = new Vector3(10, 0.3f, 10);
        edge1 = -2.5f;
        edge2 = 2.5f;
    }
}
