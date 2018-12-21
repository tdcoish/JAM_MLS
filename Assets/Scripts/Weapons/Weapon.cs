using UnityEngine;

public abstract class Weapon : MonoBehaviour, IAttack
{
    [SerializeField]
    protected Transform origin;
    
    [SerializeField]
    private float fireRate;
    
    private float deltaFire;

    [SerializeField, Range(0,1f)]
	private float cookOffStep = 0.1f;
    
    private void Update()
    {
        deltaFire += Time.deltaTime;
    }

	public float Attack(Quaternion direction)
    {
        if(deltaFire > fireRate)
        {
            WeaponAttack(direction);
            deltaFire = 0f;
            return cookOffStep;
        }
        return 0;
    }
	
	protected abstract void WeaponAttack(Quaternion direction);
}
