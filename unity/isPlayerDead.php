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

    //En primer lugar, se comprueba si el jugador está en la lista de muertes o no.
    $query = "SELECT * FROM game_deaths WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si está en la lista de muertes, entonces se comprueban una de dos posibilidades: o revive, o sigue muerto.
    if(mysqli_num_rows($result) > 0) echo('yes');
    else echo('no');

?>