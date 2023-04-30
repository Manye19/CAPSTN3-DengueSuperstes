using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    private bool attacking = true;
    private ObjectPooler objectPooler;
    private float currentAtkSpeed = 0.5f;
    public GameObject atkPrefab;
    public Transform[] atkPositions; 
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Player Attack Speed - TEMPORARY
        currentAtkSpeed = SingletonManager.Get<GameManager>().player.GetComponent<PlayerStat>().atkSpeed;
        
        // Start Pool of Player Attack Projectile
        objectPooler = SingletonManager.Get<ObjectPooler>();
        objectPooler.CreatePool(atkPrefab, 5);
        // Start Attacking Coroutine
        StartCoroutine(AtkCounter());
    }
    
    private IEnumerator AtkCounter()
    {
        while (attacking)
        {
            yield return new WaitForSecondsRealtime(currentAtkSpeed);
            GameObject atkProjectile = objectPooler.GetPooledObject();
            if (atkProjectile != null)
            {
                atkProjectile.transform.position = atkPositions[0].position;
                atkProjectile.SetActive(true);
            }
        }
        yield return null;
    }

    public void UpdateAttackSpeed(int attackSpeed)
    {
        currentAtkSpeed = attackSpeed;
    }
}
