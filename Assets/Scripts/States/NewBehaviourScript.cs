using UnityEngine;


// Clean up namespaces next


public interface IenemyAIContext
{
    void setState(State newState);    
}

public abstract class State
{
    public abstract void Enter(IenemyAIContext context);
    public abstract void Update(IenemyAIContext context);
    public abstract void Exit(IenemyAIContext context);

}







public class AIStatePattern : MonoBehaviour, IenemyAIContext
{
    private State currentState;

    void IenemyAIContext.setState(State newState)
    {
        currentState = newState;
    }
}






public class DeadState : State
{
    public override void Enter(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }


    public override void Update(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }
}



public class PatrollingState : State
{
    public override void Enter(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

}




public class PausedState : State
{
    public override void Enter(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }
    public override void Exit(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

}




public class AttackingState : State
{
    public override void Enter(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }
    public override void Exit(IenemyAIContext context)
    {
        throw new System.NotImplementedException();
    }

}
