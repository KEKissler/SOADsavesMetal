using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescroll : MonoBehaviour
{
    public GameObject sky;
    public GameObject city;
    public GameObject ground;
    public Player player;
    public GameObject[] platforms;
    public bool movePlayer;

    public float skySpeed;
    public float citySpeed;
    public float groundSpeed;

    private GameObject skyDuplicate;
    private GameObject cityDuplicate;
    private GameObject groundDuplicate;

    private float skyWidth;
    private float cityWidth;
    private float groundWidth;
    private float platformWidth;
    private float screenBound;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        skyDuplicate = Instantiate(sky, sky.transform.parent);
        Transform temp = skyDuplicate.transform.GetChild(0);
        skyWidth = temp.GetComponent<SpriteRenderer>().size.x * temp.localScale.x - 0.0001f;
        skyDuplicate.transform.Translate(new Vector3(-skyWidth, 0, 0));

        cityDuplicate = Instantiate(city, city.transform.parent);
        temp = cityDuplicate.transform;
        cityWidth = temp.GetComponent<SpriteRenderer>().size.x * temp.localScale.x - 0.0001f;
        cityDuplicate.transform.Translate(new Vector3(-cityWidth, 0, 0));

        groundDuplicate = Instantiate(ground, ground.transform.parent);
        temp = groundDuplicate.transform;
        groundWidth = temp.GetComponent<SpriteRenderer>().size.x * temp.localScale.x - 0.0001f;
        groundDuplicate.transform.Translate(new Vector3(-groundWidth, 0, 0));

        platformWidth = platforms[0].GetComponent<SpriteRenderer>().bounds.extents.x * 2;

        screenBound = Camera.main.orthographicSize * 16 / 9;

        player.desiredVelocity = groundSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sky.transform.Translate(new Vector3(skySpeed * Time.deltaTime, 0, 0));
        if(sky.transform.position.x - sky.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            sky.transform.Translate(new Vector3(-skyWidth * 2, 0, 0));
        if(sky.transform.position.x + sky.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            sky.transform.Translate(new Vector3(skyWidth * 2, 0, 0));

        skyDuplicate.transform.Translate(new Vector3(skySpeed * Time.deltaTime, 0, 0));
        if (skyDuplicate.transform.position.x - skyDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            skyDuplicate.transform.Translate(new Vector3(-skyWidth * 2, 0, 0));
        if (skyDuplicate.transform.position.x + skyDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            skyDuplicate.transform.Translate(new Vector3(skyWidth * 2, 0, 0));

        city.transform.Translate(new Vector3(citySpeed * Time.deltaTime, 0, 0));
        if (city.transform.position.x - city.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            city.transform.Translate(new Vector3(-cityWidth * 2, 0, 0));
        if (city.transform.position.x + city.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            city.transform.Translate(new Vector3(cityWidth * 2, 0, 0));

        cityDuplicate.transform.Translate(new Vector3(citySpeed * Time.deltaTime, 0, 0));
        if (cityDuplicate.transform.position.x - cityDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            cityDuplicate.transform.Translate(new Vector3(-cityWidth * 2, 0, 0));
        if (cityDuplicate.transform.position.x + cityDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            cityDuplicate.transform.Translate(new Vector3(cityWidth * 2, 0, 0));

        ground.transform.Translate(new Vector3(groundSpeed * Time.deltaTime, 0, 0));
        if (ground.transform.position.x - ground.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            ground.transform.Translate(new Vector3(-groundWidth * 2, 0, 0));
        if (ground.transform.position.x + ground.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            ground.transform.Translate(new Vector3(groundWidth * 2, 0, 0));

        groundDuplicate.transform.Translate(new Vector3(groundSpeed * Time.deltaTime, 0, 0));
        if (groundDuplicate.transform.position.x - groundDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
            groundDuplicate.transform.Translate(new Vector3(-groundWidth * 2, 0, 0));
        if (groundDuplicate.transform.position.x + groundDuplicate.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
            groundDuplicate.transform.Translate(new Vector3(groundWidth * 2, 0, 0));

        foreach (GameObject p in platforms)
        {
            p.transform.Translate(new Vector3(groundSpeed * Time.deltaTime, 0, 0));
            if (p.transform.position.x - p.GetComponentInChildren<SpriteRenderer>().bounds.extents.x > screenBound)
                p.transform.Translate(new Vector3(-screenBound * 2 - platformWidth, 0, 0));
            if (p.transform.position.x + p.GetComponentInChildren<SpriteRenderer>().bounds.extents.x < -screenBound)
                p.transform.Translate(new Vector3(screenBound * 2 + platformWidth, 0, 0));
        }
        
        lastPosition = player.transform.position;
        if(movePlayer)
        {
            player.desiredVelocity = groundSpeed;
        }
        else
        {
            player.desiredVelocity = 0f;
        }
    }
}
