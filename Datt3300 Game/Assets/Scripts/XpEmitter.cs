using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class XpEmitter : MonoBehaviour
{
    [SerializeField] private GameObject xp;
    private float timer = 0.01f;
    private int ammo = 50;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else if (ammo > 0)
        {
            Instantiate(xp, transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0), Quaternion.identity);
            Instantiate(xp, transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0), Quaternion.identity);
            ammo -= 2;
            timer = 0.01f;
        }
        else
            Destroy(gameObject);
    }
}
