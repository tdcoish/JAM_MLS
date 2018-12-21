using UnityEngine;

public interface IAttack
{
	// You can't fire to a specific spot, only with direction
	float Attack(Quaternion direction);
}
