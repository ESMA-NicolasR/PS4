using UnityEngine;

public class MiniGame<T> : Focusable where T : ResourceObjectiveData
{
    public bool mustLaunchScenario;
    protected T _scenario;
    
    public override void GainFocus()
    {
        base.GainFocus();
        if(mustLaunchScenario)
            LaunchScenario();
    }

    public void PlayScenario(T scenario)
    {
        Debug.Log("Activer visuel dispo");
        _scenario = scenario;
        mustLaunchScenario = true;
    }


    public virtual void LaunchScenario()
    {
        mustLaunchScenario = false;
    }
}
