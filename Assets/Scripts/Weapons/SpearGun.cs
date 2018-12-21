using UnityEngine;

public class SpearGun : Weapon {

	[SerializeField]
    private GameObject bullet;

    protected override void WeaponAttack(Quaternion direction)
    { 
        Instantiate(bullet, origin.position, direction);
    }

}
