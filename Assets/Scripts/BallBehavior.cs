using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 _startState;
    [SerializeField] private Vector3 _finalState;
    [SerializeField] private float _speed = 2f;
    private float _currentTime;
    private Vector3 _tempStart;
    private Vector3 _tempFinal;
    private bool _ballMovementDirection = true; // true - шар идет вправо, false - влево
    private void Start()
    {
        _tempStart = _startState;
        _tempFinal = _finalState;
    }
    void Update()
    {
        CheckBallPosition(); // Если координаты совпадают- возвращаем исходные координаты начальной и конечной точек + меняем флаг
        _currentTime += Time.deltaTime;
        var distance = Vector3.Distance(_tempStart, _tempFinal);
        var travelTime = distance / _speed;
        var progress = _currentTime / travelTime ;
        transform.position = Vector2.Lerp(_tempStart,  _tempFinal, progress);
        if (_currentTime > travelTime) // Идем в обратную сторону
        {
            _currentTime = 0;
            (_tempStart,_tempFinal) = (_tempFinal, _tempStart);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentTime = 0;
            _tempStart = transform.position; // Будем двигаться от текущей позиции шара
            if (_ballMovementDirection) // Проверка, в какую сторону двигается шар для установления конечной точки
            {
                _tempFinal = _startState;
                _ballMovementDirection = false;
            }
            else
            {
                _tempFinal = _finalState;
                _ballMovementDirection = true;
            } 
            transform.position = Vector2.Lerp(_tempStart,  _tempFinal, progress);
        }

        void CheckBallPosition()
        {
            if (transform.position == _finalState) 
            {
                _tempFinal = _startState;
                _tempStart = _finalState;
                _ballMovementDirection = false;
                _currentTime = 0;
            }
            if (transform.position == _startState) 
            {
                _tempFinal = _finalState;
                _tempStart = _startState;
                _ballMovementDirection = true;
                _currentTime = 0;
            }
        }
        
    }
}