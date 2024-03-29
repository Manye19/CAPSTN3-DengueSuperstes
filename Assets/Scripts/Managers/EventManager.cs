using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTimeCheckEvent : UnityEvent<float> { };
public class OnGamePauseEvent : UnityEvent<bool> { };

public class OnPlayerWinEvent : UnityEvent { };

public class OnDamageEvent : UnityEvent<float> { };

public class OnUpdateUpgradesEvent : UnityEvent { };

public class OnDeathEvent : UnityEvent { };
public class OnUpdateUIXP : UnityEvent<int, float, float> { };
public class OnEnemySpawnEvent : UnityEvent { };
public class OnChangeTargetEvent : UnityEvent<Transform> { };
public class OnExpDrop : UnityEvent<Vector3> { };
public class OnLevelUpEvent : UnityEvent<bool> { };
public class InteractEvent : UnityEvent<GameObject> { };