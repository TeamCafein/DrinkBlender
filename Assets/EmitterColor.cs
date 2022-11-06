using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

[RequireComponent(typeof(ObiEmitter))]
public class EmitterColor : MonoBehaviour
{
    ObiEmitter emitter;

    void Awake()
    {
        emitter = GetComponent<ObiEmitter>();
    }

    public void OnEnable() { }

    void LateUpdate()
    {
        if (!isActiveAndEnabled)
            return;

        for (int i = 0; i < emitter.solverIndices.Length; ++i)
        {
            int k = emitter.solverIndices[i];
            emitter.solver.colors[k] = new Color(emitter.solver.userData[k][0], emitter.solver.userData[k][1], emitter.solver.userData[k][2], emitter.solver.userData[k][3]);
        }
    }
}
