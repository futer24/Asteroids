using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProyectile : Weapon
{
    public override void Shoot(Vector3 direction)
    {
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        rBody.AddForce(direction * speed, ForceMode.Impulse);

    }
 



}
