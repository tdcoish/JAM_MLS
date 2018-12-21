using UnityEngine;

// This class literally just exists to spawn the stoning animation.
[RequireComponent(typeof(Health))]
public class StonedEnemy : MonoBehaviour 
{

    [SerializeField]
	private GameObject          _BreakupObj;

    [SerializeField]
    private GameObject          _HitParticles;

    [Tooltip("You spawn these upon creation")]
    [SerializeField]
    private GameObject          _CreationParticles;

    [Tooltip("These spawn when you get destroyed")]
    [SerializeField]
    private GameObject          _DestructionParticles;

    // NOTE: YOU MUST DISABLE AUTODESTROY ON THE HEALTH OBJECT
    private Health              _health;

    private void Awake(){
        _health = GetComponent<Health>();
        Instantiate(_CreationParticles, transform.position, transform.rotation);  

    }

    void Update()
    {
        if(_health.GetHealth() <= 0){
            Instantiate(_DestructionParticles, transform.position, transform.rotation);
            Instantiate(_BreakupObj, transform.position, transform.rotation);
            AkSoundEngine.PostEvent("Play_Crumbles", gameObject);
            Destroy(gameObject);
        }
    }

    // Called by Health, upon hit.
    public void SpawnHitParticles(){
        Instantiate(_HitParticles, transform.position, transform.rotation);
    }

}