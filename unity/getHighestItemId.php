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

    //Se recupera el ID del objeto con ID más alto
    $query = "SELECT * FROM game_inventory ORDER BY id DESC LIMIT 1";
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    $item = mysqli_fetch_array($result);
    echo($item["ID"]);
?>