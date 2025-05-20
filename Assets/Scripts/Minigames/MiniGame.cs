using TMPro;
using UnityEngine;

public class MiniGame<T> : Focusable where T : ResourceObjectiveData
{
    public bool mustLaunchScenario;
    protected T _scenario;
    public TextMeshPro stateText;
    public string textWhenInactive;
    public string textWhenBroken;

    protected override void Start()
    {
        base.Start();
        stateText.text = textWhenInactive;
        stateText.color = Color.green;
    }

    public override void GainFocus()
    {
        base.GainFocus();
        if(mustLaunchScenario)
            LaunchScenario();
    }

    public void PlayScenario(T scenario)
    {
        stateText.text = textWhenBroken;
        stateText.color = Color.red;
        _scenario = scenario;
        mustLaunchScenario = true;
    }


    public virtual void LaunchScenario()
    {
        mustLaunchScenario = false;
        stateText.text = "";
    }
}
