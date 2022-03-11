using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundEffectScriptableObject sfxPlayerShoot;
    [SerializeField] private SoundEffectScriptableObject sfxEnemyShoot;
    [SerializeField] private SoundEffectScriptableObject sfxExplosion;

    
    [SerializeField] private AudioSource aSourcePlayer;
    [SerializeField] private AudioSource aSourceEnemy;


    private void Awake() => Enemy.OnEnemyDestroy += PlaySfxExplosion;
    

    private void OnDestroy() => Enemy.OnEnemyDestroy -= PlaySfxExplosion;


    public void PlaySfxPlayerShoot(Vector3 position)
    {
        position.z = Camera.main.transform.position.z;
        sfxPlayerShoot.Play(position,aSourcePlayer);
       
    }

    public void PlaySfxExplosion(Vector3 position)
    {
        position.z = Camera.main.transform.position.z;
        sfxExplosion.Play(position, aSourceEnemy);
    }
}
