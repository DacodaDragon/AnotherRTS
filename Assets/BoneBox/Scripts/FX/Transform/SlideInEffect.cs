using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoneBox.Effects
{
	public class SlideInEffect : MonoBehaviour
	{
		private Vector3 m_OriginalPos;

		[SerializeField] private Transform m_StartingPoint;
		[SerializeField] private float m_Speed;

		private void Awake()
		{
			m_OriginalPos = transform.position;
			transform.position = m_StartingPoint.position;
		}

		private void Update()
		{
			if (Vector3.Distance(transform.position, m_OriginalPos) > .01f)
			{
				transform.position += transform.position - m_OriginalPos * .1f * m_Speed * Time.deltaTime;
			}
			else
			{
				transform.position = m_OriginalPos;
			}
		}
	}
}
