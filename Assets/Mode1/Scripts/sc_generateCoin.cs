using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_generateCoin : MonoBehaviour
{
    public GameObject coinPrefab, piggy;
    private GameObject coin;
    public float shootRate = 1f;
    private float nextShoot;
    public float shootSpeed = 1000;

    void Update()
    {
        if (Time.time > nextShoot)
        {
            coin = Instantiate(coinPrefab, piggy.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
            coin.GetComponent<Rigidbody2D>().AddForce(piggy.GetComponent<Transform>().TransformDirection(new Vector2(Random.Range(-100, 100),Random.Range(-100, 100))) * shootSpeed);
            nextShoot = Time.time + shootRate;
        }
    }
}
