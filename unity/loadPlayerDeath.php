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

    //En primer lugar, se crea la tabla game_deaths si esta no existe
    $query = "CREATE TABLE IF NOT EXISTS game_deaths (
        ID int(11) AUTO_INCREMENT,
        USERID bigint(10),
        DEADUNTIL datetime,
        PRIMARY KEY (ID),
        FOREIGN KEY (USERID) REFERENCES mdl_user(ID)
    )";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //A continuación, puesto que si se ha llamado esta función es porque un jugador ha perdido la vida, este se añade
    //inmediatamente a la tabla de muertes, y podrá revivir en 12 horas cuando el valor DEADUNTIL se alcance.
    $date = date('Y-m-d H:i:s', strtotime('+12 hours'));

    $query = "INSERT INTO game_deaths VALUES (null,'" . $USER->id ."','" . $date . "')";
    echo($query);
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
?>