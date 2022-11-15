using System;

namespace Ahorcado
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string randomList = Data();     //Array aleatorio

            string box = Line(randomList);  //Fabrica ---

            ShowStart(box);                 //Muestra menú inicial

            Compare(randomList);            //Arranca programa comparando
        }


        //Leemos por teclado
        static string Input()
        {
            Console.WriteLine("Introduzca una letra");
            string input = Console.ReadLine().ToLower();  //Lo ponemos en minúscula para no tener problema cuando haga la igualdad

            string exception = "";

            try                                           //Probamos si va bien o si da error (catch)
            {                                            
                if (char.IsLetter(input[0]))              //Miramos si es una letra lo introducido
                {
                    return input;                         //Devolvemos lo que introducimos por teclado si es una letra
                }
                throw new Exception("No introduzca ni números ni símbolos"); //Si no es una letra nos da un error no controlado
            }                                                                //que nos sale en el IDE y al poner catch nos sale
                                                                             //en pantalla
            catch (Exception e)                           //Si da error lo metemos en e que luego especificamos con Message
            {
                exception = e.Message;                    //Coge el mensaje de throw y lo metemos en exception para mostrarlo

                ZoneException(exception);                 //Mandamos para mostrar por pantalla

                return "";
            }
        }


        //Listado de palabras
        static readonly string[] ARRAYLIST = new string[] { "HTML", "CSS", "JavaScript", "C Sharp", "PHP" };

        //Listado - Sacar palabra aleatoria
        static string Data()
        {
            //Creamos un random para seleccionar una palabra aleatoria
            Random random = new Random();
            int randomArray = random.Next(ARRAYLIST.Length);       //Saca un número aleatorio de la cantidad/posición de elementos

            string randomList = ARRAYLIST[randomArray].ToLower();  //Metemos dicha posición aleatoria en la lista, para coger la
                                                                   //palabra aleatoria
                                                                   //ToLower() - Lo ponemos para evitar errores de minúscula/mayúscula
                                                                   //con lo introducido
            return randomList;                                     //Sacamos la palabra aleatoria para usarse en otros métodos
        }


        //Crea ----
        static string Line(string randomList)
        {
            string box = "";

            foreach (char line in randomList)   //Recorremos la palabra aleatoria para ponerle ----
            {
                if (line == ' ')                //Si tiene espacio respetar el espacio
                {
                    box += ' ';
                }
                else
                {
                    box += '-';                 //Si no tiene espacio le ponemos a cada carácter una línea (-)
                }
            }
            return box;                         //Devolvemos las líneas --- que obtenemos para usarse en otros métodos
        }


        
        //Comparar palabra introducida con la aleatoria
        static void Compare(string randomList)
        {
            const int ZERO = 0;
            int cont = 8;                          //Inicializamos a 8 para que vaya bajando según vaya fallando
            string container = "";
            string match = "";
            string match2 = "";

            do
            {
                string input = Input();            //Llamamos al método de introducir teclado y lo metemos en uan variable 


                //Obtener almacen de todas las letras introducidas
                if (!container.Contains(input[0])) //Para que no se repita las palabras
                {
                  container += input[0];           //Almacenamos el primer carácter input[0] en el almacen (container)

                  cont = cont;                     //Se guarda el mismo contador para que no varíe y se guarde
                }
                  
                
                //Obtener los carácter que coincidan de la palabra aleatoria
                foreach (char local in randomList)
                {
                    if (local == input[0])          //Si coincide lo introducido input[0] con el carácter del randomList 
                    {
                        if (!match.Contains(local)) //Para que no se repita las palabras
                        {
                            match += local;         //Se mete lo introducido input[0] en match
                        }
                    }
                }

                //Obtener la palabra aleatoria sin que se repita letras
                for (int i = 0; i < randomList.Length; i++)  //Palabra por acertar
                {
                    if (!match2.Contains(randomList[i]))     //Para que no se repita las palabras
                    {
                        match2 += randomList[i];             //Metemos el carácter en match2 sino se repite
                    }
                }

                //Restar contador sino se acierta
                if (!randomList.Contains(input[0]) ) //Si lo introducido no está en la palabra aleatoria nos resta 1
                {
                    cont--;                          // -1 por cada ciclo
                }

                

                WordUsed(randomList, match, container, cont, match2); 

            } 
         
            while (match2.Length != match.Length && cont > ZERO);  //Cuando el número de caracteres sean iguales tanto la aleatoria
                                                                   //como la insertada o el contador sea menor que 0 finaliza el
                                                                   //juego

        }


        //Ordenar alfabéticamente
        static void WordUsed(string randomList, string match, string container, int cont, string match2)
        {
            char aux = ' ';                              //Variables de char para coger la palabra en un momento
            char[] charArray = container.ToCharArray();  //Pasamos el contenedor de todas las palabras en char para poder
                                                         //operar y ordenar

            //Recorremos ambos for en la misma dirección y mismo contenido pero diferente variables [i] y [j] para poder
            //compararlo y ordenarlo
            for (int i = 0; i < container.Length; i++)
            {
                for (int j = 0; j < container.Length; j++)
                {
                    if (charArray[i] < charArray[j])   //Si i es menor que j
                    {
                        aux = charArray[j];            //Metemos la posición de la j en aux
                        charArray[j] = charArray[i];   //Metemos la posición de la i en la posición de j
                        charArray[i] = aux;            //Metemos la aux en la posición de la i 
                    }
                }
            }

            string container2 = new string(charArray); //El resultado de charArray cuando operamos lo pasamos a string de nuevo
                                                       //para poder mostrar

            Uncover(randomList, match, container2, cont, match2);
        }


        //Destapar -a-a
        static void Uncover(string randomList, string match, string container2, int cont, string match2)
        {
            string box2 = "";

            foreach (char line in randomList) //Recorremos la palabra aleatoria
            {
                if (match.Contains(line))     //Comprobamos si cada carácter de la palabra aleatoria está contenida en el almacen
                {                             //donde guardamos las que coinciden
                    box2 += line;             //Si coinciden la destapamos
                }
                else if (line == ' ')         //Si tiene espacio
                {
                    box2 += ' ';              //Respetamos espacios
                }
                else                          //Si no es acertada
                {
                    box2 += '-';              //Mantenemos tapado
                }
            }

            GameStatus(randomList, container2, cont, box2, match2);  
            
        }

        //Enumerado con los estados del juego
        enum Game { Jugando, Ganado, Perdido }  

        //Definir el estado del juego
        static void GameStatus(string randomList, string container2, int cont, string box2, string match2)
        {
            const int ZERO = 0;
            string bigBox = "";
            string bigBox2 = "";
            String win = "";
            String lose = "";
            String gaming = "";



            //Obtener la palabra aleatoria sin que se repita letras
            for (int i = 0; i < randomList.Length; i++)  //Palabra por acertar
            {
                if (!bigBox.Contains(randomList[i]))     //Para que no se repita las palabras
                {
                    bigBox += randomList[i];             //Metemos el carácter en match2 sino se repite
                }
            }

            //Obtener la palabra destapada sin que se repita letras
            for (int i = 0; i < box2.Length; i++) //Palabra destapada
            {
                if (!bigBox2.Contains(box2[i]))   //Para que no se repita las palabras
                {
                    bigBox2 += box2[i];           //Sino estaba se almacena
                }
            }
                    
            //Comparamos si la palabra aleatoria y la destapada (ambas sin repetir palabras) son iguales
            if (bigBox == bigBox2)                        //Si son iguales
            {
                Game winGame = Game.Ganado;               //Metemos el valor de Ganado en winGame
                win = $"¡¡¡Enhorabuena!!! Usted ha {winGame.ToString().ToLower()}. La palabra era: {randomList}";  //Muestra Ganado
            }
            else if (bigBox != bigBox2 && cont <= ZERO)   //Si son diferentes y ha acabado el contador(llegó a 0)
            {
                Game loseGame = Game.Perdido;             //Metemos el valor de Perdido en loseGame
                lose = $"¡Lo siento mucho! Usted ha {loseGame.ToString().ToLower()}. La palabra era: {randomList}";  //Muestra Perdido
            }
            else if (bigBox != bigBox2 && cont > ZERO)    //Si son diferentes y no ha acabado el contador(mayor de 0)
            {                 
                Game gamingGame = Game.Jugando;           //Metemos el valor de Jugando en gamingGame
                gaming = "Usted está " + gamingGame.ToString().ToLower();                                            //Muestra Jugando
            }

            ShowNew(box2, cont, container2, win, lose, gaming);

        }


        //Mostrar menú de inicio
        static void ShowStart(string box) 
        {
            Console.Clear();

            Console.WriteLine($"\"Es un lenguaje o utilidad de programación\"\n");  

            Console.WriteLine($"Palabra a adivinar: {box}");  //Muestra ---

            Console.WriteLine("Intentos restantes: 8");

            Console.WriteLine($"Letras usadas: []");

            Console.WriteLine(new string('-', 1));                                  //Muestra muñeco inicial -
            for (int i = 1; i <= 5; i++)                                            //Número de filas
            {
               Console.WriteLine(new string('|', 1));                               //Palo | por 5 veces hacia abajo
            }
            Console.WriteLine(new string('-', 3));                                  //Base ---

            Console.WriteLine($"Usted está { Game.Jugando.ToString().ToLower() }"); //Muestra Está usted Jugando
        }


        //Mostrar en ejecución
        static void ShowNew(string box2, int cont, string container2, string win, string lose, string gaming) 
        {
            Console.Clear();

            Console.WriteLine($"\"Es un lenguaje o utilidad de programación\"\n");

            Console.WriteLine($"Palabra a adivinar: {box2}");       //Muestra ---

            Console.WriteLine($"Intentos restantes: [{cont}]");     //Muestra [cont] / [8]... [7]...[0]

            Console.WriteLine($"Letras usadas: [{container2}]");    //Muestra palabras usadas en orden alfabético [abcdef...]

            switch (cont)                                           //Muestra muñeco según errores/cont obtenidos
            {
                case 8:                                             //Muñeco inicial sin errores/cont = 8
                    Console.WriteLine(new string('-', 1));          //Parte superior -
                    for (int i = 1; i <= 5; i++)                    //Número de filas
                    {
                        Console.WriteLine(new string('|', 1));      //Palo | por 5 veces hacia abajo
                    }
                    Console.WriteLine(new string('-', 3));          //Base ---
                    break;
                case 7:
                    Console.WriteLine(new string('-', 6));          //Parte superior ------
                    for (int i = 1; i <= 5; i++)
                    {
                        Console.WriteLine(new string('|', 1));      //Palo | por 5 veces hacia abajo 
                    }
                    Console.WriteLine(new string('-', 3));          //Base ---
                    break;
                case 6:
                    Console.WriteLine(new string('-', 6));                                           //Parte superior ------ 
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    |
                    for (int i = 1; i <= 4; i++)                                                     //Quitamos 1 porque hemos
                    {                                                                                //aumentado un console con
                                                                                                     //otro dibujo, la cuerda

                        Console.WriteLine(new string('|', 1));                                       //Palo | por 4 veces hacia
                                                                                                     //abajo
                    }
                    Console.WriteLine(new string('-', 3));                                           //Base ---
                    break;
                case 5:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    |
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |    O
                    for (int i = 1; i <= 3; i++)                //Quitamos otra fila por la cabeza
                    {
                        Console.WriteLine(new string('|', 1));
                    }
                    Console.WriteLine(new string('-', 3));
                    break;
                case 4:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    |
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |    O
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + Cuerpo |   |
                    for (int i = 1; i <= 2; i++)              //Quitamos otra fila por el cuerpo
                    {
                        Console.WriteLine(new string('|', 1));
                    }
                    Console.WriteLine(new string('-', 3));
                    break;
                case 3:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    | 
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |     O
                                                                                    //Palo + brazo izquiero + cuerpo |   /|
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1) + new string('|', 1));
                    for (int i = 1; i <= 2; i++)//Quitamos otra fila por el brazo izquierdo     
                    {
                        Console.WriteLine(new string('|', 1));
                    }
                    Console.WriteLine(new string('-', 3));
                    break;
                case 2:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    | 
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |     O
                                                                    //Palo + brazo izquiero + cuerpo + brazo derecho |   /|\
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1) + new string('|', 1) + new string('\\', 1));
                    for (int i = 1; i <= 2; i++)//Quitamos otra fila por el brazo derecho
                    {
                        Console.WriteLine(new string('|', 1));
                    }
                    Console.WriteLine(new string('-', 3));
                    break;
                case 1:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    | 
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |     O
                                                                    //Palo + brazo izquiero + cuerpo + brazo derecho |   /|\
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1) + new string('|', 1) + new string('\\', 1));
                                                                                            //Palo + pierna izquiero |   /
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1)); //Quitamos el for para mostrar la pierna izquierda
                    Console.WriteLine(new string('|', 1));
                    Console.WriteLine(new string('-', 3));
                    break;
                case 0:
                    Console.WriteLine(new string('-', 6));
                    Console.WriteLine(new string('|', 1) + new string(' ', 4) + new string('|', 1)); //Palo + cuerda |    |
                    Console.WriteLine(new string('|', 1) + new string(' ', 5) + new string('O', 1)); //Palo + Cabeza |     O
                                                                    //Palo + brazo izquiero + cuerpo + brazo derecho |   /|\
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1) + new string('|', 1) + new string('\\', 1));
                                                                                            //Palo + pierna izquiero |   /\
                    Console.WriteLine(new string('|', 1) + new string(' ', 3) + new string('/', 1) + new string('\\', 1)); //Mostramos la pierna derecha
                    Console.WriteLine(new string('|', 1));
                    Console.WriteLine(new string('-', 3));
                    break;
                default:
                    break;
            }
            //Muestra el estado del juego
            Console.WriteLine($"{gaming}{win}{lose}"); //Jugando/Ganado/Perdido
        }

        //Muestra si se mete un número o símbolo en lugar de letra
        static void ZoneException(string exception) //Muestra el error al introducir los datos erróneos
        {
            Console.WriteLine(exception);           //Nos sale el mensaje del throw -> No introduzca ni números ni símbolos
        }

    }
}