# unity-walkers

Nahuel Lema
lema.nahuel@gmail.com

## Trabajo Práctico Nº1 (1ºer parcial)

Utilizando Unity, deberá generar un simulador de epidemia Zombie en tiempo real, implementando los algoritmos de Steering Behaviors, State Machines (FSM) y sus conocimiento en OOP.

### Consignas mínimas:
- (X) La simulación deberá contener al menos 30 actores.
- (X) Un 10% de los actores deberá ser Zombie, el 90% restante serán actores sanos.
- (X) Debe haber una diferencia visual significativa entre Zombies y actores sanos (ej: cubos rojos vs cilindros azules).
- (X) Los actores sanos se moverán a una velocidad de 5 y los Zombies a una velodidad de 7.
	- #### Comentario: Cree un metodo para generar una velocidad random entre maximos y minimo al crearse los gameObject
- (0) Los actores sanos en escena deberán implementar una FSM la cual dirigirá las transiciones de estados y actualizaciones de movimientos basados en los steering behaviors. 
- (0) La state machine de los actores sanos deberá tener al menos 2 estados, wanderState: cuando se encuentra caminando hacia algún punto, fleeState: cuando se encuentra huyendo de un Zombie.
	
	- #### Comentario: El codigo esta pero no logre estabilizarlo

- (X) Cada Zombie deberá seleccionar un objetivo sano (aleatoriamente) y perseguirlo mediante el steering behavior "SEEK".

	- #### Comentario: El metodo esta pero implemente otro para buscar al mas cercano, lo hace mas divertido

- (X) Cada actor sano seleccionará un punto al cual se dirigirá utilizando el steering behavior " SEEK ". Si el objetivo se encuentra a menos de 2 unidades, el actor pasará a seguir otro.
- (X) Si un Zombie se acerca a menos de 20 unidades de un actor sano, éste deberá escapar del mismo utilizando el steering behavior "FLEE".
- (X) En caso de que la distancia entre el Zombie y su objetivo sea igual o menor a 2 unidades, el objetivo pasará a convertirse en Zombie. Ambos actores deberán buscar otro objetivo sano.
- (X) La simulación finalizará cuando todos los actores sanos hayan desaparecido.
	- #### Comentario: Los zombies se quedan vagando entre waypoints...

## Consigna extra:
- (X) Si el actor sano se encuentra a menos de 10 unidades de otro actor sano en el momento en que un Zombie lo atrapa, habrá una posibilidad (ej: 50%) de que los humanos empujen al Zombie hacia atrás. 
- (X) El "empuje" constará de un movimiento en el sentido contrario al que el Zombie se estaba moviendo, el cual alejará al Zombie de su objetivo y deberá buscar otro a cual seguir.
- (X) Tengan en cuenta que al momento de empujar al Zombie en la dirección contraria a la que se estaba moviendo, deberán frenar el movimiento natural del mismo.
	- #### Comentario: El metodo Kick y valida que puede hacer 10 patadas
	- #### Comentario: El movimiento no quedo prolijo

## Pautas de evaluación:
- Para obtener la mínima calificación necesaria para aprobar (4), deberá realizar todos los puntos de las "consignas mínimas".
- La calificación máxima por realizar todos los puntos de las consignas mínimas es de 7 puntos. La diferencia entre el 4 y el 7 se verá en la prolijidad del código, de la escena y el correcto uso de los conocimientos en OOP.
Para incrementar la nota deberán incluir el punto de la "consigna extra".
- La fecha de entrega será el día Viernes 12/05.
- El práctico/parcial es individual, sin excepciones.
- La entrega será de forma presencial y deberá contener una carpeta con su Apellido que incluya los siguientes archivos:
	- Un comprimido (.rar) de la carpeta "Assets" de su proyecto.
	- Un archivo de texto (.txt) que incluya su nombre y apellido completo, 	comisión y mail (para la devolución).
- Si el trabajo no cuenta con alguna de las "consignas mínimas" o de las pautas de evaluación, no será aprobado.