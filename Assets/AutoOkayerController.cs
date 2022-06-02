
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Main {
public class AutoOkayerController : MonoBehaviour
{
    
    private JewerlyControl jewerlyControl;

    public float range = 10.0f;

    public GameObject prefabJewerly;

    NavMeshAgent m_NavMeshAgent;
    ThirdPersonCharacter m_ThirdPersonCharacter;
    Animator m_Animator;
    Rigidbody m_Rigidbody;        

    AddonScript.RandomPointOnNavMesh randompoint;

    public GameObject rule;

    private int collectJaw = 0;


    private void Awake() {
        Main.Global.CollectJewerlyEvent.AddListener(CollectJewerly);
        m_Rigidbody = GetComponent<Rigidbody>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_ThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        randompoint = gameObject.AddComponent<AddonScript.RandomPointOnNavMesh>();
    }


    private void Start() {
        FindJewerly();
    }


    private void CollectJewerly()
    {
        Vector3 point;
        bool result = false;
        while(!result)
        {
            result = randompoint.RandomPoint(transform.position, range, out point);
            if (result) {
                Instantiate(prefabJewerly, point, Quaternion.identity);    
                collectJaw++;
                Global.jewerlyStartCount ++;            
                FindJewerly();
            }
        }
    }


    private void FindJewerly()
    {
        if (collectJaw < 2) {
            jewerlyControl = FindObjectOfType<JewerlyControl>();
            m_NavMeshAgent.SetDestination(jewerlyControl.transform.position); 
        } else {

            int _showRuls = PlayerPrefs.GetInt("showRules");
            if (_showRuls !=5) {
                rule.SetActive(true);
                PlayerPrefs.SetInt("showRules", 5);
            }
        }
    }

    private void OnDestroy() {
         Main.Global.CollectJewerlyEvent.RemoveListener(CollectJewerly);
    }


    // Update is called once per frame
    void Update()
    {        
        if (!Global.deadPlayerStatus && Global.playerStartStatus && jewerlyControl != null)  {            
            m_ThirdPersonCharacter.Move(m_NavMeshAgent.desiredVelocity, false, false);
        }    
    }
    


}
}
