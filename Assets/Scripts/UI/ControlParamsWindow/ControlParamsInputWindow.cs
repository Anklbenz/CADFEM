using Cysharp.Threading.Tasks;
using UnityEngine;
using ThingData;
using UnityEngine.UI;

public class ControlParamsInputWindow : MonoBehaviour {
   [SerializeField] private RectTransform contentParent;
   [SerializeField] private ControlParamsMenuItemInput menuItemInputPrefab;
   [SerializeField] private ControlParamsMenuItemDropDown menuItemDropDownPrefab;
   [SerializeField] private ControlParamsMenuItemState menuItemStatePrefab;
   [SerializeField] private Button acceptButton;

   private MenuItemFactory _itemFactory;
   private UniTaskCompletionSource<ControlParam[]> _acceptCompletionSource;
   private ControlParam[] _controlParams;

   private void Awake(){
      _itemFactory = new MenuItemFactory(menuItemInputPrefab, menuItemStatePrefab, menuItemDropDownPrefab, contentParent);
      acceptButton.onClick.AddListener(OnAcceptButtonClick);
   }

   public async UniTask<ControlParam[]> InputProcess(ControlParam[] controlParams){
      Initialize(controlParams);

      return await _acceptCompletionSource.Task;
   }
   private void OnAcceptButtonClick(){
      foreach (var controlParam in _controlParams)
         if (!controlParam.is_complete)
            break;

      _acceptCompletionSource.TrySetResult(_controlParams);
   }

   private void Initialize(ControlParam[] controlParams){
      _controlParams = controlParams;
      _acceptCompletionSource = new UniTaskCompletionSource<ControlParam[]>();
      
      foreach (var controlParam in controlParams){
         var itemMenu = _itemFactory.MenuItemCreate(controlParam.operation_cp_type_code);
         itemMenu.Initialize(controlParam);
      }
   }
}
