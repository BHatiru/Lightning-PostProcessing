using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CharacterAnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject _magicCircle;
    [SerializeField] private ParticleSystem _magicBeams;
    [SerializeField] private ParticleSystem _greenCandleFlame;
    [SerializeField] private ParticleSystem _blueCandleFlame;
    [SerializeField] private ParticleSystem _redCandleFlame;
    [SerializeField] private Light _greenLight;
    [SerializeField] private Light _blueLight;
    [SerializeField] private Light _redLight;
    private Animator _animator;
    private float _targetValue=0f;
    [SerializeField]private float _deltaBlend = 1f;
    private Vector3 _deltaScale = new Vector3(0,2f,0);
    private Vector3 _minScale = new Vector3(1f,1f,1f);
    private Vector3 _maxScale = new Vector3(1f,5f,1f);

    private float _timer;
    private bool _timerIsRunning;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(!_timerIsRunning){
            _timer = 0;
            _timerIsRunning = true;
        }else{
            _timer += Time.deltaTime;
        }
        
        
        if(_timer > 8f){
            _targetValue += _deltaBlend * Time.deltaTime;
            if(_targetValue >= 1f){
                _targetValue = 1f;
            }
            _magicCircle.transform.localScale += _deltaScale * Time.deltaTime; 
            if(_magicCircle.transform.localScale.y >= 5f){
                _magicCircle.transform.localScale = _maxScale;

                _magicBeams.GetComponent<ParticleSystemRenderer>().freeformStretching = true;

                _greenCandleFlame.startSize = 1f;
                _blueCandleFlame.startSize = 1f;
                _redCandleFlame.startSize = 1f;

                _greenLight.intensity = 1f;
                _blueLight.intensity = 1f;
                _redLight.intensity = 1f;

                Time.timeScale = 1.5f;

                if(_timer > 30f){
                    _timerIsRunning = false;
                }
            }

        }else{

            Time.timeScale = 1.0f;


            _targetValue -= 1.5f * Time.deltaTime;
            if(_targetValue <= 0){
                _targetValue = 0;
            }

            _magicCircle.transform.localScale -= _deltaScale * Time.deltaTime; 

             if(_magicCircle.transform.localScale.y <= 1f){
                _magicCircle.transform.localScale = _minScale;
                _magicBeams.GetComponent<ParticleSystemRenderer>().freeformStretching = false;

                _greenCandleFlame.startSize = 0.15f;
                _blueCandleFlame.startSize =0.15f;
                _redCandleFlame.startSize = 0.15f;

                _greenLight.intensity = 0.6f;
                _blueLight.intensity = 0.6f;
                _redLight.intensity = 0.6f;
                
            }
            

        }
        _animator.SetFloat("Blend", _targetValue);
    }
}
