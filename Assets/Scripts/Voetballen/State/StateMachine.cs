using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<Entity_Type> 
{
   Entity_Type m_pOwner;

    State<Entity_Type> m_pCurrentState;

    State<Entity_Type> m_pPreviousState;

    State<Entity_Type> m_pGlobalState;

    public StateMachine(Entity_Type owner)
    {
        m_pOwner = owner;
        m_pCurrentState = null;
        m_pPreviousState = null;
        m_pGlobalState = null;
    }

    public bool HandleMessage(Telegram_CH4 msg)
    {
        if (m_pCurrentState && m_pCurrentState.OnMessage(m_pOwner, msg))
        {
            return true;
        }

        if (m_pGlobalState && m_pGlobalState.OnMessage(m_pOwner, msg))
        {
            return true;
        }
        return false;
    }

    public void SetCurrentState(State<Entity_Type> s) { m_pCurrentState = s; }
    public void SetGlobalState(State<Entity_Type> s) { m_pGlobalState = s; }
    public void SetPreviousState(State<Entity_Type> s) { m_pPreviousState = s; }

    public void Updating()
    {
        if (m_pGlobalState)
            m_pGlobalState.Execute(m_pOwner);
        if (m_pCurrentState)
            m_pCurrentState.Execute(m_pOwner);
    }

    public void ChangeState(State<Entity_Type> pNewState)
    {
        if (pNewState == null)
        {
            Debug.Log("<StateMachine::ChangeState>: trying to change to a null state");
        }

        m_pPreviousState = m_pCurrentState;

        m_pCurrentState.Exit(m_pOwner);

        m_pCurrentState = pNewState;

        m_pCurrentState.Enter(m_pOwner);
    }

    public void RevertToPreviousState()
    {
        ChangeState(m_pPreviousState);
    }

    State<Entity_Type> CurrentState()
    {
        return m_pCurrentState;
    }
    State<Entity_Type> GlobalState()
    {
        return m_pGlobalState;
    }
    State<Entity_Type> PreviousState()
    {
        return m_pPreviousState;
    }

    public bool IsInstate(State<Entity_Type> st){
        if (st == CurrentState()) return true;
        else return false;
    }

}
