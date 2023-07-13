using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class SM_Swatter : SpawnManager
{
    protected override void Spawn()
    {
        StartCoroutine(SpawnOnLevelCoroutine(level));
    }
    protected override void UpdateWeapon()
    {
        base.UpdateWeapon();
        foreach (var weaponStat in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (weaponStat.weaponLevelSO.name.Equals(poolSO.name))
            {
                level = weaponStat.level;
            }
        }
    }

    private IEnumerator SpawnOnLevelCoroutine(int level)
    {
        switch (level)
        {
            case 0:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                break;
            case 1:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                break;
            case 2:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.75f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                break;
            case 3:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.75f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                break;
            case 4:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.75f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.25f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                break;
            case 5:
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.75f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[1].position, Quaternion.identity);
                yield return new WaitForSeconds(0.25f);
                objectPooler.SpawnFromPool(poolSO.pool.tag, spawnTransforms[0].position, Quaternion.identity);
                break;
        }
    }
}
