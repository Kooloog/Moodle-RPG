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

    //En primer lugar, se crea la tabla game_stats si esta no existe
    $query = "CREATE TABLE IF NOT EXISTS game_UI_stats (
        ID int(11) AUTO_INCREMENT,
        USERID bigint(10),
        HEALTH int(11),
        MAXHEALTH int(11),
        SCORE int(11),
        COINS int(11),
        ATTACK int(11),
        DEFENSE int(11),
        PRIMARY KEY (ID),
        FOREIGN KEY (USERID) REFERENCES mdl_user(ID)
    )";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Se comprueba si el usuario actual ya cuenta con stats básicas, para bien crearlas o actualizarlas
    $query = "SELECT * FROM game_UI_stats WHERE userid = " . $USER->id;
    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    if(mysqli_num_rows($result) > 0) {
        switch($_GET['change']) {
            case "health":
                $query = "UPDATE game_UI_stats SET health = '" . $_GET['health'] . "' WHERE userid = " . $USER->id;
                break;
            case "maxhealth":
                $query = "UPDATE game_UI_stats SET maxhealth = '" . $_GET['maxhealth'] . "' WHERE userid = " . $USER->id;
                break;
            case "score":
                $query = "UPDATE game_UI_stats SET score = '" . $_GET['score'] . "' WHERE userid = " . $USER->id;
                break;
            case "coins":
                $query = "UPDATE game_UI_stats SET coins = '" . $_GET['coins'] . "' WHERE userid = " . $USER->id;
                break;
            case "attack":
                $query = "UPDATE game_UI_stats SET attack = '" . $_GET['attack'] . "' WHERE userid = " . $USER->id;
                break;
            case "defense":
                $query = "UPDATE game_UI_stats SET defense = '" . $_GET['defense'] . "' WHERE userid = " . $USER->id;
                break;
            case "maplevel":
                $query = "UPDATE game_UI_stats SET maplevel = '" . $_GET['maplevel'] . "' WHERE userid = " . $USER->id;
                break;
            default:
                echo("Invalid stat."); break;
        }
        
        echo($query);
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    else {  
        $query = "INSERT INTO game_UI_stats VALUES (null,'" . $USER->id . "', ".
            "'10', '10', '0', '0', '1', '1'" . 
        ")";

        echo(shell_exec("php loadUIstats.php"));
        $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));
    }
    
?>