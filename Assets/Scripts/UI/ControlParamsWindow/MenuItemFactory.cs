using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class MenuItemFactory {
   private const string VALUE_CODE = "CP_VALUE";
   private const string STATE_CODE = "CP_DROPDOWN";
   private const string DROPDOWN_CODE = "CP_STATE";

   private readonly ControlParamsMenuItemInput _inputPrefab;
   private readonly ControlParamsMenuItemState _statePrefab;
   private readonly ControlParamsMenuItemDropDown _dropdownPrefab;
   private readonly RectTransform _contentParent;

   public MenuItemFactory(ControlParamsMenuItemInput inputPrefab, ControlParamsMenuItemState statePrefab,
      ControlParamsMenuItemDropDown dropdownPrefab, RectTransform contentParent){
      _inputPrefab = inputPrefab;
      _statePrefab = statePrefab; 
      _dropdownPrefab = dropdownPrefab;
      _contentParent = contentParent;
   }

   public ControlParamsMenuItem MenuItemCreate(string gettingType){
      switch (gettingType){
         case VALUE_CODE:
            return Get(_inputPrefab);
         case STATE_CODE:
            return Get(_statePrefab);
         case DROPDOWN_CODE:
            return Get(_dropdownPrefab);
      }

      throw new ArgumentException($"Control param menu element {gettingType} not found");;
   }

   private T Get<T>(T prefabType)  where T: MonoBehaviour{
      return Object.Instantiate(prefabType, _contentParent);
   }
}
