using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float COOLDOWN_TIME = 1f;
    private float actualCooldown = 0;
    [SerializeField] private GameObject leftFire, rightFire;
    [SerializeField] private float shotSpeed, shotLife = 10f;
    private string poolName;
    // Start is called before the first frame update
    void Start()
    {
        actualCooldown = COOLDOWN_TIME;
        poolName = this.transform.GetComponentInParent<LevelController>().enemyPool;
    }

    // Update is called once per frame
    void Update()
    {
        actualCooldown -= Time.deltaTime;
        if (actualCooldown < 0)
        {
            actualCooldown = COOLDOWN_TIME;
            Fire();
        }
    }
    private void Fire()
    {
        GameObject tempObject = ProjectilePool.instance.GetNextShot(poolName);
        if (tempObject != null)
        {
            ShotBehaviour tempShot = tempObject.GetComponent<ShotBehaviour>();
            tempShot.transform.position = leftFire.transform.position;
            tempShot.InitShot(shotSpeed, 0f, shotLife);
            tempShot.transform.rotation = Quaternion.identity;
        }
        tempObject = ProjectilePool.instance.GetNextShot(poolName);
        if (tempObject != null)
        {
            ShotBehaviour tempShot = tempObject.GetComponent<ShotBehaviour>();
            tempShot.transform.position = rightFire.transform.position;
            tempShot.InitShot(shotSpeed, 0f, shotLife);
            tempShot.transform.rotation = Quaternion.identity;
        }
        return;
    }
    public void TurretDestroyed()
    {
        this.gameObject.SetActive(false);
    }
    
}
