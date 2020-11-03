using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : Singleton<BossManager>
{
    [SerializeField] private float invincibleTime = 3;

    [Header("Phase 1")]
    [SerializeField] private GameObject shield;
    [SerializeField] private int startShieldHealth;
    [SerializeField] private int shieldCoolDown;
    [SerializeField] private GameObject switchRoot;
    private int shieldHealth = 0;
    private ShieldSwitch[] switches;

    [Header("Phase 2")]
    [SerializeField] private GameObject enemySwarmerPrefab;
    [SerializeField] private int enemyAmountToSpawn = 1;
    [SerializeField] private int enemyLimit = 50;
    [Range(4f, 20f)]
    [SerializeField] private float spawnTimeRate = 1;
    private int currentEnemyAmount = 0;
    private List<GameObject> enemyPool;

    [Header("Phase 3")]
    [SerializeField] private GameObject tentaclePrefab;
    [SerializeField] private int tentaclesAmount = 1;
    private List<GameObject> tentacles;

    private BossPhases currentPhase = BossPhases.ShieldPhase;
    private Animator animator;
    private Transform target;

    enum BossPhases
    {
        ShieldPhase = 0,
        SwarmPhase = 1,
        TentaclePhase = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        switches = switchRoot.GetComponentsInChildren<ShieldSwitch>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyPool = new List<GameObject>();
        tentacles = new List<GameObject>();

        TransitionToNextPhase();
    }

    public void TransitionToNextPhase()
    {
        switch (currentPhase)
        {
            case BossPhases.ShieldPhase:
                StartShieldPhase();
                currentPhase++;
                break;

            case BossPhases.SwarmPhase:
                Destroy(switchRoot);
                shield.SetActive(false);
                StartSwarmPhase();
                currentPhase++;
                break;

            case BossPhases.TentaclePhase:
                DestroyAllEnemies();
                StopCoroutine(ContinousSpawning());
                StartTentaclePhase();
                break;

            default:
                //
                break;
        }
    }

    //starts the shield and activates random switches to deactivate the shield
    void StartShieldPhase()
    {
        switchRoot.SetActive(true);
        ResetSwitches();
        shield.SetActive(true);
        ActivateSwitches();
        shieldHealth = startShieldHealth;
    }

    //Turns the shield off for a certain amount of time and then activates it again
    IEnumerator TurnOffShield()
    {
        shield.SetActive(false);
        yield return new WaitForSeconds(shieldCoolDown);
        StartShieldPhase();
    }

    //function activating random switches
    void ActivateSwitches()
    {
        System.Random rnd = new System.Random();
        int objLeftToActivate = startShieldHealth;
      
        while (objLeftToActivate > 0)
        {
            if(switches[rnd.Next(0, switches.Length)].Activate())
            {
                objLeftToActivate--;
            }                          
        }
    }

    //Resets the switches to default state
    void ResetSwitches()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].ResetSwitch();
        }
    }

    //Reduce the health for the shield until it is less than 0 so it is removed
    public void reduceShield()
    {
        shieldHealth--;
        if (shieldHealth <= 0)
        {
            StartCoroutine(TurnOffShield());
        }
    }

    //Starts spawnphase by spawning first enemies and start continousspawncoRoutine
    void StartSwarmPhase()
    {
        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            enemyPool.Add(Instantiate(enemySwarmerPrefab, transform));
        }
        StartCoroutine(ContinousSpawning());
    }

    //CoRoutine spawning a new enemy every few second
    IEnumerator ContinousSpawning()
    {
        yield return new WaitForSeconds(spawnTimeRate);
        if (currentEnemyAmount < enemyLimit)
        {
            enemyPool.Add(Instantiate(enemySwarmerPrefab, transform));
            currentEnemyAmount++;
        }
    }

    //Remove enemies left when transitioning to the next phase
    void DestroyAllEnemies()
    {       
        foreach (GameObject enemy in enemyPool)
        {
            Destroy(enemy);
        }
    }

    //Starts the tentacle phase, instantiate all the tentacles and gives them a random direction to rotate in
    void StartTentaclePhase()
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < tentaclesAmount; i++)
        {
            GameObject instanceObj = Instantiate(tentaclePrefab, transform.position, Quaternion.identity);
            Vector3 tentacleRotation = new Vector3(rnd.Next(0, 30), rnd.Next(0, 30), 0f);
            instanceObj.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().rotateDegreesPerSecond.value = tentacleRotation;
            tentacles.Add(instanceObj);
        }
    }

    //Used by bossstats to call on the coroutine becomeinvicible
    public void PrepareNextPhase()
    {
        StartCoroutine(BecomeInvincible());
    }

    //Disable Bossstats so boss cant be damaged a certain amount of time then transition to the next phase
    public IEnumerator BecomeInvincible()
    {
        GetComponent<BossStats>().enabled = false;
        shield.SetActive(true);
        yield return new WaitForSeconds(3f);
        GetComponent<BossStats>().enabled = true;
        shield.SetActive(false);
        TransitionToNextPhase();
    }



}
