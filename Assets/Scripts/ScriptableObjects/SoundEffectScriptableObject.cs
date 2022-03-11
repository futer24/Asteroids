using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectScriptableObject", menuName = "SoundEffectScriptableObject", order = 0)]
public class SoundEffectScriptableObject : ScriptableObject
{
    public AudioClip[] clips;

    [SerializeField] private int playIndex = 0;
    [SerializeField] private SoundClipPlayOrder playOrder;

    private AudioClip GetAudioClip()
    {
        // get current clip
        var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

        // find next clip
        switch (playOrder)
        {
            case SoundClipPlayOrder.in_order:
                playIndex = (playIndex + 1) % clips.Length;
                break;
            case SoundClipPlayOrder.random:
                playIndex = Random.Range(0, clips.Length);
                break;
            case SoundClipPlayOrder.reverse:
                playIndex = (playIndex + clips.Length - 1) % clips.Length;
                break;
        }

        return clip;
    }


    public AudioSource Play(Vector3 position ,   AudioSource audioSourceParam = null)
    {
        if (clips.Length == 0)
        {
            Debug.LogWarning($"Missing sound clips for {name}");
            return null;
        }

        var source = audioSourceParam;
        if (source == null)
        {
            var _obj = new GameObject("Sound", typeof(AudioSource));
            source = _obj.GetComponent<AudioSource>();
        }
        source.transform.position = position;
        source.clip = GetAudioClip();
        source.Play();
        return source;
    }

    enum SoundClipPlayOrder
    {
        random,
        in_order,
        reverse
    }
}
