using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }          

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    private void OnParticleCollision(GameObject other)
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
        Debug.Log("sada");
    }
    //private void OnParticleCollisionEnter()
    //{
    //    Debug.Log("sada");
    //}

    void OnParticleTrigger()
    {
        Debug.Log("khg");
        ;

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        //int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    //// Функция обратного вызова для столкновения частиц
    //private void OnParticleCollision(GameObject other)
    //{
    //    print(other.name);
    //}
    //// Функция обратного вызова, запускаемая частицами
    //private void OnParticleTrigger()
    //{
    //    // Пока установлен триггер системы частиц, программа будет продолжать печать после запуска
    //    //print("Сработал");

    //    // Официальный пример для иллюстрации
    //    ParticleSystem ps = transform.GetComponent<ParticleSystem>();

    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    //    // ParticleSystemTriggerEventType - это перечисляемый тип, Enter, Exit, Inside, Outside, соответствующий четырем параметрам на панели свойств системы частиц
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //    // Входим в триггер, частицы становятся красными
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        print(enter[i]);
    //        ParticleSystem.Particle p = enter[i];
    //        p.startColor = Color.red;
    //        enter[i] = p;
    //    }
    //    // Выходим из триггера, частицы становятся сине-зелеными
    //    for (int i = 0; i < numExit; i++)
    //    {
    //        ParticleSystem.Particle p = exit[i];
    //        p.startColor = Color.cyan;
    //        exit[i] = p;
    //    }

    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //}

}
















    //[SerializeField ] private ParticleSystem ps;

    //private void Start()
    //{
    //    ps=GetComponent<ParticleSystem>();

    //}

    //void GetParticleTrigger()
    //{


    //    // particles
    //    List<ParticleSystem.Particle>  inside = new ();
    //    List<ParticleSystem.Particle> exit = new ();

    //    // get
    //    int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);
    //    int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    //    // iterate
    //    for (int i = 0; i < numInside; i++)
    //    {
    //        ParticleSystem.Particle p = inside[i];
    //        if (insideData.GetColliderCount(i) == 1)
    //        {
    //            if (insideData.GetCollider(i, 0) == ps.trigger.GetCollider(0))
    //                p.startColor = new Color32(255, 0, 0, 255);
    //            else
    //                p.startColor = new Color32(0, 0, 255, 255);
    //        }
    //        else if (insideData.GetColliderCount(i) == 2)
    //        {
    //            p.startColor = new Color32(0, 255, 0, 255);
    //        }
    //        inside[i] = p;
    //    }
    //    for (int i = 0; i < numExit; i++)
    //    {
    //        ParticleSystem.Particle p = exit[i];
    //        p.startColor = new Color32(1, 1, 1, 255);
    //        exit[i] = p;
    //    }

    //    // set
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //}


    //void OnParticleTrigger()
    //{
    //    ParticleSystem ps = GetComponent<ParticleSystem>();

    //    Debug.Log()

    //    // particles
    //    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    //    // get
    //    int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    //    // iterate
    //    for (int i = 0; i < numEnter; i++)
    //    {
    //        ParticleSystem.Particle p = enter[i];
    //        p.startColor = new Color32(255, 0, 0, 255);
    //        enter[i] = p;
    //    }
    //    for (int i = 0; i < numExit; i++)
    //    {
    //        ParticleSystem.Particle p = exit[i];
    //        p.startColor = new Color32(0, 255, 0, 255);
    //        exit[i] = p;
    //    }

    //    // set
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //    ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    //}
