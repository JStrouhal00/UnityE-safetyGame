using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class QuestionMarkAnimator : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startRotation;
    public Vector3 cameraOffset = new Vector3(0f, 0f, 5f);
    public float rotationSpeed = 100;

    public float transitionDuration = 1f;

    bool rotate = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }

    [ContextMenu("show")]
    public void MoveInFrontOfCamera(Action onComplete)
    {
        transform.DOMove(GameManager.instance.camera.transform.TransformPoint(cameraOffset), transitionDuration);
        transform.DORotate(GameManager.instance.camera.transform.rotation.eulerAngles, transitionDuration).OnComplete(() => {
            rotate = true;
            onComplete?.Invoke();
            //transform.DOLocalRotateQuaternion(Quaternion.Euler(rotationSpeed), 1f).SetLoops(-1).SetEase(Ease.Linear).SetRelative();
        });
    }

    [ContextMenu("return")]
    public void ReturnToOriginalPosition()
    {
        rotate = false;
        DOTween.Kill(transform);
        transform.DOMove(startPosition, transitionDuration);
        transform.DORotate(startRotation, transitionDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
