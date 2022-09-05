# Moodle-RPG

## Resumen (Español)
Moodle RPG es un videojuego ideado para implementarse en la plataforma educativa Moodle, y que se conecta con su base de datos para premiar con monedas de juego a los estudiantes por sus calificaciones en las diferentes asignaturas en las que están matriculados. Usando estas monedas, se pueden comprar espadas, escudos y pociones dentro del juego para avanzar en la aventura, que consiste de combates por turnos en los que el estudiante deberá usar estos objetos de forma estratégica para vencer.

En cualquier momento del juego, los estudiantes podrán personalizar su propio avatar añadiendo un peinado, ropa, accesorios y rasgos faciales a su gusto con un completo editor. Según juegan, usan ataques y derrotan enemigos, se van ganando puntos, los cuales determinarán su posición en el ranking público que se podrá consultar desde el juego en cualquier momento. Este ranking anima a los estudiantes a seguir jugando e incrementar su puntuación, no sólo siendo estratégicos a la hora de atacar enemigos, sino también rindiendo en el aula y completando exitosamente trabajos y exámenes para conseguir más monedas de juego.

A mayor puntuación, además, mayor será el rango de objetos que se podrán adquirir con monedas de juego. Estos objetos pueden adquirirse en la forja (espadas y escudos) o en la tienda (pociones). Las espadas se emplean para atacar enemigos, los escudos para defenderse de sus ataques, y las pociones para ofrecer otras ventajas a los jugadores. Cada avatar cuenta con unas estadísticas de vida, ataque y defensa. La vida se incrementará en 5 cada vez que el jugador sume 1000 puntos, y el ataque o defensa se incrementarán en 1, a elección del estudiante, cada 500 puntos.

Finalmente, en el modo aventura cuyo objetivo es derrotar enemigos para sumar puntos, los jugadores podrán usar hasta dos de sus objetos por turno. En el primer movimiento, pueden atacar o defenderse del posterior ataque enemigo, y en el segundo movimiento podrán únicamente atacar con su arma de elección. En cualquier momento, además, se podrán usar pociones para incrementar cualquiera de sus estadísticas temporalmente. Una batalla se considera ganada si el estudiante derrota a todos los enemigos presentes. Sin embargo, si el jugador pierde toda su vida, pasará al estado "muerto", y será revivido tras 12 horas de tiempo real, en las cuales no podrá jugar y deberá esperar.

El objetivo del juego es, por tanto, motivar al estudiante a rendir más en el aula para poder avanzar más dentro del juego, que le ofrece recursos limitados y un sistema de espera tras la derrota para evitar los tiempos excesivos de juego.

## Summary (English)
Moodle RPG is a videogame designed to be implemented in the Moodle learning platform. It connects to its database in order to reward students for their grades with in-game currency. Using these coins, students can purchase swords, shields and potion to be used in the game's adventure, which consists of turn-based battles where students will need to strategically use these items in order to win.

At any point during gameplay, students can freely customize their avatar thanks to the character editor. As they play, perform attacks and defeat enemies, students will earn points, which will determine their position in the public ranking which can be seen from the game itself. This ranking aims to encourage students to keep playing not only by coming up with strategies during battles, but also by being more productive in real-life classes and successfully submitting exams and homeworks in order to get more in-game coins.

The higher the score, the wider the range of objects that can be purchased with in-game currency. These objects can be acquired in the forge (swords and shields) or the shop (potions). Each avatar also comes with its own health, attack and defense stats, which can be upgraded as students earn points.

During battles, players can use up to two objects per turn. In the first move, they can attack or defend themselves from the upcoming enemy attack and, in the second move, they will only be able to attack with their sword of choice. At any time, potions can be used to temporarily increase any of the avatar's stats. A battle is won if the student manages to defeat all of the enemies. However, if the player's health stat reaches zero, they will become "dead", and will be revived after 12 hours in real time, during which they won't be able to play.

The objective of this project is developing a videogame that encourages students to improve their performance in class as they advance in the game, which offers them limited resources alongside a wait system after defeat to avoid excessive play times.

## Proceso de instalación
### 1. Configuración del servidor

Moodle requiere que se tenga un servidor web en funcionamiento, por lo que tendremos que realizar este paso en primer lugar. Durante la instalación del servidor, se deberán instalar también **Apache, MySQL y PHP**. Por ello, se recomienda usar un software auxiliar como **XAMPP** o **AppServ** (solo Windows), que instalarán estos tres componentes de forma automática y, además, los ejecutarán.

- Apache y MySQL se instalarán en los **puertos** por defecto **80 y 3306**, respectivamente. Esto se puede cambiar en los ficheros de configuración de ambos programas.
- La instalación de ambos softwares, además, incluyen **PHPmyAdmin**, para la gestión manual de la base de datos desde un navegador.

