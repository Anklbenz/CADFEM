using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RequestParamClasses;
using UnityEngine;
using ThingData;
using UnityEngine.UI;

public class ControlParamsInputWindow : Window {
   [SerializeField] private ScreenshotWindow screenshotWindow;
   [SerializeField] private RectTransform contentParent;
   [SerializeField] private ControlParamsMenuItemInput menuItemInputPrefab;
   [SerializeField] private ControlParamsMenuItemDropDown menuItemDropDownPrefab;
   [SerializeField] private ControlParamsMenuItemState menuItemStatePrefab;
   [SerializeField] private Button acceptButton, takePhotoButton;

   private MenuItemFactory _itemFactory;
   private UniTaskCompletionSource<WorkLogOperationSaveCpParam[]> _acceptCompletionSource;
   private ControlParam[] _controlParams;
   private List<ControlParamsMenuItem> _controlParamsMenuItems;

   private void Awake(){
      _controlParamsMenuItems = new List<ControlParamsMenuItem>();
      _itemFactory = new MenuItemFactory(menuItemInputPrefab, menuItemStatePrefab, menuItemDropDownPrefab, contentParent);
      acceptButton.onClick.AddListener(OnAcceptButtonClick);
      takePhotoButton.onClick.AddListener(OnTakePhotoClick);
   }

   public async UniTask<WorkLogOperationSaveCpParam[]> InputProcess(ControlParam[] controlParams, bool isPhotoRequired){
      Initialize(controlParams);
      _acceptCompletionSource = new UniTaskCompletionSource<WorkLogOperationSaveCpParam[]>();

      return await _acceptCompletionSource.Task;
   }

   private void Initialize(ControlParam[] controlParams){
      
      _controlParams = controlParams;

      foreach (var controlParam in controlParams){
         var itemMenu = _itemFactory.MenuItemCreate(controlParam.operation_cp_type_code);
         itemMenu.Initialize(controlParam);
         _controlParamsMenuItems.Add(itemMenu);
      }
   }

   public void ParamsListClear(){
      foreach (var item in _controlParamsMenuItems){
         item.Delete();
      }

      _controlParamsMenuItems.Clear();
   }

   private void OnAcceptButtonClick(){
      if (GetCpSaveDataIfDataEntered(out var saveData))
         _acceptCompletionSource.TrySetResult(saveData.ToArray());
   }

   private void OnTakePhotoClick(){
      Hide();
      screenshotWindow.Show();
   }

   private bool GetCpSaveDataIfDataEntered(out List<WorkLogOperationSaveCpParam> saveParams){
      saveParams = new List<WorkLogOperationSaveCpParam>();

      foreach (var controlParamMenuItem in _controlParamsMenuItems){
         if (controlParamMenuItem.IsDataEntered())
            saveParams.Add(controlParamMenuItem.ControlParamSaveData);
         else
            return false;
      }

      return true;
   }
}
