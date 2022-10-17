using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public ParticleSystem AffectedParticles = null;
    [Range(0.0f, 1.0f)]
    public float ActivationTreshold = 0.002f;
    private Transform m_rTransform = null;
    private ParticleSystem.Particle[] m_rParticlesArray = null;
    private bool m_bWorldPosition = false;
    private float m_fCursorMultiplier = 0.01f;

    void Awake()
    {
        m_rTransform = this.transform;
        Setup();
    }

    private int m_iNumActiveParticles = 0;
    private Vector3 m_vParticlesTarget = Vector3.zero;
    private float m_fCursor = 0.0f;

    void Update()
    {
        if (AffectedParticles != null)
        {
            m_iNumActiveParticles = AffectedParticles.GetParticles(m_rParticlesArray);

            m_vParticlesTarget = m_rTransform.position;
            if (!m_bWorldPosition)
                m_vParticlesTarget -= AffectedParticles.transform.position;

            for (int iParticle = 0; iParticle < m_iNumActiveParticles; iParticle++)
            {
                {
                    m_fCursor -= ActivationTreshold;
                    m_fCursor *= m_fCursorMultiplier;
                    m_rParticlesArray[iParticle].velocity = Vector3.zero;
                    m_rParticlesArray[iParticle].position = Vector3.Lerp(m_rParticlesArray[iParticle].position, m_vParticlesTarget, m_fCursor * m_fCursor);
                }
            }
            AffectedParticles.SetParticles(m_rParticlesArray, m_iNumActiveParticles);
        }
    }

    public void Setup()
    {
        if (AffectedParticles != null)
        {
            m_rParticlesArray = new ParticleSystem.Particle[AffectedParticles.maxParticles];
            m_bWorldPosition = AffectedParticles.simulationSpace == ParticleSystemSimulationSpace.World;
            m_fCursorMultiplier = 1.0f / (1.0f - ActivationTreshold);
        }
    }

    public ParticleSystem particles;


}

