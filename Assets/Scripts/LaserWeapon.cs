using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : IWeapon
{
    public float fireRate = 0.2f;
    private float nextFire = 0f;

    public bool Shoot(Vector3 position, Vector3 direction)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject projectileInstace = PoolingController.instance.GetPoolingObject(ObjectPoolType.PROYECTILE);  
            if (projectileInstace != null)
            {
                projectileInstace.transform.position = position;//player.transform.TransformPoint(Vector3.down * 5);
                projectileInstace.SetActive(true);
                projectileInstace.GetComponent<LaserProyectile>().Shoot(direction ); // - player.transform.up
                return true;
            }
        }
        return false;
    }
}
