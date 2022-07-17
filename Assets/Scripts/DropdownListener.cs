using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownListener : MonoBehaviour
{
    public Dropdown ElementDropdown;
    public PlayerCharacter receiver;

    void Start()
    {
        ElementDropdown.onValueChanged.AddListener(delegate
        {
            ElementDropdownValueChange();
        });
    }

    public void ElementDropdownValueChange()
    {
        receiver.setElement(ElementDropdown.options[ElementDropdown.value].text);
    }
}
