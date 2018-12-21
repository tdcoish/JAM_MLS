using UnityEngine;

public class MachineGun : Weapon 
{
    [SerializeField]
    private GameObject bullet;
    
	protected override void WeaponAttack(Quaternion direction)
    {
        Instantiate(bullet, origin.position, direction);
    }
}
