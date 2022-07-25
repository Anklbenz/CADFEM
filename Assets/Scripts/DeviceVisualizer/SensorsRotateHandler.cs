using System.Collections.Generic;
using UnityEngine;

public class SensorsRotateHandler {

   private readonly List<Sensor> _rotatableSensors;
   private readonly Camera _cam;
   private bool _rotate;

   public SensorsRotateHandler(List<Sensor> sensors){
      _cam = Camera.main;
      _rotatableSensors = ComposeRotatableSensorsList(sensors);
   }

   public void Update(){
      if (!_rotate) return;
      LookAtCamera();
   }

   public void SetRotateMode(bool state){
      _rotate = state;

      if (!state)
         LookToDefaultPosition();
   }

   private void LookAtCamera(){
      foreach (var rotatableSensor in _rotatableSensors)
         rotatableSensor.transform.rotation = Quaternion.LookRotation(rotatableSensor.transform.position - _cam.transform.position);
   }

   private void LookToDefaultPosition(){
      foreach (var rotatableSensor in _rotatableSensors){
         var instance = rotatableSensor.gameObject.transform;
         instance.localRotation = Quaternion.identity; // Euler(Vector3.zero);
      }
   }

   private List<Sensor> ComposeRotatableSensorsList(List<Sensor> sensors){
      var rotetables = new List<Sensor>();
      foreach (var sensor in sensors)
         if (sensor is IRotatable)
            rotetables.Add(sensor);
    
      return rotetables;
   }
}

