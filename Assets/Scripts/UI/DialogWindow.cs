using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Window {
   private const string CONFIRM_MSG = "Подтвердите действие";

   [SerializeField] private TMP_Text headerText, contentText;
   [SerializeField] private Button acceptButton, cancelButton;
   private UniTaskCompletionSource<bool> _dialogResultCompletionSource;

   private void OnEnable(){
      acceptButton.onClick.AddListener(SetAcceptResultOnClick);
      cancelButton.onClick.AddListener(SetCancelResultOnClick);
   }

   public async UniTask<bool> ShowConfirmProcess(string msg, string windowLabel = CONFIRM_MSG){
      Show();

      _dialogResultCompletionSource = new UniTaskCompletionSource<bool>();
      headerText.text = windowLabel;
      contentText.text = msg;
      var result = await _dialogResultCompletionSource.Task;

      Hide();
      return result;
   }

   private void SetAcceptResultOnClick() => _dialogResultCompletionSource.TrySetResult(true);

   private void SetCancelResultOnClick() => _dialogResultCompletionSource.TrySetResult(false);

}
