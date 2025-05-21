using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _scoreHumansSaved;
    private int _scoreHumansKilled;
    private int _scoreMoneyGained;
    private int _scoreMoneyLost;
    public static ScoreManager Instance;

    private void Awake()
    {
        // Singleton
        if (Instance == null || Instance != this)
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
        Debug.Log(text);
    }

    public string GetFinalScore()
    {
        return $"You saved {_scoreHumansSaved} humans"
            +$"\nYou killed {_scoreHumansKilled} humans"
            +$"\nYou gained ${_scoreMoneyGained}"
            +$"\nYou lost ${_scoreMoneyLost}"
            +$"\n<b>Net result</b> : ${_scoreMoneyGained-_scoreMoneyLost}, {_scoreHumansSaved-_scoreHumansKilled} lives"
            ;
    }

    public string GetAchievements()
    {
        string result = $"<b>Achievements :</b>\n";
        // People killed
        if (_scoreHumansKilled > 100)
        {
            result += $"Mass Murderer\n";
        }
        else if (_scoreHumansKilled > 10)
        {
            result += $"Casual Manslaughter\n";
        }
        else if (_scoreHumansKilled > 0)
        {
            result += $"Nobody important was lost\n";
        }
        else
        {
            result += $"Pacifist\n";
        }
        // People saved
        if (_scoreHumansSaved > 100)
        {
            result += $"Intergalactic Saviour\n";
        }
        else if (_scoreHumansSaved > 10)
        {
            result += $"Local Hero\n";
        }
        else if (_scoreHumansSaved > 0)
        {
            result += $"Protect and Serve\n";
        }
        else
        {
            result += $"Bystander\n";
        }
        
        // Money gained
        if (_scoreMoneyGained > 100)
        {
            result += $"CEO\n";
        }
        else if (_scoreMoneyGained > 10)
        {
            result += $"Manager\n";
        }
        else if (_scoreMoneyGained > 0)
        {
            result += $"Intern\n";
        }
        else
        {
            result += $"Volunteer\n";
        }
        
        // Money lost
        if (_scoreMoneyLost > 100)
        {
            result += $"Financial Disaster\n";
        }
        else if (_scoreMoneyLost > 10)
        {
            result += $"Money Leaker\n";
        }
        else if (_scoreMoneyLost > 0)
        {
            result += $"Negligent\n";
        }
        else
        {
            result += $"Cautious\n";
        }

        return result;
    }
}
