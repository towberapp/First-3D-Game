using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


namespace Main {

public class PlayerController : MonoBehaviour
{
    // Global
    public ScriptableAnimationTrigger scriptableAnimationTrigger;

    public Camera cam;

    public GameObject bloodAnimation;

    public GameObject _simbolPlayer;


    // Component
    Rigidbody m_Rigidbody;
    NavMeshAgent m_NavMeshAgent;
    Animator m_Animator;
    ThirdPersonCharacter m_ThirdPersonCharacter;


    private void Awake() {

        Global.ClickOnMapEvent.AddListener(ClickOnMapEventResult);
        Global.PlayerGoalEvent.AddListener(PlayerGoal);
        Global.PlayerStartEvent.AddListener(PlayerStart);

        m_Rigidbody = GetComponent<Rigidbody>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_ThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();

        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.0f);  
        m_Animator.SetTrigger(scriptableAnimationTrigger.win);  
        _simbolPlayer.SetActive(true);
        

    }

    private void Start() {
       // LoadPlayerPos();

       int speedPref = PlayerPrefs.GetInt("speed"); 
       if (speedPref > 0) {
            m_NavMeshAgent.speed = 3.5f;
            PlayerPrefs.SetInt("speed", 0);

       } else {
           m_NavMeshAgent.speed = 2.7f;
       }    
    }
    
    private void LateUpdate() {
    }

    void Update()
    {   
        if (!Global.deadPlayerStatus && Global.playerStartStatus)  {
            if (m_NavMeshAgent.remainingDistance > m_NavMeshAgent.stoppingDistance) {
                m_ThirdPersonCharacter.Move(m_NavMeshAgent.desiredVelocity, false, false);
            } else {
                m_ThirdPersonCharacter.Move(Vector3.zero, false,false);
            }
        }    
    }

    private void ClickOnMapEventResult(Vector3 position) {
        if (!Global.deadPlayerStatus && !Global.winPlayerStatus)  {
            m_NavMeshAgent.SetDestination(position);
        }
    }


    #region PRIVATE METHOD 

    private void PlayerStart() {
        //Debug.LogWarning("PLAYER START");
        m_Animator.SetBool(scriptableAnimationTrigger.win, false);  
        _simbolPlayer.SetActive(false);
    }

    private void PlayerGoal() {
        GetComponent<Collider>().enabled = false;
        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.0f);        
        m_Animator.SetTrigger(scriptableAnimationTrigger.win);        
    }


    private void OnTriggerEnter(Collider other) {

        //Debug.LogWarning("TRIGGER: " + other.tag);
       
        if (other.CompareTag("Finish") &&  
            Global.deadPlayerStatus == false && 
            Global.winPlayerStatus == false && 
            Global.playerCollectAllEvent) {
                Global.winPlayerStatus = true;
                Global.playerStartStatus = false;
                Global.PlayerGoalEvent.Invoke();                  
        }

        if (other.CompareTag("Enemy") && Global.deadPlayerStatus == false && Global.winPlayerStatus == false) {
            Global.deadPlayerStatus = true;
            Global.winPlayerStatus = false;
            Global.playerStartStatus = false;
            Global.PlayerDeadEvent.Invoke();
            DeadPlayerAnimation();
        }


        if (other.CompareTag("LevelPlatform")) {
            SavePlayerPos();
            //Debug.LogWarning("PLAYER CONTROLLER -> SAVE POS");
        }
    }


    private void OnCollisionEnter(Collision other) {

        //Debug.LogWarning("Collision: " + other.collider);

        Collider tag = other.collider;
        if (tag.CompareTag("Enemy") && Global.deadPlayerStatus == false && Global.winPlayerStatus == false) {
            Global.deadPlayerStatus = true;
            Global.winPlayerStatus = false;
            Global.playerStartStatus = false;
            Global.PlayerDeadEvent.Invoke();
            DeadPlayerAnimation();
        }
    }


    private void SavePlayerPos() {
         PlayerPrefs.SetInt("savepos", 1);
        //  PlayerPrefs.SetFloat("x", transform.position.x);
        //  PlayerPrefs.SetFloat("y", transform.position.y);
        //  PlayerPrefs.SetFloat("z", transform.position.z);

         PlayerPrefs.SetFloat("x", 9.0f);
         PlayerPrefs.SetFloat("y", 1.5f);
         PlayerPrefs.SetFloat("z", 6.0f);

        // Debug.LogWarning("PLAYER POS SAVE: "+ transform.position.x+ "," + transform.position.y+ "," +transform.position.z );
    }
    

    // private void LoadPlayerPos() {
    //     int savepos = PlayerPrefs.GetInt("savepos");    
    //     if (savepos == 1 && Global.levelId == "MainMenu") {
    //         //Debug.LogWarning("LOAD POSITION");
    //         Vector3 posVec = new Vector3(PlayerPrefs.GetFloat("x"),PlayerPrefs.GetFloat("y"),PlayerPrefs.GetFloat("z"));
    //         //Debug.LogWarning("PLAYER POS LOAD: "+ PlayerPrefs.GetFloat("x")+ "," + PlayerPrefs.GetFloat("y")+"," +PlayerPrefs.GetFloat("z") );
    //         transform.position = posVec;
    //     }        
    // }





    private void DeadPlayerAnimation() {     
        bloodAnimation.GetComponent<ParticleSystem>().Play();
        m_NavMeshAgent.speed = 1f;           
        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.5f);  
        Invoke("InvokePlayerDeath", 0.3f);                                            
    }


    private void InvokePlayerDeath() {    
        m_NavMeshAgent.SetDestination(transform.position); 
        m_NavMeshAgent.enabled = false;    
        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.0f);        
        m_Animator.SetTrigger(scriptableAnimationTrigger.deathTrigger);                     
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;       
    }

    #endregion

    private void OnDestroy() {
        Global.ClickOnMapEvent.RemoveListener(ClickOnMapEventResult);
        Main.Global.PlayerGoalEvent.RemoveListener(PlayerGoal);
        Global.PlayerStartEvent.RemoveListener(PlayerStart);
    }

 }

}

    
 


