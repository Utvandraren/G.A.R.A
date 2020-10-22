using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private Transform target;

    BossPhases currentPhase = BossPhases.ShieldPhase;
    Animator animator;

    public enum BossPhases
    {
        ShieldPhase = 0,
        SwarmPhase = 1,
        TentaclePhase = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToNextPhase();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
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

    }

    void StartSwarmPhase()
    {

    }

    void StartTentaclePhase()
    {

    }
}
