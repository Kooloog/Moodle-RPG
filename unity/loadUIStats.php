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

    //En primer lugar, se comprueba si el usuario ya tiene un personaje en la base de datos
    $query = "SELECT * FROM game_UI_stats WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si el usuario ya tiene un personaje, se cargan sus datos
    if(mysqli_num_rows($result) > 0) {
        $character = mysqli_fetch_array($result);

        //Generamos el texto que leerá el script de Unity para obtener los datos.
        echo("ID," . $character['ID'] . "\n");
        echo("USERID," . $character['USERID'] . "\n");
        echo("HEALTH," . $character['HEALTH'] . "\n");
        echo("MAXHEALTH," . $character['MAXHEALTH'] . "\n");
        echo("SCORE," . $character['SCORE'] . "\n");
        echo("COINS," . $character['COINS'] . "\n");
        echo("ATTACK," . $character['ATTACK'] . "\n");
        echo("DEFENSE," . $character['DEFENSE'] . "\n");
        echo("MAPLEVEL," . $character['MAPLEVEL']);
    }
    else echo("null");
?>