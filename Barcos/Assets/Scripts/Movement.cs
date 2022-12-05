using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float _maxRotationSpeed, _maxSpeed, _speedChangeDuration;
    
    float _rotationInputScaled, _currentSpeed;
    int _speedSetting = 1;
    Vector3 _rotation;

    Coroutine lerpSpeedCoroutine;
    Rigidbody body;
    Transform _rotationPivot;
    public GameObject smallSail, bigSail;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        _currentSpeed = _maxSpeed/2;
        _rotationPivot = transform.GetChild(0);
    }
    void Update()
    {
        //change straight speed part
        if(Input.GetButtonDown("Faster"))
        {   ChangeSpeed(true);}

        else if(Input.GetButtonDown("Slower"))
        {   ChangeSpeed(false);}
        
        body.velocity = transform.forward*_currentSpeed;

        //change rotation speed part
        _rotationInputScaled = Input.GetAxis("Horizontal")*_maxRotationSpeed;
        if(Mathf.Approximately(_rotationInputScaled, body.angularVelocity.x))
        {   return;}
        
        _rotation = Vector3.up * _rotationInputScaled;
        body.angularVelocity = _rotation;
        
        //lean to one side
        Lean();
    }

    void ChangeSpeed(bool faster)
    {
        if(faster)
        {
            if(_speedSetting == 2)
            {   return;}
            _speedSetting++;
        }
        
        else
        {
            if(_speedSetting == 0)
            {   return;}
            _speedSetting--;
        }

        if(lerpSpeedCoroutine != null)
        {   StopCoroutine(lerpSpeedCoroutine);}
        
        switch(_speedSetting)
        {
            case 0:
                lerpSpeedCoroutine = StartCoroutine(LerpSpeed(0f));
                break;
            case 1:
                lerpSpeedCoroutine = StartCoroutine(LerpSpeed(_maxSpeed/2f));
                break;
            case 2:
                lerpSpeedCoroutine = StartCoroutine(LerpSpeed(_maxSpeed));
                break;
            default:
                break;
        }
        SetSails(_speedSetting);
    }

    IEnumerator LerpSpeed(float targetSpeed)
    {
        float startSpeed = _currentSpeed;
        float timer = 0;
        
        while(!Mathf.Approximately(body.velocity.magnitude, targetSpeed))
        {
            _currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, timer/_speedChangeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void Lean()
    {
        _rotationPivot.localRotation = Quaternion.Euler(0, 0, _rotationInputScaled*70f);   
    }

    void SetSails(int sails)
    {

        switch(sails)
        {
            case 0:
                smallSail.SetActive(false);
                bigSail.SetActive(false);
                break;
            case 1:
                smallSail.SetActive(false);
                bigSail.SetActive(true);
                break;
            case 2:
                smallSail.SetActive(true);
                bigSail.SetActive(true);
                break;
            default:
                break;
        }
    }     
}
