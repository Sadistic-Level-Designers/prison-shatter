using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerMultiplexerType {
    one
    , all
    , sequence
    , sequenceForced
    //, incremental
}

public class TriggerMultiplexer : TriggerBehaviour
{
    public List<TriggerBehaviour> list;
    public TriggerMultiplexerType type = TriggerMultiplexerType.one;

    void Start() {
        switch(type) {
            case TriggerMultiplexerType.sequenceForced:
                if(list.Count > 1) for(int i = 1; i < list.Count; ++i) {
                    list[i].enabled = false;
                }
                break;
        }
    }

    void LateUpdate() {
        switch(type) {
            // When just one sub-trigger is called
            case TriggerMultiplexerType.one:
                foreach(TriggerBehaviour t in list) {
                    if(t.isActive) {
                        this.isActive = true;
                        break;
                    }
                }
                break;

            // When all sub-triggers are called
            case TriggerMultiplexerType.all:
                int c = 0;

                foreach(TriggerBehaviour t in list) {
                    if(t.isActive) {
                        ++c;
                    }
                }

                if(c == list.Count) {
                    this.isActive = true;
                }
                break;

            case TriggerMultiplexerType.sequence:
                // Verify if all triggers have been activated in sequence
                bool flag = true;
                foreach(TriggerBehaviour t in list) {
                    if(flag) {
                        flag = t.isActive;
                    }
                    // Break sequence if one trigger is not activated without the previous one
                    else if(t.isActive) {
                        _ResetAll();
                        return;
                    }
                }

                // If sequence is complete, acticate multiplexer
                if(flag) {
                    this.isActive = true;
                }
                break;

            case TriggerMultiplexerType.sequenceForced:
                for(int i = 0; i < list.Count; ++i) {
                    if(list[i].enabled && list[i].isActive) {
                        list[i].enabled = false;

                        if(i == list.Count - 1) {
                            this.isActive = true;
                        } else {
                            list[i + 1].enabled = true;
                        }
                    }
                }
                break;
        } 
    }

    private void _ResetAll() {
        foreach(TriggerBehaviour t in list) {
            t.isActive = false;
        }
    }

}
