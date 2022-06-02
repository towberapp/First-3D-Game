using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


namespace Main {
public class RunButtonScript : MonoBehaviour
{
   
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject _loseSprite;
    [SerializeField] GameObject _winSprite;
    [SerializeField] GameObject _selectMenu;
    [SerializeField] GameObject _newRec;

    [SerializeField] GameObject _fadeInPanel;
    [SerializeField] GameObject _fadeOutPanel;

    [SerializeField] GameObject _goToMainMenu;
    [SerializeField] GameObject _addRewardBlock;

    [SerializeField] GameObject _rewardOkText;

    public static UnityEvent ShowRewardAdsEvent = new UnityEvent();

    public static UnityEvent RemoveBannerEvent = new UnityEvent();

    Button startButton;


    public void ClickMainMenu () {
        _fadeOutPanel.SetActive(true);
        Invoke("LoadMainMenu", 1.0f);
        RemoveBannerEvent.Invoke();
    }

    public void ClickAgane () {
        
        _fadeOutPanel.SetActive(true);
        Invoke("ReloadScene", 1.0f);
        RemoveBannerEvent.Invoke();
    }

    public void ShoAdsReward() {
        ShowRewardAdsEvent.Invoke();
        //_goToMainMenu.SetActive(false);
    }


    private void Awake() {
        Main.Global.PlayerDeadEvent.AddListener(LoosGame);
        Main.Global.PlayerGoalEvent.AddListener(WinGame);   
        Main.Global.NewRecod.AddListener(NewRecShow);   
        AdMob.AdsSeenDone.AddListener(AdsSeenOk);

        startButton = GetComponent<Button>();   
        startButton.onClick.AddListener(StartGame);   

        _fadeInPanel.SetActive(true);
        _selectMenu.SetActive(false);      
        _fadeOutPanel.SetActive(false);   
        _goToMainMenu.SetActive(true);
        _addRewardBlock.SetActive(true);
        _rewardOkText.SetActive(false);
    }
   
    private void Start() {
        Invoke("HideFadeInPanel", 1.0f);
        ShowMenu();          
    }

    private void AdsSeenOk() {
        Debug.LogWarning("ADS OK!!!!!!");
        _addRewardBlock.SetActive(false);
        _goToMainMenu.SetActive(false);
        _rewardOkText.SetActive(true);
        PlayerPrefs.SetInt("speed", 1);
    }

    private void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void HideFadeInPanel() {
        _fadeInPanel.SetActive(false);
    }

    private void ReloadScene() {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void StartGame() {
        Main.Global.playerStartStatus = true;
        Main.Global.PlayerStartEvent.Invoke();
        particle.Play();
        HideMenu();
    }

    private void LoosGame() {
        _loseSprite.SetActive(true);
        _selectMenu.SetActive(true);    
    }

    private void WinGame()
    {        
        _winSprite.SetActive(true);
        _selectMenu.SetActive(true);
    }

    private void NewRecShow() {
        _newRec.SetActive(true);
    }

    private void ShowMenu() {
        _loseSprite.SetActive(false);
        _winSprite.SetActive(false); 
        _selectMenu.SetActive(false);
        _newRec.SetActive(false);
        gameObject.SetActive(true);
    }

    private void HideMenu() {
        gameObject.SetActive(false);
    }


    private void OnDestroy() {
        Main.Global.PlayerDeadEvent.RemoveListener(LoosGame);
        Main.Global.PlayerGoalEvent.RemoveListener(WinGame);
        Main.Global.NewRecod.RemoveListener(NewRecShow);  
        AdMob.AdsSeenDone.RemoveListener(AdsSeenOk);
    }


}
}