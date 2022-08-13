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
    if(mysqli_num_rows($result) > 0) echo('yes');
    else echo('no');

?>