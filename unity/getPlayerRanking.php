<?php

    //Acceso a los datos del usuario actualmente en línea
    require_once('../config.php');
    global $USER;

    //Obteniendo datos del fichero database.txt
    $handle = fopen("database.txt", "r");
    if ($handle) {
        $i = 0;
        while (($line = fgets($handle)) !== false) {
            $linesplit = explode('=', $line);
            switch($i) {
                case 0: $hostname = substr($linesplit[1], 0, -2); break;
                case 1: $username = substr($linesplit[1], 0, -2); break;
                case 2: $password = substr($linesplit[1], 0, -2); break;
                case 3: $database = substr($linesplit[1], 0, -2); break;
            }
            $i++;
        }

        fclose($handle);
    }

    //Estableciendo conexión con la base de datos
    $dbConnection = mysqli_connect($hostname, $username, $password, $database) or die ("Conexión con la base de datos fallida");

    //Se obtiene una lista de todos los jugadores ordenados por puntuación
    $query = "SELECT c.*, s.score FROM game_characters AS c 
                JOIN game_ui_stats AS s ON c.userid = s.userid
                ORDER BY s.score DESC";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Se obtienen los 10 (como maximo) primeros jugadores ordenados por puntuación, y se pasan todos sus datos.
    if(mysqli_num_rows($result) > 0) {

        $iterations = min(10, mysqli_num_rows($result));

        for($i=0; $i<$iterations; $i++) {
            $character = mysqli_fetch_array($result);

            //Obtenemos la puntuación de este jugador en concreto
            $scoreQuery = "SELECT * FROM game_ui_stats WHERE userid = " . $character['USERID'];
            $scoreResult = mysqli_query($dbConnection, $scoreQuery) or die (mysqli_error($dbConnection));
            $score = mysqli_fetch_array($scoreResult);

            //Generamos el texto que leerá el script de Unity para obtener los datos.
            echo("SKINTONE," . $character['SKINTONE'] . "\n");
            echo("EYECOLOR," . $character['EYECOLOR'] . "\n");
            echo("HAIRCOLOR," . $character['HAIRCOLOR'] . "\n");
            echo("HAIRSTYLE," . $character['HAIRSTYLE'] . "\n");
            echo("SHIRTCOLOR," . $character['SHIRTCOLOR'] . "\n");
            echo("SHIRTSTYLE," . $character['SHIRTSTYLE'] . "\n");
            echo("PANTSCOLOR," . $character['PANTSCOLOR'] . "\n");
            echo("PANTSSTYLE," . $character['PANTSSTYLE'] . "\n");
            echo("SHOECOLOR," . $character['SHOECOLOR'] . "\n");
            echo("SHOESTYLE," . $character['SHOESTYLE'] . "\n");
            echo("FACEHAIRCOLOR," . $character['FACEHAIRCOLOR'] . "\n");
            echo("FACEHAIRSTYLE," . $character['FACEHAIRSTYLE'] . "\n");
            echo("FACEHAIRALPHA," . $character['FACEHAIRALPHA'] . "\n");
            echo("GLASSESCOLOR," . $character['GLASSESCOLOR'] . "\n");
            echo("GLASSESSTYLE," . $character['GLASSESSTYLE'] . "\n");
            echo("COLLARCOLOR," . $character['COLLARCOLOR'] . "\n");
            echo("COLLARSTYLE," . $character['COLLARSTYLE'] . "\n");
            echo("CHARNAME," . $character['CHARNAME'] . "\n");
            echo("SCORE," . $score['SCORE']);
            
            if($i < (mysqli_num_rows($result)-1)) echo("|");
        }
    }

?>