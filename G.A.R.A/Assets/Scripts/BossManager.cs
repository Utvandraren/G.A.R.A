using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : Singleton<BossManager>
{
    private Transform target;

    [Header("Phase 1")]
    [SerializeField] private GameObject shield;
    [SerializeField] private int startShieldHealth;
    [SerializeField] private int shieldCoolDown;
    [SerializeField] private GameObject switchRoot;
    private int shieldHealth = 0;
    private ShieldSwitch[] switches;

    [Header("Phase 2")]

    [Header("Phase 3")]


    BossPhases currentPhase = BossPhases.ShieldPhase;
    Animator animator;

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
                StartSwarmPhase();
                currentPhase++;
                break;

            case BossPhases.TentaclePhase:
                StartTentaclePhase();
                break;

            default:
                //
                break;
        }
    }

    void StartShieldPhase()
    {
        ResetSwitches();
        shield.SetActive(true);
        ActivateSwitches();
        shieldHealth = startShieldHealth;
    }

    void StartSwarmPhase()
    {

    }

    void StartTentaclePhase()
    {

    }

    public void reduceShield()
    {
        shieldHealth--;
        if(shieldHealth <= 0)
        {
            StartCoroutine(TurnOffShield());
        }
    }

    IEnumerator TurnOffShield()
    {
        shield.SetActive(false);
        yield return new WaitForSeconds(shieldCoolDown);
        StartShieldPhase();
    }

    void ActivateSwitches()
    {
        
        System.Random rnd = new System.Random();
        int objLeftToActivate = startShieldHealth;
        List<ShieldSwitch> activatedObjs = new List<ShieldSwitch>();

        while (objLeftToActivate > 0)
        {
            for (int i = 0; i < switches.Length; i++)
            {
                if (objLeftToActivate < 0)
                {
                    break;
                }
                else if (IsAlreadyActivated(switches[i], activatedObjs))
                {
                    continue;
                }
                else if (rnd.Next(0, 10) > 5)
                {                  
                    switches[i].Activate();
                    activatedObjs.Add(switches[i]);
                    objLeftToActivate--;
                }
                
            }
        }
    }
    void ResetSwitches()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].ResetSwitch();
        }
    }

    bool IsAlreadyActivated(ShieldSwitch currentObj, List<ShieldSwitch> activatedObjs)
    {
        foreach (ShieldSwitch activatedObj in activatedObjs)
        {
            if (currentObj == activatedObj)
            {
                return true;
            }
        }
        return false;
    }
}
