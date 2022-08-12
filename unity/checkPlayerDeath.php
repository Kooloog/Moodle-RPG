<?php

    //Acceso a los datos del usuario actualmente en línea
    require_once('../config.php');
    global $USER;

    //Estableciendo conexión con la base de datos
    $hostname = 'localhost';
    $username = 'root';
    $password = '';
    $database = 'moodle';
    $dbConnection = mysqli_connect($hostname, $username, $password, $database) or die ("Conexión con la base de datos fallida");

    //En primer lugar, se comprueba si el jugador está en la lista de muertes o no.
    $query = "SELECT * FROM game_deaths WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si está en la lista de muertes, entonces se comprueban una de dos posibilidades: o revive, o sigue muerto.
    if(mysqli_num_rows($result) > 0) {
        $death = mysqli_fetch_array($result);

        //Se obtiene la fecha y hora actuales.
        $dateNow = date('Y-m-d H:i:s');
        $dateRevive = new DateTime($death['DEADUNTIL']);

        //Se comprueba si el jugador puede revivir, o si debe continuar muerto
        if($dateNow < $dateRevive) {
            //TODO
        }
        else {
            $query = "DELETE FROM game_deaths WHERE userid = " . $USER->id;
            $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
            $echo('revive');
        }
    }
    else {
        echo('alive');
    }
?>