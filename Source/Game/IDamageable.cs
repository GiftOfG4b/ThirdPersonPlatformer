using System;
using System.Collections.Generic;
using FlaxEngine;

//namespace Game;

/*
An interface contains definitions for a group of related functionalities that a non-abstract class or a struct must implement. 
An interface may define static methods, which must have an implementation
*/
//basically mini inheritance, also c# can both inherit and interface (inherit class before interface)
//<T>

//public class InterfaceInCSharp : Script, IMyInterface
public interface IDamageable{
    //void Damage(T damageTaken);
    void TakeHit(Vector3 damageDir, Vector3 damagePos);
    void InstaDeath();
}
