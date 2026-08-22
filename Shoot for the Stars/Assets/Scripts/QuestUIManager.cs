using UnityEngine;

public class QuestUIManager : MonoBehaviour{

        public GameObject questListPanel;
       
       
        public void ToggleQuestList(){
            bool isNowActive= !questListPanel.activeSelf;
            questListPanel.SetActive(isNowActive);
       
       Time.timeScale = isNowActive ? 0f : 1f;
       Cursor.lockState = isNowActive ? CursorLockMode.None : CursorLockMode.Locked;
       Cursor.visible = isNowActive;

       Debug.Log("Quest list active" + isNowActive + " | timeScale" + Time.timeScale);
    }}