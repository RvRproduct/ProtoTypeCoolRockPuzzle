using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private InstrumentUIManager instrumentUIManager;
    private void Start()
    {
        PlayerInputManager.OnChangeInstrument += instrumentUIManager.ActiveInstrument;
    }

    private void OnDisable()
    {
        PlayerInputManager.OnChangeInstrument -= instrumentUIManager.ActiveInstrument;
    }
}
