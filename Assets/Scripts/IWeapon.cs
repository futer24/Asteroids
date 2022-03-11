using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon  
{
    bool Shoot(Vector3 position, Vector3 direction);
}
