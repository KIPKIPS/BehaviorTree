using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject {
    public enum State {
        Running,Failure,Success,
    }

    public State state;
    public bool started = false;
    
    /// <summary>
    /// 更新逻辑
    /// </summary>
    /// <returns></returns>
    public State Update() {
        if (!started) {
            OnStart();
            started = true;
        }
        state = OnUpdate();
        if (state == State.Failure || state == State.Success) {
            OnStop();
            started = false;
        }
        return state;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