### 2. Creación de la base de datos
A continuación, tendremos que crear desde PHPmyAdmin la base de datos que usará Moodle. Para ello, nos aseguramos que Xampp o AppServ tengan MySQL en ejecución, y desde cualquier navegador web accedemos a la URL *localhost/phpmyadmin*.
- Por defecto, si se ha usado Xampp, el usuario para acceder a PHPmyAdmin será *root*, dejando la contraseña vacía. Sin embargo, si se ha usado AppServ, estas credenciales fueron configuradas durante el proceso de instalación.
- Para crear una base de datos nueva en PHPmyAdmin, pinchamos sobre el botón *Nueva* en la parte superior de la columna izquierda.
- La instalación de Moodle nos solicitará añadir un usuario a la base de datos. Para ello, una vez en la base de datos, pinchamos sobre *Privilegios* en la barra superior y, posteriormente, sobre *Agregar cuenta de usuario*.

### 3. Instalación de Moodle
Una vez creada la base de datos, y estando al menos Apache y MySQL en ejecución, debemos descargar la última versión de **Moodle**.
- Este fichero comprimido descargado contiene una única carpeta, de nombre *moodle*, que deberá ser extraída en una carpeta específica, según si hemos usado Xampp o AppServ:
  - **Xampp:** C:\xampp\htdocs
  - **AppServ:** C:\AppServ\www
- Una vez extraído, accedemos a la URL *localhost/moodle* y seguimos los pasos, que nos guiarán a la hora de asociar Moodle a la base de datos creada en el paso 2 e instalar todos los componentes y ficheros PHP necesarios. Se recomienda seguir la **documentación oficial de Moodle** ante cualquier duda o funcionalidad extra que se desee añadir.
- En este paso, Moodle generará todas las tablas automáticamente sobre la base de datos. Además, si ocurriese algún error, este será mostrado por pantalla con un pequeño texto con ayuda para solucionarlo.

### 4. Importar ficheros PHP
Tras la instalación de Moodle, se debe copiar la carpeta *unity* de los archivos del proyecto dentro de la carpeta *moodle* extraída en el paso anterior. Esta carpeta *unity* contiene los ficheros PHP para comunicar el juego con la base de datos de Moodle.
- Para asegurar la conexión con la base de datos, se deben revisar los contenidos del fichero *database.txt*, con el nombre de usuario y contraseña introducidos para la configuración de PHPmyAdmin. El fichero contiene, por defecto, los valores introducidos automáticamente durante la instalación de XAMPP. Se deben modificar si no son estos.

### 5. Crear un curso
Durante el proceso de instalación de Moodle, se ha debido de crear un **usuario administrador**. Para crear un curso, accedemos a Moodle desde la dirección *localhost/moodle* e introducimos las credenciales de dicho administrador. Hecho esto, pinchamos sobre *Inicio del sitio* en la columna izquierda, pinchamos sobre *Crear nuevo curso* y seguimos las instrucciones mostradas.

### 6. Subir Moodle RPG en el curso
Dentro del nuevo curso, y logueados como administrador, activamos el modo edición, y pinchamos sobre *Añadir una actividad o recurso* para subir ahí el proyecto Moodle RPG.
- Hecho esto, seleccionamos el recurso *Archivo*, le damos nombre y, en el apartado *Seleccionar archivos* subimos el contenido completo de la carpeta **Build** del proyecto, configurando *index.html* como el archivo principal.
- En el submenú *Apariencia*, nos aseguramos que el campo Mostrar tiene el valor *Abrir* y, finalmente, guardamos los cambios.

## Para desarrolladores
Si eres un desarrollador buscando importar Moodle RPG en Unity para revisar el proyecto, o bien realizar modificaciones, debes seguir los pasos anteriores hasta el cuarto, incluyendo el propio Paso 4. Hecho esto, importar el **unitypackage** proporcionado en un nuevo proyecto de Unity (versión LTS recomendada: **2020.3.19f1**)

## Licencia
El contenido de este proyecto, tanto del código fuente como de esta memoria, tiene licencia **CC BY-NC-SA 4.0.**

Está permitido: 

- **Compartir:** Copiar y redistribuir el material en cualquier medio o formato.
- **Adaptar:** Remezclar, transformar y construir a partir del material.

Bajo las siguientes condiciones:

- **Reconocimiento:** Debe reconocer adecuadamente la autoría, proporcionar un enlace a la licencia e indicar si se han realizado cambios. Puede hacerlo de cualquier manera razonable, pero no de una manera que sugiera que tiene el apoyo del licenciador o lo recibe por el uso que hace.
- **No comercial:** No puede utilizar el material para una finalidad comercial.
- **ShareAlike:** Si remezcla, transforma o crea a partir del material, deberá difundir sus contribuciones bajo la misma licencia que el original.
- **Sin restricciones adicionales:** No puede aplicar términos legales o medidas tecnológicas que legalmente restrinjan realizar aquello que la licencia permite.
