using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{

    public void SpawnHealthBar(DamageableEntity entity, Vector3 offset)
    {
        var prefab = Instantiate(Resources.Load<GameObject>("Prefabs/HealthBar"), transform);
        var w2s = prefab.GetComponent<WorldToScreen>();
        var hb = prefab.GetComponent<HealthBarUI>();
        w2s.TargetTransform = entity.transform;
        w2s.TargetOffset = offset;
        hb.TargetEntity = entity;
    }

    public void SpawnFloatingText(string text, Vector3 position)
    {
        var prefab = Instantiate(Resources.Load<GameObject>("Prefabs/FloatingText"), transform);
        var w2s = prefab.GetComponent<WorldToScreen>();
        var tm = prefab.GetComponent<TextMeshProUGUI>();
        var target = new GameObject("FloatingTextTarget");
        target.AddComponent<FloatingTextTarget>();
        tm.text = text;
        w2s.DestroyOnTargetNull = true;
        w2s.TargetTransform = target.transform;
        w2s.TargetOffset = position;
    }
    
}
