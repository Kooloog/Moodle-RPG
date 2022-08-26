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

    //Si los usos son 0, borra el objeto del inventario y la base de datos. Si no, actualiza los usos.
    if($_GET['uses'] == "0" || $_GET['uses'] == 0) {
        $query = "DELETE FROM game_inventory WHERE id = " . $_GET['id'];
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    else {
        $query = "UPDATE game_inventory SET uses = " . $_GET['uses'] . " WHERE id = " .  $_GET['id'];
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    
?>