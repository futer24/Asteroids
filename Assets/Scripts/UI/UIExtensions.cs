using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using TMPro;

public static class UIExtensions  
{
    public static LTDescr LeanFloatingDamageText(this TMP_Text textMesh, Vector3 position, float to, float time)
    {
        var _color = textMesh.color;
        var _tween = LeanTween
            .value(textMesh.gameObject, _color.a, to, time)
            .setOnStart(
            () =>
            {
              
                LeanTween.move(textMesh.gameObject, position - (Vector3.forward * 13f), 0f); 
                LeanTween.moveY(textMesh.gameObject, textMesh.gameObject.transform.position.y + 5f, time);
            }
            )
             
            .setOnUpdate((float _value) =>
            {
                _color.a = _value;
                textMesh.color = _color;
            })
            .setEaseInOutSine()
            .setDestroyOnComplete(true)
             
            ;
        return _tween;
    }
}
