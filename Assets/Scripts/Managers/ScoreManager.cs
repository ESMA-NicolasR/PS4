using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int scoreHumansSaved;
    public int scoreHumansKilled;
    public int scoreMoneyGained;
    public int scoreMoneyLost;
    public static ScoreManager Instance;
    [SerializeField] private FloatingText _floatingText;
    private string _textToDisplay;


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

    public void Score(int humans, int money)
    {
        _textToDisplay = "";
        ScoreHumans(humans);
        _textToDisplay += "\n";
        ScoreMoney(money);
        DisplayFloatingText(_textToDisplay);
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
        else
        {
            _textToDisplay += "No human life affected";
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
        else
        {
            _textToDisplay += "No money was lost";
        }
    }

    private void SaveHumans(int score)
    {
        scoreHumansSaved += score;
        _textToDisplay += $"You saved {score} humans";
    }
    
    private void KillHumans(int score)
    {
        scoreHumansKilled += score;
        _textToDisplay += $"You killed {score} humans";
    }

    private void GainMoney(int score)
    {
        scoreMoneyGained += score;
        _textToDisplay += $"You gained ${score}";
    }

    private void LoseMoney(int score)
    {
        scoreMoneyLost += score;
        _textToDisplay += $"You lost ${score}";
    }

    private void DisplayFloatingText(string text)
    {
        _floatingText.DisplayText(text);
    }

    public string GetStatistics()
    {
        return $"Humans saved: {scoreHumansSaved} ({GetHumansSavedTitle()})"
            +$"\nHumans killed: {scoreHumansKilled} ({GetHumansKilledTitle()})"
            +$"\nMoney gained ${scoreMoneyGained} ({GetMoneyGainedTitle()})"
            +$"\nMoney lost ${scoreMoneyLost} ({GetMoneyLostTitle()})"
            +$"\n<u>Net result</u> : ${scoreMoneyGained-scoreMoneyLost}, {scoreHumansSaved-scoreHumansKilled} lives"
            ;
    }

    private string GetHumansSavedTitle()
    {
        if (scoreHumansSaved > 500)
        {
            return "Intergalactic Saviour";
        }
        if (scoreHumansSaved > 100)
        {
            return "Local Hero";
        }
        if (scoreHumansSaved > 0)
        {
            return "Protect and Serve";
        }
        return $"Bystander";
    }

    private string GetHumansKilledTitle()
    {
        // People killed
        if (scoreHumansKilled > 500)
        {
            return "Mass Murderer";
        }
        if (scoreHumansKilled > 100)
        {
            return "Casual Manslaughter";
        }
        if (scoreHumansKilled > 0)
        {
            return "Nobody important was lost";
        }
        return "Pacifist";
    }

    private string GetMoneyGainedTitle()
    {
        if (scoreMoneyGained > 1500000)
        {
            return "CEO";
        }
        if (scoreMoneyGained > 500000)
        {
            return "Manager";
        }
        if (scoreMoneyGained > 0)
        {
            return "Intern";
        }
        return "Volunteer";
    }

    private string GetMoneyLostTitle()
    {
        // Money lost
        if (scoreMoneyLost > 1500000)
        {
            return "Financial trough";
        }
        if (scoreMoneyLost > 500000)
        {
            return "Money Leaker";
        }
        if (scoreMoneyLost > 0)
        {
            return "Negligent";
        }
        return "Cautious";
    }
}
