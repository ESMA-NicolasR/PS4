using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _scoreHumansSaved;
    private int _scoreHumansKilled;
    private int _scoreMoneyGained;
    private int _scoreMoneyLost;
    public static ScoreManager Instance;
    [SerializeField] private FloatingText _floatingText;


    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ScoreHumans(int score)
    {
        if (score > 0)
        {
            SaveHumans(score);
        }
        else if (score < 0)
        {
            KillHumans(-score);
        }
    }
    
    public void ScoreMoney(int score)
    {
        if (score > 0)
        {
            GainMoney(score);
        }
        else if (score < 0)
        {
            LoseMoney(-score);
        }
    }

    private void SaveHumans(int score)
    {
        _scoreHumansSaved = +score;
        DisplayFloatingText($"You saved {score} humans", Color.green);
    }
    
    private void KillHumans(int score)
    {
        _scoreHumansKilled = +score;
        DisplayFloatingText($"You killed {score} humans", Color.red);

    }

    private void GainMoney(int score)
    {
        _scoreMoneyGained += score;
        DisplayFloatingText($"You gained ${score}", Color.green);

    }

    private void LoseMoney(int score)
    {
        _scoreMoneyLost += score;
        DisplayFloatingText($"You lost ${score}", Color.red);

    }

    private void DisplayFloatingText(string text, Color color)
    {
        _floatingText.DisplayText(text);
    }

    public string GetStatistics()
    {
        return $"Humans saved: {_scoreHumansSaved} ({GetHumansSavedTitle()})"
            +$"\nHumans killed: {_scoreHumansKilled} ({GetHumansKilledTitle()})"
            +$"\nMoney gained ${_scoreMoneyGained} ({GetMoneyGainedTitle()})"
            +$"\nMoney lost ${_scoreMoneyLost} ({GetMoneyLostTitle()})"
            +$"\n<u>Net result</u> : ${_scoreMoneyGained-_scoreMoneyLost}, {_scoreHumansSaved-_scoreHumansKilled} lives"
            ;
    }

    private string GetHumansSavedTitle()
    {
        if (_scoreHumansSaved > 500)
        {
            return "Intergalactic Saviour";
        }
        if (_scoreHumansSaved > 100)
        {
            return "Local Hero";
        }
        if (_scoreHumansSaved > 0)
        {
            return "Protect and Serve";
        }
        return $"Bystander";
    }

    private string GetHumansKilledTitle()
    {
        // People killed
        if (_scoreHumansKilled > 500)
        {
            return "Mass Murderer";
        }
        if (_scoreHumansKilled > 100)
        {
            return "Casual Manslaughter";
        }
        if (_scoreHumansKilled > 0)
        {
            return "Nobody important was lost";
        }
        return "Pacifist";
    }

    private string GetMoneyGainedTitle()
    {
        if (_scoreMoneyGained > 100)
        {
            return "CEO";
        }
        if (_scoreMoneyGained > 10)
        {
            return "Manager";
        }
        if (_scoreMoneyGained > 0)
        {
            return "Intern";
        }
        return "Volunteer";
    }

    private string GetMoneyLostTitle()
    {
        // Money lost
        if (_scoreMoneyLost > 1500000)
        {
            return "Financial trough";
        }
        if (_scoreMoneyLost > 500000)
        {
            return "Money Leaker";
        }
        if (_scoreMoneyLost > 0)
        {
            return "Negligent";
        }
        return "Cautious";
    }
}
