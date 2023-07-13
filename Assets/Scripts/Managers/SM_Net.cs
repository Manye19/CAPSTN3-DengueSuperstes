using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SM_Net : SpawnManager
{
    protected override void Start()
    {
        spawnT = spawnTransforms[5];
        base.Start();
    }

    private void LateUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                spawnT = spawnTransforms[5];
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                spawnT = spawnTransforms[5];
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                spawnT = spawnTransforms[2];
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                spawnT = spawnTransforms[3];
            }
            
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = spawnTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0)
            {
                //Debug.Log("diagonal working");
                spawnT = spawnTransforms[1];
            }
            else if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = spawnTransforms[7];
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0)
            {
                //Debug.Log("diagonal working");
                spawnT = spawnTransforms[7];
            }
        }
    }

    protected override void Spawn()
    {
        if (!isEvolved)
        {
            GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
            //float speed = go.GetComponent<Projectile>().projectileSpeed;
            //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
        }
        else
        {
            StartCoroutine(EvolvedCoroutine());
        }
    }

    protected override void Evolve()
    {
        isEvolved = true;
    }

    private IEnumerator EvolvedCoroutine()
    {
        GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag,
            new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
        //float speed = go.GetComponent<Projectile>().projectileSpeed;
        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;

        yield return new WaitForSeconds(0.5f);
        
        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
            new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
        //speed = go.GetComponent<Projectile>().projectileSpeed;
        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
    }
}
