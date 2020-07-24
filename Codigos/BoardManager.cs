using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class BoardManager : Spatial
{
    [Export]
    public int columns = 8;//numero de columnas
    [Export]
    public int rows = 8;//numero de filas
    [Export]
    public int foodQueantityMin = 1;//cantidad minima de comida en un escenario
    [Export]
    public int foodQueantityMax = 1;//cantidad minima de comida en un escenario
    [Export]
    public int wallQuantityMin = 5;//cantidad minima de obstaculos en un escenario;
    [Export]
    public int wallQuantityMax = 9;//cantidad máxima de obstaculos en un escenario;
    [Export]
    public int controlDificulty = 1;//esta variable se usa para multiplicar la función logaritmica que instancia cantidad de objetos y de esta forma poder aumentar o bajar la cantidad de enemigos,osea manejar la dificultad

    [Export]
    private Array<PackedScene> floorTiles,outerWallTiles,wallTiles,foodTiles,enemyTiles;//suelos empaquetados para precargar desde el editor

    
    [Export]
    private PackedScene exit;//referencía a la escena que posee el cartel de exit
    private List<Vector3> gridPositions = new List<Vector3>();//esta lista guardara todas las posiciones para poder evitar que se superpongan algunos obstaculos y enemigos
    private Spatial toInstantiate;//referencia a los bloques del suelo que seran instanciados
    private Spatial _Board;//para poner todos los suelos instanciados
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _Board = (Spatial)GetTree().GetNodesInGroup("Board")[0];//busco el nodo para guardar los suelos que van a ser creados
        GD.Randomize();//creo una semilla aleatoria para que la aleatoriedad siempre sea distinta
    }

    private void InitialiceList()//lista que guarda las posiciones
    {
        gridPositions.Clear();//vacia todo el contenido de la lista
        //recorremos con doble bucle para obtener la x y las z y como hicimos antes
        //empezamos por el x= 1 y =1 para dejar un borde
        for(int x = 1; x < columns - 1;x++)
        {
            for(int z = 1; z < rows - 1;z++)
            {
                gridPositions.Add(new Vector3(x,0,z));
            }    
        }
    }
    
    private Vector3 RandomPosition()//para obtener una posición aleatoría
    {
        int RandomIndex = (int)GD.RandRange(0,(double)gridPositions.Count);//tengo el total de elementos de la lista
        Vector3 randomPosition = gridPositions[RandomIndex];//la posicion aleatorio que hemos obtenido
        gridPositions.RemoveAt(RandomIndex);//remueve la posicion que acabamos de obtener para que no aparescan encima del otro
        return randomPosition;//nos aseguramos que no duelva una posición que nos dio antes
    }

    ////////////instanciar muros,personajes,alimentos o objetos TIPO SPATIAL///////////////////////
    private Spatial GetRandomInArray(Array<PackedScene> array)//metodo que devuelve un spatial de suelo aleatoriamente del array que le pasamos por parametro
    {
        return (Spatial)array[(int)GD.RandRange(0,(double)array.Count)].Instance();//devuelve un objeto del array aleatorio
    }

    private void LayoutObjectAtRandom(Array<PackedScene> tileArray,int min ,int max,int altura)//posicionar objeto en lugar aleatorio//array con todos los muros y creame entre una cantidad minima y maxima de muros
    {
        int objectCount = (int)GD.RandRange(min,(double)max + 1);//numero total de objetos que tenemos que crear
        for(int i = 0; i < objectCount ; i ++)
        {
            Vector3 randomPosition = RandomPosition();//posición aleatoria que recibo de la lista de bloques libres
            Spatial tileChoise = GetRandomInArray(tileArray);
            _Board.AddChild(tileChoise);
            tileChoise.Translation = new Vector3(randomPosition.x * 2,altura,randomPosition.z * 2);
            tileChoise.RotationDegrees = new Vector3(0,180,0);
        }
    }

    public void LayoutObjectExit(Vector3 posicion)
    {
        Spatial exitCartel = (Spatial)exit.Instance();//instancía el cartel
        _Board.AddChild(exitCartel);
        //exitCartel.Translation = new Vector3((columns - 1) * 2, 1, (rows -1) * 2);
        exitCartel.Translation = posicion;
        
    }

    ///////////////////////////////Crea el suelo en la escena//////////////////////////////////////
    private void BoardSetup()//metodo para crear el escenario inicial
    {
        for(int x=-1; x<columns + 1 ;x++)//recorremos x
        {
            for(int z=-1; z<rows + 1 ;z++)//recorremos z
            {
                toInstantiate = GetRandomInArray(floorTiles);//devuelve un objeto instanciado
                if(x == -1 || z ==-1 || x == columns || z == rows )//si la posicion pertenece al borde
                {
                   
                    //referencio a un elemento aleatorio del borde
                    toInstantiate = GetRandomInArray(outerWallTiles);//devuelve un objeto isntanciado de tipo sprite toma como parametro un array de packetscene
                    _Board.AddChild(toInstantiate);
                    toInstantiate.Translation = new Vector3(x *2,2,z*2);
                }
                else
                {
                    GD.Print("x: ",x *2," z: ",z*2);
                    if(x== 0 && z ==15)
                    {
                        LayoutObjectExit(new Vector3(x * 2,1,z * 2));//instancio el cartel exit
                        continue;
                    }
                   
                    _Board.AddChild(toInstantiate);
                    toInstantiate.Translation = new Vector3(x *2,0,z*2);
                    //toInstantiate.RotationDegrees = GD.Vector3.zero;
                }
                
            }
        }
    }


    ////////Importante función para iniciar el escenario es llamada desde Game manager//////////////////
    public void SetupScene(int level)
    {
        int enemyCount = 0;//inicializo el contador enemigos
        //enemyCount = 5;
        BoardSetup();//inicio todo el escenario incluyendo paredes y suelo
        InitialiceList();//metemos en la lista todas las posiciones donde pueden aparecer objetos de forma aleatoría
        LayoutObjectAtRandom(enemyTiles,5,5,1);//esto se utiliza para iniciar los enemigos cantodad minima y cantidad maxima
        LayoutObjectAtRandom(wallTiles,5,5,2);//esto lo utilizo par iniciar los cubos
        
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
