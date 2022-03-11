using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    public float speed = 1f;
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.x + speed * Time.deltaTime, image.uvRect.y, image.uvRect.width, image.uvRect.height);
    }
}
