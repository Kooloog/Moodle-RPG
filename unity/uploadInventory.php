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

    //En primer lugar, se crea la tabla game_characters si esta no existe
    $query = "CREATE TABLE IF NOT EXISTS game_inventory (
        ID int(11) AUTO_INCREMENT,
        USERID bigint(10),
        ITEMTYPE varchar(255),
        ITEMID int(11),
        USES int(11),
        PRIMARY KEY (ID),
        FOREIGN KEY (USERID) REFERENCES mdl_user(ID)
    )";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    
    $query = "INSERT INTO game_inventory VALUES (null,'" . $USER->id ."','".
        $_GET['itemtype'] ."','". 
        $_GET['itemid'] ."','". 
        $_GET['uses'] .
    "')";

    echo($query);
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
?>