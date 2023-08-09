using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        Elevator elevator = new Elevator();

        // Mostrar o painel de controle do elevador
        for (int floor = 1; floor <= elevator.NumFloors; floor++)
        {
            Console.WriteLine($"|_[{floor}]_|");
        }

        Console.WriteLine("Digite o número do andar e pressione Enter para chamar o elevador (ou 'b' para sair):");

        string input;
        while ((input = Console.ReadLine()) != "b")
        {
            if (int.TryParse(input, out int selectedFloor))
            {
                if (selectedFloor >= 1 && selectedFloor <= elevator.NumFloors)
                {
                    elevator.AddFloorRequest(selectedFloor);
                }
                else
                {
                    Console.WriteLine("Andar inválido. Digite um número de andar válido.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Digite um número de andar válido.");
            }
        }
    }
}

class Elevator
{
    private int currentFloor;
    private bool isMoving;
    private List<int> floorRequests;
    private const int stopDurationInSeconds = 5;

    public int NumFloors { get; }

    public Elevator()
    {
        NumFloors = 10;
        currentFloor = 1;
        isMoving = false;
        floorRequests = new List<int>();
    }

    public void AddFloorRequest(int floor)
    {
        floorRequests.Add(floor);

        if (!isMoving)
        {
            ProcessFloorRequests();
        }
    }

    private void ProcessFloorRequests()
    {
        isMoving = true;

        while (floorRequests.Count > 0)
        {
            int nextFloor = floorRequests[0];
            floorRequests.RemoveAt(0);

            // Mover para o próximo andar
            if (currentFloor < nextFloor)
            {
                MoveUp(nextFloor);
            }
            else if (currentFloor > nextFloor)
            {
                MoveDown(nextFloor);
            }

            // Simular a parada do elevador
            Console.WriteLine($"Elevador parado no andar {currentFloor} por {stopDurationInSeconds} segundos...");
            Thread.Sleep(stopDurationInSeconds * 1000);

            // Atualizar o andar atual
            currentFloor = nextFloor;
            Console.WriteLine($"Elevador chegou ao andar {currentFloor}.");

            // Tocar som de chegada
            Console.Beep();
        }

        // Retornar ao primeiro andar quando não houver mais solicitações
        if (currentFloor != 1)
        {
            MoveDown(1);
            currentFloor = 1;
            Console.WriteLine("Elevador retornou ao primeiro andar.");
        }

        isMoving = false;
    }

    private void MoveUp(int targetFloor)
    {
        Console.WriteLine($"Elevador subindo...");

        while (currentFloor < targetFloor)
        {
            currentFloor++;
            Console.WriteLine($"Elevador está no andar {currentFloor}.");
            Thread.Sleep(1000);
        }
    }

    private void MoveDown(int targetFloor)
    {
        Console.WriteLine($"Elevador descendo...");

        while (currentFloor > targetFloor)
        {
            currentFloor--;
            Console.WriteLine($"Elevador está no andar {currentFloor}.");
            Thread.Sleep(1000);
        }
    }
}
