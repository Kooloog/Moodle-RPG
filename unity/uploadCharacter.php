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
    $query = "CREATE TABLE IF NOT EXISTS game_characters (
        ID int(11) AUTO_INCREMENT,
        USERID bigint(10),
        SKINTONE int(11),
        EYECOLOR int(11),
        HAIRCOLOR varchar(255),
        HAIRSTYLE int(11),
        SHIRTCOLOR varchar(255),
        SHIRTSTYLE int(11),
        PANTSCOLOR varchar(255),
        PANTSSTYLE int(11),
        SHOECOLOR varchar(255),
        SHOESTYLE int(11),
        FACEHAIRCOLOR varchar(255),
        FACEHAIRSTYLE int(11),
        FACEHAIRALPHA float(11),
        GLASSESCOLOR varchar(255),
        GLASSESSTYLE int(11),
        COLLARCOLOR varchar(255),
        COLLARSTYLE int(11),
        CHARNAME varchar(255),
        CHARGENDER varchar(255),
        PRIMARY KEY (ID),
        FOREIGN KEY (USERID) REFERENCES mdl_user(ID)
    )";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Se comprueba si el usuario actual ya cuenta con un personaje, para o bien modificarlo, o bien subirlo de cero.
    $query = "SELECT * FROM game_characters WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    if(mysqli_num_rows($result) > 0) {
        $query = "UPDATE game_characters SET ".
            "skintone = '" . $_GET['skintone'] ."',". 
            "eyecolor = '" . $_GET['eyecolor'] ."',". 
            "haircolor = '" . $_GET['haircolor'] ."',". 
            "hairstyle = '" . $_GET['hairstyle'] ."',". 
            "shirtcolor = '" . $_GET['shirtcolor'] ."',". 
            "shirtstyle = '" . $_GET['shirtstyle'] ."',". 
            "pantscolor = '" . $_GET['pantscolor'] ."',". 
            "pantsstyle = '" . $_GET['pantsstyle'] ."',". 
            "shoecolor = '" . $_GET['shoecolor'] ."',". 
            "shoestyle = '" . $_GET['shoestyle'] ."',". 
            "facehaircolor = '" . $_GET['facehaircolor'] ."',". 
            "facehairstyle = '" . $_GET['facehairstyle'] ."',". 
            "facehairalpha = '" . $_GET['facehairalpha'] ."',". 
            "glassescolor = '" . $_GET['glassescolor'] ."',". 
            "glassesstyle = '" . $_GET['glassesstyle'] ."',". 
            "collarcolor = '" . $_GET['collarcolor'] ."',". 
            "collarstyle = '" . $_GET['collarstyle'] ."',". 
            "charname = '" . $_GET['charname'] ."',".
            "chargender = '" . $_GET['chargender'] .
        "' WHERE userid = ". $USER->id;    
        
        echo($query);
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    else {   
        $query = "INSERT INTO game_characters VALUES (null,'" . $USER->id ."','".
            $_GET['skintone'] ."','". 
            $_GET['eyecolor'] ."','". 
            $_GET['haircolor'] ."','". 
            $_GET['hairstyle'] ."','". 
            $_GET['shirtcolor'] ."','". 
            $_GET['shirtstyle'] ."','". 
            $_GET['pantscolor'] ."','". 
            $_GET['pantsstyle'] ."','". 
            $_GET['shoecolor'] ."','". 
            $_GET['shoestyle'] ."','". 
            $_GET['facehaircolor'] ."','". 
            $_GET['facehairstyle'] ."','". 
            $_GET['facehairalpha'] ."','". 
            $_GET['glassescolor'] ."','". 
            $_GET['glassesstyle'] ."','". 
            $_GET['collarcolor'] ."','". 
            $_GET['collarstyle'] ."','". 
            $_GET['charname'] ."','".
            $_GET['chargender'] .
        "')";

        echo($query);
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }

?>