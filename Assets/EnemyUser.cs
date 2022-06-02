
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;


namespace Main {
public class EnemyUser : MonoBehaviour {


    public ScriptableAnimationTrigger scriptableAnimationTrigger;

    private PlayerController playerObject;

    NavMeshAgent m_NavMeshAgent;
    ThirdPersonCharacter m_ThirdPersonCharacter;
    Animator m_Animator;
    Rigidbody m_Rigidbody;        

    Animator animatorController;

    private void Awake() {        
        Global.PlayerDeadEvent.AddListener(DeadPlayerResult);
        Main.Global.PlayerGoalEvent.AddListener(PlayerGoal);

        m_NavMeshAgent = GetComponent<NavMeshAgent>();      
        m_Animator = GetComponent<Animator>();
        m_ThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        m_Rigidbody = GetComponent<Rigidbody>();
        playerObject = FindObjectOfType<PlayerController>();
    }

    private void Start() {
        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.0f);  
    }

    void Update() {

        if (!Global.deadPlayerStatus && !Global.winPlayerStatus && Global.playerStartStatus && playerObject != null)  {            
            m_NavMeshAgent.SetDestination(playerObject.transform.position);
            m_ThirdPersonCharacter.Move(m_NavMeshAgent.desiredVelocity, false, false);
        }
    }


    private void PlayerGoal() {       

        m_Animator.SetFloat(scriptableAnimationTrigger.forward, 0.0f); 
        m_Animator.SetTrigger(scriptableAnimationTrigger.deathTrigger);
        m_NavMeshAgent.SetDestination(transform.position); 
        m_NavMeshAgent.enabled = false;          ;              
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;  
    }

    private void DeadPlayerResult() {
        m_ThirdPersonCharacter.Move(Vector3.zero, false,false);
        m_Animator.SetTrigger(scriptableAnimationTrigger.win);
        WinExpode();
    }

    private void WinExpode() {
        float radius = 2.0F;
        float power = 500.0F;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in colliders)
            {                           
                Rigidbody m_Rigidbody = hit.GetComponent<Rigidbody>();
                if (m_Rigidbody != null) {
                    m_Rigidbody.AddExplosionForce(power, transform.position, radius, 3.0F);
                }
            }
    }

    private void OnDestroy() {
        Global.PlayerDeadEvent.RemoveListener(DeadPlayerResult);
        Main.Global.PlayerGoalEvent.RemoveListener(PlayerGoal);
    }

}
}