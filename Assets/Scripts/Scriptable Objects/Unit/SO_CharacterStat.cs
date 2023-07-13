using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Scriptable Object", menuName = "Scriptable Objects/Character")]
public class SO_CharacterStat : ScriptableObject
{
    public string characterName;
    public Sprite characterInGameSprite;
    public Sprite CharacterPanelSprite;
}
