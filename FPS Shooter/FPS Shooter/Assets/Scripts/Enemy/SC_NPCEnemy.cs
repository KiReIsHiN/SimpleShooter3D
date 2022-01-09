using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class SC_NPCEnemy : MonoBehaviour, IEntity
{
    public float attackDistance = 3f;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    //How much damage will npc deal to the player
    public float npcDamage = 20;
    public float attackRate;
    public Transform firePoint;

    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    NavMeshAgent agent;
    float nextAttackTime = 0;
    Rigidbody r;

    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip audioShot;
    public GameObject bulletPrefab;

    SC_Weapon npcWeapon;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        r.useGravity = false;
        r.isKinematic = true;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        npcWeapon = GetComponentInChildren<SC_Weapon>();
        npcDamage = npcWeapon.weaponDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance - attackDistance < 0.01f)
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate;

                //Attack
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.red);

                        audioSource.PlayOneShot(audioShot);
                        muzzleFlash.Play();

                        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
                        //Set bullet damage according to weapon damage value
                        bullet.SetDamage(npcDamage);
                    }
                }
            }
        }
        //Move towards the player
        agent.destination = playerTransform.position;
        //Always look at player
        transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
        //Gradually reduce rigidbody velocity if the force was applied by the bullet
        r.velocity *= 0.99f;
    }

    public void ApplyDamage(float points)
    {
        npcHP -= points;
        if (npcHP <= 0)
        {
            //Slightly bounce the npc dead prefab up
            gameObject.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            Destroy(gameObject, 2.0f);
        }
    }
}