using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : Singleton<BossManager>
{
    [SerializeField] private float invincibleTime = 3;
    [SerializeField] private GameObject trackingLaser;
    [SerializeField] private AudioClip transitionAudioClip;


    [Header("Phase 1")]
    [SerializeField] private GameObject shield;
    [SerializeField] private int startShieldHealth;
    [SerializeField] private int shieldCoolDown;
    [SerializeField] private GameObject switchRoot;
    [SerializeField] private AudioClip powerDownClip;
    [SerializeField] private AudioClip powerUpClip;
    private int shieldHealth = 0;
    private ShieldSwitch[] switches;
    private BossStats bossStats;

    [Header("Phase 2")]
    [SerializeField] private GameObject enemySwarmerPrefab;
    [SerializeField] private int enemyAmountToSpawn = 1;
    [SerializeField] private int enemyLimit = 50;
    [Range(4f, 20f)]
    [SerializeField] private float spawnTimeRate = 1;
    [SerializeField] private Transform spawnPoint;

    private int currentEnemyAmount = 0;
    private List<GameObject> enemyPool;

    [Header("Phase 3")]
    [SerializeField] private GameObject tentaclePrefab;
    [SerializeField] private GameObject frontShieldPrefab;
    [SerializeField] private int tentaclesAmount = 1;
    private List<GameObject> tentacles;
    [SerializeField] private Transform tentacleSpawnPoint;
    [SerializeField] private float distanceFromBoss;


    private BossPhases currentPhase = BossPhases.ShieldPhase;   //----------------------------
    private Animator animator;
    private Transform target;
    private float blendPower = 0;
    private AudioSource source;

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
        animator = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyPool = new List<GameObject>();
        tentacles = new List<GameObject>();
        bossStats = GetComponent<BossStats>();
        source = GetComponent<AudioSource>();
        TransitionToNextPhase();
    }

    //void Update()
    //{
    //    blendPower -= Time.deltaTime * 0.08f;
    //    blendPower = Mathf.Clamp01(blendPower);
    //    animator.SetFloat("Blend", 1);
    //}

    public void TransitionToNextPhase()
    {
        switch (currentPhase)
        {
            case BossPhases.ShieldPhase:
                StartShieldPhase();
                StartCoroutine(ContinousSpawning());
                currentPhase++;
                break;

            case BossPhases.SwarmPhase:
                Destroy(switchRoot);
                shield.SetActive(false);
                StartSwarmPhase();
                currentPhase++;
                break;

            case BossPhases.TentaclePhase:
                StopCoroutine(ContinousSpawning());
                DestroyAllEnemies();
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
        bossStats.isInvicible = true;
    }

    //Turns the shield off for a certain amount of time and then activates it again
    IEnumerator TurnOffShield()
    {
        source.PlayOneShot(powerDownClip);
        shield.SetActive(false);
        bossStats.isInvicible = false;
        yield return new WaitForSeconds(shieldCoolDown);
        StartShieldPhase();
        source.PlayOneShot(powerUpClip);
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
        trackingLaser.SetActive(false);
        StartCoroutine(FastSpawnEnemies());
        source.Play();
    }

    IEnumerator FastSpawnEnemies()
    {
        float blendPower = 0f;
        for (int i = 0; i < 5; i++)
        {
            blendPower += 0.2f;
            animator.SetFloat("Blend", blendPower);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < enemyAmountToSpawn; i++)
        {
            //blendPower += 0.2f;
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);

        }
    }

    //CoRoutine spawning a new enemy every few second
    IEnumerator ContinousSpawning()
    {
        yield return new WaitForSeconds(spawnTimeRate);
        if (currentEnemyAmount < enemyLimit)
        {
            SpawnEnemy();
            currentEnemyAmount++;
        }
    }

    void SpawnEnemy()
    {
        Vector3 posToSpawn = spawnPoint.position + Random.onUnitSphere * 20f;
        enemyPool.Add(Instantiate(enemySwarmerPrefab, posToSpawn, Quaternion.identity, spawnPoint));
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
        source.Stop();
        trackingLaser.SetActive(true);
        frontShieldPrefab.SetActive(true);
        StartCoroutine(StartWavingPartsAnimation());
        //System.Random rnd = new System.Random();
        for (int i = 0; i < tentaclesAmount; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            Vector3 tentaclePosition = tentacleSpawnPoint.position + distanceFromBoss * direction;
            //GameObject instanceObj = Instantiate(tentaclePrefab, tentacleSpawnPoint.position, Quaternion.identity, tentacleSpawnPoint);
            GameObject instanceObj = Instantiate(tentaclePrefab, tentaclePosition, Quaternion.identity, tentacleSpawnPoint);
            //Vector3 tentacleRotation = new Vector3(rnd.Next(0, 10), rnd.Next(0, 10), 0f);
            instanceObj.transform.forward = direction;
            Vector3 tentacleRotation = new Vector3(Random.Range(0,10), Random.Range(0, 10), 0f);
            instanceObj.GetComponent<UnityStandardAssets.Utility.AutoMoveAndRotate>().rotateDegreesPerSecond.value = tentacleRotation;
            tentacles.Add(instanceObj);
        }
    }

    IEnumerator StartWavingPartsAnimation()
    {
        while (true)
        {
            animator.SetFloat("Blend", Mathf.Sin(Time.time/2) + 1.2f);
            yield return new WaitForSeconds(0.01f);

        }
    }

    //Used by bossstats to call on the coroutine becomeinvicible
    public void PrepareNextPhase()
    {
        StartCoroutine(BecomeInvincible());
        source.PlayOneShot(transitionAudioClip);
    }

    //Disable Bossstats so boss cant be damaged a certain amount of time then transition to the next phase
    public IEnumerator BecomeInvincible()
    {
        bossStats.isInvicible = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(3f);
        bossStats.isInvicible = false;
        shield.SetActive(false);
        TransitionToNextPhase();
    }



}
