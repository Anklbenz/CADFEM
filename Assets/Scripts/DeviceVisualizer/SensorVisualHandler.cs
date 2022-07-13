using System.Collections.Generic;
using UnityEngine;

public class SensorVisualHandler {

   private readonly List<Sensor> _rotatableSensors;
   private readonly Camera _cam;
   private bool _rotate;

   public SensorVisualHandler(List<Sensor> sensors){
      _cam = Camera.main;
      _rotatableSensors = new List<Sensor>();
      GetRotatableSensors(sensors);
   }

   private void GetRotatableSensors(List<Sensor> sensors){
      foreach (var sensor in sensors)
         if (sensor is IRotatable)
            _rotatableSensors.Add(sensor);
   }

   public void Update(){
      if (!_rotate) return;
      foreach (var rotatableSensor in _rotatableSensors)
         rotatableSensor.gameObject.transform.LookAt(_cam.transform.position);
   }

   public void SetRotateMode(bool state){
      _rotate = state;

      if (!state)
         RotateToStartPosition();
   }

   private void RotateToStartPosition(){
      foreach (var rotatableSensor in _rotatableSensors){
         var instance = rotatableSensor.gameObject.transform;
         instance.localRotation = Quaternion.identity; // Euler(Vector3.zero);
      }
   }
}

