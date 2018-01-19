using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace AnotherRTS.Gameplay.Entities.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicPhysicsMovementController : MonoBehaviour, IMovementController
    {
        NavMeshAgent aStar;
        Rigidbody rigid;
        public Vector3 previousVelocity;
        public bool isrolling = false;
        public float Diff = 0;
        public Vector3 target;
        public Coroutine routine;

        float upTimer = 1;

        public void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            aStar = GetComponent<NavMeshAgent>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;

            aStar.updateUpAxis = false;
        }

        public void Start()
        {

        }

        public void Update()
        {
            if (isrolling)
            {
                if (rigid.velocity.magnitude < 0.75f)
                {
                    if (transform.up.y < 0.8f)
                    {
                        if (upTimer > 0)
                            upTimer -= Time.deltaTime;
                        else if (routine == null)
                            routine = StartCoroutine(PutUpright());
                    }
                    else if (routine == null)
                    {
                        StopRolling();
                    }
                }
                else upTimer = 1f;
            }
            else
            {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 0.5f, -(transform.up), out hit))
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
                }

                if (Math.Abs(previousVelocity.magnitude - aStar.velocity.magnitude) > 10f)
                {
                    StartRolling();
                }
                previousVelocity = aStar.velocity;

            }
            Diff = Math.Abs(previousVelocity.magnitude - aStar.velocity.magnitude);
        }



        private void StartRolling()
        {
            rigid.constraints = RigidbodyConstraints.None;
            rigid.velocity = previousVelocity;
            aStar.isStopped = true;
            aStar.updateRotation = false;
            aStar.updatePosition = false;
            //aStar.updateUpAxis = false;
            isrolling = true;
        }

        public void Lock()
        {
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            rigid.velocity = Vector3.zero;
        }

        public void Unlock()
        {
            rigid.constraints = RigidbodyConstraints.None;
            rigid.velocity = Vector3.zero;
        }

        private void StopRolling()
        {
            aStar.isStopped = false;
            isrolling = false;
            aStar.velocity = Vector3.zero;
            previousVelocity = Vector3.zero;
            aStar.Warp(transform.position);
            MoveTowards(target);
            aStar.updateRotation = true;
            aStar.updatePosition = true;
            //aStar.updateUpAxis = true;

            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void Follow(Transform target)
        {
            throw new NotImplementedException();
        }

        public void MoveTowards(Vector3 target)
        {
            NavMeshPath path = new NavMeshPath();
            aStar.CalculatePath(target, path);
            aStar.SetPath(path);
            this.target = target;
        }

        public void MoveTowards(Transform target)
        {
            aStar.SetDestination(target.position);
        }

        public bool HasReachedTarget()
        {
            return (aStar.remainingDistance <= aStar.stoppingDistance);
        }

        private IEnumerator PutUpright()
        {
            Lock();
            StartCoroutine(LiftUp(0.5f));
            yield return new WaitForSeconds(1f);
            StartCoroutine(RotateUp(0.5f));
            yield return new WaitForSeconds(1f);
            Unlock();
            yield return new WaitForSeconds(1f);
            routine = null;
        }

        private IEnumerator LiftUp(float length)
        {
            Vector3 startpos = transform.position;
            Vector3 endpos = startpos + (Vector3.up * 4);
            float StartTime = Time.time;

            while (Time.time < StartTime + length)
            {
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.Slerp(startpos, endpos, (Time.time - StartTime) / length);
            }
        }

        private IEnumerator RotateUp(float length)
        {
            float y = transform.eulerAngles.y;
            float StartTime = Time.time;

            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0, y, 0);

            while (Time.time < StartTime + length)
            {
                yield return new WaitForEndOfFrame();
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, (Time.time - StartTime) / length);
            }
        }
    }
}

