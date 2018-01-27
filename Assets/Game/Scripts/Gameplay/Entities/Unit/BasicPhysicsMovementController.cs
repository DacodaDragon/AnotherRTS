using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using AnotherRTS.Management.RemappableInput;

namespace AnotherRTS.Gameplay.Entities.Units
{
	using Camera = UnityEngine.Camera;
	using Logger = BoneBox.Debug.Logger;

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
		[Header("Poking")]
		[SerializeField]
		private float m_PokeStrength = 1.0f;
		[SerializeField] private LayerMask m_PokingMask;

		private InputManager m_InputManager;
		private int m_MiddleMouseID;
		private Camera m_Camera;
		private Collider m_Collider;

		float upTimer = 1;

        public void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            aStar = GetComponent<NavMeshAgent>();
			m_Collider = GetComponent<Collider>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;

            aStar.updateUpAxis = false;
		}

		private  void Start()
		{
			m_InputManager = InputManager.Instance;
			m_Camera = Camera.main;

			m_MiddleMouseID = m_InputManager.GetKeyID("global p");
		}

		public void Update()
        {
            if (isrolling)
            {
                if (rigid.velocity.magnitude < 0.80f)
                {
                    if (transform.up.y < 0.98f)
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

				Diff = Math.Abs(rigid.velocity.magnitude);

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
				Diff = Math.Abs(previousVelocity.magnitude - aStar.velocity.magnitude);

			}

			if (m_InputManager.GetKeyUp(m_MiddleMouseID))
			{
				Vector2 mousePos = Input.mousePosition;
				Ray ray = m_Camera.ScreenPointToRay(mousePos);
				RaycastHit hitInfo;
				bool hitSuccess = Physics.Raycast(ray, out hitInfo, m_Camera.farClipPlane, m_PokingMask);

				if (hitSuccess && hitInfo.collider == m_Collider)
				{
					StartRolling();
					Unlock();
					Vector3 force = (transform.position - hitInfo.point);
					force.y *= -1;
					rigid.AddForceAtPosition(force /*Vector3.up*/ * m_PokeStrength, hitInfo.point, ForceMode.Impulse);
					Logger.Log(this, "poking " + name);
				}
			}
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

