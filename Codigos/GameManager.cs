using Godot;
using System;
using System.Collections.Generic;

public class GameManager : Spatial
{
    public BoardManager _BoardManager;//crep una referencía al board manager
    public int playerFoodPoint = 100;//puntos iniciales de comida de jugador
    public bool playerTurn = true;//si es el turno del jugador
    public float turnDelay = 0.1f;//tiempo que va a tardar cada turno
    private List<KinematicBody> enemies = new List<KinematicBody>();//aqui guardo los enemigos
    private bool enemiesMoving;//si el enemigo se esta moviento
    private int level = 0;//cuenta el nivel en el que estamos lo usamos como parametro de setupScene
    private Label levelText;//referencía al texto del nivel actual
    private Label foodText;//referencía a la comida que posee el jugador
    private ColorRect fondoColorUI;//referencía al fondo

    private bool doingSetup;//es para saber si todavia esta preparandose la escena,osea la UI que aparece en el comienzo

    private Timer timerReiniciarJuego;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
         _BoardManager = (BoardManager)GetTree().GetNodesInGroup("BoardManager")[0];//como el script esta en el nodo lo busco de esta manera
        _BoardManager.SetupScene(1);
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
