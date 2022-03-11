using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : InfiniteWorldBehaviour, IDestroyable
{

    public static event Action<Vector3,int> OnEnemyDamage;
    public static event Action<Vector3> OnEnemyDestroy;

    private int health;
    private int scorePointsForDestroy = 1400;
    private float torqueRange = 200f;
    private float speed;

    [SerializeField] private EnemyDataScriptableObject enemyData;
 

    private void OnEnable()
    {
        Init();
        SetSize();
        AddForceMovement();
        AddTorqueMovement();
    }

    private void OnDisable()
    {
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
    }

    void Init() {
        torqueRange = enemyData.torqueRange;
        scorePointsForDestroy = enemyData.scorePointsForDestroy;
        health = enemyData.size/2;
    }

    private void SetSize() =>  transform.localScale = Vector3.one * health;

    private void AddForceMovement() =>  rBody.AddForce(UnityEngine.Random.insideUnitCircle.normalized * speed);

    private void AddTorqueMovement() => rBody.AddTorque(Utils.GetRandomTorque(torqueRange));

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(scorePointsForDestroy);
        }
    }

    private void TakeDamage(int scoreDamage)
    {
        if (health > 0)
        {
            health--;
            OnEnemyDamage?.Invoke(transform.position,scoreDamage);
        }
        else
        {
            OnEnemyDestroy?.Invoke(transform.position);
            gameObject.SetActive(false);
              
        }

    }

    public void SetLevelValues(DifficultyCurvesScriptableObject scaling, int level) =>
        speed = enemyData.speed * scaling.SpeedCurve.Evaluate(level);

    public void Destroy()
    {
        Debug.Log("DestroyEnemy + " + gameObject.name);
        if (TryGetComponent(out Fracture f))
        {
            Debug.Log("FractureObject");
            f.FractureObject();

        }
    }
}
