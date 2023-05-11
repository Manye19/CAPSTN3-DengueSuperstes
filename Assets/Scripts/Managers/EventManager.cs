using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeathEvent : UnityEvent { };
public class OnUpdateUIXP : UnityEvent<int, float, float> { };