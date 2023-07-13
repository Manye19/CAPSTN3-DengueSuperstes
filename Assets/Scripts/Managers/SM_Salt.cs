using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SM_Salt : SpawnManager
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
            GameObject go;
            //float speed;
            switch (level)
            {
                case 0:
                    go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                        new Vector3(spawnT.position.x, spawnT.position.y, 0f), spawnT.rotation);
                    //speed = go.GetComponent<Projectile>().projectileSpeed;
                    //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    break;
                case 1:
                    for (int i = 0; i < 2; i++)
                    {
                        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                            new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                            spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                        //speed = go.GetComponent<Projectile>().projectileSpeed;
                        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    }
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                            new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                            spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                        //speed = go.GetComponent<Projectile>().projectileSpeed;
                        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                            new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                            spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                        //speed = go.GetComponent<Projectile>().projectileSpeed;
                        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    }
                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                    {
                        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                            new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                            spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                        //speed = go.GetComponent<Projectile>().projectileSpeed;
                        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    }
                    break;
                case 5:
                    for (int i = 0; i < 4; i++)
                    {
                        go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                            new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                            spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                        //speed = go.GetComponent<Projectile>().projectileSpeed;
                        //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
                    }
                    break;
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject go = objectPooler.SpawnFromPool(poolSO.pool.tag,
                    new Vector3(spawnT.position.x, spawnT.position.y, 0f), 
                    spawnT.rotation * Quaternion.Euler(0,0, Random.Range(-30, 30)));
                //float speed = go.GetComponent<Projectile>().projectileSpeed;
                //go.GetComponent<Rigidbody2D>().velocity = spawnT.up * speed;
            }
        }
    }

    protected override void UpdateWeapon()
    {
        base.UpdateWeapon();
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                level = ws.level;
            }
        }
    }

    protected override void Evolve()
    {
        base.Evolve();
    }
}
