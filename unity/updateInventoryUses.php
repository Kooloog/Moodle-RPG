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

    //Si los usos son 0, borra el objeto del inventario y la base de datos. Si no, actualiza los usos.
    if($_GET['uses'] == "0") {
        $query = "DELETE FROM game_inventory WHERE id = " . $_GET['id'];
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    else {
        $query = "UPDATE game_inventory SET uses = " . $_GET['uses'] . " WHERE id = " .  $_GET['id'];
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    
?>