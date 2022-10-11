using UnityEngine;
using ProjectRevolt.Movement;
using ProjectRevolt.Combat;
using ProjectRevolt.Core;

namespace ProjectRevolt.Control 
{
    public class AIController : MonoBehaviour
    {
        //variables
        [SerializeField] private float chaseDistance;

        //components
        private Fighter fighter;
        private Mover mover;
        private Health health;

        //game objects
        private GameObject player;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                Debug.Log(this.name + " should give chase.");
                fighter.Attack(player);
                //add chase behaviour here
            }
        }

        //behaviour methods

        //guard behaviour

        //patrol behaviour

        //suspicion behaviour

        //chase behaviour

        //attack/aggression behaviour

        //calculations
        private bool InAttackRangeOfPlayer() 
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            
        }
    }

}