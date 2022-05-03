
<?php

    $hostname = 'localhost';
    $username = 'root';
    $password = '';
    $database = 'moodle';

    $dbConnection = mysqli_connect($hostname, $username, $password, $database) or die ("Conexión con la base de datos fallida");

    $query = "CREATE TABLE IF NOT EXISTS characters (
        ID int(11) AUTO_INCREMENT,
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
        PRIMARY KEY (ID)
    )";

    $result = mysqli_query($dbConnection, $query);

    $query = "INSERT INTO characters VALUES (null,'" .
        $_GET['skintone'] ."','". $_GET['eyecolor'] ."','". $_GET['haircolor'] ."','". $_GET['hairstyle'] ."','". $_GET['shirtcolor'] ."','". $_GET['shirtstyle'] ."','". 
        $_GET['pantscolor'] ."','".  $_GET['pantsstyle'] ."','".  $_GET['shoecolor'] ."','".  $_GET['shoestyle'] ."','".  $_GET['facehaircolor'] ."','". 
        $_GET['facehairstyle'] ."','".  $_GET['facehairalpha'] ."','".  $_GET['glassescolor'] ."','".  $_GET['glassesstyle'] ."','". 
        $_GET['collarcolor'] ."','".  $_GET['collarstyle'] ."','".  $_GET['charname'] ."','".  $_GET['chargender'] .
    "')";

    echo($query);

    $result = mysqli_query($dbConnection, $query) or die ("No se ha subido bien");
?>