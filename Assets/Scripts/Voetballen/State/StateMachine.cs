using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine <T> 
{
   T m_pOwner;

    State<T> m_pCurrentState;

    State<T> m_pPreviousState;

    State<T> m_pGlobalState;

    public StateMachine(T owner)
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

    public void SetCurrentState(State<T> s) { m_pCurrentState = s; }
    public void SetGlobalState(State<T> s) { m_pGlobalState = s; }
    public void SetPreviousState(State<T> s) { m_pPreviousState = s; }

    public void Updating()
    {
        if (m_pGlobalState)
            m_pGlobalState.Execute(m_pOwner);
        if (m_pCurrentState)
            m_pCurrentState.Execute(m_pOwner);
    }

    public void ChangeState(State<T> pNewState)
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

    State<T> CurrentState()
    {
        return m_pCurrentState;
    }
    State<T> GlobalState()
    {
        return m_pGlobalState;
    }
    State<T> PreviousState()
    {
        return m_pPreviousState;
    }

    public bool IsInstate(State<T> st){
        if (st == CurrentState()) return true;
        else return false;
    }

}
