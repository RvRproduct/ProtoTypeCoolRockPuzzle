using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentUIManager : MonoBehaviour
{
    [SerializeField]
    private Color inActivateColor;
    [SerializeField]
    private Color activateColor;
    [SerializeField]
    private Image keyboardIcon;
    [SerializeField]
    private Image drumIcon;
    [SerializeField]
    private Image guitarIcon;
    [SerializeField]
    private Image VocalIcon;
    private PlayerInstrumentType currentInstrumentType;
    private Image currentActiveIcon;

    private void Start()
    {
        currentActiveIcon = guitarIcon;
        ActiveInstrument(PlayerInstrumentType.Guitar);
    }
    
    public void ActiveInstrument(PlayerInstrumentType instrumentType)
    {
        currentActiveIcon.color = inActivateColor;
        currentInstrumentType = instrumentType;
        switch(currentInstrumentType)
        {
            case PlayerInstrumentType.Guitar:  
                guitarIcon.color = activateColor;
                currentActiveIcon = guitarIcon;
                break;
            case PlayerInstrumentType.Drum:
                drumIcon.color = activateColor;
                currentActiveIcon = drumIcon;
                break;
            case PlayerInstrumentType.Keyboard:
                keyboardIcon.color = activateColor; 
                currentActiveIcon = keyboardIcon;
                break;
            case PlayerInstrumentType.Vocal:
            VocalIcon.color = activateColor;
                currentActiveIcon = VocalIcon;
                break;
        }
    }
}
