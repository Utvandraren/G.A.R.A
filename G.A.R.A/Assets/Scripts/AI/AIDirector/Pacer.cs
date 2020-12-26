using UnityEngine;
using System.IO;
public class Pacer : MonoBehaviour
{
    public enum TempoType
    {
        BUILDUP,
        SUSTAIN,
        FADE,
        RELAX,
    }

    public int panicIncreaseModifier = 2;
    public float sustainTime = 5f;
    [HideInInspector] public TempoType currentTempo;
    private PlayerReader playerReader;
    private float panicScore;
    private float maxPanicScore = 10;
    [SerializeField] private float panicReductionRate = 0.1f;
    private bool active;
    private float upperThreshold = 10f;
    private float lowerThreshold = 0f;
    private float tempoTimer;

    private bool reducePanic = true;
    private float reducePanicTimer;
    [SerializeField] private float reducePanicInterval = 2f;

    private float activeTimer;
    private float printTimer;
    private StreamWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        currentTempo = TempoType.BUILDUP;
        writer = new StreamWriter(@"panic.txt", true);
        writer.AutoFlush = true;
        writer.WriteLine(string.Format("Reduction rate: {0} point/sec; Panic inctease Multiplier: {1}", panicReductionRate, panicIncreaseModifier));
        writer.WriteLine("---------------------------------------------------------------------------------------");
    }

    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }

    private void OnDestroy()
    {
        writer.WriteLine("---------------------------------------------------------------------------------------");
        writer.Flush();
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        activeTimer += Time.deltaTime;
        printTimer += Time.deltaTime;
        ChangeTempo();
        if (reducePanic)
        {
            panicScore = Mathf.Max(Mathf.Min(panicScore - panicReductionRate * Time.deltaTime, maxPanicScore), 0);
        }
        else
        {
            reducePanicTimer -= Time.deltaTime;
            if(reducePanicTimer <= 0)
            {
                reducePanic = true;
            }
        }
        if (printTimer >= 1f)
        {
            printTimer -= 1f;
            writer.WriteLine(activeTimer + " " + panicScore + " " + currentTempo);
        }
    }

    private void ChangeTempo()
    {
        switch (currentTempo)
        {
            case TempoType.BUILDUP:
                if (panicScore >= upperThreshold)
                {
                    currentTempo = TempoType.SUSTAIN;
                    tempoTimer = 0;
                    Debug.Log("Current tempo" + currentTempo);
                }
                break;
            case TempoType.SUSTAIN:
                tempoTimer += Time.deltaTime;
                if (tempoTimer > sustainTime)
                {
                    currentTempo = TempoType.FADE;
                    Debug.Log("Current tempo" + currentTempo);
                    ResetReductionTimer();
                }
                break;
            case TempoType.FADE:
                if (panicScore <= lowerThreshold)
                {
                    currentTempo = TempoType.RELAX;
                    tempoTimer = 0;
                    Debug.Log("Current tempo" + currentTempo);
                }
                break;
            case TempoType.RELAX:
                tempoTimer += Time.deltaTime;
                if (tempoTimer > sustainTime)
                {
                    currentTempo = TempoType.BUILDUP;
                    Debug.Log("Current tempo" + currentTempo);
                }
                break;
            default:
                break;
        }
    }

    public void StorePlayerReader(PlayerReader playerReader)
    {
        this.playerReader = playerReader;
    }

    public void IncreasePanicOnDamageTaken(float damagePercent)
    {
        if (!active)
        {
            return;
        }
        float panicIncrease = damagePercent;
        panicScore = Mathf.Min(panicScore + panicIncrease * panicIncreaseModifier, maxPanicScore);
        ResetReductionTimer();
    }
    public void IncreasePanicOnKill(float distanceToEnemy)
    {
        if (!active)
        {
            return;
        }
        float panicIncrease = 1f / distanceToEnemy;
        panicScore = Mathf.Min(panicScore + panicIncrease * panicIncreaseModifier, maxPanicScore);
        ResetReductionTimer();
    }

    private void ResetReductionTimer()
    {
        reducePanic = false;
        reducePanicTimer = reducePanicInterval;
    }
}
