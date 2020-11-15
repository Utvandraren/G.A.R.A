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
    float panicReductionRate = 0.5f;
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
    void FixedUpdate()
    {
        if (!started)
            return;

        levelTime += Time.fixedDeltaTime;
        ChangeTempo();
        CalcPanicReductionRate();
        if (!playerReader.InCombat())
            panicScore = Mathf.Max(Mathf.Min(panicScore - panicReductionRate * Time.deltaTime, maxPanicScore), 0);
    }

    private void DetermineThreshold()
    {
        upperThreshold = 10;
        lowerThreshold = 0;
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
        panicReductionRate = hpPercent + shieldPercent * 0.5f;
    }

    public void StartedLevel()
    {
        started = true;
    }

    public void IncreasePanic(float damagePercent)
    {
        float panicIncrease = damagePercent * maxPanicScore;
        panicScore = Mathf.Min(panicScore + panicIncrease * 2, maxPanicScore);
    }
}
