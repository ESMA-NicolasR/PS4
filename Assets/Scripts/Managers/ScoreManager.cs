using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _scoreHumansSaved;
    private int _scoreHumansKilled;
    private int _scoreMoneyGained;
    private int _scoreMoneyLost;
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
        _scoreHumansSaved = +score;
        _textToDisplay += $"You saved {score} humans";
    }
    
    private void KillHumans(int score)
    {
        _scoreHumansKilled = +score;
        _textToDisplay += $"You killed {score} humans";
    }

    private void GainMoney(int score)
    {
        _scoreMoneyGained += score;
        _textToDisplay += $"You gained ${score}";
    }

    private void LoseMoney(int score)
    {
        _scoreMoneyLost += score;
        _textToDisplay += $"You lost ${score}";
    }

    private void DisplayFloatingText(string text)
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
