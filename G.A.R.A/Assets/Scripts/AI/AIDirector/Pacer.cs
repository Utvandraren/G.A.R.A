using UnityEngine;

public class Pacer : MonoBehaviour
{
    public enum TempoType
    {
        BUILDUP,
        SUSTAIN,
        FADE,
        RELAX,
    }
    public TempoType currentTempo;
    PlayerReader playerReader;
    float panicScore;
    float maxPanicScore = 10;
    float panicReductionRate;
    float nodeTime;
    float levelTime;
    private bool started;
    private float upperThreshold;
    private float lowerThreshold;
    private float tempoTimer;
    public float sustainTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        DetermineThreshold();
        currentTempo = TempoType.BUILDUP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        levelTime += Time.deltaTime;
        if (!playerReader.InCombat())
            panicScore = Mathf.Max(Mathf.Min(panicScore - panicReductionRate * Time.deltaTime, maxPanicScore), 0);
        ChangeTempo();
    }

    private void DetermineThreshold()
    {
        upperThreshold = 10; // Random.Range(5, 10);
        lowerThreshold = 0;  // Random.Range(0, 2);
    }

    private void ChangeTempo()
    {
        switch (currentTempo)
        {
            case TempoType.BUILDUP:
                if (panicScore > upperThreshold)
                {
                    currentTempo = TempoType.SUSTAIN;
                    tempoTimer = 0;
                }
                break;
            case TempoType.SUSTAIN:
                tempoTimer += Time.deltaTime;
                if (tempoTimer > sustainTime)
                {
                    currentTempo = TempoType.FADE;
                }
                break;
            case TempoType.FADE:
                if (panicScore < lowerThreshold)
                {
                    currentTempo = TempoType.RELAX;
                    tempoTimer = 0;
                }
                break;
            case TempoType.RELAX:
                tempoTimer += Time.deltaTime;
                if (tempoTimer > sustainTime)
                {
                    currentTempo = TempoType.BUILDUP;
                    DetermineThreshold();
                }
                break;
            default:
                break;
        }
    }

    internal void StorePlayerReader(PlayerReader playerReader)
    {
        this.playerReader = playerReader;
    }

    void CalcPanicReductionRate()
    {
        float hpPercent = playerReader.GetHPPercent();
        float shieldPercent = playerReader.GetShieldPercent();
        panicReductionRate = maxPanicScore * hpPercent + (maxPanicScore / 4) * shieldPercent;
    }

    public void StartedLevel()
    {
        started = true;
    }

    public void IncreasePanic(float panicAddition)
    {
        panicScore = Mathf.Min(panicScore + panicAddition, maxPanicScore);
    }
}
