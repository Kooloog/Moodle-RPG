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

    //En primer lugar, se crea la tabla game_inventory si esta no existe
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