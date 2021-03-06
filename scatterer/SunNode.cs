﻿using UnityEngine;
using System.Collections;

namespace scatterer
{
	/*
	 * The SunNode contains the suns direction as well as a few other settings related to the sun.
	 * Binds any uniforms to shaders that require the sun settings.
	 * NOTE - all scenes must contain a SunNode.
	 */
	
	
	public class SunNode : MonoBehaviour 
	{
		Vector3 directionTosun;
		//Dont change this
		static readonly Vector3 Z_AXIS = new Vector3(0,0,1);

		Vector3 m_startSunDirection = Z_AXIS;

		float m_sunIntensity = 100.0f;
		
		Matrix4x4 m_worldToLocalRotation;
		
		public Vector3 GetDirection() {
			return directionTosun.normalized;
		}

		
		//The rotation needed to move the sun direction back to the z axis.
		//The sky shader requires that the sun direction is always at the z axis
		public Matrix4x4 GetWorldToLocalRotation() {
			return m_worldToLocalRotation;
		}
		//The rotation needed to move the sun direction from the z axis 
		public Matrix4x4 GetLocalToWorldRotation() {
			return m_worldToLocalRotation.inverse;
		}
		
		public void Start() 
		{
			//if the sun direction entered is (0,0,0) which is not valid, change to default
			
			
			//if(m_startSunDirection.magnitude < Mathf.Epsilon)
			m_startSunDirection = Z_AXIS;
			
			//Vector3 tmp = m_manager.getDirectionToSun();
			//tmp=tmp.normalized;
			
			//transform.forward = m_startSunDirection;
			
			directionTosun = m_startSunDirection.normalized;
		}
		
		public void SetUniforms(Material mat)
		{
			//Sets uniforms that this or other gameobjects may need
			if(mat == null) return;
			
			mat.SetFloat("_Sun_Intensity", m_sunIntensity);
			mat.SetVector("_Sun_WorldSunDir", GetDirection());
		}

		// Update is called once per frame
		public void UpdateNode() 
		{
			directionTosun=directionTosun.normalized;

			Quaternion q = Quaternion.FromToRotation(GetDirection(), Z_AXIS);
			m_worldToLocalRotation = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
			
		}
		
		public Vector3 getSunNodeDirection()
		{
			return(directionTosun);
		}
		
		public  void setDirectionToSun(Vector3 direction)
		{
			directionTosun=direction.normalized;
		}
		
	}
	
}