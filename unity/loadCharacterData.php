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
    $query = "SELECT * FROM game_characters WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si el usuario ya tiene un personaje, se cargan sus datos
    if(mysqli_num_rows($result) > 0) {
        $character = mysqli_fetch_array($result);

        //Generamos el texto que leerá el script de Unity para obtener los datos.
        echo("ID," . $character['ID'] . "\n");
        echo("USERID," . $character['USERID'] . "\n");
        echo("SKINTONE," . $character['SKINTONE'] . "\n");
        echo("EYECOLOR," . $character['EYECOLOR'] . "\n");
        echo("HAIRCOLOR," . $character['HAIRCOLOR'] . "\n");
        echo("HAIRSTYLE," . $character['HAIRSTYLE'] . "\n");
        echo("SHIRTCOLOR," . $character['SHIRTCOLOR'] . "\n");
        echo("SHIRTSTYLE," . $character['SHIRTSTYLE'] . "\n");
        echo("PANTSCOLOR," . $character['PANTSCOLOR'] . "\n");
        echo("PANTSSTYLE," . $character['PANTSSTYLE'] . "\n");
        echo("SHOECOLOR," . $character['SHOECOLOR'] . "\n");
        echo("SHOESTYLE," . $character['SHOESTYLE'] . "\n");
        echo("FACEHAIRCOLOR," . $character['FACEHAIRCOLOR'] . "\n");
        echo("FACEHAIRSTYLE," . $character['FACEHAIRSTYLE'] . "\n");
        echo("FACEHAIRALPHA," . $character['FACEHAIRALPHA'] . "\n");
        echo("GLASSESCOLOR," . $character['GLASSESCOLOR'] . "\n");
        echo("GLASSESSTYLE," . $character['GLASSESSTYLE'] . "\n");
        echo("COLLARCOLOR," . $character['COLLARCOLOR'] . "\n");
        echo("COLLARSTYLE," . $character['COLLARSTYLE'] . "\n");
        echo("CHARNAME," . $character['CHARNAME'] . "\n");
        echo("CHARGENDER," . $character['CHARGENDER']);
    }
    else echo("null");
?>