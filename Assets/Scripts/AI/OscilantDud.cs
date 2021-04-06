using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OscilantDud : MonoBehaviour, IDamageable
{
    [Header("Behavior")]
    private int state = 1;
    public float idleTime = 2;
    public float lungePrepTime = 1;
    public GameObject deathVFX;

    [Header("Atributes")]
    public int hpMax = 3;
    [SerializeField]
    private int hp;

    [Header("Attack")]
    public float detectionRadius;
    public CircleCollider2D detectionTrigger;
    public float lungeForce;
    public int damage;
    public float knockback;

    [Header("Movement")]
    public Vector2 moveDirection;
    public float speed;

        void Start()
        {
            hp = hpMax;
            detectionTrigger.radius = detectionRadius;
        }

        IEnumerator ReadyLunge(GameObject target)
        {
            state = 2;
            GetComponent<Animator>().SetInteger("state", 2);
            yield return new WaitForSeconds(lungePrepTime);
            Lunge(target);
        }

        private void Lunge(GameObject target)
        {
            state = 3;
            GetComponent<Animator>().SetInteger("state", 3);

            //Get difference between self and target positions
            Vector3 difference = new Vector3(target.transform.position.x, target.transform.position.y + 1) - transform.position;
        
            //Aim self rotation at target position
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            //Gets direction values for velocity
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            float lungeForceVariance = lungeForce * Random.Range(0.8f, 1.2f);

            //Applies lunge to direction of target
            GetComponent<Rigidbody2D>().velocity = direction * lungeForceVariance;

        }

        IEnumerator ReadyIdle()
        {
            state = 0;
            GetComponent<Animator>().SetInteger("state", 0);

            yield return new WaitForSeconds(idleTime);

            state = 1;
            GetComponent<Animator>().SetInteger("state", 1);
        }

        public void ForceIdle()
        {
            StopAllCoroutines();
            StartCoroutine(ReadyIdle());
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && state == 1)
            {
                StopAllCoroutines();
                StartCoroutine(ReadyLunge(collision.gameObject));
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(state == 1) moveDirection *= -1;

            if (state == 3) StartCoroutine(ReadyIdle());

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && collision.otherCollider.gameObject.CompareTag("Attack"))
            {
                collision.gameObject.GetComponent<Player>().Damage(damage, knockback, transform);

            }

        }

        private void FixedUpdate()
        {
            //Constantly
            if (state ==1) Creature.Move(moveDirection, speed, gameObject);

        }

    void Death()
    {
        state = 5;
        StopAllCoroutines();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().SetBool("death", true);

        Instantiate(deathVFX, transform);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

        void IDamageable.Damage(int iHpLoss, float iKnockback, Transform iSourcePosition)
        {
            hp -= iHpLoss;
            Mathf.Clamp(hp, 0, hpMax);
            if (hp <= 0) Death();
        }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 0, 0, 255);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
