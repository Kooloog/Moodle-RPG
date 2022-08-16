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

    //Se obtiene una lista de todos los jugadores ordenados por puntuación
    $query = "SELECT count(*) as c FROM game_ui_stats WHERE score > (SELECT score FROM game_ui_stats WHERE userid = " . $USER->id . ");";
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    $position = mysqli_fetch_array($result);
    echo($position["c"] + 1);

?>
