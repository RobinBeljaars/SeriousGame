using UnityEngine;

public class State<Entity_Type> : MonoBehaviour
{

    public virtual bool OnMessage(Entity_Type entityType, Telegram_CH4 telegram)
    {
        return false;
    }

    public virtual void Enter(Entity_Type entity)
    {
 

    }


    public virtual void Execute(Entity_Type entity)
    {

    }

    public virtual void Exit(Entity_Type entity)
    {

    }
}