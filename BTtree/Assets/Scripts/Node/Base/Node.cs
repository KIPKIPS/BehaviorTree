using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class Node : ScriptableObject {
        public enum State {
            Running,Failure,Success,
        }

        [HideInInspector]public State state;
        [HideInInspector]public bool started = false;
        [HideInInspector]public string guid;
        [HideInInspector]public Vector2 position;
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
        protected abstract State OnUpdate();
        protected abstract void OnStop();

        public virtual Node Clone() {
            return Instantiate(this);
        }
    }
    
}
