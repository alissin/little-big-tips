using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Attack")]
public class AttackSO : ScriptableObject {

    public enum Type {
        Body, Head
    }

    public enum Phase {
        Armor, Health
    }

    public float baseDamage;

    public float headDamageFactor;

    public float criticalHitChance;
    public float criticalHitDamageFactor;

    public float damageToArmorFactor;
    public float damageToHealthFactor;

    public float CalculateDamage(Type type, Phase phase) {
        float phaseDamage = CalculatePhaseDamage(phase);

        bool isCriticalHit = IsCriticalHit;
        float bodyDamage = CalculateBodyDamage(isCriticalHit);

        float finalDamage = 0.0f;
        switch (type) {
            case Type.Body:
                finalDamage = bodyDamage + phaseDamage;
                break;

            case Type.Head:
                finalDamage = CalculateHeadDamage(bodyDamage) + phaseDamage;
                break;
        }

        return finalDamage;
    }

    bool IsCriticalHit {
        get => Random.value <= criticalHitChance;
    }

    float CalculateBodyDamage(bool isCriticalHit) {
        return baseDamage + (isCriticalHit ? baseDamage * criticalHitDamageFactor : 0);
    }

    float CalculateHeadDamage(float bodyDamage) {
        return bodyDamage + (baseDamage * headDamageFactor);
    }

    float CalculatePhaseDamage(Phase phase) {
        float phaseDamage = 0.0f;
        switch (phase) {
            case Phase.Armor:
                phaseDamage = baseDamage * damageToArmorFactor;
                break;

            case Phase.Health:
                phaseDamage = baseDamage * damageToHealthFactor;
                break;
        }
        return phaseDamage;
    }
}