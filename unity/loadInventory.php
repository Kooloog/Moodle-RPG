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

    //En primer lugar, se comprueba si el usuario ya tiene un personaje en la base de datos
    $query = "SELECT * FROM game_inventory WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si el usuario ya tiene un personaje, se cargan sus datos
    if(mysqli_num_rows($result) > 0) {

        for($i=0; $i<mysqli_num_rows($result); $i++) {
            $character = mysqli_fetch_array($result);

            //Generamos el texto que leerá el script de Unity para obtener los datos.
            echo("ID," . $character['ID'] . "\n");
            echo("USERID," . $character['USERID'] . "\n");
            echo("ITEMTYPE," . $character['ITEMTYPE'] . "\n");
            echo("ITEMID," . $character['ITEMID'] . "\n");
            echo("USES," . $character['USES']);
            
            if($i < (mysqli_num_rows($result)-1)) echo("|");
        }
    }
    else echo("null");
?>