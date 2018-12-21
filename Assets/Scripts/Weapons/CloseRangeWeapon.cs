using UnityEngine;

public class CloseRangeWeapon : Weapon
{
    [SerializeField]
    private float damage = 5f;

	[SerializeField]
    private GameObject hitBox;

    protected override void WeaponAttack(Quaternion direction)
    {
        GameObject newGO = Instantiate(hitBox, origin.position, direction);
        newGO.GetComponent<HitBox>().damage = damage;
    }

    public float GetDamage()
    {
        return damage;
    }
}
