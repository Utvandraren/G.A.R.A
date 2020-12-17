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
    [SerializeField]private float panicReductionRate = 0.1f;
    private bool started;
    private float upperThreshold = 10f;
    private float lowerThreshold = 0f;
    private float tempoTimer;

    private float activeTimer;
    private float printTimer;
    private StreamWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        currentTempo = TempoType.BUILDUP;
        writer = new StreamWriter(@"panic.txt", true);
        writer.AutoFlush = true;
        writer.WriteLine(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " " + System.DateTime.Now.ToString() + " " + panicReductionRate + " " + panicIncreaseModifier);
        writer.WriteLine("----------------------------------------");
    }

    private void OnDestroy()
    {
        writer.Flush();
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;
        activeTimer += Time.deltaTime;
        printTimer += Time.deltaTime;
        ChangeTempo();
        //CalcPanicReductionRate();
        if (!playerReader.InCombat())
            panicScore = Mathf.Max(Mathf.Min(panicScore - panicReductionRate * Time.deltaTime, maxPanicScore), 0);
        if(printTimer >= 1f)
        {
            printTimer -= 1f;
            writer.WriteLine(activeTimer + "/" + panicScore + ":" + currentTempo);
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

    private void CalcPanicReductionRate()
    {
        float hpPercent = playerReader.GetHPPercent();
        float shieldPercent = playerReader.GetShieldPercent();
        panicReductionRate = hpPercent + shieldPercent * 0.5f;
    }

    public void StartedLevel()
    {
        started = true;
    }

    public void IncreasePanicOnDamageTaken(float damagePercent)
    {
        float panicIncrease = damagePercent;
        panicScore = Mathf.Min(panicScore + panicIncrease * panicIncreaseModifier, maxPanicScore);
        Debug.Log("increased Panic with " + panicIncrease * panicIncreaseModifier);
    }
    public void IncreasePanicOnKill(float distanceToEnemy)
    {
        float panicIncrease = 1f / distanceToEnemy;
        panicScore = Mathf.Min(panicScore + panicIncrease * panicIncreaseModifier, maxPanicScore);
        Debug.Log("increased Panic with " + panicIncrease * panicIncreaseModifier);
    }
}
