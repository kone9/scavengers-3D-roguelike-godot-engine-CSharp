using Godot;
using System;

public class MovingObject : Spatial
{
    public float moveTime = 0.1f;//tiempo en moverse de una casilla a otra
    public float MovementSpeed;//velocidad a la que se movera un objeto para pasar de casillas
    public int blokingLayer;//referenncía a la capa activa se representa por z-index
    public CollisionShape BoxCollider;
    public RayCast rayo;
    public GameManager _GameManager;//referencía al GameManager con las opciones globales
    public bool enable;//para saber si el personaje puede moverse
    public Tween moverConTween;//referencía a un nodo tween

    protected void SmoothMovementWithTween(Vector3 end)//esta función va a procesar el movimiento
    {
        moverConTween.InterpolateProperty(
            this,//el objeto donde creo la interpolación
            "Translation",//la propiedad que quiero interpolar
            this.Translation,//posición inicial
            end,//posición final
            moveTime,//tiempo para ir de una posición a otra
            Tween.TransitionType.Linear,//tipo de interpolación
            Tween.EaseType.InOut//como termina la transición 
        );
        moverConTween.Start();//inicializo el tween
    }

    protected bool Move(int xDir,int yDir,RayCast hitRaycast)
    {
        return true;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
