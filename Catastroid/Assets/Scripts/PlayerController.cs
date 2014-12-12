﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Range(1f, 10f)]
    public float moveSpeed = 1f;

    [Range(0.01f, 1f)]
    public float forwardSpeed = 0.01f;

    Vector3 nextPosition;

    List<GameObject> bullets;
    int currentBullet;

    float nextFireTime;
    float fireDelay = 0.1f;

    float maxWidth = GameController.MAX_WIDTH;
    float maxHeight = GameController.MAX_HEIGHT;

    void Start()
    {
        bullets = new List<GameObject>();
    }

    void Update()
    {
        nextPosition.x += Input.GetAxis("Horizontal") * moveSpeed / 10f;
        if (nextPosition.x >= maxWidth) nextPosition.x = maxWidth;
        if (nextPosition.x <= -maxWidth) nextPosition.x = -maxWidth;
        nextPosition.y += Input.GetAxis("Vertical") * moveSpeed / 10f;
        if (nextPosition.y >= maxHeight) nextPosition.y = maxHeight;
        if (nextPosition.y <= -maxHeight) nextPosition.y = -maxHeight;
        nextPosition.z += 0.01f;
        transform.position = nextPosition;
        if (Input.GetButtonDown("Fire") && Time.time > nextFireTime) Fire();
    }

    public void Fire()
    {
        if (bullets.Count < 1) MakeBullets();
        bullets[currentBullet].transform.position = transform.position;
        bullets[currentBullet].SetActive(true);
        //Give the bullet motion
        currentBullet++; if (currentBullet >= bullets.Count) currentBullet = 0;
        nextFireTime += fireDelay;
    }

    void MakeBullets()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            bullets.Add(Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject))) as GameObject);
            bullets[i].gameObject.SetActive(false);
        }
    }
}
